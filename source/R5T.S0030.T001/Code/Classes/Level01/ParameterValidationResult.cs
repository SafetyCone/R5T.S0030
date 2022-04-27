using System;


namespace R5T.S0030.T001.Level01
{
    /// <summary>
    /// Specifies details relevant to validating a service implementation constructor parameter.
    /// This class does not make conclusions about whether the details constitute a failure, warning, or success. Just describes them.
    /// </summary>
    public class ParameterValidationResult
    {
        public string ParameterName { get; set; }

        /// <summary>
        /// True if a parameter's type is not recognized as a service component, and the parameter does not have the <see cref="T0064.NotServiceComponentAttribute"/>.
        /// It is acceptable for a service implementation to depend on types that are not service components. They shouldn't, but for implementations like constructor-based providers, they need to, and the non-service component parameter value is provided manually.
        /// </summary>
        public bool NotRecognizedAndMissingExplicitNotServiceComponentAttribute { get; set; }
    }
}
