using System;


namespace R5T.S0030.T003.N013
{
    public interface IInterfaceContext :
        IHasInterfaceAnnotation,
        IHasInterfaceContext,
        N001.IHasCompilationUnitContext,
        N002.IHasNamespaceContext,
        N012.IHasInterfaceContext
    {
    }
}
