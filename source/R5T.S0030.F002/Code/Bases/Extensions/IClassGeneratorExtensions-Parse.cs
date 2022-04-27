using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.B0006;


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace R5T.S0030.F002
{
    public static partial class IClassGeneratorExtensions
    {
        public static ClassDeclarationSyntax Parse(this IClassGenerator _,
            string text)
        {
            var output = Instances.SyntaxFactory.ParseClassDeclaration(text);
            return output;
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
