using System;

using R5T.T0036;


namespace R5T.S0030.F001
{
    public static class IParameterNameOperatorExtensions
    {
        public static string AddActionSuffix(this IParameterNameOperator _,
            string typeName)
        {
            var output = Instances.StringOperator.Suffix(
                typeName,
                Instances.TypeNameAffix.Action());

            return output;
        }

        /// <inheritdoc cref="System.IParameterNameOperatorExtensions.GetDefaultParameterNameForTypeName_HandleInterface(IParameterNameOperator, string)"/>
        public static string GetServiceActionParameterName(this IParameterNameOperator _,
            string serviceDefinitionTypeName)
        {
            var defaultParameterNameForTypeName = _.GetDefaultParameterNameForTypeName(serviceDefinitionTypeName);

            var output = _.AddActionSuffix(defaultParameterNameForTypeName);
            return output;
        }
    }
}
