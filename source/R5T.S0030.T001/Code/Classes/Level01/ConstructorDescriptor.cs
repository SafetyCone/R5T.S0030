using System;


namespace R5T.S0030.T001.Level01
{
    public class ConstructorDescriptor
    {
        public string MethodName { get; set; }
        public bool HasServiceImplementationConstructorAttribute { get; set; }
        public ParameterDescriptor[] ParameterDescriptors { get; set; }
    }
}
