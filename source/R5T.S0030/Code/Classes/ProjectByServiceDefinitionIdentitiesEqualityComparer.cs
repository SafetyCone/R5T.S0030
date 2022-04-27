using System;
using System.Collections.Generic;

using R5T.T0097;

using R5T.S0030.Repositories;


namespace R5T.S0030
{
    using ProjectByServiceDefinition = KeyValuePair<ServiceDefinition, Project>;


    public class ProjectByServiceDefinitionIdentitiesEqualityComparer : IEqualityComparer<ProjectByServiceDefinition>
    {
        #region Static

        public static ProjectByServiceDefinitionIdentitiesEqualityComparer Instance { get; } = new();

        #endregion


        public bool Equals(ProjectByServiceDefinition x, ProjectByServiceDefinition y)
        {
            var output = true
                && x.Key.Identity == y.Key.Identity
                && x.Value.Identity == y.Value.Identity
                ;

            return output;
        }

        public int GetHashCode(ProjectByServiceDefinition obj)
        {
            var output = HashCode.Combine(
                obj.Key.Identity,
                obj.Value.Identity);

            return output;
        }
    }
}
