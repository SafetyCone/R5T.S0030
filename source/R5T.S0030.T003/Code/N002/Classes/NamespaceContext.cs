using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.D0116;
using R5T.T0126;


namespace R5T.S0030.T003.N002
{
    public class NamespaceContext : INamespaceContext
    {
        public ISyntaxNodeAnnotation<NamespaceDeclarationSyntax> NamespaceAnnotation { get; set; }

        public IUsingDirectivesFormatter UsingDirectivesFormatter { get; set; }

        INamespaceContext IHasNamespaceContext.NamespaceContext_N002 => this;
    }
}
