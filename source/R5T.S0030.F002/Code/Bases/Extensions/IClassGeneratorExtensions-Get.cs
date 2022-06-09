using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.B0006;


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace R5T.S0030.F002
{
    public static partial class IClassGeneratorExtensions
    {
        public static ClassDeclarationSyntax GetClass_Synchronous(this IClassGenerator _,
            string className)
        {
            var output = Instances.Operation.Parse(
                () => _.GetClassText(className),
                _.PreParse,
                _.Parse,
                _.PostParse);

            return output;
        }

        public static ClassDeclarationSyntax GetPublicClass_Synchronous(this IClassGenerator _,
            string className)
        {
            var output = Instances.Operation.Parse(
                () => _.GetPublicClassText(className),
                _.PreParse,
                _.Parse,
                _.PostParse);

            return output;
        }

        public static Task<ClassDeclarationSyntax> GetPublicClass(this IClassGenerator _,
            string className)
        {
            var output = _.GetPublicClass_Synchronous(className);

            return Task.FromResult(output);
        }

        public static ClassDeclarationSyntax GetPublicStaticPartialClass_Synchronous(this IClassGenerator _,
            string className)
        {
            var output = Instances.Operation.Parse(
                () => _.GetPublicStaticPartialClassText(className),
                _.PreParse,
                _.Parse,
                _.PostParse);

            return output;
        }

        public static Task<ClassDeclarationSyntax> GetPublicStaticPartialClass(this IClassGenerator _,
            string className)
        {
            var output = _.GetPublicStaticPartialClass_Synchronous(className);

            return Task.FromResult(output);
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
