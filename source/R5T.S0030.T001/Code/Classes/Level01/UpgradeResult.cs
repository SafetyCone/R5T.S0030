using System;

using R5T.Magyar.Results;


namespace R5T.S0030.T001.Level01
{
    /// <summary>
    /// Assuming that an <see cref="ImplementationDescriptor"/> can be upgraded to an <see cref="Level02.ImplementationDescriptor"/> (after a successful validation result), this specifies details of the upgrade process.
    /// The upgrade process is basically just choosing one (1) service definition to implement, and one (1) constructor to use as the service dependencies set.
    /// </summary>
    public class UpgradeResult
    {
        public string NamespacedTypeName { get; set; }

        public DefinitionSelectionMethod DefinitionSelectionMethod { get; set; }
        public DependencySetSelectionMethod DependencySetSelectionMethod { get; set; }

        public MultipleActionResult ValidationSuccess { get; set; }
        public ImplementationValidationResult ValidationResult { get; set; }
    }
}
