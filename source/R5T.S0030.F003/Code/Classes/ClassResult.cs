using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;


namespace R5T.S0030.F003
{
    public class ClassResult
    {
        public string ClassName { get; set; }
        public ISyntaxNodeAnnotation<ClassDeclarationSyntax> Annotation { get; set; }
        public MethodResult[] MethodResults { get; set; }
    }
}
