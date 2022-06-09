using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

using R5T.Lombardy;
using R5T.Magyar;

using R5T.D0078;
using R5T.D0079;
using R5T.D0083;
using R5T.D0101;
using R5T.T0020;

using LocalData;

using R5T.S0030.Repositories;

namespace R5T.S0030
{
    /// <summary>
    /// There is a long list of service definitions without marker attribute (or interface) created by <see cref="O002_IdentifyPossibleServiceDefinitions"/>.
    /// For the hundreds of service definitions on that list, modify the code file, containing project, and containing solution to include the <see cref="R5T.T0064.ServiceDefinitionMarkerAttribute"/> and interface.
    /// </summary>
    public class O200_AddServiceDefinitionMarkerAttributeAndInterface : IActionOperation
    {
        private IProjectRepository ProjectRepository { get; }
        private IStringlyTypedPathOperator StringlyTypedPathOperator { get; }
        private IVisualStudioProjectFileOperator VisualStudioProjectFileOperator { get; }
        private IVisualStudioProjectFileReferencesProvider VisualStudioProjectFileReferencesProvider { get; }
        private IVisualStudioSolutionFileOperator VisualStudioSolutionFileOperator { get; }


        public O200_AddServiceDefinitionMarkerAttributeAndInterface(
            IProjectRepository projectRepository,
            IStringlyTypedPathOperator stringlyTypedPathOperator,
            IVisualStudioProjectFileOperator visualStudioProjectFileOperator,
            IVisualStudioProjectFileReferencesProvider visualStudioProjectFileReferencesProvider,
            IVisualStudioSolutionFileOperator visualStudioSolutionFileOperator)
        {
            this.ProjectRepository = projectRepository;
            this.StringlyTypedPathOperator = stringlyTypedPathOperator;
            this.VisualStudioProjectFileOperator = visualStudioProjectFileOperator;
            this.VisualStudioProjectFileReferencesProvider = visualStudioProjectFileReferencesProvider;
            this.VisualStudioSolutionFileOperator = visualStudioSolutionFileOperator;
        }

