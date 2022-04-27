using System;


namespace R5T.S0030.T001.Level01
{
    public enum DependencySetSelectionMethod
    {
        NoConstructor,
        OnlyConstructor,
        FirstConstructor,
        ExplicitServiceImplementationConstructorMarkerAttribute,
        FirstExplicitServiceImplementationConstructorMarkerAttribute,
    }
}
