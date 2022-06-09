using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using R5T.D0048;
using R5T.D0096;
using R5T.D0096.D003;
using R5T.D0105;
using R5T.F0001.F002;
using R5T.T0020;
using R5T.T0094;

using R5T.S0030.Repositories;


namespace R5T.S0030
{
    /// <summary>
    /// Initial attempt.
    /// See <see cref="O010_DescribeAllServiceImplementations"/>, <see cref="O105A_IdentifyServiceImplementations"/>, and <see cref="O105B_AddServiceImplementationsToRepository"/>.
    /// 
    /// For each service implementation in the repository, determine the *single* service definition the service implementation implements.
    /// Make the assumption that each service definition can only implement a *single* service definition.
    /// 
    /// For each service implementation in the repository, examine the code file, find the implementation class, then examine each and every base type, testing each base type for whether it exists in the list of service definitions from the repository.
    /// * If none is found, report an error.
    /// * If two or more are found, report a warning that only the first one will be 
    /// </summary>
    [OperationMarker]
    public class O103_AddImplementedDefinitionToRepository : IActionOperation
    {
        private IHumanOutput HumanOutput { get; }
        private IHumanOutputFilePathProvider HumanOutputFilePathProvider { get; }
        private ILogger Logger { get; }
        private IMainFileContextFilePathsProvider MainFileContextFilePathsProvider { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private IOutputFilePathProvider OutputFilePathProvider { get; }
        private IServiceRepository ServiceRepository { get; }


        public O103_AddImplementedDefinitionToRepository(
            IHumanOutput humanOutput,
            IHumanOutputFilePathProvider humanOutputFilePathProvider,
            ILogger<O103_AddImplementedDefinitionToRepository> logger,
            IMainFileContextFilePathsProvider mainFileContextFilePathsProvider,
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            IOutputFilePathProvider outputFilePathProvider,
            IServiceRepository serviceRepository)
        {
            this.HumanOutput = humanOutput;
            this.HumanOutputFilePathProvider = humanOutputFilePathProvider;
            this.Logger = logger;
            this.MainFileContextFilePathsProvider = mainFileContextFilePathsProvider;
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.OutputFilePathProvider = outputFilePathProvider;
            this.ServiceRepository = serviceRepository;
        }

        public async Task Run()
        {
            /// Get input data.
            this.Logger.LogInformation("Getting all current service implementations and service definitions...");

            var implementations = await this.ServiceRepository.GetAllServiceImplementations();

            //// For debugging.
            //implementations = implementations.Where(x => x.TypeName == "R5T.S0030.ProjectFilePathsProvider").Now();

            var definitions = await this.ServiceRepository.GetAllServiceDefinitions();

            var implementationsCount = implementations.Length;

            this.HumanOutput.WriteLine($"Found {implementationsCount} service implementations and {definitions.Length} service definitions...");

            this.Logger.LogInformation($"Found {implementationsCount} service implementations and {definitions.Length} service definitions...");

            // Verify. Can disaggregate service definitions with the same namespaced type name using to-project mapping information, but that is lots of extra work!
            // For now just error.
            definitions.VerifyDistinctByName();

            var definitionsByNamespacedTypeNames = definitions
                .ToDictionary(
                    x => x.TypeName);

            /// Determine implemented service definitions for each service implementation.
            var availableDefinitionNamespacedTypeNames = definitionsByNamespacedTypeNames.Keys;

            var failures = new List<Failure<ServiceImplementation>>();
            var warnings = new List<Warning<ServiceImplementation>>();

            var definitionsByImplementation = new Dictionary<ServiceImplementation, ServiceDefinition>();

            foreach (var implementation in implementations)
            {
                var implementationDescription = $"{Instances.NamespacedTypeNameOperator.GetTypeName(implementation.TypeName)}: {implementation.TypeName}";

                this.Logger.LogInformation($"Processing service implementation '{implementationDescription}'...");

                var codeFileExists = Instances.FileSystemOperator.FileExists(implementation.CodeFilePath);
                if(!codeFileExists)
                {
                    failures.Add(
                        Failure.Of(implementation, $"Unable to find code file: {implementation.CodeFilePath}"));

                    this.Logger.LogError($"Unable to find code file for service implementation {implementation.TypeName}:\n{implementation.CodeFilePath}");

                    continue;
                }

                var originalCompilationUnit = await Instances.CompilationUnitOperator_Old.Load(implementation.CodeFilePath);

                var compilationUnit = originalCompilationUnit;

                var implementationNamespacedTypeName = implementation.TypeName;

                var implementationNamespaceName = Instances.NamespacedTypeNameOperator.GetNamespaceName(implementationNamespacedTypeName);
                var implementationClassName = Instances.NamespacedTypeNameOperator.GetTypeName(implementationNamespacedTypeName);

                var namespaceWasFound = originalCompilationUnit.HasNamespace_HandleNested(implementationNamespaceName);
                if(!namespaceWasFound)
                {
                    failures.Add(
                        Failure.Of(implementation, $"Unable to find namespace '{implementationNamespaceName}'."));

                    this.Logger.LogError($"Unable to find namespace '{implementationNamespaceName}' in code file:\n{implementation.CodeFilePath}");

                    continue;
                }

                var classWasFound = namespaceWasFound.Result.HasClass(implementationClassName);
                if(!classWasFound)
                {
                    failures.Add(
                        Failure.Of(implementation, $"Unable to find class '{implementationClassName}'."));

                    this.Logger.LogError($"Unable to find class '{implementationClassName}' in namespace: {implementationNamespaceName}.");

                    continue;
                }

                // Determine if the class has the ImplementsServiceDefinition (or ImplementsServiceDefinitionAttribute) attribute.
                var hasAttribute = classWasFound.Result.HasAttributeOfType<T0064.ImplementsServiceDefinitionAttribute>();
                if(hasAttribute)
                {
                    var attribute = classWasFound.Result.GetAttributeOfTypeSingle<T0064.ImplementsServiceDefinitionAttribute>();

                    var firstArgument = attribute.ArgumentList.Arguments.First();

                    if(firstArgument.Expression is TypeOfExpressionSyntax typeOfExpressionSyntax)
                    {
                        // Guess the type provided for the typeof operator.
                        compilationUnit = compilationUnit.AnnotateNode_Typed(
                            typeOfExpressionSyntax.Type,
                            out var typedAnnotation);

                        var attributeDefinitionNamespacedTypeNameWasFound = Instances.Operation.TryGuessNamespacedTypeName(
                            availableDefinitionNamespacedTypeNames,
                            compilationUnit,
                            typedAnnotation);

                        if(attributeDefinitionNamespacedTypeNameWasFound)
                        {
                            var attributeBasedDefinition = definitionsByNamespacedTypeNames[attributeDefinitionNamespacedTypeNameWasFound.Result];

                            definitionsByImplementation.Add(implementation, attributeBasedDefinition);

                            this.Logger.LogInformation($"Processed service implementation {implementationDescription} using {nameof(T0064.ImplementsServiceDefinitionAttribute)} attribute, found service definition: {attributeBasedDefinition.TypeName}.");

                            // Move to the next implementation.
                            // Do not check that the type provided to the attribute exists in the base type list; too much work.
                            continue;
                        }
                        else
                        {
                            // Warning, not failure, since there is the possibility of analyzing the base types list, so move on.
                            warnings.Add(
                                Warning.For(implementation, $"Implementation had {nameof(T0064.ImplementsServiceDefinitionAttribute)} attribute, but it's type was not recognized as a service definition."));

                            this.Logger.LogWarning($"Unable to recognize type paramter of {nameof(T0064.ImplementsServiceDefinitionAttribute)} attribute of class {implementationClassName} as a service definition.");
                        }
                    }
                    else
                    {
                        // Warning, not failure, since there is the possibility of analyzing the base types list, so move on.
                        warnings.Add(
                            Warning.For(implementation, $"Implementation had {nameof(T0064.ImplementsServiceDefinitionAttribute)} attribute, but its first argument type parameter could not be handled (was not a nameof() expression)."));

                        this.Logger.LogWarning($"Unable to handle {nameof(T0064.ImplementsServiceDefinitionAttribute)} attribute of class {implementationClassName}.");
                    }
                }

                // Since the attribute was not found, examine the base types list.
                var hasBaseTypes = classWasFound.Result.HasBaseTypes();
                if(!hasBaseTypes)
                {
                    failures.Add(
                        Failure.Of(implementation, $"No base types found for implementation class '{implementationClassName}'."));

                    this.Logger.LogError($"Unable to base types for class {implementationClassName}.");

                    continue;
                }

                var baseTypeTypes = hasBaseTypes.Result
                    .Select(x => x.Type)
                    .Now();

                compilationUnit = compilationUnit.AnnotateNodes_Typed(
                    baseTypeTypes,
                    out var typedAnnotationsByBaseTypeType);

                var baseTypeTypesByTypedAnnotation = typedAnnotationsByBaseTypeType.Invert();

                var baseTypeDefinitionNamespacedTypeNamesByTypedAnnotation = Instances.Operation.TryGuessNamespacedTypeNames(
                    availableDefinitionNamespacedTypeNames,
                    compilationUnit,
                    baseTypeTypesByTypedAnnotation.Keys);

                var baseTypeDefinitionNamespacedTypeNamesFound = baseTypeDefinitionNamespacedTypeNamesByTypedAnnotation.Values
                    .Where(x => x.Exists)
                    .Select(x => x.Result)
                    .Now();

                var noBaseTypeDefinitionNamespacedTypeNameWasFound = baseTypeDefinitionNamespacedTypeNamesFound.None();
                if(noBaseTypeDefinitionNamespacedTypeNameWasFound)
                {
                    failures.Add(
                        Failure.Of(implementation, $"No service definition found among the base types for implementation class '{implementationClassName}'."));

                    this.Logger.LogError($"No service definition found among base types for implementation class '{implementationClassName}'.");

                    continue;
                }

                var multipleBaseTypeDefinitionNamespacedTypeNameWereFound = baseTypeDefinitionNamespacedTypeNamesFound.Multiple();
                if(multipleBaseTypeDefinitionNamespacedTypeNameWereFound)
                {
                    // Use the first base type found.
                    var found = false;

                    foreach (var baseTypeType in baseTypeTypes)
                    {
                        var baseTypeTypedAnnotation = typedAnnotationsByBaseTypeType[baseTypeType];

                        var definitionNamespacedTypeNameWasFound = baseTypeDefinitionNamespacedTypeNamesByTypedAnnotation[baseTypeTypedAnnotation];
                        if(definitionNamespacedTypeNameWasFound)
                        {
                            var currentDefinition = definitionsByNamespacedTypeNames[definitionNamespacedTypeNameWasFound.Result];

                            definitionsByImplementation.Add(implementation, currentDefinition);

                            warnings.Add(
                                Warning.For(implementation, $"Multiple service definitions found for implementation class {implementationClassName}. Using first found."));

                            this.Logger.LogError($"Multiple service definitions found for implementation class {implementationClassName}. Using first found.");

                            found = true;

                            break;
                        }
                    }

                    // Note, the above for-loop will always succeed, but check.
                    if(!found)
                    {
                        throw new Exception("Service definition should have been found, but was not.");
                    }

                    continue;
                }

                // At this point, there is exactly one base type, definition namespaced type name found.
                var definitionNamespacedTypeName = baseTypeDefinitionNamespacedTypeNamesFound.Single();

                var definition = definitionsByNamespacedTypeNames[definitionNamespacedTypeName];

                definitionsByImplementation.Add(implementation, definition);

                this.Logger.LogInformation($"Processed service implementation {implementationDescription}, found service definition: {definition.TypeName}.");
            }

            this.Logger.LogInformation("Finished processing service implementations.");

            /// Summarize.
            var failureCount = failures.Count;
            var warningsCount = warnings.Count;
            var successesCount = definitionsByImplementation.Count;
            var resultsCount = failureCount + warningsCount + successesCount;

            this.HumanOutput.WriteLine($"Determined service definitions for {implementations.Length} service implementations:\n\t+ {failureCount} failures,\n\t+ {warningsCount} warnings,\n\t+ {successesCount} successes,\n\t= {resultsCount} (all accounted for: {(resultsCount == implementationsCount).YesOrNo().ToUpperInvariant()})");

            /// Update the repository (with the successes).
            this.Logger.LogInformation("Determining new and departed service implementation-to-implemented definition mappings...");

            var implementedDefinitionByImplementation = await this.ServiceRepository.HasImplementedDefinitions(
                definitionsByImplementation.Keys);

            var definitionsJoinedByImplementations = definitionsByImplementation
                .Join(implementedDefinitionByImplementation,
                    x => x.Key,
                    x => x.Key,
                    (x, y) => new { Implementation = x.Key, CurrentDefinition = x.Value, RepositoryDefinition = y.Value },
                    NamedFilePathedEqualityComparer.Instance)
                ;

            var departedImplementedDefinitionMappings = definitionsJoinedByImplementations
                .Where(x => x.RepositoryDefinition.IsFound() && NamedFilePathedEqualityComparer.Instance.NotEquals(x.CurrentDefinition, x.RepositoryDefinition.Result))
                .Select(x => ImplementedDefinitionMapping.From(x.Implementation, x.CurrentDefinition))
                .Now();

            var newImplementedDefinitionMappings = definitionsJoinedByImplementations
                .Where(x => x.RepositoryDefinition.IsNotFound() || NamedFilePathedEqualityComparer.Instance.NotEquals(x.CurrentDefinition, x.RepositoryDefinition.Result))
                .Select(x => ImplementedDefinitionMapping.From(x.Implementation, x.CurrentDefinition))
                .Now();

            var departedImplementedDefinitionMappingsCount = departedImplementedDefinitionMappings.Length;
            this.HumanOutput.WriteLine($"Found {departedImplementedDefinitionMappingsCount} departed service implementation-to-implemented definition mappings.");

            var newImplementedDefinitionMappingsCount = newImplementedDefinitionMappings.Length;
            this.HumanOutput.WriteLine($"Found {newImplementedDefinitionMappingsCount} new service implementation-to-implemented definition mappings.");

            // Delete first (in case any mappings have changed, so we don't violate the uniqueness constraint on implementation).
            this.Logger.LogInformation("Removing departed service implementation-to-implemented definition mappings from the repository...");

            await this.ServiceRepository.DeleteImplementedDefinitions(departedImplementedDefinitionMappings);

            // Then add new.
            this.Logger.LogInformation("Adding new service implementation-to-implemented definition mappings to the repository...");

            await this.ServiceRepository.AddImplementedDefinitions(newImplementedDefinitionMappings);

            this.Logger.LogInformation("Finished updating service repository.");

            /// Write and show output unique to the operation.
            this.Logger.LogInformation("Writing output files.");

            var outputFilePath = await this.OutputFilePathProvider.GetOutputFilePath("Implementation to Definition Results.txt");

            var lines = failures
                .OrderBy(x => x.Value.TypeName)
                .Select(x => $"{x.Value.TypeName}:\n\t{x.Message}\n({x.Value.CodeFilePath})\n")
                ;

            await FileHelper.WriteAllLines(
                outputFilePath,
                lines);

            await this.NotepadPlusPlusOperator.OpenFilePath(outputFilePath);

            /// Display human output.
            var humanOutputFilePath = await this.HumanOutputFilePathProvider.GetHumanOutputFilePath();

            await this.NotepadPlusPlusOperator.OpenFilePath(humanOutputFilePath);

            /// Display results.
            var toImplementedDefinitionMappingsJsonFilePath = await this.MainFileContextFilePathsProvider.GetToImplementedDefinitionMappingsJsonFilePath();

            await this.NotepadPlusPlusOperator.OpenFilePath(toImplementedDefinitionMappingsJsonFilePath);
        }
    }
}
