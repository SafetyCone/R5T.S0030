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
    /// There is a long list of service implementations without marker attribute (or interface) created by <see cref="O004_IdentifyPossibleServiceImplementations"/>.
    /// For the hundreds of service definitions on that list, modify the code file, containing project, and containing solution to include the <see cref="T0064.ServiceImplementationMarkerAttribute"/> and interface.
    /// </summary>
    public class O201_AddServiceImplementationMarkerAttributeAndInterface : IActionOperation
    {
        private IProjectRepository ProjectRepository { get; }
        private IStringlyTypedPathOperator StringlyTypedPathOperator { get; }
        private IVisualStudioProjectFileOperator VisualStudioProjectFileOperator { get; }
        private IVisualStudioProjectFileReferencesProvider VisualStudioProjectFileReferencesProvider { get; }
        private IVisualStudioSolutionFileOperator VisualStudioSolutionFileOperator { get; }


        public O201_AddServiceImplementationMarkerAttributeAndInterface(
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

            /// Run.
            var failures = new List<Failure<ServiceImplementation>>();

            /// Verify project(s) containing the code file path, and solution(s) containing the project(s), have references to R5T.T0064.
            var r5T_T0064_ProjectIdentityString = Instances.ProjectPath.R5T_T0064();
            var r5T_T0064_ProjectFilePath = await this.ProjectRepository.GetFilePathForProjectIdentityString(r5T_T0064_ProjectIdentityString);

            // Keep a list of all project files that have been verified to contain a reference to R5T.T0064.
            var verifiedProjectFilePathsHash = new HashSet<string>();

            // Keep a list of all solution files that have been verified to contain a reference to R5T.T0064.
            var verifiedSolutionFilePathsHash = new HashSet<string>();

            var serviceImplementations = JsonFileHelper.LoadFromFile<ServiceImplementation[]>(missingMarkerDataFilePath);

            foreach (var serviceImplementation in serviceImplementations)
            {
                // Find the project file path(s) for the code file path.
                var projectFilePaths = Instances.ProjectPathsOperator.FindProjectFilesInFileDirectoryOrDirectParentDirectories(
                    serviceImplementation.CodeFilePath);

                // For each project, ensure it has a reference to R5T.T0064, and that the solution files that contain it have a reference to R5T.T0064.
                foreach (var projectFilePath in projectFilePaths)
                {
                    var alreadyVerifiedProject = verifiedProjectFilePathsHash.Contains(projectFilePath);
                    if (!alreadyVerifiedProject)
                    {
                        // Ensure it has a *recursive* reference to R5T.T0064.
                        await VisualStudioProjectFileOperator.EnsureHasProjectReferenceRecursivelyElseAddDirect(
                            projectFilePath,
                            r5T_T0064_ProjectFilePath,
                            this.VisualStudioProjectFileReferencesProvider);

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
                var compilationUnit = await Instances.CompilationUnitOperator.Load_AndStandardizeToLeadingTrivia(serviceImplementation.CodeFilePath);

                // Add the R5T.T0064 namespace to the compilation unit.
                var hasR5T_T0064_Namespace = compilationUnit.HasUsing(
                    Instances.NamespaceName.R5T_T0064());

                if (!hasR5T_T0064_Namespace)
                {
                    compilationUnit = compilationUnit.AddUsing(
                        Instances.NamespaceName.R5T_T0064());
                }

                var hasServiceImplementationClass = compilationUnit.HasClassByNamespacedTypeName(serviceImplementation.TypeName);
                if (!hasServiceImplementationClass)
                {
                    failures.Add(
                        Failure.Of(serviceImplementation, $"Unable to find service implementation class: {serviceImplementation.TypeName}"));

                    continue;
                }

                var originalServiceImplementationInterface = hasServiceImplementationClass.Result;
                var serviceImplementationInterface = originalServiceImplementationInterface;

                // Does it have the marker attribute?
                var hasMarkerAttribute = Instances.ClassOperator.HasServiceImplementationMarkerAttribute(
                    serviceImplementationInterface);

                // If not, add it (immediately before the type declaration, at the end of the base types list).
                if(!hasMarkerAttribute)
                {
                    var attribute = Instances.SyntaxFactory.Attribute(
                        Instances.AttributeTypeName.GetEnsuredNonAttributeSuffixedTypeName(
                            Instances.TypeName.ServiceImplementationMarkerAttribute()));

                    var attributeList = Instances.SyntaxFactory.AttributeList();

                    attributeList = attributeList.AddAttributes(attribute);

                    serviceImplementationInterface = serviceImplementationInterface.WithAttributeLists(
                        serviceImplementationInterface.AttributeLists.Add(attributeList));
                }

                // Does it have the marker interface?
                var hasMarkerInterface = Instances.ClassOperator.HasServiceImplementationMarkerInterface(
                    serviceImplementationInterface);

                // If not, add it (at the end of the base types list).
                if(!hasMarkerInterface)
                {
                    var baseType = Instances.SyntaxFactory.BaseType(
                        Instances.TypeName.IServiceImplementation());

                    serviceImplementationInterface = serviceImplementationInterface.AddBaseListTypes(baseType);
                }

                compilationUnit = compilationUnit.ReplaceNode_Better(originalServiceImplementationInterface, serviceImplementationInterface);

                await Instances.CompilationUnitOperator.Save(
                    serviceImplementation.CodeFilePath,
                    compilationUnit);
            }
        }
    }
}
