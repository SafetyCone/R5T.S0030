using System;

using R5T.T0094;


namespace R5T.S0030
{
    public interface ITypeNamedCodeFilePathed : INamedFilePathed
    {
        public string TypeName { get; set; }
        public string CodeFilePath { get; set; }
    }
}
