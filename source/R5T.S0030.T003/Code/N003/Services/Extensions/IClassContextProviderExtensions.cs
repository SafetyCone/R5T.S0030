using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;

using R5T.S0030.T003.N003;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    public static partial class IClassContextProviderExtensions
    {
        public static Task<ClassContext> GetContext(this IClassContextProvider _,
            ClassAnnotation classAnnotation)
        {
            var context = new ClassContext
            {
                ClassAnnotation = classAnnotation,
            };

            return Task.FromResult(context);
        }

        public static async Task<CompilationUnitSyntax> For(this IClassContextProvider classContextProvider,
            ClassAnnotation classAnnotation,
            Func<IClassContext, Task<CompilationUnitSyntax>> afterAdditionClassModifierAction)
        {
            // Get the context.
            var context = await classContextProvider.GetContext(classAnnotation);

            var compilationUnit = await context.Modify(afterAdditionClassModifierAction);
            return compilationUnit;
        }

        public static async Task<CompilationUnitSyntax> For(this IClassContextProvider classContextProvider,
            CompilationUnitSyntax compilationUnit,
            ClassAnnotation classAnnotation,
            Func<CompilationUnitSyntax, IClassContext, Task<CompilationUnitSyntax>> afterAdditionClassModifierAction)
        {
            // Get the context.
            var context = await classContextProvider.GetContext(classAnnotation);

            compilationUnit = await context.Modify(
                compilationUnit,
                afterAdditionClassModifierAction);

            return compilationUnit;
        }

        public static async Task<CompilationUnitSyntax> In(this IClassContextProvider classContextProvider,
            string className,
            NamespaceAnnotation namespaceAnnotation,
            CompilationUnitSyntax compilationUnit,
            Func<CompilationUnitSyntax, IClassContext, Task<CompilationUnitSyntax>> afterAdditionClassModifierAction)
        {
            var hasClass = namespaceAnnotation.Get(
                compilationUnit,
                @namespace => @namespace.HasClass(className));

            if(!hasClass)
            {
                throw new Exception($"Class name '{className}' not found in namespace '{namespaceAnnotation.GetFullName(compilationUnit)}'.");
            }

            var @namespace = hasClass.Result.AnnotateTyped(out var classAnnotation);

            compilationUnit = await classContextProvider.For(
                compilationUnit,
                classAnnotation,
                afterAdditionClassModifierAction);

            return compilationUnit;
        }

        public static async Task<CompilationUnitSyntax> InAcquired(this IClassContextProvider classContextProvider,
            string className,
            NamespaceAnnotation namespaceAnnotation,
            CompilationUnitSyntax compilationUnit,
            Func<string, Task<ClassDeclarationSyntax>> classConstructor,
            Func<NamespaceDeclarationSyntax, ClassDeclarationSyntax, Task<NamespaceDeclarationSyntax>> addClassAction,
            Func<CompilationUnitSyntax, IClassContext, Task<CompilationUnitSyntax>> afterAdditionClassModifierAction)
        {
            var hasClass = namespaceAnnotation.Get(
                compilationUnit,
                @namespace => @namespace.HasClass(className));

            var @class = hasClass
                ? hasClass.Result
                : await classConstructor(className)
                ;

            @class = @class.AnnotateTyped(out var classAnnotation);

            if(!hasClass)
            {
                compilationUnit = await namespaceAnnotation.Modify(
                    compilationUnit,
                    async @namespace => await addClassAction(
                        @namespace,
                        @class));
            }

            compilationUnit = await classContextProvider.For(
                compilationUnit,
                classAnnotation,
                afterAdditionClassModifierAction);

            return compilationUnit;
        }

        public static Task<CompilationUnitSyntax> InAcquired(this IClassContextProvider classContextProvider,
            string className,
            NamespaceAnnotation namespaceAnnotation,
            CompilationUnitSyntax compilationUnit,
            Func<CompilationUnitSyntax, IClassContext, Task<CompilationUnitSyntax>> afterAdditionClassModifierAction)
        {
            return classContextProvider.InAcquired(
                className,
                namespaceAnnotation,
                compilationUnit,
                Instances.ClassGenerator.GetClass_Latest,
                Instances.ClassOperator.AddClass_Latest,
                afterAdditionClassModifierAction);
        }

        /// <summary>
        /// Creates a class, adds it to the compilation unit relative to a namespace, and calls the modifier action.
        /// </summary>
        /// <param name="afterAdditionNamespaceModifierAction">An action run after the class has been added to the compilation unit relative to the namespace.</param>
        /// <param name="addNamespaceAction">Action to add a class to the compilation unit relative to the namespace.</param>
        public static async Task<CompilationUnitSyntax> InCreated(this IClassContextProvider classContextProvider,
            string className,
            NamespaceAnnotation namespaceAnnotation,
            CompilationUnitSyntax compilationUnit,
            Func<CompilationUnitSyntax, IClassContext, Task<CompilationUnitSyntax>> afterAdditionClassModifierAction,
            Func<NamespaceDeclarationSyntax, ClassDeclarationSyntax, Task<NamespaceDeclarationSyntax>> addClassAction)
        {
            // Provide a new, annotated, class.
            var @class = Instances.ClassGenerator.GetClass_LatestSynchronous(className)
                .AnnotateTyped(out var classAnnotation);

            // Add class to the namespace.
            compilationUnit = await namespaceAnnotation.Modify(
                compilationUnit,
                async (namespaceDeclaration) =>
                {
                    namespaceDeclaration = await addClassAction(
                        namespaceDeclaration,
                        @class);

                    return namespaceDeclaration;
                });

            compilationUnit = await classContextProvider.For(
                compilationUnit,
                classAnnotation,
                afterAdditionClassModifierAction);

            return compilationUnit;
        }

        /// <inheritdoc cref="InCreated(IClassContextProvider, string, NamespaceAnnotation, CompilationUnitSyntax, Func{CompilationUnitSyntax, IClassContext, Task{CompilationUnitSyntax}}, Func{NamespaceDeclarationSyntax, ClassDeclarationSyntax, Task{NamespaceDeclarationSyntax}})"/>
        public static Task<CompilationUnitSyntax> InCreated(this IClassContextProvider classContextProvider,
            string className,
            NamespaceAnnotation namespaceAnnotation,
            CompilationUnitSyntax compilationUnit,
            Func<CompilationUnitSyntax, IClassContext, Task<CompilationUnitSyntax>> afterAdditionClassModifierAction)
        {
            var output = classContextProvider.InCreated(
                className,
                namespaceAnnotation,
                compilationUnit,
                afterAdditionClassModifierAction,
                Instances.ClassOperator.AddClass_Simple);

            return output;
        }
    }
}
