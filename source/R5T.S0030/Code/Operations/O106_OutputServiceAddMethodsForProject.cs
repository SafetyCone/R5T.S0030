using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using R5T.Magyar.Extensions;

using R5T.D0105;
using R5T.T0020;

using R5T.S0030.F002;

using N004 = R5T.S0030.T003.N004;


namespace R5T.S0030
{
    /// <summary>
    /// Given a project file path, discovers service implementations and describes them to:
    ///     * \Bases\Extensions\IServiceActionExtensions-AUTOMATED.cs
    ///     * \Extensions\IServiceCollectionExtensions-AUTOMATED.cs
    /// Note: cannot handle generically typed service definitions, and cannot handle service implementations that implement generically typed service implementations, or that have generically typed service dependencies.
    /// </summary>
    public class O106_OutputServiceAddMethodsForProject : IActionOperation
    {
        private N004.IClassContextProvider ClassContextProvider_N004 { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private IServiceImplementationCodeFilePathsProvider ServiceImplementationCodeFilePathsProvider { get; }
        private Repositories.IServiceRepository ServiceRepository { get; }


        public O106_OutputServiceAddMethodsForProject(
            N004.IClassContextProvider classContextProvider_N004,
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            IServiceImplementationCodeFilePathsProvider serviceImplementationCodeFilePathsProvider,
            Repositories.IServiceRepository serviceRepository)
        {
            this.ClassContextProvider_N004 = classContextProvider_N004;
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.ServiceImplementationCodeFilePathsProvider = serviceImplementationCodeFilePathsProvider;
            this.ServiceRepository = serviceRepository;
        }

        public async Task Run()
        {
            /// Inputs.
            var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0030\source\R5T.S0030\R5T.S0030.csproj";
            var failuresFilePath = @"C:\Temp\Failures.txt";

            /// Run.
            var projectDirectoryPath = Instances.ProjectPathsOperator.GetProjectDirectoryPath(projectFilePath);

            var basesExtensionsDirectoryPath = Instances.ProjectPathsOperator.GetBasesExtensionsDirectoryPath(projectDirectoryPath);
            var serviceActionExtensionsCodeFilePath = Instances.PathOperator.GetFilePath(
                basesExtensionsDirectoryPath,
                Instances.FileName.AutomatedIServiceActionExtensions());

            var extensionsDirectoryPath = Instances.ProjectPathsOperator.GetExtensionsDirectoryPath(projectDirectoryPath);
            var serviceCollectionExtensionsCodeFilePath = Instances.PathOperator.GetFilePath(
                extensionsDirectoryPath,
                Instances.FileName.AutomatedIServiceCollectionExtensions());

            var projectDefaultNamespaceName = Instances.ProjectPathsOperator.GetDefaultProjectNamespaceName(
                projectFilePath);

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
                projectDefaultNamespaceName,
                typeNameForIServiceActionExtensions);

            var serviceActionExtensionsCompilationUnit = await this.ClassContextProvider_N004.InAcquired(
                iServiceCollectionExtensionsNamespacedTypeName,
                (compilationUnit, classContext) =>
                {
                    var allRequiredNamespaceNames = requiredNamespaceNames
                        .Append(
                            Instances.NamespaceNameOperator.GetAddXActionMethodsRequiredNamespaces())
                        .Now();

                    compilationUnit = classContext.Cast<N004.IClassContext, N004.IHasClassContext>().AddUsings(
                        compilationUnit,
                        allRequiredNamespaceNames);

                    foreach (var addMethodSet in implementationAddMethodSets)
                    {
                        compilationUnit = Instances.MethodOperator.AddMethod(
                            compilationUnit,
                            classContext,
                            addMethodSet.AddXActionMethod);
                    }

                    // Sort and space methods within the class.
                    compilationUnit = classContext.Annotation.ModifySynchronous(
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

            // Overwrite existing file.
            await serviceActionExtensionsCompilationUnit.WriteTo(serviceActionExtensionsCodeFilePath);

            // Create IServiceCollectionExtensions class.
            var typeNameForIServiceCollectionExtensions = Instances.TypeName.IServiceCollectionExtensions();

            var iServiceActionExtensionsNamespacedTypeName = Instances.NamespacedTypeNameOperator.GetNamespacedTypeName(
                projectDefaultNamespaceName,
                typeNameForIServiceCollectionExtensions);

            var serviceCollectionExtensionsCompilationUnit = await this.ClassContextProvider_N004.InAcquired(
                iServiceActionExtensionsNamespacedTypeName,
                (compilationUnit, classContext) =>
                {
                    var allRequiredNamespaceNames = requiredNamespaceNames
                        .Append(
                            Instances.NamespaceNameOperator.GetAddXMethodsRequiredNamespaces())
                        .Now();

                    compilationUnit = classContext.Cast<N004.IClassContext, N004.IHasClassContext>().AddUsings(
                        compilationUnit,
                        allRequiredNamespaceNames);

                    foreach (var addMethodSet in implementationAddMethodSets)
                    {
                        compilationUnit = Instances.MethodOperator.AddMethod(
                            compilationUnit,
                            classContext,
                            addMethodSet.AddXMethod);
                    }

                    // Sort and space methods within the class.
                    compilationUnit = classContext.Annotation.ModifySynchronous(
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

            // Overwrite existing file.
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
            //await this.NotepadPlusPlusOperator.OpenFilePath(serviceCollectionExtensionsCodeFilePath);
            //await this.NotepadPlusPlusOperator.OpenFilePath(serviceActionExtensionsCodeFilePath);
            await this.NotepadPlusPlusOperator.OpenFilePath(failuresFilePath);
        }
    }
}
