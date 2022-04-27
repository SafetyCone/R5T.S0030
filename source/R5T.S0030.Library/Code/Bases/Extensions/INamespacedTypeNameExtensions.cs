using System;

using R5T.B0003;

using Instances = R5T.S0030.Library.Instances;


namespace System
{
    public static class INamespacedTypeNameExtensions
    {
        public static string R5T_T0062_IServiceAction_Base(this INamespacedTypeName _)
        {
            var output = _.From(
                Instances.NamespaceName.R5T_T0062(),
                Instances.TypeName.IServiceAction_Base());

            return output;
        }

        public static string R5T_T0063_IServiceAction(this INamespacedTypeName _)
        {
            var output = _.From(
                Instances.NamespaceName.R5T_T0063(),
                Instances.TypeName.IServiceAction());

            return output;
        }

        public static string R5T_T0064_INoServiceDefinition(this INamespacedTypeName _)
        {
            var output = _.From(
                Instances.NamespaceName.R5T_T0064(),
                Instances.TypeName.INoServiceDefinition());

            return output;
        }

        public static string R5T_T0064_IServiceDefinition(this INamespacedTypeName _)
        {
            var output = _.From(
                Instances.NamespaceName.R5T_T0064(),
                Instances.TypeName.IServiceDefinition());

            return output;
        }

        public static string R5T_T0064_IServiceImplementation(this INamespacedTypeName _)
        {
            var output = _.From(
                Instances.NamespaceName.R5T_T0064(),
                Instances.TypeName.IServiceImplementation());

            return output;
        }

        public static string R5T_T0064_ServiceDefinitionMarkerAttribute(this INamespacedTypeName _)
        {
            var output = _.From(
                Instances.NamespaceName.R5T_T0064(),
                Instances.TypeName.ServiceDefinitionMarkerAttribute());

            return output;
        }

        public static string R5T_T0064_ServiceImplementationMarkerAttribute(this INamespacedTypeName _)
        {
            var output = _.From(
                Instances.NamespaceName.R5T_T0064(),
                Instances.TypeName.ServiceImplementationMarkerAttribute());

            return output;
        }
    }
}
