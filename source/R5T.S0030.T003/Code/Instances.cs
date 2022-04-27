using System;

using R5T.B0002;
using R5T.B0003;
using R5T.B0006;
using R5T.T0044;
using R5T.T0045;


namespace R5T.S0030.T003
{
    public static class Instances
    {
        public static B0006.IClassOperator ClassOperator { get; } = B0006.ClassOperator.Instance;
        public static T0045.IClassGenerator ClassGenerator { get; } = T0045.ClassGenerator.Instance;
        public static ICompilationUnitGenerator CompilationUnitGenerator { get; } = T0045.CompilationUnitGenerator.Instance;
        public static ICompilationUnitOperator CompilationUnitOperator { get; } = T0045.CompilationUnitOperator.Instance;
        public static IFileSystemOperator FileSystemOperator { get; } = T0044.FileSystemOperator.Instance;
        public static INamespaceName NamespaceName { get; } = B0002.NamespaceName.Instance;
        public static INamespaceNameOperator NamespaceNameOperator { get; } = B0002.NamespaceNameOperator.Instance;
        public static INamespacedTypeNameOperator NamespacedTypeNameOperator { get; } = B0003.NamespacedTypeNameOperator.Instance;
        public static T0045.INamespaceGenerator NamespaceGenerator { get; } = T0045.NamespaceGenerator.Instance;
        public static INamespaceOperator NamespaceOperator { get; } = B0006.NamespaceOperator.Instance;
    }
}
