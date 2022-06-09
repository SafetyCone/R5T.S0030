using System;
using System.Threading.Tasks;

using R5T.Magyar.Extensions;

using R5T.T0045;

using R5T.S0030.F002;
using R5T.S0030.T003.N001;

using R5T.S0030.Library;

using N004 = R5T.S0030.T003.N004;
using N010 = R5T.S0030.T003.N010;
using N013 = R5T.S0030.T003.N013;


namespace R5T.S0030
{
    public static class ICodeFileGeneratorExtensions
    {
        public static Func<N010.ICodeFileContext, Task> CreateContextClass(this ICodeFileGenerator _,
            string namespaceName,
            string contextClassTypeName,
            string contextInterfaceNamespacedTypeName,
            N004.IClassContextProvider classContextProvider)
        {
            var contextClassNamespacedTypeName = Instances.NamespacedTypeNameOperator.GetNamespacedTypeName(
                namespaceName,
                contextClassTypeName);

            async Task Internal(N010.ICodeFileContext codeFileContext)
            {
                var contextClass = await Instances.CompilationUnitGenerator.CreateEmptyClass(
                    contextClassNamespacedTypeName,
                    classContextProvider);

                contextClass = await classContextProvider.In(
                    contextClass.Result,
                    contextClass.CreatedNodeAnnotation,
                    (compilationUnit, classContext) =>
                    {
                        compilationUnit = classContext.AddBaseTypeByNamespacedTypeName(
                            compilationUnit,
                            contextInterfaceNamespacedTypeName);

                        return Task.FromResult(compilationUnit);
                    },
                    optionsModifierAction: Instances.CompilationUnitOperator.NoNamespaceAdditions);

                await contextClass.Result.WriteTo(codeFileContext.CodeFilePath);
            }

            return Internal;
        }

        public static Func<N010.ICodeFileContext, Task> CreateHasContextInterface(this ICodeFileGenerator _,
            string namespaceName,
            string namespaceNumber,
            string contextInterfaceTypeName,
            string hasContextInterfaceTypeName,
            N013.IInterfaceContextProvider interfaceContextProvider)
        {
            var contextInterfaceNamespacedTypeName = Instances.NamespacedTypeNameOperator.GetNamespacedTypeName(
                namespaceName,
                contextInterfaceTypeName);

            var hasContextInterfaceNamespacedTypeName = Instances.NamespacedTypeNameOperator.GetNamespacedTypeName(
                namespaceName,
                hasContextInterfaceTypeName);

            var propertyName = contextInterfaceTypeName + "_" + namespaceNumber;

            async Task Internal(N010.ICodeFileContext codeFileContext)
            {
                var hasContextInterface = await Instances.CompilationUnitGenerator.CreateEmptyInterface(
                    hasContextInterfaceNamespacedTypeName,
                    interfaceContextProvider);

                hasContextInterface = await interfaceContextProvider.In(
                    hasContextInterface.Result,
                    hasContextInterface.CreatedNodeAnnotation,
                    Instances.TypeOperator.AddProperty<N013.IInterfaceContext>(
                        contextInterfaceNamespacedTypeName,
                        propertyName),
                    optionsModifierAction: Instances.CompilationUnitOperator.NoNamespaceAdditions);

                await hasContextInterface.Result.WriteTo(codeFileContext.CodeFilePath);
            }

            return Internal;
        }

        public static Func<N010.ICodeFileContext, Task> CreateContextInteface(this ICodeFileGenerator _,
            string namespaceName,
            string contextInterfaceTypeName,
            string hasContextInterfaceTypeName,
            N013.IInterfaceContextProvider interfaceContextProvider)
        {
            var contextInterfaceNamespacedTypeName = Instances.NamespacedTypeNameOperator.GetNamespacedTypeName(
                namespaceName,
                contextInterfaceTypeName);

            var hasContextInterfaceNamespacedTypeName = Instances.NamespacedTypeNameOperator.GetNamespacedTypeName(
                namespaceName,
                hasContextInterfaceTypeName);

            async Task Internal(N010.ICodeFileContext codeFileContext)
            {
                var contextInterface = await Instances.CompilationUnitGenerator.CreateEmptyInterface(
                    contextInterfaceNamespacedTypeName,
                    interfaceContextProvider);

                contextInterface = await interfaceContextProvider.In(
                    contextInterface.Result,
                    contextInterface.CreatedNodeAnnotation,
                    (compilationUnit, interfaceContext) =>
                    {
                        compilationUnit = interfaceContext.AddBaseTypeByNamespacedTypeName(
                            compilationUnit,
                            hasContextInterfaceNamespacedTypeName);

                        return Task.FromResult(compilationUnit);
                    });

                await contextInterface.Result.WriteTo(codeFileContext.CodeFilePath);
            }

            return Internal;
        }

        public static Func<N010.ICodeFileContext, Task> CreateServiceDefinitionInterfaceExtensions(this ICodeFileGenerator _,
            string namespaceName,
            string serviceDefinitionInterfaceTypeName,
            string serviceDefinitionIntefaceExtensionsTypeName,
            N004.IClassContextProvider classContextProvider)
        {
            var serviceDefinitionInterfaceNamespacedTypeName = Instances.NamespacedTypeNameOperator.GetNamespacedTypeName(
                namespaceName,
                serviceDefinitionInterfaceTypeName);

            var serviceDefinitionInterfaceExtensionsNamespacedTypeName = Instances.NamespacedTypeNameOperator.GetNamespacedTypeName(
                namespaceName,
                serviceDefinitionIntefaceExtensionsTypeName);

            async Task Internal(N010.ICodeFileContext codeFileContext)
            {
                var compilationUnit = await Instances.CompilationUnitGenerator.CreateExtensionsClass(
                    serviceDefinitionInterfaceExtensionsNamespacedTypeName,
                    serviceDefinitionInterfaceNamespacedTypeName,
                    classContextProvider);

                await compilationUnit.Result.WriteTo(codeFileContext.CodeFilePath);
            }

            return Internal;
        }

