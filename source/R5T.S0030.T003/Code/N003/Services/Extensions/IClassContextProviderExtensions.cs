using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;

using R5T.S0030.T003.N003;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    using ClassAnnotation = ISyntaxNodeAnnotation<ClassDeclarationSyntax>;
    using NamespaceAnnotation = ISyntaxNodeAnnotation<NamespaceDeclarationSyntax>;

    using ClassModifierAction = Func<CompilationUnitSyntax, IClassContext, Task<CompilationUnitSyntax>>;


    public static partial class IClassContextProviderExtensions
    {
        public static ClassContext GetContext(this IClassContextProvider _,
            ClassAnnotation annotation)
        {
            var context = new ClassContext
            {
                Annotation = annotation,
            };

            return context;
        }

        public static async Task<CompilationUnitSyntax> For(this IClassContextProvider classContextProvider,
            ClassAnnotation classAnnotation,
            Func<IClassContext, Task<CompilationUnitSyntax>> classCompilationUnitGenerator)
        {
            // Get the context.
            var context = classContextProvider.GetContext(classAnnotation);

            var compilationUnit = await context.Modify(classCompilationUnitGenerator);
            return compilationUnit;
        }

        public static async Task<CompilationUnitSyntax> For(this IClassContextProvider classContextProvider,
            CompilationUnitSyntax compilationUnit,
            ClassAnnotation classAnnotation,
            ClassModifierAction afterAdditionClassModifierAction)
        {
            // Get the context.
            var context = classContextProvider.GetContext(classAnnotation);

            compilationUnit = await context.Modify(
                compilationUnit,
                afterAdditionClassModifierAction);

            return compilationUnit;
        }

        public static async Task<CompilationUnitSyntax> In(this IClassContextProvider classContextProvider,
            string className,
            NamespaceAnnotation namespaceAnnotation,
            CompilationUnitSyntax compilationUnit,
            ClassModifierAction afterAdditionClassModifierAction)
        {
            var hasClass = namespaceAnnotation.Get(
                compilationUnit,
                @namespace => @namespace.HasClass(className));

            if(!hasClass)
            {
                throw new Exception($"Class '{className}' not found in namespace '{namespaceAnnotation.GetFullName(compilationUnit)}'.");
            }

            // ?? Not sure why this here, does it work? 20220502
            var @namespace = hasClass.Result.Annotate_Typed(out var classAnnotation);

            compilationUnit = await classContextProvider.For(
                compilationUnit,
                classAnnotation,
                afterAdditionClassModifierAction);

            return compilationUnit;
        }

        public static Task<CompilationUnitSyntax> In(this IClassContextProvider classContextProvider,
            CompilationUnitSyntax compilationUnit,
            ClassAnnotation interfaceAnnotation,
            ClassModifierAction interfaceModifierAction)
        {
            return classContextProvider.For(
                compilationUnit,
                interfaceAnnotation,
                interfaceModifierAction);
        }

        public static async Task<CompilationUnitSyntax> InAcquired(this IClassContextProvider classContextProvider,
            string className,
            NamespaceAnnotation namespaceAnnotation,
            CompilationUnitSyntax compilationUnit,
            Func<string, Task<ClassDeclarationSyntax>> classConstructor,
            Func<NamespaceDeclarationSyntax, ClassDeclarationSyntax, Task<NamespaceDeclarationSyntax>> addClassAction,
            ClassModifierAction afterAdditionClassModifierAction)
        {
            var hasClass = namespaceAnnotation.Get(
                compilationUnit,
                @namespace => @namespace.HasClass(className));

            var @class = hasClass
                ? hasClass.Result
                : await classConstructor(className)
                ;

            // Annotate the class before adding to the namespace.
            @class = @class.Annotate_Typed(out var classAnnotation);

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
            ClassModifierAction afterAdditionClassModifierAction)
        {
            return classContextProvider.InAcquired(
                className,
                namespaceAnnotation,
                compilationUnit,
                Instances.ClassGenerator.GetClass_Latest,
                Instances.ClassOperator.AddClass_Latest,
                afterAdditionClassModifierAction);
        }

        public static async Task<CompilationUnitSyntax> InCreated(this IClassContextProvider classContextProvider,
            string className,
            NamespaceAnnotation namespaceAnnotation,
            CompilationUnitSyntax compilationUnit,
            Func<string, Task<ClassDeclarationSyntax>> classConstructor,
            Func<NamespaceDeclarationSyntax, ClassDeclarationSyntax, Task<NamespaceDeclarationSyntax>> addClassAction,
            ClassModifierAction afterAdditionClassModifierAction)
        {
            var @class = await classConstructor(className);

            // Annotate the class before adding to the namespace.
            @class = @class.Annotate_Typed(out var classAnnotation);

            compilationUnit = await namespaceAnnotation.Modify(
                compilationUnit,
                async @namespace => await addClassAction(
                    @namespace,
                    @class));

            compilationUnit = await classContextProvider.For(
                compilationUnit,
                classAnnotation,
                afterAdditionClassModifierAction);

            return compilationUnit;
        }

        /// <summary>
        /// Creates a class, adds it to the compilation unit relative to a namespace, and calls the modifier action.
        /// </summary>
        /// <param name="afterAdditionClassModifierAction">An action run after the class has been added to the compilation unit relative to the namespace.</param>
        /// <param name="addClassAction">Action to add a class to the compilation unit relative to the namespace.</param>
        public static Task<CompilationUnitSyntax> InCreated(this IClassContextProvider classContextProvider,
            string className,
            NamespaceAnnotation namespaceAnnotation,
            CompilationUnitSyntax compilationUnit,
            ClassModifierAction afterAdditionClassModifierAction,
            Func<NamespaceDeclarationSyntax, ClassDeclarationSyntax, Task<NamespaceDeclarationSyntax>> addClassAction)
        {
            //// Provide a new, annotated, class.
            //var @class = Instances.ClassGenerator.GetClass_LatestSynchronous(className)
            //    .Annotate_Typed(out var classAnnotation);

            //// Add class to the namespace.
            //compilationUnit = await namespaceAnnotation.Modify(
            //    compilationUnit,
            //    async (namespaceDeclaration) =>
            //    {
            //        namespaceDeclaration = await addClassAction(
            //            namespaceDeclaration,
            //            @class);

            //        return namespaceDeclaration;
            //    });

            //compilationUnit = await classContextProvider.For(
            //    compilationUnit,
            //    classAnnotation,
            //    afterAdditionClassModifierAction);

            //return compilationUnit;

            return classContextProvider.InCreated(
                className,
                namespaceAnnotation,
                compilationUnit,
                Instances.ClassGenerator.GetClass_Latest,
                addClassAction,
                afterAdditionClassModifierAction);
        }

        /// <inheritdoc cref="InCreated(IClassContextProvider, string, NamespaceAnnotation, CompilationUnitSyntax, ClassModifierAction, Func{NamespaceDeclarationSyntax, ClassDeclarationSyntax, Task{NamespaceDeclarationSyntax}})"/>
        public static Task<CompilationUnitSyntax> InCreated(this IClassContextProvider classContextProvider,
            string className,
            NamespaceAnnotation namespaceAnnotation,
            CompilationUnitSyntax compilationUnit,
            ClassModifierAction afterAdditionClassModifierAction)
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
