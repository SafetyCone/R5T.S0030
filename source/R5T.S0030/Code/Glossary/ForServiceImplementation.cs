using System;


namespace R5T.S0030.Y000
{
    public static partial class Glossary
    {
        public static class ForServiceImplementation
        {
            public static class Requirements
            {
                /// <summary>
                /// A service implementation candidate can satisfy different levels for the the requirements for a service implementation.
                /// </summary>
                public static readonly object CandidateLevels;

                /// <summary>
                /// To meet the Level 00 requirements to be a service implementation, a type declaration:
                /// <para>1) <inheritdoc cref="MustBeClass" path="/description"/></para>
                /// </summary>
                /// <name><i>level 00 requirements</i></name>
                public static readonly object Level_00;

                /// <summary>
                /// To meet the Level 01 requirements to be a service implementation, a class declaration:
                /// <para>1) <inheritdoc cref="MustNotBeAbstract" path="/description"/></para>
                /// <para>2) <inheritdoc cref="MustNotBeStatic" path="/description"/></para>
                /// </summary>
                /// <name><i>level 01 requirements</i></name>
                public static readonly object Level_01;

                /// <summary>
                /// To meet the Level 02 requirements to be a service implementation, a class declaration must either:
                /// <para>1) <inheritdoc cref="ImplementRecognizedServiceDefinition" path="/description"/></para>
                /// - or -
                /// <para>2) <inheritdoc cref="HasNoServiceDefinitionInterface" path="/description"/></para>
                /// - or -
                /// <para>3) <inheritdoc cref="MarkedWithImplementsServiceDefinitionAttribute" path="/description"/></para>
                /// </summary>
                /// <name><i>level 02 requirements</i></name>
                public static readonly object Level_02;

                /// <summary>
                /// To meet the Level 03 requirements to be a service implementation, a class declaration:
                /// <para>1A) <inheritdoc cref="MustHaveZeroOrOneConstructors" path="/description"/></para>
                /// - or -
                /// <para>1B) <inheritdoc cref="OneOfManyConstructorsMarkedWithServiceImplementationConstructorAttribute" path="/description"/></para>
                /// <para>and 2) <inheritdoc cref="AllConstructorParameterTypesMustBeServiceDefinitions" path="/description"/></para>
                /// </summary>
                /// <name><i>level 03 requirements</i></name>
                public static readonly object Level_03;

                /// <summary>
                /// To meet the Level 04 requirements to be a service implementation, a class declaration:
                /// <para>1) <inheritdoc cref="MustHaveMarkerAttribute" path="/description"/></para>
                /// <para>2) <inheritdoc cref="MustHaveMarkerInterface" path="/description"/></para>
                /// </summary>
                /// <name><i>level 04 requirements</i></name>
                public static readonly object Level_04;

                /// <reason>
                /// An implementation must actually implement something, and interfaces cannot have their own implementations of anything. So the service implementation must be a class.
                /// </reason>
                /// <remarks>
                /// Note: As of C# 8.0, interfaces can have default implementations, but we are keeping it simple and not handling those.
                /// </remarks>
                /// <description>Must be a class.</description>
                public static readonly object MustBeClass;

                /// <reason>
                /// An implementation must be instantiable, and abstract class are not.
                /// </reason>
                /// <description>Must not be abstract.</description>
                public static readonly object MustNotBeAbstract;

                /// <reason>
                /// An implementation must be instantiable, and static class are not.
                /// </reason>
                /// <description>Must not be static.</description>
                public static readonly object MustNotBeStatic;

                /// <reason>
                /// A service implementation always implements one (or more) service definitions.
                /// </reason>
                /// <description>Implement a recognized service definition (have a recognized service definition in its base types list).</description>
                public static readonly object ImplementRecognizedServiceDefinition;

                /// <reason>
                /// A service implementation always implements one (or more) service definitions.
                /// There might be a reason the service definition does not appear in the base types list (e.g. perhaps it would be a duplicate of some base interface).
                /// </reason>
                /// <description>Be marked with the <see cref="T0064.ImplementsServiceDefinitionAttribute"/> specifying the type of a recognized service definition.</description>
                public static readonly object MarkedWithImplementsServiceDefinitionAttribute;

                /// <reason>
                /// A service can only be built by an automatic construction container using types registered with the container.
                /// We make the assumption that only service definition types will be regisered with the container (not simple types like string, or service immplementations directly)
                /// </reason>
                /// <remarks>
                /// It is possible to use any service component (implementation or definition) as a dependency.
                /// But for now we are limiting service dependencies to only service definitions.
                /// </remarks>
                /// <description>All constructor parameter types must be recognized service definitions.</description>
                public static readonly object AllConstructorParameterTypesMustBeServiceDefinitions;

                /// <reason>
                /// A service implementation must be constructed, and so needs either zero (which is technically one, the parameterless constructor) or one constructors.
                /// </reason>
                /// <description>Must have zero or one constructors.</description>
                public static readonly object MustHaveZeroOrOneConstructors;

                /// <reason>
                /// A service implementation will be built by an automatic construction container.
                /// We make the assumption that the container will not search through multiple constructors.
                /// </reason>
                /// <remarks>
                /// The Microsoft service provider will in fact search through all of a service implementations constructors. However, it simplifies things to choose a single constructor.
                /// </remarks>
                /// <description>If many constructors, at least one must be marked with the <see cref="T0064.ServiceImplementationConstructorMarkerAttribute"/> specifying the type of a recognized service definition.</description>
                public static readonly object OneOfManyConstructorsMarkedWithServiceImplementationConstructorAttribute;

                /// <reason>
                /// A service implementation is definitively indicated by having the service implementation marker attribute.
                /// This attribute is the only way that a type in compiled code, in an assembly, can indicate itself as a service implementation.
                /// </reason>
                /// <description>Must have the <see cref="T0064.ServiceImplementationMarkerAttribute"/>.</description>
                public static readonly object MustHaveMarkerAttribute;

                /// <reason>
                /// A service implementation is indicated by having the service implementation marker interface as a base type.
                /// Having this interface as a base type allows creating extension methods that work on all service implementation instances.
                /// </reason>
                /// <description>Must have the <see cref="T0064.IServiceImplementation"/>.</description>
                public static readonly object MustHaveMarkerInterface;

                /// <reason>
                /// A service implementation should implement a service definition. However, some don't.
                /// The <see cref="T0064.INoServiceDefinition"/> interface is provided to allow service implementations to communicate that they do not implement a service definition, and that's ok.
                /// </reason>
                /// <description>Has the <see cref="T0064.INoServiceDefinition"/> interface among its base types list.</description>
                public static readonly object HasNoServiceDefinitionInterface;
            }
        }
    }
}
