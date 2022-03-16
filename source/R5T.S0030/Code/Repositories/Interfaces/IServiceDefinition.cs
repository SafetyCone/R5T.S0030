using System;


namespace R5T.S0030.Repositories
{
    public interface IServiceDefinition
    {
        Guid Identity { get; }
        string TypeName { get; }
        string CodeFilePath { get; }
    }
}
