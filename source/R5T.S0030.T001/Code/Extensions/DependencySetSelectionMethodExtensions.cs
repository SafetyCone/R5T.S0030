using System;

using R5T.Magyar;

using R5T.T0064;

using R5T.S0030.T001.Level01;


namespace System
{
    public static class DependencySetSelectionMethodExtensions
    {
        public static string ToString_Description(this DependencySetSelectionMethod dependencySetSelectionMethod)
        {
            return dependencySetSelectionMethod switch
            {
                DependencySetSelectionMethod.ExplicitServiceImplementationConstructorMarkerAttribute =>
                    $"Constructor explicitly marked with {nameof(ServiceImplementationConstructorMarkerAttribute)} attribute.",
                DependencySetSelectionMethod.FirstConstructor =>
                    "First constructor used.",
                DependencySetSelectionMethod.FirstExplicitServiceImplementationConstructorMarkerAttribute =>
                    $"First constructor explicitly marked with {nameof(ServiceImplementationConstructorMarkerAttribute)} attribute used.",
                DependencySetSelectionMethod.NoConstructor =>
                    "No constructor, so no service dependencies.",
                DependencySetSelectionMethod.OnlyConstructor =>
                    "Only constructor used.",
                _ => throw EnumerationHelper.SwitchDefaultCaseException(dependencySetSelectionMethod),
            };
        }
    }
}
