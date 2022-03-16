using System;

using R5T.B0001;
using R5T.T0040;
using R5T.T0044;
using R5T.T0045;
using R5T.T0062;
using R5T.T0070;
using R5T.T0098;


namespace R5T.S0030
{
    public static class Instances
    {
        public static IClassOperator ClassOperator { get; } = T0045.ClassOperator.Instance;
        public static ICompilationUnitOperator CompilationUnitOperator { get; } = T0045.CompilationUnitOperator.Instance;
        public static IFileSystemOperator FileSystemOperator { get; } = T0044.FileSystemOperator.Instance;
        public static IHost Host { get; } = T0070.Host.Instance;
        public static IInterfaceOperator InterfaceOperator { get; } = T0045.InterfaceOperator.Instance;
        public static IOperation Operation { get; } = T0098.Operation.Instance;
        public static IProjectPathsOperator ProjectPathsOperator { get; } = T0040.ProjectPathsOperator.Instance;
        public static IServiceAction ServiceAction { get; } = T0062.ServiceAction.Instance;
        public static ITypeName TypeName { get; } = B0001.TypeName.Instance;
        public static ITypeNameOperator TypeNameOperator { get; } = B0001.TypeNameOperator.Instance;
    }
}