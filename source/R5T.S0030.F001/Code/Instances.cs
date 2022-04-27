using System;

using R5T.B0000;
using R5T.B0001;
using R5T.B0003;
using R5T.B0006;
using R5T.B0007;
using R5T.L0011.T001;
using R5T.T0036;
//using R5T.T0045;
using R5T.T0066;


namespace R5T.S0030.F001
{
    public static class Instances
    {
        public static IDocumentationGenerator DocumentationGenerator { get; } = B0006.DocumentationGenerator.Instance;
        public static IDocumentationLine DocumentationLine { get; } = T0036.DocumentationLine.Instance;
        public static IExpressionGenerator ExpressionGenerator { get; } = B0006.ExpressionGenerator.Instance;
        public static B0007.IIndentation Indentation { get; } = B0007.Indentation.Instance;
        public static ILineIndentation LineIndentation { get; } = B0007.LineIndentation.Instance;
        public static IMethodGenerator MethodGenerator { get; } = B0006.MethodGenerator.Instance;
        public static IMethodName MethodName { get; } = T0036.MethodName.Instance;
        public static IMethodNameOperator MethodNameOperator { get; } = T0036.MethodNameOperator.Instance;
        public static INameGenerator NameGenerator { get; } = B0006.NameGenerator.Instance;
        public static INamespacedTypeNameOperator NamespacedTypeNameOperator { get; } = B0003.NamespacedTypeNameOperator.Instance;
        public static IParameterGenerator ParameterGenerator { get; } = B0006.ParameterGenerator.Instance;
        public static T0045.IParameterGenerator ParameterGenerator_Old { get; } = T0045.ParameterGenerator.Instance;
        public static IParameterOperator ParameterOperator { get; } = B0006.ParameterOperator.Instance;
        public static IParameterName ParameterName { get; } = T0036.ParameterName.Instance;
        public static IParameterNameOperator ParameterNameOperator { get; } = T0036.ParameterNameOperator.Instance;
        public static IServiceComponentNameOperator ServiceComponentNameOperator { get; } = T0066.ServiceComponentNameOperator.Instance;
        public static IStatementGenerator StatementGenerator { get; } = B0006.StatementGenerator.Instance;
        public static IStringOperator StringOperator { get; } = B0000.StringOperator.Instance;
        public static ISyntaxFactory SyntaxFactory { get; } = L0011.T001.SyntaxFactory.Instance;
        public static ITypeName TypeName { get; } = B0001.TypeName.Instance;
        public static ITypeNameAffix TypeNameAffix { get; } = B0001.TypeNameAffix.Instance;
        public static ITypeNameOperator TypeNameOperator { get; } = B0001.TypeNameOperator.Instance;
        public static IVariableName VariableName { get; } = T0036.VariableName.Instance;
    }
}
