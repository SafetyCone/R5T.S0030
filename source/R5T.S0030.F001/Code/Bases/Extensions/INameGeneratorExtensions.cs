using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.B0006;

using Instances = R5T.S0030.F001.Instances;


namespace System
{
    public static class INameGeneratorExtensions
    {
        public static GenericNameSyntax AddSingleton(this INameGenerator _,
            string serviceDefinitionTypeName,
            string serviceImplementationTypeName)
        {
            var output = Instances.SyntaxFactory.GenericName(
                    Instances.MethodName.AddSingleton())
                .AddTypeArgumentListArguments(
                    Instances.SyntaxFactory.Type(serviceDefinitionTypeName),
                    Instances.SyntaxFactory.Type(serviceImplementationTypeName))
                .NormalizeWhitespace(); // Ok.

            return output;
        }
    }
}
