using System;

using R5T.B0000;
using R5T.L0011.T001;


namespace R5T.S0030.F004
{
    public static class Instances
    {
        public static IPredicate Predicate { get; } = B0000.Predicate.Instance;
        public static ISyntaxFactory SyntaxFactory { get; } = L0011.T001.SyntaxFactory.Instance;
    }
}
