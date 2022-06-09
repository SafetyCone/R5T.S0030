using System;
using System.Linq;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0030
{
    /// <summary>
    /// Finds all classes in the compilation unit.
    /// </summary>
    [ServiceImplementationMarker]
    public class ServiceImplementationCandidateIdentifier : IServiceImplementationCandidateIdentifier, IServiceImplementation
    {
        public async Task<ServiceImplementationCandidate[]> GetCandidateServiceImplementations(string codeFilePath)
        {
            var compilationUnit = await Instances.CompilationUnitOperator_Old.Load(codeFilePath);

            var classes = compilationUnit.GetClasses();

            var output = classes
                .Select(xClass => new ServiceImplementationCandidate
                {
                    Class = xClass,
                    CodeFilePath = codeFilePath,
                    CompilationUnit = compilationUnit,
                })
                .Now();

            return output;
        }
    }
}
