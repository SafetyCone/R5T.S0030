using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.N0;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar.Extensions;

using R5T.D0079;
using R5T.D0105;
using R5T.T0020;

using R5T.S0030.F002;
using R5T.S0030.T003.N001;
using R5T.S0030.Z001;

using R5T.S0030.Library;

using N001 = R5T.S0030.T003.N001;
using N002 = R5T.S0030.T003.N002;
using N003 = R5T.S0030.T003.N003;
using N004 = R5T.S0030.T003.N004;
using N005 = R5T.S0030.T003.N005;
using N007 = R5T.S0030.T003.N007;
using N008 = R5T.S0030.T003.N008;
using N009 = R5T.S0030.T003.N009;
using N010 = R5T.S0030.T003.N010;
using N013 = R5T.S0030.T003.N013;

using Instances = R5T.S0030.Instances;


namespace R5T.S0030
{
    /// <summary>
    /// Experiment to create an AddX() method for a service implementation.
    /// </summary>
    public class E001_CodeElementCreation : IActionOperation
    {
        private N003.IClassContextProvider ClassContextProvider_N003 { get; }
        private N004.IClassContextProvider ClassContextProvider_N004 { get; }
        private N010.ICodeFileContextProvider CodeFileContextProvider_N010 { get; }
        private N001.ICompilationUnitContextProvider CompilationUnitContextProvider { get; }
        private N013.IInterfaceContextProvider InterfaceContextProvider { get; }
        private N007.ILocalRepositoryContextProvider LocalRepositoryContextProvider_N007 { get; }
        private N002.INamespaceContextProvider NamespaceContextProvider { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private N009.IProjectContextProvider ProjectContextProvider { get; }
        private N005.IRemoteRepositoryContextProvider RemoteRepositoryContextProvider_N005 { get; }
        private IServiceImplementationCodeFilePathsProvider ServiceImplementationCodeFilePathsProvider { get; }
        private Repositories.IServiceRepository ServiceRepository { get; }
        private N008.ISolutionContextProvider SolutionContextProvider { get; }


        public E001_CodeElementCreation(
            N003.IClassContextProvider classContextProvider_N003,
            N004.IClassContextProvider classContextProvider_N004,
            N010.ICodeFileContextProvider codeFileContextProvider_N010,
            N001.ICompilationUnitContextProvider compilationUnitContextProvider,
            N013.IInterfaceContextProvider interfaceContextProvider,
            N007.ILocalRepositoryContextProvider localRepositoryContextProvider,
            N002.INamespaceContextProvider namespaceContextProvider,
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            N009.IProjectContextProvider projectContextProvider,
            N005.IRemoteRepositoryContextProvider remoteRepositoryContextProvider_N005,
            IServiceImplementationCodeFilePathsProvider serviceImplementationCodeFilePathsProvider,
            Repositories.IServiceRepository serviceRepository,
            N008.ISolutionContextProvider solutionContextProvider)
        {
            this.ClassContextProvider_N003 = classContextProvider_N003;
            this.ClassContextProvider_N004 = classContextProvider_N004;
            this.CodeFileContextProvider_N010 = codeFileContextProvider_N010;
            this.CompilationUnitContextProvider = compilationUnitContextProvider;
            this.InterfaceContextProvider = interfaceContextProvider;
            this.LocalRepositoryContextProvider_N007 = localRepositoryContextProvider;
            this.NamespaceContextProvider = namespaceContextProvider;
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.ProjectContextProvider = projectContextProvider;
            this.RemoteRepositoryContextProvider_N005 = remoteRepositoryContextProvider_N005;
            this.ServiceImplementationCodeFilePathsProvider = serviceImplementationCodeFilePathsProvider;
            this.ServiceRepository = serviceRepository;
            this.SolutionContextProvider = solutionContextProvider;
        }

        public async Task Run()
        {
            //await this.CreateCompilationUnit();
            //await this.CreateCompilationUnitAddLotsOfUsings();
            //await this.CreateCompilationWithNamespace();
            //await this.CreateClass();
            //await this.CreateClass_WithN004();
            //await this.CreateAddXMethod_V00();
            //await this.CreateAddXMethod_V01();
            //await this.CreateAddXActionMethod_V00();
            //await this.CreateAllAddMethodsForProject();
            //await this.SearchForAddXMethods();
            //await this.CreateRepository();
            //await this.DeleteRepository();
            //await this.CreateAndCloneRepository();
            //await this.DeleteRepositoryBothLocalAndRemote();
            //await this.CreateSolutionInLocalRepository();
            //await this.DeleteLocalAndReClone();
            //await this.CreateProject();
            //await this.DeleteProjectFile();
            //await this.DeleteProjectDirectory();
            //await this.CreateProjectAndAddToSolution();
            //await this.CreateTextFileInProject();

            await this.CreateNamespaceDomainDirectories();
        }

        #region Experiments

#pragma warning disable IDE0051 // Remove unused private members

        private async Task CreateNamespaceDomainDirectories()
        {
            /// Inputs.
            var codeDirectoryPath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0030\source\R5T.S0030.T003\Code\N011\";
            var namespaceNumber = "N011";
            var projectName = "R5T.S0030.T003";
            var contextNameStem = "SolutionFileSet";
            var documentationComment = "Context for a set of multiple solutions with functionality for adding project references.";

            /// Run.
            var contextName = contextNameStem + "Context";

            var contextClassTypeName = contextName;
            var contextInterfaceTypeName = Instances.TypeNameOperator.GetInterfaceName(contextClassTypeName);

            var contextClassName = Instances.TypeNameOperator.GetClassName(contextName);
            var contextInterfaceName = Instances.TypeNameOperator.GetInterfaceName(contextName);

            var hasContextInterfaceTypeName = Instances.TypeNameOperator.GetInterfaceName("Has" + contextClassName);

            var serviceNameStem = Instances.ServiceNameOperator.GetServiceNameStem_FromClassTypeName(contextClassName);

            var contextProviderServiceNameStem = Instances.ServiceNameOperator.GetProviderServiceNameStem(serviceNameStem);
            var contextProviderServiceDefinitionInterfaceTypeName = Instances.ServiceNameOperator.GetServiceDefinitionInterfaceTypeName(contextProviderServiceNameStem);
            var contextProviderServiceImplementationClassTypeName = Instances.ServiceNameOperator.GetServiceImplementationClassTypeName(contextProviderServiceNameStem);

            var contextProviderServiceDefinitionInterfaceExtensionsTypeName = Instances.TypeNameOperator.GetExtensionsOfTypeNameTypeName(contextProviderServiceDefinitionInterfaceTypeName);

            var defaultNamespaceName = Instances.ProjectNameOperator.GetDefaultNamespaceNameFromProjectName(projectName);
            var numberedNamespaceName = Instances.NamespaceNameOperator.CombineTokens(
                defaultNamespaceName,
                namespaceNumber);

            // Get directories.
            var basesExtensionsDirectoryPath = Instances.ProjectPathsOperator.GetBasesExtensionsDirectoryPath(codeDirectoryPath);

            var classesDirectoryPath = Instances.ProjectPathsOperator.GetClassesDirectoryPath(codeDirectoryPath);

            var extensionsDirectoryPath = Instances.ProjectPathsOperator.GetExtensionsDirectoryPath(codeDirectoryPath);

            var interfacesDirectoryPath = Instances.ProjectPathsOperator.GetInterfacesDirectoryPath(codeDirectoryPath);

            var servicesDefinitionsDirectoryPath = Instances.ProjectPathsOperator.GetServicesDefinitionsDirectoryPath(codeDirectoryPath);

            var servicesExtensionsDirectoryPath = Instances.ProjectPathsOperator.GetServicesExtensionsDirectoryPath(codeDirectoryPath);

            var servicesImplementationsDirectoryPath = Instances.ProjectPathsOperator.GetServicesImplementationsDirectoryPath(codeDirectoryPath);

            // Create all directories.
            var allDirectoryPaths = new[]
            {
                basesExtensionsDirectoryPath,
                classesDirectoryPath,
                extensionsDirectoryPath,
                interfacesDirectoryPath,
                servicesDefinitionsDirectoryPath,
                servicesExtensionsDirectoryPath,
                servicesImplementationsDirectoryPath,
            };

            foreach (var directoryPath in allDirectoryPaths)
            {
                Instances.FileSystemOperator.CreateDirectory(directoryPath);
            }

            // Now create code files.
            // IServiceActionExtensions.
            var iServiceActionExtensionCodeFilePath = Instances.PathOperator.GetFilePath(
                basesExtensionsDirectoryPath,
                Instances.CodeFileName.GetCSharpFileNameForTypeName(
                    Instances.TypeName.IServiceActionExtensions()));

            await this.CodeFileContextProvider_N010.For(
                iServiceActionExtensionCodeFilePath,
                Instances.CodeFileGenerator.CreateEmptyIServiceActionExtensions(
                    numberedNamespaceName,
                    this.ClassContextProvider_N004));

            // IServiceCollectionExtensions
            var iServiceCollectionExtensionCodeFilePath = Instances.PathOperator.GetFilePath(
                extensionsDirectoryPath,
                Instances.CodeFileName.GetCSharpFileNameForTypeName(
                    Instances.TypeName.IServiceCollectionExtensions()));

            await this.CodeFileContextProvider_N010.For(
                iServiceCollectionExtensionCodeFilePath,
                Instances.CodeFileGenerator.CreateEmptyIServiceCollectionExtensions(
                    numberedNamespaceName,
                    this.ClassContextProvider_N004));

            // Service definition.
            var contextProviderServiceDefinitionCodeFilePath = Instances.PathOperator.GetFilePath(
                servicesDefinitionsDirectoryPath,
                Instances.CodeFileName.GetCSharpFileNameForTypeName(
                    contextProviderServiceDefinitionInterfaceTypeName));

            await this.CodeFileContextProvider_N010.For(
                contextProviderServiceDefinitionCodeFilePath,
                Instances.CodeFileGenerator.CreateServiceDefinitionInterface(
                    numberedNamespaceName,
                    contextProviderServiceDefinitionInterfaceTypeName,
                    this.InterfaceContextProvider));

            // Service implementation.
            var contextProviderServiceImplementationCodeFilePath = Instances.PathOperator.GetFilePath(
                servicesImplementationsDirectoryPath,
                Instances.CodeFileName.GetCSharpFileNameForTypeName(
                    contextProviderServiceImplementationClassTypeName));

            await this.CodeFileContextProvider_N010.For(
                contextProviderServiceImplementationCodeFilePath,
                Instances.CodeFileGenerator.CreateServiceImplementationClass(
                    numberedNamespaceName,
                    contextProviderServiceImplementationClassTypeName,
                    contextProviderServiceDefinitionInterfaceTypeName,
                    this.ClassContextProvider_N004));

            // Create the extensions class.
            var contextProviderServiceDefinitionExtensionsCodeFilePath = Instances.PathOperator.GetFilePath(
                servicesExtensionsDirectoryPath,
                Instances.CodeFileName.GetCSharpFileNameForTypeName(
                    contextProviderServiceDefinitionInterfaceExtensionsTypeName));

            await this.CodeFileContextProvider_N010.For(
                contextProviderServiceDefinitionExtensionsCodeFilePath,
                Instances.CodeFileGenerator.CreateServiceDefinitionInterfaceExtensions(
                    numberedNamespaceName,
                    contextProviderServiceDefinitionInterfaceTypeName,
                    contextProviderServiceDefinitionInterfaceExtensionsTypeName,
                    this.ClassContextProvider_N004));

            // Create the context interface.
            var contextInterfaceCodeFilePath = Instances.PathOperator.GetFilePath(
                interfacesDirectoryPath,
                Instances.CodeFileName.GetCSharpFileNameForTypeName(
                    contextInterfaceTypeName));

            await this.CodeFileContextProvider_N010.For(
                contextInterfaceCodeFilePath,
                Instances.CodeFileGenerator.CreateContextInteface(
                    numberedNamespaceName,
                    contextInterfaceTypeName,
                    hasContextInterfaceTypeName,
                    this.InterfaceContextProvider));

            // Create the has-context interface.
            var hasContextInterfaceCodeFilePath = Instances.PathOperator.GetFilePath(
                interfacesDirectoryPath,
                Instances.CodeFileName.GetCSharpFileNameForTypeName(
                    hasContextInterfaceTypeName));

            await this.CodeFileContextProvider_N010.For(
                hasContextInterfaceCodeFilePath,
                Instances.CodeFileGenerator.CreateHasContextInterface(
                    numberedNamespaceName,
                    namespaceNumber,
                    contextInterfaceTypeName,
                    hasContextInterfaceTypeName,
                    this.InterfaceContextProvider));

            // Create the context class.
            var contextClassCodeFilePath = Instances.PathOperator.GetFilePath(
                classesDirectoryPath,
                Instances.CodeFileName.GetCSharpFileNameForTypeName(
                    contextClassTypeName));

            var contextInterfaceNamespacedTypeName = Instances.NamespacedTypeNameOperator.GetNamespacedTypeName(
                numberedNamespaceName,
                contextInterfaceTypeName);

            await this.CodeFileContextProvider_N010.For(
                contextClassCodeFilePath,
                Instances.CodeFileGenerator.CreateContextClass(
                    numberedNamespaceName,
                    contextClassTypeName,
                    contextInterfaceNamespacedTypeName,
                    this.ClassContextProvider_N004));
        }

        ///// <summary>
        ///// In an existing project, create a code file.
        ///// </summary>
        //private async Task CreateCodeFileInProject()
        //{
        //    /// Inputs.
        //    var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.Testing.S0030\source\R5T.Testing.S0030\R5T.Testing.S0030.csproj";

        //    /// Run.
        //    await this.ProjectContextProvider.In(
        //        projectFilePath,
        //        projectContext =>
        //        {
        //            //var solutionFiles = projectContext.GetSolutionFilesInDirectoryOrDirectParentDirectories();

        //            this.code
        //        });
        //}

        /// <summary>
        /// In an existing project, create a text file.
        /// </summary>
        private async Task CreateTextFileInProject()
        {
            /// Inputs.
            var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.Testing.S0030\source\R5T.Testing.S0030\R5T.Testing.S0030.csproj";

            var projectDescription = Instances.Example.ProjectDescription();

            /// Run.
            await this.ProjectContextProvider.In(
                projectFilePath,
                async projectContext =>
                {
                    var projectPlanFilePath = projectContext.GetProjectDirectoryRelativeFilePath(
                        Instances.ProjectPathsOperator.GetProjectPlanFilePath_Standard);

                    var line = $"{projectContext.GetProjectName()} - {projectDescription}";

                    await FileHelper.WriteAllLines(
                        projectPlanFilePath,
                        lines: line);
                });
        }

        private async Task CreateProjectAndAddToSolution()
        {
            /// Inputs.
            var solutionFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.Testing.S0030\source\R5T.Testing.S0030.sln";

            var libraryName = Instances.Example.LibraryName();

            /// Run.
            var projectName = Instances.LibraryNameOperator.GetProjectName(libraryName);

            await this.SolutionContextProvider.In(
                solutionFilePath,
                async solutionContext =>
                {
                    await this.ProjectContextProvider.InAcquired(
                        projectName,
                        solutionContext,
                        VisualStudioProjectType.Console,
                        async projectContext =>
                        {
                            await solutionContext.AddProjectReference(
                                projectContext.ProjectFilePath);
                        });

                    var solutionProjectReferences = await solutionContext.ListProjectReferences();

                    var line = String.Join(
                        "\n",
                        solutionProjectReferences);

                    Console.WriteLine(line);
                });
        }

        /// <summary>
        /// Deletes the whole project directory.
        /// </summary>
        private async Task DeleteProjectDirectory()
        {
            /// Inputs.
            var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.Testing.S0030\source\R5T.Testing.S0030\R5T.Testing.S0030.csproj";

            /// Run.
            await this.ProjectContextProvider.For(
                projectFilePath,
                projectContext =>
                {
                    var exists = projectContext.ProjectDirectoryExists();
                    if (exists)
                    {
                        projectContext.DeleteProjectDirectory_OkIfNotExist();
                    }

                    return Task.CompletedTask;
                });
        }

        /// <summary>
        /// Deletes the project file (as opposed to the whole project directory).
        /// </summary>
        private async Task DeleteProjectFile()
        {
            /// Inputs.
            var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.Testing.S0030\source\R5T.Testing.S0030\R5T.Testing.S0030.csproj";

            /// Run.
            await this.ProjectContextProvider.For(
                projectFilePath,
                projectContext =>
                {
                    var exists = projectContext.ProjectFileExists();
                    if (exists)
                    {
                        projectContext.DeleteProjectFile_OkIfNotExists();
                    }

                    return Task.CompletedTask;
                });
        }

        private async Task CreateProject()
        {
            /// Inputs.
            var libraryName = Instances.Example.LibraryName();
            var solutionDirectoryPath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.Testing.S0030\source";

            /// Run.
            var projectName = Instances.LibraryNameOperator.GetProjectName(libraryName);

            var projectDirectoryPath = Instances.ProjectPathsOperator.GetProjectDirectoryPath(
                solutionDirectoryPath,
                projectName);

            var projectFilePath = Instances.ProjectPathsOperator.GetProjectFilePath(
                projectDirectoryPath,
                projectName);

            await this.ProjectContextProvider.InAcquired(
                projectFilePath,
                VisualStudioProjectType.Console,
                projectContext =>
                {
                    var exists = projectContext.ProjectFileExists();

                    Console.WriteLine($"Project file exists: {exists}\n\t{projectContext.ProjectFilePath}");

                    return Task.CompletedTask;
                });
        }

        /// <summary>
        /// Deletes the local repository, re-clones it, then creates a new solution file.
        /// </summary>
        private async Task DeleteLocalAndReClone()
        {
            /// Inputs.
            var libraryName = Instances.Example.LibraryName();

            /// Run.
            var repositoryName = Instances.LibraryNameOperator.GetRepositoryName(libraryName);
            var solutionName = Instances.LibraryNameOperator.GetSolutionName(libraryName);

            await this.LocalRepositoryContextProvider_N007.For(
                repositoryName,
                LocalRepositoryModifier);

            Task SolutionModifier(N008.ISolutionContext solutionContext)
            {
                var exists = solutionContext.SolutionFileExists();

                Console.WriteLine($"Exists: {exists}");

                return Task.CompletedTask;
            }

            async Task LocalRepositoryModifier(N007.ILocalRepositoryContext localRepositoryContext)
            {
                // Delete local.
                localRepositoryContext.DeleteLocalRepositoryDirectory();

                // Re-clone.
                await localRepositoryContext.Clone();

                var solutionFilePath = localRepositoryContext.GetSourceSolutionFilePath(
                    solutionName);

                await this.SolutionContextProvider.InAcquired(
                    solutionFilePath,
                    SolutionModifier);
            }
        }

        private async Task CreateSolutionInLocalRepository()
        {
            /// Inputs.
            var libraryName = Instances.Example.LibraryName();

            var description = Instances.Example.RepositoryDescription();
            var isPrivate = false;

            /// Run.
            var repositoryName = Instances.LibraryNameOperator.GetRepositoryName(libraryName);
            var solutionName = Instances.LibraryNameOperator.GetSolutionName(libraryName);

            await this.LocalRepositoryContextProvider_N007.InCreatedThenCloned(
                repositoryName,
                description,
                isPrivate,
                LocalRepositoryModifier);

            Task SolutionModifier(N008.ISolutionContext solutionContext)
            {
                var exists = solutionContext.SolutionFileExists();

                Console.WriteLine($"Exists: {exists}");

                return Task.CompletedTask;
            }

            // Use an internal method to choose the context type.
            Task LocalRepositoryModifier(N007.ILocalRepositoryContext localRepositoryContext)
            {
                var solutionFilePath = localRepositoryContext.GetSourceSolutionFilePath(
                    solutionName);

                return this.SolutionContextProvider.InAcquired(
                    solutionFilePath,
                    SolutionModifier);
            }
        }

        private async Task DeleteRepositoryBothLocalAndRemote()
        {
            /// Inputs.
            var repositoryName = Instances.Example.RepositoryName();

            /// Run.
            await this.LocalRepositoryContextProvider_N007.In(
                repositoryName,
                DeleteRepositoryBothLocalAndRemote_Internal);

            static Task DeleteRepositoryBothLocalAndRemote_Internal(N007.ILocalRepositoryContext localRepositoryContext)
            {
                localRepositoryContext.DeleteLocalRepositoryDirectory();

                return localRepositoryContext.DeleteRemoteRepository();
            }
        }

        private async Task CreateAndCloneRepository()
        {
            /// Inputs.
            var repositoryName = Instances.Example.RepositoryName();
            var description = Instances.Example.RepositoryDescription();

            var isPrivate = true;

            /// Run.
            await this.LocalRepositoryContextProvider_N007.InCreatedThenCloned(
                repositoryName,
                description,
                isPrivate,
                CreateAndCloneRepository_Internal);

            // Use an internal method to choose the context type.
            static Task CreateAndCloneRepository_Internal(N007.ILocalRepositoryContext localRepositoryContext)
            {
                localRepositoryContext.InGitHubRepository(gitHubRepository =>
                {
                    Console.WriteLine($"{gitHubRepository.Name}:\n\tPrivate: {gitHubRepository.IsPrivate}\n\tDescription: \"{gitHubRepository.Description}\"");

                    return Task.CompletedTask;
                });

                return Task.CompletedTask;
            }
        }

        private async Task DeleteRepository()
        {
            /// Inputs.
            var repositoryName = Instances.Example.RepositoryName();

            /// Run.
            await this.RemoteRepositoryContextProvider_N005.In(
                repositoryName,
                remoteRepositoryContext =>
                {
                    return remoteRepositoryContext.DeleteRemoteRepository(); 
                });
        }

        private async Task CreateRepository()
        {
            /// Inputs.
            var repositoryName = Instances.Example.RepositoryName();
            var description = Instances.Example.RepositoryDescription();

            var isPrivate = true;

            /// Run.
            await this.RemoteRepositoryContextProvider_N005.InCreated(
                repositoryName,
                description,
                isPrivate,
                async remoteRepositoryContext =>
                {
                    //var exists = await remoteRepositoryContext.Exists();
                    //var description = await remoteRepositoryContext.GetDescription();
                    //var isPrivate = await remoteRepositoryContext.IsPrivate();

                    //var (exists, description, isPrivate) = await TaskHelper.WhenAll(
                    //    remoteRepositoryContext.Exists(),
                    //    remoteRepositoryContext.GetDescription(),
                    //    remoteRepositoryContext.IsPrivate());

                    var exists = await remoteRepositoryContext.Exists();
                    if(exists)
                    {
                        await remoteRepositoryContext.GitHubOperator.InGitHubRepository_SafetyCone(
                            repositoryName,
                            repository =>
                            {
                                Console.WriteLine($"{repository.Name}:\n\tPrivate: {repository.IsPrivate}\n\tDescription: \"{repository.Description}\"");

                                return Task.CompletedTask;
                            });
                    }
                    else
                    {
                        Console.WriteLine($"{remoteRepositoryContext.Name}: repository does not exist.");
                    }
                });
        }

        //private async Task UpdateAddMethods()
        //{
        //    /// Inputs.
        //    var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0030\source\R5T.S0030\R5T.S0030.csproj";

        //    var addXMethodsFilePath = @"C:\Temp\AddXMethods.txt";
        //    var addXActionMethodsFilePath = @"C:\Temp\AddXActionMethods.txt";

        //    /// Run.
        //    // Search for AddX() methods.
        //    var addXMethodSearchResult = await Instances.Operation.SearchForAddXMethods(projectFilePath);
        //    var addXActionMethodSearchResult = await Instances.Operation.SearchForAddXActionMethods(projectFilePath);

        //    // Search for and describe service implementations.
        //    var serviceImplementations = await Instances.Operation.DescribeServiceImplementationsInProject(
        //        projectFilePath,
        //        this.ServiceImplementationCodeFilePathsProvider,
        //        this.ServiceRepository);


        //}

        private async Task SearchForAddXMethods()
        {
            // Really, searching a project for extensions to IServiceCollection.

            /// Inputs.
            var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0030\source\R5T.S0030\R5T.S0030.csproj";

            var outputFilePath = @"C:\Temp\AddXMethods.txt";

            /// Run.
            var projectResult = await Instances.Operation.SearchForAddXMethods(projectFilePath);

            // Write output.
            var lines = projectResult.CodeFileResults
                .SelectMany(xCodeFileResult => xCodeFileResult.ClassResults
                    .SelectMany(xClassResult => xClassResult.MethodResults
                        .Select(xMethodResult => (xMethodResult.MethodName, xClassResult.ClassName, xCodeFileResult.CodeFilePath))))
                .GroupBy(x => x.CodeFilePath)
                .OrderBy(x => x.Key)
                .SelectMany(xGroup =>
                {
                    var output = EnumerableHelper.From($"{xGroup.Key}")
                        .OrderAlphabetically()
                        .Append(xGroup
                            .Select(xTuple => $"\t{xTuple.MethodName}"))
                        .Append(String.Empty)
                        ;

                    return output;
                })
                ;

            await FileHelper.WriteAllLines(
                outputFilePath,
                lines);

            /// Display output.
            await this.NotepadPlusPlusOperator.OpenFilePath(outputFilePath);
        }

        private async Task CreateAllAddMethodsForProject()
        {
            /// Inputs.
            var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0030\source\R5T.S0030\R5T.S0030.csproj";
            var failuresFilePath = @"C:\Temp\Failures.txt";

            var namespaceNameForExtensionsClasses = "R5T.S0030.E001";
            var serviceActionExtensionsCodeFilePath = @"C:\Temp\IServiceActionExtensions.cs";
            var serviceCollectionExtensionsCodeFilePath = @"C:\Temp\IServiceCollectionExtensions.cs";

            /// Run.
            var projectResult = await Instances.Operation.DescribeServiceImplementationsInProject(
                projectFilePath,
                this.ServiceImplementationCodeFilePathsProvider,
                this.ServiceRepository);

            var implementationAddMethodSets = Instances.Operation.GetAddMethodSets(
                projectResult,
                out var failures);

            // Determine namespaces required by service implementations.
            var requiredNamespaceNames = implementationAddMethodSets
                .SelectMany(x => x.ImplementationDescriptor.GetRequiredNamespaces())
                .Distinct()
                .OrderAlphabetically()
                .Now();

            // Create IServiceActionExtensions class.
            var typeNameForIServiceActionExtensions = Instances.TypeName.IServiceActionExtensions();

            var iServiceCollectionExtensionsNamespacedTypeName = Instances.NamespacedTypeNameOperator.GetNamespacedTypeName(
                namespaceNameForExtensionsClasses,
                typeNameForIServiceActionExtensions);

            var serviceActionExtensionsCompilationUnit = await this.ClassContextProvider_N004.InAcquired(
                iServiceCollectionExtensionsNamespacedTypeName,
                (compilationUnit, classContext) =>
                {
                    var allRequiredNamespaceNames = requiredNamespaceNames
                        .Append(
                            Instances.NamespaceNameOperator.GetAddXActionMethodsRequiredNamespaces())
                        .Now();

                    foreach (var addMethodSet in implementationAddMethodSets)
                    {
                        compilationUnit = Instances.MethodOperator.AddMethod(
                            compilationUnit,
                            classContext,
                            requiredNamespaceNames,
                            addMethodSet.AddXActionMethod);
                    }

                    // Sort and space methods within the class.
                    compilationUnit = classContext.ClassAnnotation.ModifySynchronous(
                        compilationUnit,
                        xClass => xClass
                            .SortMethods()
                            .SetMethodSpacing(
                                Instances.LineIndentation.BlankLine_SyntaxTriviaList()
                                    .Append(Instances.Indentation.ByTabCount_SyntaxTriviaList(2))));

                    return Task.FromResult(compilationUnit);
                },
                classConstructor: Instances.ClassGenerator.GetPublicStaticPartialClass,
                compilationContextOptionsModifier: compilationUnitOptions =>
                {
                    compilationUnitOptions.AddUsingNamespace_System_Threading_Tasks = false;
                });

            await serviceActionExtensionsCompilationUnit.WriteTo(serviceActionExtensionsCodeFilePath);

            // Create IServiceCollectionExtensions class.
            var typeNameForIServiceCollectionExtensions = Instances.TypeName.IServiceCollectionExtensions();

            var iServiceActionExtensionsNamespacedTypeName = Instances.NamespacedTypeNameOperator.GetNamespacedTypeName(
                namespaceNameForExtensionsClasses,
                typeNameForIServiceCollectionExtensions);

            var serviceCollectionExtensionsCompilationUnit = await this.ClassContextProvider_N004.InAcquired(
                iServiceActionExtensionsNamespacedTypeName,
                (compilationUnit, classContext) =>
                {
                    var allRequiredNamespaceNames = requiredNamespaceNames
                        .Append(
                            Instances.NamespaceNameOperator.GetAddXMethodsRequiredNamespaces())
                        .Now();

                    foreach (var addMethodSet in implementationAddMethodSets)
                    {
                        compilationUnit = Instances.MethodOperator.AddMethod(
                            compilationUnit,
                            classContext,
                            requiredNamespaceNames,
                            addMethodSet.AddXMethod);
                    }

                    // Sort and space methods within the class.
                    compilationUnit = classContext.ClassAnnotation.ModifySynchronous(
                        compilationUnit,
                        xClass => xClass
                            .SortMethods()
                            .SetMethodSpacing(
                                Instances.LineIndentation.BlankLine_SyntaxTriviaList()
                                    .Append(Instances.Indentation.ByTabCount_SyntaxTriviaList(2))));

                    return Task.FromResult(compilationUnit);
                },
                classConstructor: Instances.ClassGenerator.GetPublicStaticPartialClass,
                compilationContextOptionsModifier: compilationUnitOptions =>
                {
                    compilationUnitOptions.AddUsingNamespace_System_Threading_Tasks = false;
                });

            await serviceCollectionExtensionsCompilationUnit.WriteTo(serviceCollectionExtensionsCodeFilePath);

            // Write out failures.
            var failureLines = failures
                .GroupBy(x => x.Value)
                .OrderAlphabetically(x => x.Key)
                .SelectMany(x =>
                {
                    return EnumerableHelper.From($"Service: {x.Key}")
                        .Append(x
                            .Select(x => $"\t{x.Message}")
                            .OrderAlphabetically())
                        // New line between reasons.
                        .Append(String.Empty)
                        ;
                })
                .Now();

            await FileHelper.WriteAllLines(
                failuresFilePath,
                failureLines);

            /// Show outputs.
            await this.NotepadPlusPlusOperator.OpenFilePath(serviceCollectionExtensionsCodeFilePath);
            await this.NotepadPlusPlusOperator.OpenFilePath(serviceActionExtensionsCodeFilePath);
            await this.NotepadPlusPlusOperator.OpenFilePath(failuresFilePath);
        }

        private async Task CreateAddXActionMethod_V00()
        {
            /// Inputs.
            var namespaceNameForExtensions = "R5T.S0030.E001";

            var codeFilePath = Instances.Example.CodeFilePath();
            var serviceImplementation = Instances.Example.BasicServiceImplementationDescriptor().ToImplementationDescriptor_Level02();

            /// Run.
            // Get output file path.
            var fileNameForIServiceActionExtensions = Instances.CodeFileName.IServiceActionExtensions();

            var tempDirectoryPath = @"C:\Temp";

            var codeFilePathForIServiceCollectionExtensions = Instances.PathOperator.GetFilePath(
                tempDirectoryPath,
                fileNameForIServiceActionExtensions);

            // Create IServiceActionExtensions class.
            var typeNameForIServiceActionExtensions = Instances.TypeName.IServiceActionExtensions();

            var iServiceActionExtensionsNamespacedTypeName = Instances.NamespacedTypeNameOperator.GetNamespacedTypeName(
                namespaceNameForExtensions,
                typeNameForIServiceActionExtensions);

            var compilationUnit = await this.ClassContextProvider_N004.InAcquired(
                iServiceActionExtensionsNamespacedTypeName,
                classModificationAction: Instances.MethodGenerator.CreateAddXActionMethod(serviceImplementation),
                classConstructor: Instances.ClassGenerator.GetPublicStaticPartialClass,
                compilationContextOptionsModifier: compilationUnitOptions =>
                {
                    compilationUnitOptions.AddUsingNamespace_System_Threading_Tasks = false;
                });

            await compilationUnit.WriteTo(codeFilePath);
        }

        private async Task CreateAddXMethod_V01()
        {
            /// Inputs.
            var namespaceNameForIServiceCollectionExtensions = "R5T.S0030.E001";

            var codeFilePath = Instances.Example.CodeFilePath();
            var serviceImplementation = Instances.Example.BasicServiceImplementationDescriptor().ToImplementationDescriptor_Level02();

            /// Run.
            // Get output file path.
            var fileNameForIServiceCollectionExtensions = Instances.CodeFileName.IServiceCollectionExtensions();

            var tempDirectoryPath = @"C:\Temp";

            var codeFilePathForIServiceCollectionExtensions = Instances.PathOperator.GetFilePath(
                tempDirectoryPath,
                fileNameForIServiceCollectionExtensions);

            // Create IServiceCollectionExtensions class.
            var typeNameForIServiceCollectionExtensions = Instances.TypeName.IServiceCollectionExtensions();

            var iServiceCollectionExtensionsNamespacedTypeName = Instances.NamespacedTypeNameOperator.GetNamespacedTypeName(
                namespaceNameForIServiceCollectionExtensions,
                typeNameForIServiceCollectionExtensions);

            var compilationUnit = await this.ClassContextProvider_N004.InAcquired(
                iServiceCollectionExtensionsNamespacedTypeName,
                classModificationAction: Instances.MethodGenerator.CreateAddXMethod(serviceImplementation),
                classConstructor: Instances.ClassGenerator.GetPublicStaticPartialClass,
                compilationContextOptionsModifier: compilationUnitOptions =>
                {
                    compilationUnitOptions.AddUsingNamespace_System_Threading_Tasks = false;
                });

            await compilationUnit.WriteTo(codeFilePath);
        }

        private async Task CreateAddXMethod_V00()
        {
            /// Inputs.
            var namespaceNameForIServiceCollectionExtensions = "R5T.S0030.E001";
            
            var codeFilePath = Instances.Example.CodeFilePath();
            var serviceImplementation = Instances.Example.BasicServiceImplementationDescriptor().ToImplementationDescriptor_Level02();

            /// Run.
            // Get output file path.
            var fileNameForIServiceCollectionExtensions = Instances.CodeFileName.IServiceCollectionExtensions();

            var tempDirectoryPath = @"C:\Temp";

            var codeFilePathForIServiceCollectionExtensions = Instances.PathOperator.GetFilePath(
                tempDirectoryPath,
                fileNameForIServiceCollectionExtensions);

            // Create the AddX() method.
            // Start by adding namespaces.
            var requiredNamespaceNames = new[]
            {
                Instances.NamespacedTypeNameOperator.GetNamespaceName(serviceImplementation.NamespacedTypeName),
                Instances.NamespacedTypeNameOperator.GetNamespaceName(serviceImplementation.ServiceDefinitionNamespacedTypeName),
            }
            // Service dependency namespaces.
            .Append(
                serviceImplementation.ServiceDependencyNamespacedTypeNames
                    .Select(x => Instances.NamespacedTypeNameOperator.GetNamespaceName(x)))
            // IServiceColletionNamespace.
            .Append(
                Instances.NamespacedTypeNameOperator.GetNamespaceName(
                    Instances.NamespacedTypeName.IServiceCollection()))
            // IServiceAction<T> namespace.
            .Append(
                Instances.NamespacedTypeNameOperator.GetNamespaceName(
                    Instances.NamespacedTypeName.R5T_T0063_IServiceAction()))
            .Now();

            var method = Instances.MethodGenerator.GetAddX(
                serviceImplementation);

            // Create IServiceCollectionExtensions class.
            var typeNameForIServiceCollectionExtensions = Instances.TypeName.IServiceCollectionExtensions();

            var iServiceCollectionExtensionsNamespacedTypeName = Instances.NamespacedTypeNameOperator.GetNamespacedTypeName(
                namespaceNameForIServiceCollectionExtensions,
                typeNameForIServiceCollectionExtensions);

            var compilationUnit = await this.ClassContextProvider_N004.InAcquired(
                iServiceCollectionExtensionsNamespacedTypeName,
                classModificationAction: (compilationUnit, classContext) =>
                {
                    var classIndentation = classContext.GetIndentation(
                        compilationUnit);

                    compilationUnit = classContext.AddUsings(
                        compilationUnit,
                        requiredNamespaceNames);

                    compilationUnit = classContext.ClassAnnotation.ModifySynchronous(
                        compilationUnit,
                        (@class) =>
                        {
                            @class = @class.AddMethod(
                                method.IndentBlock(
                                    Instances.Indentation.IncreaseByTab_SyntaxTriviaList(
                                        classIndentation)));

                            return @class;
                        });

                    return Task.FromResult(compilationUnit);
                },
                classConstructor: Instances.ClassGenerator.GetPublicStaticPartialClass,
                compilationContextOptionsModifier: compilationUnitOptions =>
                {
                    compilationUnitOptions.AddUsingNamespace_System_Threading_Tasks = false;
                });

            await compilationUnit.WriteTo(codeFilePath);
        }

        public async Task CreateClass_WithN004()
        {
            /// Inputs.
            var codeFilePath = Instances.Example.CodeFilePath();
            var namespacedTypeNameForClass = "Example.Namespace.Class1";

            /// Run.
            var compilationUnit = await this.ClassContextProvider_N004.InAcquired(
                namespacedTypeNameForClass,
                (compilationUnit, classContext) =>
                {
                    return Task.FromResult(compilationUnit);
                });

            await compilationUnit.WriteTo(codeFilePath);
        }

        public async Task CreateClass()
        {
            var compilationUnit = await this.CompilationUnitContextProvider.InCreated(
                async (compilationUnit, compilationContext) =>
                {
                    compilationUnit = await this.NamespaceContextProvider.InCreated(
                        Instances.Example.NamespaceName(),
                        compilationUnit,
                        async (compilationUnit, namespaceContext) =>
                        {
                            compilationUnit = await this.ClassContextProvider_N003.InCreated(
                                "Class1",
                                namespaceContext.NamespaceAnnotation,
                                compilationUnit,
                                (compilationUnit, classContext) =>
                                {
                                    // Indent the whole class block.
                                    compilationUnit = classContext.ClassAnnotation.ModifySynchronous(
                                        compilationUnit,
                                        @class => @class.IndentBlock_Old(
                                            Instances.Indentation_Old.Class()));

                                    // Set the indentation of the first line of the class.
                                    compilationUnit = compilationUnit.SetLineIndentation(
                                        classContext.ClassAnnotation,
                                        Instances.Indentation_Old.Class());

                                    return Task.FromResult(compilationUnit);
                                });

                            return compilationUnit;
                        });

                    return compilationUnit;
                });

            var codeFilePath = Instances.Example.CodeFilePath();

            await compilationUnit.WriteTo(codeFilePath);
        }

        public async Task CreateCompilationWithNamespace()
        {
            var compilationUnit = await this.CompilationUnitContextProvider.InCreated(
                async (compilationUnit, compilationContext) =>
                {
                    compilationUnit = compilationUnit
                        .AddUsings(
                            Instances.Example.NamespaceNames())
                        .AddUsings(
                            Instances.Example.NameAliases());

                    compilationUnit = await this.NamespaceContextProvider.InCreated(
                        Instances.Example.NamespaceName(),
                        compilationUnit,
                        (compilationUnit, namespaceContext) =>
                        {
                            return Task.FromResult(compilationUnit);
                        },
                        // Show how to set the separation.
                        Instances.NamespaceOperator.AddNamespace_Simple);

                    return compilationUnit;
                });

            var codeFilePath = Instances.Example.CodeFilePath();

            await compilationUnit.WriteTo(codeFilePath);
        }

        public async Task CreateCompilationUnitAddLotsOfUsings()
        {
            var compilationUnit = await this.CompilationUnitContextProvider.InCreated((compilationUnit, compilationContext) =>
            {
                compilationUnit = compilationUnit
                    .AddUsings(
                        Instances.Example.NamespaceNames())
                    .AddUsings(
                        Instances.Example.NameAliases());

                return Task.FromResult(compilationUnit);
            },
            // Show how to use a local namespace name.
            options =>
            {
                options.CompilationUnitLocalNamespaceNameOverride.SetOverride(
                    Instances.Example.LocalNamespaceName_R5T_S0029());
            });

            var codeFilePath = Instances.Example.CodeFilePath();

            await compilationUnit.WriteTo(codeFilePath);
        }

        public async Task CreateCompilationUnit()
        {
            var compilationUnit = await this.CompilationUnitContextProvider.InCreated((compilationUnit, compilationContext) =>
            {
                return Task.FromResult(compilationUnit);
            });

            var codeFilePath = Instances.Example.CodeFilePath();

            await compilationUnit.WriteTo(codeFilePath);
        }

#pragma warning restore IDE0051 // Remove unused private members

        #endregion
    }
}
