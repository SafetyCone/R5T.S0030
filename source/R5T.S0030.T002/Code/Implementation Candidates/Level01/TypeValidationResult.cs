using System;


namespace R5T.S0030.T002.ImplementationCandidates.Level01
{
    public class TypeValidationResult
    {
        public string NamespacedTypeName { get; set; }

        public bool IsClass { get; set; }

        public bool IsAbstract { get; set; }
        public bool IsStatic { get; set; }
    }
}
