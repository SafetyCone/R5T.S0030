using System;

using R5T.B0000;
using R5T.B0001;
using R5T.B0003;
using R5T.B0005;
using R5T.B0006;
using R5T.B0007;
using R5T.L0011.T001;
using R5T.T0021;
using R5T.T0034;
using R5T.T0036;
using R5T.T0037;
using R5T.T0040;
using R5T.T0041;
using R5T.T0044;
using R5T.T0045;
//using R5T.T0055;
using R5T.T0062;
using R5T.T0065;
using R5T.T0070;
using R5T.T0098;
using R5T.T0113;


namespace R5T.S0030
{
    public static class Instances
    {
        public static IAttributeTypeName AttributeTypeName { get; } = T0034.AttributeTypeName.Instance;
        public static B0006.IClassGenerator ClassGenerator { get; } = B0006.ClassGenerator.Instance;
        public static T0045.IClassOperator ClassOperator { get; } = T0045.ClassOperator.Instance;
        public static ICodeFileName CodeFileName { get; } = T0037.CodeFileName.Instance;
        public static ICompilationUnitOperator CompilationUnitOperator { get; } = T0045.CompilationUnitOperator.Instance;
        public static IExample Example { get; } = B0005.Example.Instance;
        public static IFileName FileName { get; } = T0021.FileName.Instance;
        public static IFileSystemOperator FileSystemOperator { get; } = T0044.FileSystemOperator.Instance;
        public static T0055.IGuidOperator GuidOperator { get; } = T0055.GuidOperator.Instance;
        public static IHost Host { get; } = T0070.Host.Instance;
        public static B0007.IIndentation Indentation { get; } = B0007.Indentation.Instance;
        public static T0036.IIndentation Indentation_Old { get; } = T0036.Indentation.Instance;
        public static IInterfaceOperator InterfaceOperator { get; } = T0045.InterfaceOperator.Instance;
        public static ILineIndentation LineIndentation { get; } = B0007.LineIndentation.Instance;
        public static B0006.IMethodGenerator MethodGenerator { get; } = B0006.MethodGenerator.Instance;
        public static B0006.IMethodOperator MethodOperator { get; } = B0006.MethodOperator.Instance;
        public static B0002.INamespaceName NamespaceName { get; } = B0002.NamespaceName.Instance;
        public static B0002.INamespaceNameOperator NamespaceNameOperator { get; } = B0002.NamespaceNameOperator.Instance;
        public static B0003.INamespacedTypeName NamespacedTypeName { get; } = B0003.NamespacedTypeName.Instance;
        public static INamespaceOperator NamespaceOperator { get; } = B0006.NamespaceOperator.Instance;
        public static INamespacedTypeNameOperator NamespacedTypeNameOperator { get; } = B0003.NamespacedTypeNameOperator.Instance;
        public static IOperation Operation { get; } = T0098.Operation.Instance;
        public static IPathOperator PathOperator { get; } = T0041.PathOperator.Instance;
        public static IPredicate Predicate { get; } = B0000.Predicate.Instance;
        public static IProjectPath ProjectPath { get; } = T0040.ProjectPath.Instance;
        public static IProjectPathsOperator ProjectPathsOperator { get; } = T0040.ProjectPathsOperator.Instance;
        public static IServiceAction ServiceAction { get; } = T0062.ServiceAction.Instance;
        public static IServiceImplementationOperator ServiceImplementationOperator { get; } = T0065.ServiceImplementationOperator.Instance;
        public static ISolutionOperator SolutionOperator { get; } = T0113.SolutionOperator.Instance;
        public static ISolutionPathsOperator SolutionPathsOperator { get; } = T0040.SolutionPathsOperator.Instance;
        public static ISyntaxFactory SyntaxFactory { get; } = L0011.T001.SyntaxFactory.Instance;
        public static B0001.ITypeName TypeName { get; } = B0001.TypeName.Instance;
        public static T0034.ITypeName TypeName_Old { get; } = T0034.TypeName.Instance;
        public static ITypeNameOperator TypeNameOperator { get; } = B0001.TypeNameOperator.Instance;
    }
}