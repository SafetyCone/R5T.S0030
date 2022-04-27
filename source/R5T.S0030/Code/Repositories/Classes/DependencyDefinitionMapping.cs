using System;


namespace R5T.S0030.Repositories
{
    /// <summary>
    /// For mapping an implementation to the definitions on which it depends.
    /// </summary>
    public class DependencyDefinitionMapping : IServiceImplementationAndDefinitionPair
    {
        #region Static

        public static DependencyDefinitionMapping From(
            ServiceImplementation serviceImplementation,
            ServiceDefinition serviceDefinition)
        {
            var output = new DependencyDefinitionMapping
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
