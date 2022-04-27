using System;


namespace R5T.S0030.T001.Level01
{
    /// <summary>
    /// Specifies details relevant to validating a service implementation constructor.
    /// This class does not make conclusions about whether the details constitute a failure, warning, or success. Just describes them.
    /// </summary>
    public class ConstructorValidationResult
    {
        public string MethodName { get; set; }

        public ParameterValidationResult[] ParameterResults { get; set; }
    }
}
