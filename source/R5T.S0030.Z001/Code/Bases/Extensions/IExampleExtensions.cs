using System;

using R5T.B0005;


namespace R5T.S0030.Z001
{
    public static class IExampleExtensions
    {
        public static string[] NamespaceNames(this IExample _)
        {
            var output = new[]
            {
                "System",
                "System.Threading.Tasks",
                "R5T.Lombardy",
                "R5T.T0020",
                "R5T.S0029.Library",
                "LocalData",
                "Microsoft.Extensions.Hosting",
                "R5T.D0088",
                "R5T.D0090",
                // Yes, repeats.
                "System",
                "System.Threading.Tasks",
                "Microsoft.Extensions.Configuration",
                "Microsoft.Extensions.DependencyInjection",
                "R5T.Magyar",
                "R5T.Ostrogothia.Rivet",
                "R5T.A0003",
                "R5T.D0048.Default",
                "R5T.D0077.A002",
                "R5T.D0078.A002",
                "R5T.D0079.A002",
                "R5T.D0081.I001",
                "R5T.D0083.I001",
                "R5T.D0088.I0002",
                "R5T.D0101.I0001",
                "R5T.D0101.I001",
                "R5T.D0108.I0001",
                "R5T.D0108.I001",
                "R5T.D0109.I0001",
                "R5T.D0109.I001",
                "R5T.T0063",
                "R5T.S0029.Library",
                // Local namespace name.
                "TestProject.Library"
            };

            return output;
        }

        public static (string DestinationName, string SourceExpression)[] NameAliases(this IExample _)
        {
            var output = new[]
            {
                ("IProvidedServiceActionAggregation", "R5T.D0088.I0002.IProvidedServiceActionAggregation"),
                ("ServicesPlatformRequiredServiceActionAggregation", "R5T.A0003.RequiredServiceActionAggregation"),
                ("IRequiredServiceActionAggregation", "R5T.D0088.I0002.IRequiredServiceActionAggregation"),
                ("Documentation", "TestProject.Documentation"),
                ("Instances", "TestProject.Code.Instances"),
                ("Glossary", "TestProject.Glossary")
            };

            return output;
        }
    }
}