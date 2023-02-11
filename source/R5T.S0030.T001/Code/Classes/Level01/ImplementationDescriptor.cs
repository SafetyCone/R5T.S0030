using System;


namespace R5T.S0030.T001.Level01
{
    /// <summary>
    /// Describes a class in terms relevant to being a service implementation.
    /// This is the basic level, that merely records details of a class.
    /// The only non-descriptive information it contains is whether different type names are recognized as service definitions (TODO, should be service components).
    /// This type is generated via (CompilationUnit, ClassDeclaration, ServiceDefinitionNamespacedTypeNames) => ImplementationDescriptor.
    /// </summary>
    public class ImplementationDescriptor
    {
        public string NamespacedTypeName { get; set; }

        // Even though candidate has already been validated, duplicate here so that we get all information relevant to the implementation (even if obvious) and in case someone does not remember to do the candidate type validation first.
        public bool IsClass { get; set; }
        public bool IsAbstract { get; set; }
        public bool IsStatic { get; set; }

        public bool HasExplicitNoServiceDefinitionMarkerInterface { get; set; }

        public BaseTypeDescriptor[] BaseTypeDescriptors { get; set; }

        public bool HasSpecifyServiceDefinitionAttribute { get; set; }
        public ServiceDefinitionDependencyDescriptor SpecifyServiceDefinitionType { get; set; }

        public ConstructorDescriptor[] ConstructorDescriptors { get; set; }

        public bool HasServiceImplementationMarkerAttribute { get; set; }
        public bool HasServiceImplementationMarkerInterface { get; set; }

        /// <summary>
        /// The <see cref="T0064.INoServiceDefinitionMarker"/> interface should be used instead of the <see cref="T0064.INoServiceDefinition"/> interface.
        /// </summary>
        public bool HasOldNoServiceDefinitionInterface { get; set; }

        /// <summary>
        /// The <see cref="T0064.IServiceImplementation"/> interface should be used instead of the <see cref="T0064.IServiceImplementation"/> interface.
        /// </summary>
        public bool HasOldServiceImplementationInterface { get; set; }

        /// <summary>
        /// The <see cref="T0064.SpecifyServiceDefinitionAttribute"/> interface should be used instead of the <see cref="T0064.ImplementsServiceDefinitionAttribute"/> interface.
        /// </summary>
        public bool HasOldImplementsServiceDefinitionAttribute { get; set; }
    }
}
