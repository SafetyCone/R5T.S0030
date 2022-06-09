using System;

using R5T.B0002;
using R5T.B0003;
using R5T.B0006;
using R5T.B0007;
using R5T.D0082.T001;
using R5T.L0011.T001;
using R5T.T0034;
using R5T.T0040;
using R5T.T0041;
using R5T.T0044;
using R5T.T0045;
using R5T.T0108;
using R5T.T0110;
using R5T.T0113;


namespace R5T.S0030.T003
{
    public static partial class Instances
    {
        public static IAttributeTypeName AttributeTypeName { get; } = T0034.AttributeTypeName.Instance;
        public static B0006.IClassOperator ClassOperator { get; } = B0006.ClassOperator.Instance;
        public static T0045.IClassGenerator ClassGenerator { get; } = T0045.ClassGenerator.Instance;
        public static T0045.ICompilationUnitGenerator CompilationUnitGenerator { get; } = T0045.CompilationUnitGenerator.Instance;
        public static B0006.ICompilationUnitOperator CompilationUnitOperator { get; } = B0006.CompilationUnitOperator.Instance;
        public static T0045.ICompilationUnitOperator CompilationUnitOperator_Old { get; } = T0045.CompilationUnitOperator.Instance;
        public static IFileSystemOperator FileSystemOperator { get; } = T0044.FileSystemOperator.Instance;
        public static IGitHubRepositorySpecificationGenerator GitHubRepositorySpecificationGenerator { get; }
        public static B0006.IInterfaceGenerator InterfaceGenerator { get; } = B0006.InterfaceGenerator.Instance;
        public static B0006.IInterfaceOperator InterfaceOperator { get; } = B0006.InterfaceOperator.Instance;
        public static ILibraryNameOperator LibraryNameOperator { get; } = T0110.LibraryNameOperator.Instance;
        public static ILineIndentation LineIndentation { get; } = B0007.LineIndentation.Instance;
        public static INamespaceName NamespaceName { get; } = B0002.NamespaceName.Instance;
        public static INamespaceNameOperator NamespaceNameOperator { get; } = B0002.NamespaceNameOperator.Instance;
        public static INamespacedTypeNameOperator NamespacedTypeNameOperator { get; } = B0003.NamespacedTypeNameOperator.Instance;
        public static T0045.INamespaceGenerator NamespaceGenerator { get; } = T0045.NamespaceGenerator.Instance;
        public static INamespaceOperator NamespaceOperator { get; } = B0006.NamespaceOperator.Instance;
        public static IPathOperator PathOperator { get; } = T0041.PathOperator.Instance;
        public static IProjectOperator ProjectOperator { get; } = T0113.ProjectOperator.Instance;
        public static IProjectPathsOperator ProjectPathsOperator { get; } = T0040.ProjectPathsOperator.Instance;
        public static IPropertyGenerator PropertyGenerator { get; } = T0045.PropertyGenerator.Instance;
        public static IRepositoryNameOperator RepositoryNameOperator { get; } = T0108.RepositoryNameOperator.Instance;
        public static ISolutionOperator SolutionOperator { get; } = T0113.SolutionOperator.Instance;
        public static ISolutionPathsOperator SolutionPathsOperator { get; } = T0040.SolutionPathsOperator.Instance;
        public static ISyntaxFactory SyntaxFactory { get; } = L0011.T001.SyntaxFactory.Instance;

        public static Z Z { get; } = new Z();
    }
}
