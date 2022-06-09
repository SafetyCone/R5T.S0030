using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;

using IOperation = R5T.T0098.IOperation;


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace R5T.S0030.F002
{
    using N8;


    public static partial class IOperationExtensions
    {
        public static string PreParse(this IOperation _,
            string text)
        {
            var output = _.PreParse_04242022(text);
            return output;
        }
    }
}


namespace R5T.S0030.F002.N8
{
    public static partial class IOperationExtensions
    {
        public static string PreParse_04242022(this IOperation _,
            string text)
        {
            var output = text
                .Trim()
                ;

            return output;
        }
    }
}