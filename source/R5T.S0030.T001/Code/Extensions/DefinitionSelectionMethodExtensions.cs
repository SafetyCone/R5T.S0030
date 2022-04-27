using System;

using R5T.Magyar;

using R5T.T0064;

using R5T.S0030.T001.Level01;


namespace System
{
    public static class DefinitionSelectionMethodExtensions
    {
        public static string ToString_Description(this DefinitionSelectionMethod definitionSelectionMethod)
        {
            return definitionSelectionMethod switch
            {
                DefinitionSelectionMethod.ExplicitNoServiceDefinitionMarkerAttribute =>
                    $"Explicitly marked with {nameof(INoServiceDefinitionMarker)} (or old {nameof(INoServiceDefinition)}) interface.",
                DefinitionSelectionMethod.FirstServiceDefinitionBaseType =>
                    "First (of multiple) service definition base types.",
                DefinitionSelectionMethod.OnlyServiceDefinitionBaseType =>
                    "Single service definition base type.",
                DefinitionSelectionMethod.SpecifyServiceDefinitionAttribute =>
                    $"Specified using {nameof(SpecifyServiceDefinitionAttribute)} (or old {nameof(ImplementsServiceDefinitionAttribute)}) attribute.",
                _ => throw EnumerationHelper.SwitchDefaultCaseException(definitionSelectionMethod),
            };
        }
    }
}
