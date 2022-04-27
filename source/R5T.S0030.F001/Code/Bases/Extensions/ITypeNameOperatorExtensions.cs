using System;
using System.N0; // The low-dependency implementation primary namespace name.

using R5T.B0001;

using Instances = R5T.S0030.F001.Instances;

using N8;


namespace System
{
    public static class ITypeNameOperatorExtensions
    {
        public static string GetIServiceActionOfTypeNameTypeName(this ITypeNameOperator _,
            string serviceDefinitionTypeName)
        {
            var output = _.GetGenericOfTypeNameTypeName(
                Instances.TypeName.IServiceAction_Base(),
                serviceDefinitionTypeName);

            return output;
        }
    }
}
