using System;
using System.N0;

using R5T.T0036;

using Instances = R5T.S0030.F001.Instances;


namespace R5T.S0030.F001
{
    public static class IMethodNameOperatorExtensions
    {
        public static string AddPrefix(this IMethodNameOperator _,
            string prefix,
            string typeName)
        {
            var output = Instances.StringOperator.Prefix(
                prefix,
                typeName);

            return output;
        }

        public static string AddAddPrefix(this IMethodNameOperator _,
            string methodName)
        {
            var output = _.AddPrefix(
                NameAffixes.Add,
                methodName);

            return output;
        }

        public static string AddActionSuffix(this IMethodNameOperator _,
            string methodName)
        {
            var output = Instances.StringOperator.Suffix(
                methodName,
                NameAffixes.Action);

            return output;
        }

        public static string GetAddXMethodName(this IMethodNameOperator _,
            string implementationTypeName)
        {
            var typeNameStem = Instances.TypeNameOperator.GetTypeNameStem_HandleInterface(implementationTypeName);
            
            // Method name stem is the same as the type name stem.
            var methodNameStem = typeNameStem;

            var output = _.AddAddPrefix(methodNameStem);
            return output;
        }

        public static string GetAddXActionMethodName(this IMethodNameOperator _,
            string implementationTypeName)
        {
            var addXMethodName = _.GetAddXMethodName(implementationTypeName);

            var output = _.AddActionSuffix(addXMethodName);
            return output;
        }

        public static string GetServiceActionNewOfTypeNameMethodName(this IMethodNameOperator _,
            string serviceDefinitionTypeName)
        {
            // Abuse the type name operator since it provides the same string functionality we need for method name.
            var output = Instances.TypeNameOperator.GetGenericOfTypeNameTypeName(
                Instances.MethodName.New(),
                serviceDefinitionTypeName);

            return output;
        }
    }
}
