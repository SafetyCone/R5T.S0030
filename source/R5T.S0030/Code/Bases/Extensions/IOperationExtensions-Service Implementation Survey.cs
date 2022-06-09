using System;
using System.Collections.Generic;
using System.Linq;
using System.N0;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using R5T.T0098;

using R5T.S0030.Library;


namespace R5T.S0030
{
    public static partial class IOperationExtensions
    {
        public static ImplementationAddMethodSet[] GetAddMethodSets(this IOperation _,
            ServiceImplementationProjectEvaluationResult projectResult,
            out Failure<string>[] failures)
        {
            var failuresList = new List<Failure<string>>();

            var implementationAddMethodSets = projectResult.ProjectImplementationSuccessSet.ImplementationSuccesses
                .SelectMany(x => x.ImplementationSuccesses
                    .Select(x => x.Implementation))
                .Where(x =>
                {
                    var include = true;

                    // Generic implementations currently cannot be handled.
                    var implementationIsGeneric = Instances.TypeNameOperator.IsGeneric(x.NamespacedTypeName);

                    if (implementationIsGeneric)
                    {
                        failuresList.Add(Failure.Of(x.NamespacedTypeName, "Cannot currently handle generic service implementations."));

                        include = false;
                    }

                    // Generic service definitions currently do not work.
                    var implementsGenericallyTypedDefinition = x.HasServiceDefinition
                        && Instances.TypeNameOperator.IsGeneric(x.ServiceDefinitionNamespacedTypeName);

                    if (implementsGenericallyTypedDefinition)
                    {
                        failuresList.Add(Failure.Of(x.NamespacedTypeName, "Cannot currently handle generic service definitions."));

                        include = false;
                    }

                    var anyGenericDependencies = x.HasServiceDependencies
                        && x.ServiceDependencyNamespacedTypeNames
                            .Where(x => Instances.TypeNameOperator.IsGeneric(x))
                            .Any();

                    if (anyGenericDependencies)
                    {
                        failuresList.Add(Failure.Of(x.NamespacedTypeName, "Cannot currently handle generic service dependencies."));

                        include = false;
                    }

                    return include;
                })
                .Select(xImplementationDescriptor =>
                {
                    var addXMethod = Instances.MethodGenerator.GetAddX(
                        xImplementationDescriptor);

                    var addXActionMethod = Instances.MethodGenerator.GetAddXAction(
                        xImplementationDescriptor);

                    var output = new ImplementationAddMethodSet
                    {
                        AddXActionMethod = addXActionMethod,
                        AddXMethod = addXMethod,
                        ImplementationDescriptor = xImplementationDescriptor,
                    };

                    return output;
                })
                .Now();

            failures = failuresList.ToArray();

            return implementationAddMethodSets;
        }

        public static async Task<ServiceImplementationProjectEvaluationResult> DescribeServiceImplementationsInProject(this IOperation _,
            string projectFilePath,
            IServiceImplementationCodeFilePathsProvider serviceImplementationCodeFilePathsProvider,
            Repositories.IServiceRepository serviceRepository)
        {
            var output = new ServiceImplementationProjectEvaluationResult
            {
                ProjectImplementationSuccessSet = new ProjectImplementationSuccessSet
                {
                    ProjectFilePath = projectFilePath,
                },
            };

            // Get all service definitions.
            var allDefinitions = await serviceRepository.GetAllServiceDefinitions();

            var definitionNamespacedTypeNames = allDefinitions
                .Select(x => x.TypeName)
                .Now();

            var serviceImplementationCandidateCodeFilePaths = await serviceImplementationCodeFilePathsProvider.GetServiceImplementationCodeFilePaths(
                projectFilePath);

            var successes = new List<CodeFileImplementationSuccessSet>();

            foreach (var codeFilePath in serviceImplementationCandidateCodeFilePaths)
            {
                var compilationUnit = await Instances.CompilationUnitOperator_Old.Load(codeFilePath);

                // Get candidate classes.
                var candidateClasses = Instances.Operation.GetCandidateClasses(
                    compilationUnit,
                    out var failedCandidates);

                if (failedCandidates.Any())
                {
                    output.CandidateTypeFailures.Add(new CodeFileTypeUpgradeFailureSet
                    {
                        CodeFilePath = codeFilePath,
                        UpgradeResults = failedCandidates,
                    });
                }

                // Now get implementation types.
                var upgradeResultPairs = new List<(T001.Level01.UpgradeResult UpgradeResult, T001.Level02.ImplementationDescriptor ImplementationDescriptor)>();

                foreach (var classDeclaration in candidateClasses)
                {
                    var implementationDescriptor = Instances.ServiceImplementationOperator.Describe(
                        classDeclaration,
                        compilationUnit,
                        definitionNamespacedTypeNames);

                    var upgradeResult = Instances.Operation.Upgrade(
                        implementationDescriptor,
                        out var upgradedImplementationDescriptor);

                    upgradeResultPairs.Add((upgradeResult, upgradedImplementationDescriptor));
                }

                var failures = new List<T001.Level01.UpgradeResult>();
                var implementationSuccesses = new List<ImplementationSuccess>();

                foreach (var (upgradeResult, implementationDescriptor) in upgradeResultPairs)
                {
                    var succeeded = upgradeResult.ValidationSuccess.Succeeded();
                    if (succeeded)
                    {
                        implementationSuccesses.Add(new ImplementationSuccess
                        {
                            Implementation = implementationDescriptor,
                            Result = upgradeResult,
                        });
                    }
                    else
                    {
                        failures.Add(upgradeResult);
                    }
                }

                if (implementationSuccesses.Any())
                {
                    successes.Add(new CodeFileImplementationSuccessSet
                    {
                        CodeFilePath = codeFilePath,
                        ImplementationSuccesses = implementationSuccesses.ToArray(),
                    });
                }

                if (failures.Any())
                {
                    output.UpgradeFailures.Add(new CodeFileCandidateUpgradeFailureSet
                    {
                        CodeFilePath = codeFilePath,
                        UpgradeResults = failures.ToArray(),
                    });
                }
            }

            if (successes.Any())
            {
                output.ProjectImplementationSuccessSet.ImplementationSuccesses = successes.ToArray();
            }

            return output;
        }
    }
}
