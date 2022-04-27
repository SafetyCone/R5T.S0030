using System;

using R5T.T0101;


namespace R5T.S0030.FileContexts.Entities
{
    public class ImplementionToDefinitionMapping : IIdentityMapping
    {
        public Guid ImplementationIdentity { get; set; }
        public Guid DefinitionIdentity { get; set; }

        Guid ITypedIdentityMapping<Guid, Guid>.LocalIdentity
        {
            get
            {
                return this.ImplementationIdentity;
            }
            set
            {
                this.ImplementationIdentity = value;
            }
        }

        Guid ITypedIdentityMapping<Guid, Guid>.ExternalIdentity
        {
            get
            {
                return this.DefinitionIdentity;
            }
            set
            {
                this.DefinitionIdentity = value;
            }
        }
    }
}
