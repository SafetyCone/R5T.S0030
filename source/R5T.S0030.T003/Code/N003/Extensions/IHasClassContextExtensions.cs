using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.S0030.T003.N003;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    public static partial class IHasClassContextExtensions
    {
        /// <summary>
        /// Gets the line indentation of class signature line (line indentation includes any leading new line).
        /// See also: <seealso cref="GetLineIndentation"/>.
        /// </summary>
        public static SyntaxTriviaList GetLineIndentation<TNode>(this IHasClassContext hasClassContext,
            TNode classNodeOrParent)
            where TNode: SyntaxNode
        {
            var lineIndentation = hasClassContext.ClassContext_N003.ClassAnnotation.Get(
                classNodeOrParent,
                @class => @class.GetSeparatingLeadingTrivia());

            return lineIndentation;
        }

        /// <summary>
        /// Gets the indentation of class signature line (line indentation minus any leading new line).
        /// See also: <seealso cref="GetLineIndentation"/>.
        /// </summary>
        public static SyntaxTriviaList GetIndentation<TNode>(this IHasClassContext classContext,
            TNode classNodeOrParent)
            where TNode : SyntaxNode
        {
            var lineIndentation = classContext.GetLineIndentation(classNodeOrParent);

            var indentation = lineIndentation.RemoveLeadingNewLine();
            return indentation;
        }

        public static async Task<CompilationUnitSyntax> Modify(this IHasClassContext classContext,
            Func<IClassContext, Task<CompilationUnitSyntax>> afterAdditionClassModifierAction)
        {
            // Run modifier.
            var compilationUnit = await afterAdditionClassModifierAction(
                classContext.ClassContext_N003);

            // Perform after modification actions.
            compilationUnit = await classContext.RunAfterModificationActions(
                compilationUnit);

            return compilationUnit;
        }

        public static async Task<CompilationUnitSyntax> Modify(this IHasClassContext classContext,
            CompilationUnitSyntax compilationUnit,
            Func<CompilationUnitSyntax, IClassContext, Task<CompilationUnitSyntax>> afterAdditionClassModifierAction)
        {
            // Run modifier.
            compilationUnit = await afterAdditionClassModifierAction(
                compilationUnit,
                classContext.ClassContext_N003);

            // Perform after modification actions.
            compilationUnit = await classContext.RunAfterModificationActions(
                compilationUnit);

            return compilationUnit;
        }

        private static Task<CompilationUnitSyntax> RunAfterModificationActions(this IHasClassContext _,
            CompilationUnitSyntax compilationUnit)
        {
            // NONE.

            return Task.FromResult(compilationUnit);
        }
    }
}
