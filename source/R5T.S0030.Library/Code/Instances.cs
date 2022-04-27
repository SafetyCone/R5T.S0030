using System;

using R5T.B0001;
using R5T.B0002;
using R5T.B0003;
using R5T.T0065;
using R5T.T0098;


namespace R5T.S0030.Library
{
    public static class Instances
    {
        public static INamespacedTypeName NamespacedTypeName { get; } = B0003.NamespacedTypeName.Instance;
        public static INamespacedTypeNameOperator NamespacedTypeNameOperator { get; } = B0003.NamespacedTypeNameOperator.Instance;
        public static INamespaceName NamespaceName { get; } = B0002.NamespaceName.Instance;
        public static IOperation Operation { get; } = T0098.Operation.Instance;
        public static IServiceImplementationOperator ServiceImplementationOperator { get; } = T0065.ServiceImplementationOperator.Instance;
        public static ITypeName TypeName { get; } = B0001.TypeName.Instance;
    }
}
