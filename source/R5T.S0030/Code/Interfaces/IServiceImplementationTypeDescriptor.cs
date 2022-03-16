using System;


namespace R5T.S0030
{
    public interface IServiceImplementationTypeDescriptor : ITypeNamedCodeFilePathed
    {
        string ServiceDefinitionTypeName { get; }
        string[] DependencyServiceDefinitionTypeNames { get; }
    }
}
