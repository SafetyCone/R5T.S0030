using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.S0030.T003.N001;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    public static class ICompilationUnitContextProviderExtensions
    {
        public static CompilationUnitContext GetContext(this ICompilationUnitContextProvider compilationUnitContextProvider)
        {
            var context = new CompilationUnitContext
            {
                UsingDirectivesFormatter = compilationUnitContextProvider.UsingDirectivesFormatter,
            };

            return context;
        }

        public static async Task<CompilationUnitSyntax> For(this ICompilationUnitContextProvider compilationUnitContextProvider,
            Func<ICompilationUnitContext, Task<CompilationUnitSyntax>> compilationUnitConstructor,
            Action<CompilationUnitContextOptions> optionsModifierAction = default)
        {
            // Get the context.
            var context = compilationUnitContextProvider.GetContext();

            // Do after modification actions.
            var compilationUnit = await context.Modify(
                compilationUnitConstructor,
                optionsModifierAction);

            return compilationUnit;
        }

        public static async Task<CompilationUnitSyntax> For(this ICompilationUnitContextProvider compilationUnitContextProvider,
            CompilationUnitSyntax compilationUnit,
            Func<CompilationUnitSyntax, ICompilationUnitContext, Task<CompilationUnitSyntax>> compilationUnitModifierAction,
            Action<CompilationUnitContextOptions> optionsModifierAction = default)
        {
            // Get the context.
            var context = compilationUnitContextProvider.GetContext();

            var output = await context.Modify(
                compilationUnit,
                compilationUnitModifierAction,
                optionsModifierAction);

            return output;
        }

        /// <summary>
        /// Checks that the <paramref name="codeFilePath"/> exists.
        /// </summary>
        public static async Task<CompilationUnitSyntax> In(this ICompilationUnitContextProvider compilationUnitContextProvider,
            string codeFilePath,
            Func<CompilationUnitSyntax, ICompilationUnitContext, Task<CompilationUnitSyntax>> compilationUnitModifierAction,
            Action<CompilationUnitContextOptions> optionsModifierAction = default)
        {
            var fileExists = Instances.FileSystemOperator.FileExists(codeFilePath);
            if(!fileExists)
            {
                throw new FileNotFoundException("Code file path not found.", codeFilePath);
            }

            var compilationUnit = await Instances.CompilationUnitOperator_Old.Load(codeFilePath);

            var output = await compilationUnitContextProvider.For(
                compilationUnit,
                compilationUnitModifierAction,
                optionsModifierAction);

            return output;
        }

        public static async Task<CompilationUnitSyntax> In(this ICompilationUnitContextProvider compilationUnitContextProvider,
            CompilationUnitSyntax compilationUnit,
            Func<CompilationUnitSyntax, ICompilationUnitContext, Task<CompilationUnitSyntax>> compilationUnitModifierAction,
            Action<CompilationUnitContextOptions> optionsModifierAction = default)
        {
            var output = await compilationUnitContextProvider.For(
                compilationUnit,
                compilationUnitModifierAction,
                optionsModifierAction);

            return output;
        }

        public static async Task<CompilationUnitSyntax> InAcquired(this ICompilationUnitContextProvider compilationUnitContextProvider,
            string codeFilePath,
            Func<Task<CompilationUnitSyntax>> compilationUnitConstructor,
            Func<CompilationUnitSyntax, ICompilationUnitContext, Task<CompilationUnitSyntax>> compilationUnitModifierAction,
            Action<CompilationUnitContextOptions> optionsModifierAction = default)
        {
            var fileExists = Instances.FileSystemOperator.FileExists(codeFilePath);

            var compilationUnit = fileExists
                ? await Instances.CompilationUnitOperator_Old.Load(codeFilePath)
                : await compilationUnitConstructor()
                ;

            var output = await compilationUnitContextProvider.For(
                compilationUnit,
                compilationUnitModifierAction,
                optionsModifierAction);

            return output;
        }

        public static Task<CompilationUnitSyntax> InAcquired(this ICompilationUnitContextProvider compilationUnitContextProvider,
            string codeFilePath,
            Func<CompilationUnitSyntax, ICompilationUnitContext, Task<CompilationUnitSyntax>> compilationUnitModifierAction,
            Action<CompilationUnitContextOptions> optionsModifierAction = default)
        {
            return compilationUnitContextProvider.InAcquired(
                codeFilePath,
                Instances.CompilationUnitGenerator.GetNewCompilationUnit,
                compilationUnitModifierAction,
                optionsModifierAction);
        }

        /// <summary>
        /// Creates the compilation unit, calls the modifier action, then formats using declarations (unless options specify otherwise).
        /// </summary>
        public static async Task<CompilationUnitSyntax> InCreated(this ICompilationUnitContextProvider compilationUnitContextProvider,
            Func<CompilationUnitSyntax, ICompilationUnitContext, Task<CompilationUnitSyntax>> compilationUnitModifierAction,
            Action<CompilationUnitContextOptions> optionsModifierAction = default)
        {
            // Provide a new compilation unit.
            var compilationUnit = Instances.CompilationUnitGenerator.NewCompilationUnit();

            var output = await compilationUnitContextProvider.For(
                compilationUnit,
                compilationUnitModifierAction,
                optionsModifierAction);

            return output;
        }
    }
}