        public static Func<N010.ICodeFileContext, Task> CreateServiceDefinitionInterface(this ICodeFileGenerator _,
            string namespaceName,
            string serviceDefinitionInterfaceName,
            N013.IInterfaceContextProvider interfaceContextProvider)
        {
            var serviceDefinitionInterfaceNamespacedTypeName = Instances.NamespacedTypeNameOperator.GetNamespacedTypeName(
                namespaceName,
                serviceDefinitionInterfaceName);

            async Task Internal(N010.ICodeFileContext codeFileContext)
            {
                var compilationUnit = await Instances.CompilationUnitGenerator.CreateServiceDefinitionInterface(
                    serviceDefinitionInterfaceNamespacedTypeName,
                    interfaceContextProvider);

                await compilationUnit.Result.WriteTo(codeFileContext.CodeFilePath);
            }

            return Internal;
        }

        public static Func<N010.ICodeFileContext, Task> CreateServiceImplementationClass(this ICodeFileGenerator _,
            string namespaceName,
            string serviceImplementationClassName,
            string serviceDefinitionInterfaceName,
            N004.IClassContextProvider classContextProvider)
        {
            var serviceImplementationClassNamespacedTypeName = Instances.NamespacedTypeNameOperator.GetNamespacedTypeName(
                namespaceName,
                serviceImplementationClassName);

            var serviceDefinitionInterfaceNamespacedTypeName = Instances.NamespacedTypeNameOperator.GetNamespacedTypeName(
                namespaceName,
                serviceDefinitionInterfaceName);

            async Task Internal(N010.ICodeFileContext codeFileContext)
            {
                var compilationUnit = await Instances.CompilationUnitGenerator.CreateServiceImplementationClass(
                    serviceImplementationClassNamespacedTypeName,
                    serviceDefinitionInterfaceNamespacedTypeName,
                    classContextProvider);

                await compilationUnit.Result.WriteTo(codeFileContext.CodeFilePath);
            }

            return Internal;
        }

        public static Func<N010.ICodeFileContext, Task> CreateEmptyPublicInterface(this ICodeFileGenerator _,
            string namespaceName,
            string interfaceName,
            string[] usingNamespaces,
            N013.IInterfaceContextProvider interfaceContextProvider)
        {
            async Task Internal(N010.ICodeFileContext codeFileContext)
            {
                var interfaceNamespacedTypeName = Instances.NamespacedTypeNameOperator.GetNamespacedTypeName(
                    namespaceName,
                    interfaceName);

                var compilationUnit = await interfaceContextProvider.InCreated(
                    interfaceNamespacedTypeName,
                    (compilationUnit, interfaceContext) =>
                    {
                        compilationUnit = interfaceContext.AddUsings(compilationUnit,
                            usingNamespaces);

                        // Otherwise, do nothing.

                        return Task.FromResult(compilationUnit);
                    },
                    interfaceConstructor: Instances.InterfaceGenerator.GetPublicInterface,
                    compilationContextOptionsModifier: options =>
                    {
                        options.AddUsingNamespace_System_Threading_Tasks = false;
                    });

                await compilationUnit.Result.WriteTo(codeFileContext.CodeFilePath);
            }

            return Internal;
        }

        public static Func<N010.ICodeFileContext, Task> CreateEmptyPublicStaticClass(this ICodeFileGenerator _,
            string namespaceName,
            string className,
            string[] usingNamespaces,
            N004.IClassContextProvider classContextProvider)
        {
            async Task Internal(N010.ICodeFileContext codeFileContext)
            {
                var classNamespacedTypeName = Instances.NamespacedTypeNameOperator.GetNamespacedTypeName(
                    namespaceName,
                    className);

                var compilationUnit = await classContextProvider.InCreated(
                    classNamespacedTypeName,
                    (compilationUnit, classContext) =>
                    {
                        compilationUnit = classContext.AddUsings(compilationUnit,
                            usingNamespaces);

                        // Otherwise, do nothing.

                        return Task.FromResult(compilationUnit);
                    },
                    classConstructor: Instances.ClassGenerator.GetPublicStaticPartialClass,
                    compilationContextOptionsModifier: options =>
                    {
                        options.AddUsingNamespace_System_Threading_Tasks = false;
                    });

                await compilationUnit.Result.WriteTo(codeFileContext.CodeFilePath);
            }

            return Internal;
        }

        public static Func<N010.ICodeFileContext, Task> CreateEmptyIServiceActionExtensions(this ICodeFileGenerator _,
            string namespaceName,
            N004.IClassContextProvider classContextProvider)
        {
            return _.CreateEmptyPublicStaticClass(
                namespaceName,
                Instances.TypeName.IServiceActionExtensions(),
                new[]
                {
                    Instances.NamespaceName.R5T_T0062(),
                    Instances.NamespaceName.R5T_T0063(),
                },
                classContextProvider);
        }

        public static Func<N010.ICodeFileContext, Task> CreateEmptyIServiceCollectionExtensions(this ICodeFileGenerator _,
            string namespaceName,
            N004.IClassContextProvider classContextProvider)
        {
            return _.CreateEmptyPublicStaticClass(
                namespaceName,
                Instances.TypeName.IServiceCollectionExtensions(),
                new[]
                {
                    Instances.NamespaceName.Microsoft_Extensions_DependencyInjection(),
                    Instances.NamespaceName.R5T_T0063(),
                },
                classContextProvider);
        }
    }
}
