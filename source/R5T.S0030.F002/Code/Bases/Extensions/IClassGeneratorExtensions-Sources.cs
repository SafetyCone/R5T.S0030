using System;

using R5T.B0006;


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace R5T.S0030.F002
{
    public static partial class IClassGeneratorExtensions
    {
        public static string GetClassText(this IClassGenerator _,
            string className)
        {
            var output = $"class {className}";
            return output;
        }

        public static string GetPublicClassText(this IClassGenerator _,
            string className)
        {
            var output = $"public class {className}";
            return output;
        }

        public static string GetPublicStaticClassText(this IClassGenerator _,
            string className)
        {
            var output = $"public static class {className}";
            return output;
        }

        public static string GetPublicStaticPartialClassText(this IClassGenerator _,
            string className)
        {
            var output = $"public static partial class {className}";
            return output;
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
