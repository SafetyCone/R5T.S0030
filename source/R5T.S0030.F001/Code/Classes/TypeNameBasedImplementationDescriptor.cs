using System;


namespace R5T.S0030.F001
{
    public class TypeNameBasedImplementationDescriptor
    {
        public string ImplementationTypeName { get; set; }
        public string DefinitionTypeName { get; set; }
        public string[] DependencyTypeNames { get; set; }
    }
}
