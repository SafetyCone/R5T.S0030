using System;

using R5T.Magyar.Results;


namespace R5T.S0030.T002.ImplementationCandidates.Level01
{
    public class TypeUpgradeResult
    {
        public string NamespacedTypeName { get; set; }

        public MultipleActionResult ValidationSuccess { get; set; }
        public TypeValidationResult ValidationResult { get; set; }
    }
}
