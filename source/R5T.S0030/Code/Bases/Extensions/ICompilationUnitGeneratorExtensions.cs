using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar.Extensions;

using R5T.B0006;
using R5T.L0011.X000.N8;
using R5T.T0126;

using R5T.S0030.F002;
using R5T.S0030.T003.N001;

using R5T.S0030.Library;

using N004 = R5T.S0030.T003.N004;
using N013 = R5T.S0030.T003.N013;


namespace R5T.S0030
{
    using ClassCreationResult = SyntaxNodeCreationResult<CompilationUnitSyntax, ClassDeclarationSyntax>;
    using InterfaceCreationResult = SyntaxNodeCreationResult<CompilationUnitSyntax, InterfaceDeclarationSyntax>;


    public static class ICompilationUnitGeneratorExtensions
    {
        public static async Task<ClassCreationResult> CreateExtensionsClass(this ICompilationUnitGenerator compilationUnitGenerator,
            string extensionsClassNamespacedTypeName,
            string extensionBaseTypeNamespacedTypeName,
            N004.IClassContextProvider classContextProvider)
        {
            var emptyClass = await compilationUnitGenerator.CreateEmptyClass(
                extensionsClassNamespacedTypeName,
                classContextProvider);

            var output = await classContextProvider.In(
                emptyClass.Result,
                emptyClass.CreatedNodeAnnotation,
                (compilationUnit, classContext) =>
                {
                    (compilationUnit, _) = classContext.MakeStatic(compilationUnit);
                    (compilationUnit, _) = classContext.MakePartial(compilationUnit);

                    // Make sure the namespace for the extension base type is available.
                    var extensionBaseTypeNamespaceName = Instances.NamespacedTypeNameOperator.GetNamespaceName(extensionBaseTypeNamespacedTypeName);

                    compilationUnit = classContext.AddUsings(
                        compilationUnit,
                        extensionBaseTypeNamespaceName);

                    return Task.FromResult(compilationUnit);
                },
                optionsModifierAction: Instances.CompilationUnitOperator.NoNamespaceAdditions);

            return output;
        }

        public static async Task<InterfaceCreationResult> CreateServiceDefinitionInterface(this ICompilationUnitGenerator compilationUnitGenerator,
            string serviceDefinitionInterfaceNamespacedTypeName,
            N013.IInterfaceContextProvider interfaceContextProvider)
        {
            var emptyInterface = await compilationUnitGenerator.CreateEmptyInterface(
                serviceDefinitionInterfaceNamespacedTypeName,
                interfaceContextProvider);

            var output = await interfaceContextProvider.In(
                emptyInterface.Result,
                emptyInterface.CreatedNodeAnnotation,
                (compilationUnit, interfaceContext) =>
                {
                    compilationUnit = interfaceContext.AddAttributeByNamespacedTypeName(
                        compilationUnit,
                        Instances.NamespacedTypeName.R5T_T0064_ServiceDefinitionMarkerAttribute());

                    compilationUnit = interfaceContext.AddBaseTypeByNamespacedTypeName(
                        compilationUnit,
                        Instances.NamespacedTypeName.R5T_T0064_IServiceDefinition());

                    return Task.FromResult(compilationUnit);
                },
                optionsModifierAction: Instances.CompilationUnitOperator.NoNamespaceAdditions);

            return output;
        }

        public static async Task<ClassCreationResult> CreateServiceImplementationClass(this ICompilationUnitGenerator compilationUnitGenerator,
            string serviceImplementationClassNamespacedTypeName,
            string serviceDefinitionInterfaceNamespacedTypeName,
            N004.IClassContextProvider classContextProvider) 
        {
            var emptyClass = await compilationUnitGenerator.CreateEmptyClass(
                serviceImplementationClassNamespacedTypeName,
                classContextProvider);

            var output = await classContextProvider.In(
                emptyClass.Result,
                emptyClass.CreatedNodeAnnotation,
                (compilationUnit, classContext) =>
                {
                    compilationUnit = classContext.AddAttributeByNamespacedTypeName(
                        compilationUnit,
                        Instances.NamespacedTypeName.R5T_T0064_ServiceImplementationMarkerAttribute());

                    // Base type first.
                    compilationUnit = classContext.AddBaseTypeByNamespacedTypeName(
                        compilationUnit,
                        serviceDefinitionInterfaceNamespacedTypeName);

                    // Then service implementation marker interface.
                    compilationUnit = classContext.AddBaseTypeByNamespacedTypeName(
                        compilationUnit,
                        Instances.NamespacedTypeName.R5T_T0064_IServiceImplementation());

                    return Task.FromResult(compilationUnit);
                },
                optionsModifierAction: Instances.CompilationUnitOperator.NoNamespaceAdditions);

            return output;
        }

        public static async Task<ClassCreationResult> CreateEmptyClass(this ICompilationUnitGenerator _,
            string classNamespacedTypeName,
            N004.IClassContextProvider classContextProvider)
        {
            var result = await classContextProvider.InCreated(
                classNamespacedTypeName,
                Instances.ClassOperator.NoModifications,
                classConstructor: Instances.ClassGenerator.GetPublicClass,
                compilationContextOptionsModifier: Instances.CompilationUnitOperator.NoSystem_Threading_Tasks);

            return result;
        }

        public static Task<InterfaceCreationResult> CreateEmptyInterface(this ICompilationUnitGenerator compilationUnitGenerator,
            string namespaceName,
            string interfaceName,
            N013.IInterfaceContextProvider interfaceContextProvider)
        {
            var interfaceNamespacedTypeName = Instances.NamespacedTypeNameOperator.GetNamespacedTypeName(
                    namespaceName,
                    interfaceName);

            return compilationUnitGenerator.CreateEmptyInterface(
                interfaceNamespacedTypeName,
                interfaceContextProvider);
        }

        public static async Task<InterfaceCreationResult> CreateEmptyInterface(this ICompilationUnitGenerator _,
            string interfaceNamespacedTypeName,
            N013.IInterfaceContextProvider interfaceContextProvider)
        {
            var result = await interfaceContextProvider.InCreated(
                interfaceNamespacedTypeName,
                Instances.InterfaceOperator.NoModifications,
                interfaceConstructor: Instances.InterfaceGenerator.GetPublicInterface,
                compilationContextOptionsModifier: Instances.CompilationUnitOperator.NoSystem_Threading_Tasks);

            return result;
        }
    }
}
