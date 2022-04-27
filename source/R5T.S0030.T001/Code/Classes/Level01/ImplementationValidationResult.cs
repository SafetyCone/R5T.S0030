using System;


namespace R5T.S0030.T001.Level01
{
    /// <summary>
    /// Specifies details relevant to validating a service implementation.
    /// This class does not make conclusions about whether the details constitute a failure, warning, or success. Just describes them.
    /// </summary>
    public class ImplementationValidationResult
    {
        public bool IsClass { get; set; }
        public bool IsAbstract { get; set; }
        public bool IsStatic { get; set; }

        /// <summary>
        /// True if the type argument of the <see cref="T0064.SpecifyServiceDefinitionAttribute"/> is not recognized as a service definition type.
        /// </summary>
        public bool SpecifyServiceDefinitionTypeUnrecognized { get; set; }
        /// <summary>
        /// The <see cref="T0064.SpecifyServiceDefinitionAttribute"/> specifies which of the multiple service definitions in the base types list is the single service definition for the service implementation.
        /// If the specified type is *not* found in the base types list, we might want to know.
        /// </summary>
        public bool SpecifiedServiceDefinitionNotFoundInBaseTypes { get; set; }

        /// <summary>
        /// No service definition was recognized in the base types of the service implementation.
        /// </summary>
        public bool NoServiceDefinitionBaseTypeRecognized { get; set; }
        /// <summary>
        /// True if there are multiple service implementation types in the base types list of the service implementation, but there is no <see cref="T0064.SpecifyServiceDefinitionAttribute"/> to select one of them.
        /// </summary>
        public bool MultipleServiceDefinitionBaseTypesWithoutMarkerAttribute { get; set; }

        public bool HasExplicitNoServiceDefinitionMarkerButHasDefinitionBaseType { get; set; }
        public bool HasExplicitNoServiceDefinitionMarkerButHasSpecifyDefinitionAttribute { get; set; }

        /// <summary>
        /// True if there are multiple constructors without the <see cref="T0064.ServiceImplementationConstructorMarkerAttribute"/>.
        /// </summary>
        public bool MultipleConstructorsWithoutMarkerAttribute { get; set; }
        public bool MultipleConstructorsWithMarkerAttribute { get; set; }

        public ConstructorValidationResult[] ConstructorResults { get; set; }
        public ConstructorValidationResult ChosenConstructorResult { get; set; }

        /// <inheritdoc cref="ImplementationDescriptor.HasOldNoServiceDefinitionInterface"/>
        public bool HasOldNoServiceDefinitionInterface { get; set; }
        /// <inheritdoc cref="ImplementationDescriptor.HasOldServiceImplementationInterface"/>
        public bool HasOldServiceImplementationInterface { get; set; }
        /// <inheritdoc cref="ImplementationDescriptor.HasOldImplementsServiceDefinitionAttribute"/>
        public bool HasOldImplementsServiceDefinitionAttribute { get; set; }
    }
}
