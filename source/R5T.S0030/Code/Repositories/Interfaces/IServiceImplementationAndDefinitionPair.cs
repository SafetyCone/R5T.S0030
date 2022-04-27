using System;


namespace R5T.S0030.Repositories
{
    public interface IServiceImplementationAndDefinitionPair
    {
        public ServiceImplementation Implementation { get; }
        public ServiceDefinition Definition { get; }
    }
}
