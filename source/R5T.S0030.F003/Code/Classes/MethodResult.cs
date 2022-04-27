using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;


namespace R5T.S0030.F003
{
    public class MethodResult
    {
        public string MethodName { get; set; }
        public ISyntaxNodeAnnotation<MethodDeclarationSyntax> Annotation { get; set; }
    }
}
