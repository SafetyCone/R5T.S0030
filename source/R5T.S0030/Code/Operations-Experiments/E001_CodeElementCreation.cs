using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.N0;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar.Extensions;

using R5T.D0105;
using R5T.T0020;

using R5T.S0030.F002;
using R5T.S0030.F003;
using R5T.S0030.Z001;

using N001 = R5T.S0030.T003.N001;
using N002 = R5T.S0030.T003.N002;
using N003 = R5T.S0030.T003.N003;
using N004 = R5T.S0030.T003.N004;

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
        private N001.ICompilationUnitContextProvider CompilationUnitContextProvider { get; }
        private N002.INamespaceContextProvider NamespaceContextProvider { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private IServiceImplementationCodeFilePathsProvider ServiceImplementationCodeFilePathsProvider { get; }
        private Repositories.IServiceRepository ServiceRepository { get; }


        public E001_CodeElementCreation(
            N003.IClassContextProvider classContextProvider_N003,
            N004.IClassContextProvider classContextProvider_N004,
            N001.ICompilationUnitContextProvider compilationUnitContextProvider,
            N002.INamespaceContextProvider namespaceContextProvider,
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            IServiceImplementationCodeFilePathsProvider serviceImplementationCodeFilePathsProvider,
            Repositories.IServiceRepository serviceRepository)
        {
            this.ClassContextProvider_N003 = classContextProvider_N003;
            this.ClassContextProvider_N004 = classContextProvider_N004;
            this.CompilationUnitContextProvider = compilationUnitContextProvider;
            this.NamespaceContextProvider = namespaceContextProvider;
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.ServiceImplementationCodeFilePathsProvider = serviceImplementationCodeFilePathsProvider;
            this.ServiceRepository = serviceRepository;
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
            await this.SearchForAddXMethods();
        }

        #region Experiments

#pragma warning disable IDE0051 // Remove unused private members

        private async Task UpdateAddMethods()
        {
            /// Inputs.
            var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0030\source\R5T.S0030\R5T.S0030.csproj";

            var addXMethodsFilePath = @"C:\Temp\AddXMethods.txt";
            var addXActionMethodsFilePath = @"C:\Temp\AddXActionMethods.txt";

            /// Run.
            // Search for AddX() methods.
            var addXMethodSearchResult = await Instances.Operation.SearchForAddXMethods(projectFilePath);
            var addXActionMethodSearchResult = await Instances.Operation.SearchForAddXActionMethods(projectFilePath);

            // Search for and describe service implementations.
            var serviceImplementations = await Instances.Operation.DescribeServiceImplementationsInProject(
                projectFilePath,
                this.ServiceImplementationCodeFilePathsProvider,
                this.ServiceRepository);


        }

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

                    compilationUnit = classContext.Cast<N004.IClassContext, N004.IHasClassContext>().AddUsings(
                        compilationUnit,
                        requiredNamespaceNames);

                    compilationUnit = classContext.Annotation.ModifySynchronous(
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
                                    compilationUnit = classContext.Annotation.ModifySynchronous(
                                        compilationUnit,
                                        @class => @class.IndentBlock_Old(
                                            Instances.Indentation_Old.Class()));

                                    // Set the indentation of the first line of the class.
                                    compilationUnit = compilationUnit.SetLineIndentation(
                                        classContext.Annotation,
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
