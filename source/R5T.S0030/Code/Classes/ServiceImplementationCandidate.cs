using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace R5T.S0030
{
    public class ServiceImplementationCandidate
    {
        public ClassDeclarationSyntax Class { get; set; }
        public string CodeFilePath { get; set; }
        public CompilationUnitSyntax CompilationUnit { get; set; }


        public override string ToString()
        {
            var representation = this.Class.GetNamespacedTypeName();
            return representation;
        }
    }
}
