using System;

using R5T.B0006;


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace R5T.S0030.F002
{
    public static partial class IInterfaceGeneratorExtensions
    {
        public static string GetInterfaceText(this IInterfaceGenerator _,
            string interfaceName)
        {
            var output = $"interface {interfaceName}";
            return output;
        }

        public static string GetPublicInterfaceText(this IInterfaceGenerator _,
            string interfaceName)
        {
            var output = $"public interface {interfaceName}";
            return output;
        }
    }
}
