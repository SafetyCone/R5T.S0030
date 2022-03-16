using System;
using System.Linq;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0030
{
    [ServiceImplementationMarker]
    public class ServiceImplementationTypeIdentifier : IServiceImplementationTypeIdentifier, IServiceImplementation
    {
        public async Task<ITypeNamedCodeFilePathed[]> GetServiceImplementationTypes(
            string serviceDefinitionCodeFilePath)
        {
            var output = await Instances.Operation.GetTypeNamedCodeFilePatheds(
                serviceDefinitionCodeFilePath,
                compilationUnit =>
                {
                    var classes = compilationUnit.GetClasses();

                    var serviceImplementationClasses = classes
                        .Where(Instances.ClassOperator.IsServiceImplementation)
                        .Now();

                    var serviceDefinitionInterfaceTypeNames = serviceImplementationClasses.GetNamespacedTypeParameterizedWithConstraintsTypeNames().Now();

                    return Task.FromResult(serviceDefinitionInterfaceTypeNames);
                });

            return output;
        }
    }
}
