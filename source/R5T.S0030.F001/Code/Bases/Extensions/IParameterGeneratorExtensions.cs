using System;
using System.Collections.Generic;
using System.Linq;

using System.N0;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;


using R5T.B0006;


namespace R5T.S0030.F001
{
    public static class IParameterGeneratorExtensions
    {
        public static ParameterSyntax GetServiceActionParameter(this IParameterGenerator _,
            string definitionTypeName)
        {
            var iServiceActionOfDefinitionTypeName = Instances.TypeNameOperator.GetGenericOfTypeNameTypeName(
                Instances.TypeName.IServiceAction(),
                definitionTypeName);

            var parameterName = Instances.ParameterNameOperator.GetServiceActionParameterName(definitionTypeName);

            var output = Instances.ParameterGenerator.GetParameter(
                iServiceActionOfDefinitionTypeName,
                parameterName);

            return output;
        }

        public static ParameterSyntax[] GetServiceActionParameters_InInputOrder(this IParameterGenerator _,
            IEnumerable<string> dependencyDefinitionTypeNames)
        {
            var output = dependencyDefinitionTypeNames
                .Select(dependencyDefinitionTypeName =>
                {
                    var parameter = _.GetServiceActionParameter(dependencyDefinitionTypeName);
                    return parameter;
                })
                .ToArray();

            return output;
        }

        public static ParameterSyntax[] GetServiceActionParameters(this IParameterGenerator _,
            IEnumerable<string> dependencyDefinitionTypeNames)
        {
            var output = _.GetServiceActionParameters_InInputOrder(dependencyDefinitionTypeNames);
            return output;
        }
    }
}
