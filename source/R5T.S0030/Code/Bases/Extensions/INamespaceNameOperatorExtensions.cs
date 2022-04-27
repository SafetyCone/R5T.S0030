using System;
using System.Linq;

using R5T.B0002;


namespace R5T.S0030
{
    public static class INamespaceNameOperatorExtensions
    {
        /// <summary>
        /// Gets infrastructure namespace required for AddX() methods.
        /// </summary>
        public static string[] GetAddXActionMethodsRequiredNamespaces(this INamespaceNameOperator _)
        {
            var output = _.GetAddXMethodsRequiredNamespaces()
                .Append(
                    Instances.NamespacedTypeNameOperator.GetNamespaceName(
                        Instances.NamespacedTypeName.R5T_T0062_IServiceAction_Base()))
                .Now();

            return output;
        }

        /// <summary>
        /// Gets infrastructure namespace required for AddX() methods.
        /// </summary>
        public static string[] GetAddXMethodsRequiredNamespaces(this INamespaceNameOperator _)
        {
            var output = new[]
            {
                // IServiceColletionNamespace.
                Instances.NamespacedTypeNameOperator.GetNamespaceName(
                        Instances.NamespacedTypeName.IServiceCollection()),
                // IServiceAction<T> namespace.
                Instances.NamespacedTypeNameOperator.GetNamespaceName(
                        Instances.NamespacedTypeName.R5T_T0063_IServiceAction()),
            };

            return output;
        }
    }
}
