using System;
using System.Collections.Generic;

using R5T.T0097;

using R5T.S0030.Repositories;


namespace R5T.S0030
{
    using ProjectByServiceImplementation = KeyValuePair<ServiceImplementation, Project>;


    public class ProjectByServiceImplementationIdentitiesEqualityComparer : IEqualityComparer<ProjectByServiceImplementation>
    {
        #region Static

        public static ProjectByServiceImplementationIdentitiesEqualityComparer Instance { get; } = new();

        #endregion


        public bool Equals(ProjectByServiceImplementation x, ProjectByServiceImplementation y)
        {
            var output = true
                && x.Key.Identity == y.Key.Identity
                && x.Value.Identity == y.Value.Identity
                ;

            return output;
        }

        public int GetHashCode(ProjectByServiceImplementation obj)
        {
            var output = HashCode.Combine(
                obj.Key.Identity,
                obj.Value.Identity);

            return output;
        }
    }
}
