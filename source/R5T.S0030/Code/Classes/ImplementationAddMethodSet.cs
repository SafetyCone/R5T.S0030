using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.S0030.T001.Level02;


namespace R5T.S0030
{
    public class ImplementationAddMethodSet
    {
        public ImplementationDescriptor ImplementationDescriptor { get; set; }
        public MethodDeclarationSyntax AddXMethod { get; set; }
        public MethodDeclarationSyntax AddXActionMethod { get; set; }
    }
}
