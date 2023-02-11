using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace R5T.S0030.F001
{
    public static class StatementSyntaxExtensions
    {
        public static TStatement IndentDotOperators<TStatement>(this TStatement statement,
            SyntaxTriviaList indentation)
            where TStatement : StatementSyntax
        {
            var dotTokens = statement.DescendantTokens()
                .Where(xToken => xToken.IsKind(SyntaxKind.DotToken))
                .Now_OLD();

            statement = statement.AnnotateTokens(
                dotTokens,
                out var annotationsByDotToken);

            foreach (var dotTokenAnnotation in annotationsByDotToken.Values)
            {
                statement = statement.SetLineIndentation_ForTokenAnnotation(
                    dotTokenAnnotation,
                    indentation);
            }

            return statement;
        }
    }
}
