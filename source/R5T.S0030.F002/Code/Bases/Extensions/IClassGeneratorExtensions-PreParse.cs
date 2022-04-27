using System;

using R5T.B0006;


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace R5T.S0030.F002
{
    using N8;


    public static partial class IClassGeneratorExtensions
    {
        public static string PreParse(this IClassGenerator _,
            string text)
        {
            var output = _.PreParse_04242022(text);
            return output;
        }
    }
}


namespace R5T.S0030.F002.N8
{
    public static partial class IClassGeneratorExtensions
    {
        public static string PreParse_04242022(this IClassGenerator _,
            string text)
        {
            var output = text
                .Trim()
                ;

            return output;
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
