using System;


namespace R5T.S0030
{
    public interface IServiceComponentDescriptor : ITypeNamedCodeFilePathed
    {
        string ProjectFilePath { get; }
    }
}