        public async Task Run()
        {
            var missingMarkerDataFilePath = @"C:\Temp\Missing marker attribute.json";

            //// Get the code file path and type name of the unmarked service definition.
            //var serviceDefinitionNamespacedTypeName = "R5T.Egaleo.IOrganizationDirectoryNameConvention";
            //var codeFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.Egaleo.Base\source\R5T.Egaleo.Base\Code\Services\Definitions\IOrganizationDirectoryNameConvention.cs";

            /// Run.
            var failures = new List<Failure<ServiceDefinition>>();

            /// Verify project(s) containing the code file path, and solution(s) containing the project(s), have references to R5T.T0064.
            var r5T_T0064_ProjectIdentityString = Instances.ProjectPath.R5T_T0064();
            var r5T_T0064_ProjectFilePath = await this.ProjectRepository.GetFilePathForProjectIdentityString(r5T_T0064_ProjectIdentityString);

            // Keep a list of all project files that have been verified to contain a reference to R5T.T0064.
            var verifiedProjectFilePathsHash = new HashSet<string>();

            // Keep a list of all solution files that have been verified to contain a reference to R5T.T0064.
            var verifiedSolutionFilePathsHash = new HashSet<string>();

            // Replace with all from possible.
            //var serviceDefinitions = new[]
            //{
            //    new ServiceDefinition
            //    {
            //        TypeName = serviceDefinitionNamespacedTypeName,
            //        CodeFilePath = codeFilePath,
            //    },
            //};

            var serviceDefinitions = JsonFileHelper.LoadFromFile<ServiceDefinition[]>(missingMarkerDataFilePath);

            foreach (var serviceDefinition in serviceDefinitions)
            {
                // Find the project file path(s) for the code file path.
                var projectFilePaths = Instances.ProjectPathsOperator.FindProjectFilesInFileDirectoryOrDirectParentDirectories(
                    serviceDefinition.CodeFilePath);

                // For each project, ensure it has a reference to R5T.T0064, and that the solution files that contain it have a reference to R5T.T0064.
                foreach (var projectFilePath in projectFilePaths)
                {
                    var alreadyVerifiedProject = verifiedProjectFilePathsHash.Contains(projectFilePath);
                    if (!alreadyVerifiedProject)
                    {
                        // Ensure it has a reference to R5T.T0064.
                        await VisualStudioProjectFileOperator.EnsureHasProjectReference(
                            projectFilePath,
                            r5T_T0064_ProjectFilePath);

                        // For the project file path(s) containing the code file path, find all solution file paths containing the project path.
                        var solutionFilePaths = await Instances.SolutionOperator.GetSolutionFilePathsContainingProject(
                            projectFilePath,
                            this.StringlyTypedPathOperator,
                            this.VisualStudioSolutionFileOperator);

                        // For each solution, ensure it has a recursive project reference to R5T.T0064.
                        foreach (var solutionFilePath in solutionFilePaths)
                        {
                            var alreadyVerifiedSolution = verifiedSolutionFilePathsHash.Contains(solutionFilePath);
                            if (!alreadyVerifiedSolution)
                            {
                                await Instances.SolutionOperator.AddDependencyProjectReferenceAndRecursiveDependencies(
                                    solutionFilePath,
                                    r5T_T0064_ProjectFilePath,
                                    this.StringlyTypedPathOperator,
                                    this.VisualStudioProjectFileReferencesProvider,
                                    this.VisualStudioSolutionFileOperator);

                                verifiedSolutionFilePathsHash.Add(solutionFilePath);
                            }
                        }

                        verifiedProjectFilePathsHash.Add(projectFilePath);
                    }
                }

                /// Now modify the code file.
                var compilationUnit = await Instances.CompilationUnitOperator_Old.Load_AndStandardizeToLeadingTrivia(serviceDefinition.CodeFilePath);

                // Add the R5T.T0064 namespace to the compilation unit.
                var hasR5T_T0064_Namespace = compilationUnit.HasUsing(
                    Instances.NamespaceName.R5T_T0064());

                if (!hasR5T_T0064_Namespace)
                {
                    compilationUnit = compilationUnit.AddUsing(
                        Instances.NamespaceName.R5T_T0064());
                }

                var hasServiceDefinitionInterface = compilationUnit.HasInterfaceByNamespacedTypeName(serviceDefinition.TypeName);
                if (!hasServiceDefinitionInterface)
                {
                    failures.Add(
                        Failure.Of(serviceDefinition, $"Unable to find service definition interface: {serviceDefinition.TypeName}"));

                    continue;
                }

                var originalServiceDefinitionInterface = hasServiceDefinitionInterface.Result;
                var serviceDefinitionInterface = originalServiceDefinitionInterface;

                // Does it have the marker attribute?
                var hasMarkerAttribute = Instances.InterfaceOperator_Old.HasServiceDefinitionMarkerAttribute(
                    serviceDefinitionInterface);

                // If not, add it (immediately before the type declaration, at the end of the base types list).
                if(!hasMarkerAttribute)
                {
                    var attribute = Instances.SyntaxFactory.Attribute(
                        Instances.AttributeTypeName.GetEnsuredNonAttributeSuffixedTypeName(
                            Instances.TypeName.ServiceDefinitionMarkerAttribute()));

                    var attributeList = Instances.SyntaxFactory.AttributeList();

                    attributeList = attributeList.AddAttributes(attribute);

                    serviceDefinitionInterface = serviceDefinitionInterface.WithAttributeLists(
                        serviceDefinitionInterface.AttributeLists.Add(attributeList));
                }

                // Does it have the marker interface?
                var hasMarkerInterface = Instances.InterfaceOperator_Old.HasServiceDefinitionMarkerInterface(
                    serviceDefinitionInterface);

                // If not, add it (at the end of the base types list).
                if(!hasMarkerInterface)
                {
                    var baseType = Instances.SyntaxFactory.BaseType(
                        Instances.TypeName.IServiceDefinition());

                    serviceDefinitionInterface = serviceDefinitionInterface.AddBaseListTypes(baseType);
                }

                compilationUnit = compilationUnit.ReplaceNode_Better(originalServiceDefinitionInterface, serviceDefinitionInterface);

                await Instances.CompilationUnitOperator_Old.Save(
                    serviceDefinition.CodeFilePath,
                    compilationUnit);
            }
        }
    }
}
