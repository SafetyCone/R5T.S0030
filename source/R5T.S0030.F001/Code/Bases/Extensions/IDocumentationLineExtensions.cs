using System;

using R5T.T0036;


namespace R5T.S0030.F001
{
    public static class IDocumentationLineExtensions
    {
        public static string ForServiceAddXMethod(this IDocumentationLine _,
            string serviceDefinitionTypeName,
            string serviceImplementationTypeName)
        {
            var output = $"/// Adds the <see cref=\"{serviceImplementationTypeName}\"/> implementation of <see cref=\"{serviceDefinitionTypeName}\"/> as a <see cref=\"ServiceLifetime.Singleton\"/>.";
            return output;
        }
    }
}
