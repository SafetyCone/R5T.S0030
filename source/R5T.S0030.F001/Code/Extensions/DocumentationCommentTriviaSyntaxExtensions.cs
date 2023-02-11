using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace R5T.S0030.F001
{
    public static class DocumentationCommentTriviaSyntaxExtensions
    {
        /// <summary>
        /// Indentation must be added to <see cref="SyntaxKind.DocumentationCommentExteriorTrivia"/>.
        /// </summary>
        public static DocumentationCommentTriviaSyntax Indent(this DocumentationCommentTriviaSyntax documentation,
            SyntaxTriviaList indentation)
        {
            var documentationCommentExteriors = documentation.DescendantTrivia()
                .Where(x => x.IsKind(SyntaxKind.DocumentationCommentExteriorTrivia))
                .Now_OLD();

            documentation = documentation.AnnotateTrivias(
                documentationCommentExteriors,
                out var annotationsByDocumentationCommentExteriors);

            foreach (var annotation in annotationsByDocumentationCommentExteriors.Values)
            {
                documentation = documentation.SetLineIndentation_ForTriviaAnnotation(
                    annotation,
                    indentation);
            }

            return documentation;
        }

        public static DocumentationCommentTriviaSyntax SetNewLines(this DocumentationCommentTriviaSyntax documentation)
        {
            var output = documentation.Indent(
                Instances.LineIndentation.NewLine());

            return output;
        }
    }
}
