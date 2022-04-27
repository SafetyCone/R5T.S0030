using System;
using System.Collections.Generic;


namespace R5T.S0030.Repositories
{
    public class IdentityBasedServiceImplementationAndDefinitionPairEqualityComparer : IEqualityComparer<IServiceImplementationAndDefinitionPair>
    {
        #region Static

        public static IdentityBasedServiceImplementationAndDefinitionPairEqualityComparer Instance { get; } = new();

        #endregion


        public bool Equals(IServiceImplementationAndDefinitionPair x, IServiceImplementationAndDefinitionPair y)
        {
            var output = true
                && x.Implementation.Identity == y.Implementation.Identity
                && x.Definition.Identity == y.Definition.Identity
                ;

            return output;
        }

        public int GetHashCode(IServiceImplementationAndDefinitionPair obj)
        {
            var output = HashCode.Combine(
                obj.Implementation.Identity,
                obj.Definition.Identity);

            return output;
        }
    }


    public class IdentityBasedServiceImplementationAndDefinitionPairEqualityComparer<T> : IEqualityComparer<T>
        where T : IServiceImplementationAndDefinitionPair
    {
        #region Static

        public static IdentityBasedServiceImplementationAndDefinitionPairEqualityComparer<T> Instance { get; } = new();

        #endregion


        public bool Equals(T x, T y)
        {
            var output = true
                && x.Implementation.Identity == y.Implementation.Identity
                && x.Definition.Identity == y.Definition.Identity
                ;

            return output;
        }

        public int GetHashCode(T obj)
        {
            var output = HashCode.Combine(
                obj.Implementation.Identity,
                obj.Definition.Identity);

            return output;
        }
    }
}
