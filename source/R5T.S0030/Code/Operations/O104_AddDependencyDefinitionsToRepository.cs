using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

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
    /// See <see cref="O105B_AddServiceImplementationsToRepository"/>.
    /// 
    /// Foreach service implementation in the repository, determine the *zero, one, or many* service definitions upon which the service implementation depends.
    /// Make the assumption that a service implementation only depends on service definitions (i.e. that a service implementation will not have service implementation as a dependency).
    /// TODO: this should be service *components* not definitions, but we'll go with definitions for now.
    /// 
    /// For each service implementation in the repository, examine the code file, find the implementation class, then find the class's constructor, it if has one.
    /// * If the class has no constructor, then we are done: the service implementation has no service definition dependencies.
    /// * If the class has more than one constructor, look for the constructor marked with the R5T.T0064.ServiceImplementationConstructorMarkerAttribute.
    ///     * If none is found, warn that we are going with the first constructor.
    /// * If one constructor is found, use it.
    /// 
    /// Then examine all input parameter types for the constructor.
    /// * If all parameter types are recognized as service definitions, then add the service definitions as dependencies of the service implementation.
    /// * If any of the parameter types are *not* recognized as service definitions, do not add the service definitions as dependencies of the service implementation.
    /// </summary>
    [OperationMarker]
    public class O104_AddDependencyDefinitionsToRepository : IActionOperation
    {
        private IHumanOutput HumanOutput { get; }
        private IHumanOutputFilePathProvider HumanOutputFilePathProvider { get; }
        private ILogger Logger { get; }
        private IMainFileContextFilePathsProvider MainFileContextFilePathsProvider { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private IOutputFilePathProvider OutputFilePathProvider { get; }
        private IServiceRepository ServiceRepository { get; }


        public O104_AddDependencyDefinitionsToRepository(
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
            var noDependencies = new List<Message<ServiceImplementation>>();

            var dependencySetsByImplementation = new Dictionary<ServiceImplementation, ServiceDefinition[]>();

            foreach (var implementation in implementations)
            {
                var implementationDescription = $"{Instances.NamespacedTypeNameOperator.GetTypeName(implementation.TypeName)}: {implementation.TypeName}";

                this.Logger.LogInformation($"Processing service implementation '{implementationDescription}'...");

                var codeFileExists = Instances.FileSystemOperator.FileExists(implementation.CodeFilePath);
                if (!codeFileExists)
                {
                    failures.Add(
                        Failure.Of(implementation, $"Unable to find code file: {implementation.CodeFilePath}"));

                    this.Logger.LogError($"Unable to find code file for service implementation {implementation.TypeName}:\n{implementation.CodeFilePath}");

                    continue;
                }

                var originalCompilationUnit = await Instances.CompilationUnitOperator.Load(implementation.CodeFilePath);

                var compilationUnit = originalCompilationUnit;

                var implementationNamespacedTypeName = implementation.TypeName;

                var implementationNamespaceName = Instances.NamespacedTypeNameOperator.GetNamespaceName(implementationNamespacedTypeName);
                var implementationClassName = Instances.NamespacedTypeNameOperator.GetTypeName(implementationNamespacedTypeName);

                var namespaceWasFound = originalCompilationUnit.HasNamespace_HandleNested(implementationNamespaceName);
                if (!namespaceWasFound)
                {
                    failures.Add(
                        Failure.Of(implementation, $"Unable to find namespace '{implementationNamespaceName}'."));

                    this.Logger.LogError($"Unable to find namespace '{implementationNamespaceName}' in code file:\n{implementation.CodeFilePath}");

                    continue;
                }

                var classWasFound = namespaceWasFound.Result.HasClass(implementationClassName);
                if (!classWasFound)
                {
                    failures.Add(
                        Failure.Of(implementation, $"Unable to find class '{implementationClassName}'."));

                    this.Logger.LogError($"Unable to find class '{implementationClassName}' in namespace: {implementationNamespaceName}.");

                    continue;
                }

                var constructors = classWasFound.Result
                    .GetConstructors()
                    .Now();

                var constructorCount = constructors.Length;
                if (constructorCount < 1)
                {
                    noDependencies.Add(
                        Message.For(implementation, "No dependencies found: no constructor method found."));

                    // No need to do anything since the implementation has no dependencies.
                    // Do *not* check properties of the implementation since it's only the constructor that is actually used by the DI container.
                    this.Logger.LogInformation($"No contructor found for service implementation {implementationClassName}, so no service depenencies.");

                    continue;
                }

                // Use the first constructor regardless of whether there are one or more.
                // If there are more, and a constructor other than the first has the marker attribute, the first constructor with the marker attribute will be used.
                var constructor = constructors.First();

                if(constructorCount > 1)
                {
                    // Do any of the constructors have the T0064.ServiceImplementationConstructorMarkerAttribute?
                    var constructorsWithMarkerAttribute = constructors
                        .Where(x => x.HasAttributeOfType<T0064.ServiceImplementationConstructorMarkerAttribute>())
                        .Now();

                    var constructorsWithMarkerAttributeCount = constructorsWithMarkerAttribute.Length;

                    if(constructorsWithMarkerAttributeCount < 1)
                    {
                        warnings.Add(
                            Warning.For(implementation, $"Multiple constructors found for service implementation {implementationClassName}, but none had the {nameof(T0064.ServiceImplementationConstructorMarkerAttribute)} attribute. Thus, the first constructor will be used."));

                        this.Logger.LogWarning($"Multiple constructors found for service implementation {implementationClassName}, but none had the {nameof(T0064.ServiceImplementationConstructorMarkerAttribute)} attribute. Thus, the first constructor will be used.");
                    }
                    else
                    {
                        // The first constructor with the marker attribute will be used regardless of wehther ther are one or more.
                        constructor = constructorsWithMarkerAttribute.First();

                        if(constructorsWithMarkerAttributeCount > 1)
                        {
                            warnings.Add(
                            Warning.For(implementation, $"Multiple constructors found for service implementation {implementationClassName} with the {nameof(T0064.ServiceImplementationConstructorMarkerAttribute)} attribute. Thus, the first constructor with the attribute will be used."));

                            this.Logger.LogWarning($"Multiple constructors found for service implementation {implementationClassName} with the {nameof(T0064.ServiceImplementationConstructorMarkerAttribute)} attribute. Thus, the first constructor with the attribute will be used.");
                        }
                    }
                }

                var parameters = constructor
                    .GetParameters()
                    .Now();

                var parametersCount = parameters.Length;

                if(parametersCount < 1)
                {
                    noDependencies.Add(
                        Message.For(implementation, "No dependencies found: no input parameters for constructor method found."));

                    // No need to do anything since the implementation has no dependencies.
                    // Do *not* check properties of the implementation since it's only the constructor that is actually used by the DI container.
                    this.Logger.LogInformation($"No parameters found for for contructor for service implementation {implementationClassName}, so no service depenencies.");

                    continue;
                }

                var parameterTypes = parameters
                    // Type should not be null, be apparently could be, since I think the parameter syntax is used both on the caller and callee sides, and the caller would not specify the type.
                    .Select(x => x.Type)
                    .Now();

                compilationUnit = compilationUnit.AnnotateNodes_Typed(
                    parameterTypes,
                    out var typedAnnotationsByParameterTypes);

                var parameterTypesByTypedAnnotation = typedAnnotationsByParameterTypes.Invert();

                var parameterDefinitionNamespacedTypeNamesByTypedAnnotation = Instances.Operation.TryGuessNamespacedTypeNames(
                    availableDefinitionNamespacedTypeNames,
                    compilationUnit,
                    parameterTypesByTypedAnnotation.Keys);

                var parameterDefinitionNamespacedTypeNamesFound = parameterDefinitionNamespacedTypeNamesByTypedAnnotation.Values
                    .Where(x => x.Exists)
                    .Select(x => x.Result)
                    .Now();

                var noParameterDefinitionNamespacedTypeNameWasFound = parameterDefinitionNamespacedTypeNamesFound.None();
                if (noParameterDefinitionNamespacedTypeNameWasFound)
                {
                    failures.Add(
                        Failure.Of(implementation, $"No service definitions found among the parameter types for the chosen constructor of implementation class '{implementationClassName}'."));

                    this.Logger.LogError($"No service definitions found among the parameter types for the chosen constructor of implementation class '{implementationClassName}'.");

                    continue;
                }

                var allParametersAreDefinitions = parameters.Length == parameterDefinitionNamespacedTypeNamesFound.Length;
                if(!allParametersAreDefinitions)
                {
                    failures.Add(
                        Failure.Of(implementation, $"Not all input parameters of the chosen constructor were service definitions found among the available service definitions for implementation class '{implementationClassName}'."));

                    this.Logger.LogError($"Not all input parameters of the chosen constructor were service definitions found among the available service definitions for implementation class '{implementationClassName}'.");

                    // Disregard the service dependencies that were found since not all were found.
                    continue;
                }

                var dependencyServiceDefinitions = parameterDefinitionNamespacedTypeNamesFound
                    .Select(x => definitionsByNamespacedTypeNames[x])
                    .Now();

                dependencySetsByImplementation.Add(implementation, dependencyServiceDefinitions);

                this.Logger.LogInformation($"Processed service implementation {implementationDescription}, found {dependencyServiceDefinitions.Length} dependency services.");
            }

            this.Logger.LogInformation("Finished processing service implementations.");

            /// Summarize.
            var failureCount = failures.Count;
            var warningsCount = warnings.Count;
            var noDependenciesCount = noDependencies.Count;
            var successesCount = dependencySetsByImplementation.Count;
            var resultsCount = failureCount + successesCount + noDependenciesCount;

            this.HumanOutput.WriteLine($"Determined service definitions for {implementations.Length} service implementations:\n\t+ {failureCount} failures,\n\t+ {successesCount} successes,\n\t+ {noDependenciesCount} no dependencies,\n\t= {resultsCount} (all accounted for: {(resultsCount == implementationsCount).YesOrNo().ToUpperInvariant()})\n\tAdditionally, {warningsCount} warnings.");

            /// Update the repository (with the successes).
            this.Logger.LogInformation("Determining new and departed service implementation-to-dependency definition mappings...");

            var dependencyDefinitionsByImplementation = await this.ServiceRepository.HasDependencyDefinitions(
                dependencySetsByImplementation.Keys);

            var dependencySetsJoinedByImplementations = dependencySetsByImplementation
                .Join(dependencyDefinitionsByImplementation,
                    x => x.Key,
                    x => x.Key,
                    (x, y) => new { Implementation = x.Key, CurrentDependencies = x.Value, RepositoryDependencies = y.Value.Result },
                    NamedFilePathedEqualityComparer.Instance)
                ;

            var departedDependencyDefinitionMappings = dependencySetsJoinedByImplementations
                .SelectMany(x =>
                {
                    var departed = x.RepositoryDependencies
                        .Except(x.CurrentDependencies,
                            NamedFilePathedEqualityComparer<ServiceDefinition>.Instance)
                        .Select(xServiceDefinition => DependencyDefinitionMapping.From(x.Implementation, xServiceDefinition))
                        ;

                    return departed;
                })
                .Now();

            var newDependencyDefinitionMappings = dependencySetsJoinedByImplementations
                .SelectMany(x =>
                {
                    var departed = x.CurrentDependencies
                        .Except(x.RepositoryDependencies,
                            NamedFilePathedEqualityComparer<ServiceDefinition>.Instance)
                        .Select(xServiceDefinition => DependencyDefinitionMapping.From(x.Implementation, xServiceDefinition))
                        ;

                    return departed;
                })
                .Now();

            var departedImplementedDefinitionMappingsCount = departedDependencyDefinitionMappings.Length;
            this.HumanOutput.WriteLine($"Found {departedImplementedDefinitionMappingsCount} departed service implementation-to-dependency definition mappings.");

            var newImplementedDefinitionMappingsCount = newDependencyDefinitionMappings.Length;
            this.HumanOutput.WriteLine($"Found {newImplementedDefinitionMappingsCount} new service implementation-to-dependency definition mappings.");

            // Delete first (in case any mappings have changed, so we don't violate the uniqueness constraint on implementation).
            this.Logger.LogInformation("Removing departed service implementation-to-dependency definition mappings from the repository...");

            await this.ServiceRepository.DeleteDependencyDefinitions(departedDependencyDefinitionMappings);

            // Then add new.
            this.Logger.LogInformation("Adding new service implementation-to-dependency definition mappings to the repository...");

            await this.ServiceRepository.AddDependencyDefinitions(newDependencyDefinitionMappings);

            this.Logger.LogInformation("Finished updating service repository.");

            /// Display human output.
            var humanOutputFilePath = await this.HumanOutputFilePathProvider.GetHumanOutputFilePath();

            await this.NotepadPlusPlusOperator.OpenFilePath(humanOutputFilePath);

            /// Write and show output unique to the operation.
            this.Logger.LogInformation("Writing output files.");

            var outputFilePath = await this.OutputFilePathProvider.GetOutputFilePath("Implementation Dependency Results.txt");

            var lines = failures
                .OrderBy(x => x.Value.TypeName)
                .Select(x => $"{x.Value.TypeName}:\n\t{x.Message}\n({x.Value.CodeFilePath})\n")
                ;

            await FileHelper.WriteAllLines(
                outputFilePath,
                lines);

            await this.NotepadPlusPlusOperator.OpenFilePath(outputFilePath);

            /// Display results.
            var toDependencyDefinitionMappingsJsonFilePath = await this.MainFileContextFilePathsProvider.GetToDependencyDefinitionMappingsJsonFilePath();

            await this.NotepadPlusPlusOperator.OpenFilePath(toDependencyDefinitionMappingsJsonFilePath);
        }
    }
}
