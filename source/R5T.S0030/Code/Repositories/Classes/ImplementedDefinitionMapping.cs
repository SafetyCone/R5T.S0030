using System;


namespace R5T.S0030.Repositories
{
    /// <summary>
    /// For mapping an implementation to the definition it implements.
    /// </summary>
    public class ImplementedDefinitionMapping : IServiceImplementationAndDefinitionPair
    {
        #region Static

        public static ImplementedDefinitionMapping From(
            ServiceImplementation serviceImplementation,
            ServiceDefinition serviceDefinition)
        {
            var output = new ImplementedDefinitionMapping
            {
                Definition = serviceDefinition,
                Implementation = serviceImplementation,
            };

            return output;
        }

        #endregion


        public ServiceImplementation Implementation { get; set; }
        public ServiceDefinition Definition { get; set; }
    }
}
