using System;

using R5T.L0011.T001;
using R5T.T0098;


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace R5T.S0030.F002
{
    public static class Instances
    {
        public static IOperation Operation { get; } = T0098.Operation.Instance;
        public static ISyntaxFactory SyntaxFactory { get; } = L0011.T001.SyntaxFactory.Instance;
    }
}
