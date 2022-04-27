using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.B0006;

using Instances = R5T.S0030.F001.Instances;


namespace R5T.S0030.F001
{
    public static class IDocumentationGeneratorExtensions
    {
        public static XmlElementSyntax AddXMethodXmlSummaryElement(this IDocumentationGenerator _,
            string serviceDefinitionTypeName,
            string serviceImplementationTypeName)
        {
            var documentationLineText = Instances.DocumentationLine.ForServiceAddXMethod(serviceDefinitionTypeName, serviceImplementationTypeName);

            var output = Instances.SyntaxFactory.XmlSummary()
                .AddContentLine(documentationLineText)
                ;

            return output;
        }

        public static DocumentationCommentTriviaSyntax AddXMethodXmlSummary(this IDocumentationGenerator documentationGenerator,
            string serviceDefinitionTypeName,
            string serviceImplementationTypeName)
        {
            var xmlSummaryElement = documentationGenerator.AddXMethodXmlSummaryElement(serviceDefinitionTypeName, serviceImplementationTypeName);

            var output = Instances.SyntaxFactory.DocumentationComment()
                .AddContent(xmlSummaryElement);

            return output;
        }

        public static DocumentationCommentTriviaSyntax AddXActionMethodXmlSummary(this IDocumentationGenerator documentationGenerator,
            string serviceDefinitionTypeName,
            string serviceImplementationTypeName)
        {
            // Same as the AddX() method documentation.
            var output = documentationGenerator.AddXMethodXmlSummary(
                serviceDefinitionTypeName,
                serviceImplementationTypeName);

            return output;
        }
    }
}
