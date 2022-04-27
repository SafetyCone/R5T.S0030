using System;

using R5T.T0101;


namespace R5T.S0030.FileContexts.Entities
{
    public class ServiceComponentToProjectMapping : IIdentityMapping
    {
        public Guid ServiceComponentIdentity { get; set; }
        public Guid ProjectIdentity { get; set; }

        Guid ITypedIdentityMapping<Guid, Guid>.LocalIdentity
        {
            get
            {
                return this.ServiceComponentIdentity;
            }
            set
            {
                this.ServiceComponentIdentity = value;
            }
        }

        Guid ITypedIdentityMapping<Guid, Guid>.ExternalIdentity
        {
            get
            {
                return this.ProjectIdentity;
            }
            set
            {
                this.ProjectIdentity = value;
            }
        }
    }
}
