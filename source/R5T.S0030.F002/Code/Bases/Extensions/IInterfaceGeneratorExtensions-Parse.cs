using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.B0006;


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace R5T.S0030.F002
{
    public static partial class IInterfaceGeneratorExtensions
    {
        public static InterfaceDeclarationSyntax Parse(this IInterfaceGenerator _,
            string text)
        {
            var output = Instances.SyntaxFactory.ParseInterfaceDeclaration(text);
            return output;
        }
    }
}
