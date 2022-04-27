using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace R5T.S0030.F003
{
    public class CodeFileResult
    {
        public string CodeFilePath { get; set; }
        public CompilationUnitSyntax CompilationUnit { get; set; }
        public ClassResult[] ClassResults { get; set; }
    }
}
