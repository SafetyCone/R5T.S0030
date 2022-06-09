using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.B0006;


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace R5T.S0030.F002
{
    public static partial class IInterfaceGeneratorExtensions
    {
        public static Task<InterfaceDeclarationSyntax> GetInterface(this IInterfaceGenerator _,
            string interfaceName)
        {
            var output = _.GetInterface_Synchronous(interfaceName);

            return Task.FromResult(output);
        }

        public static InterfaceDeclarationSyntax GetInterface_Synchronous(this IInterfaceGenerator _,
            string interfaceName)
        {
            var output = Instances.Operation.Parse(
                () => _.GetInterfaceText(interfaceName),
                Instances.Operation.PreParse,
                _.Parse,
                Instances.Operation.PostParse_ForTypes);

            return output;
        }

        public static Task<InterfaceDeclarationSyntax> GetPublicInterface(this IInterfaceGenerator _,
            string interfaceName)
        {
            var output = _.GetPublicInterface_Synchronous(interfaceName);

            return Task.FromResult(output);
        }

        public static InterfaceDeclarationSyntax GetPublicInterface_Synchronous(this IInterfaceGenerator _,
            string interfaceName)
        {
            var output = Instances.Operation.Parse(
                () => _.GetPublicInterfaceText(interfaceName),
                Instances.Operation.PreParse,
                _.Parse,
                Instances.Operation.PostParse_ForTypes);

            return output;
        }
    }
}
