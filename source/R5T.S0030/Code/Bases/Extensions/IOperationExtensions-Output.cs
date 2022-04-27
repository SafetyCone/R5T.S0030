using System;
using System.Collections.Generic;
using System.Linq;

using R5T.T0098;

using R5T.S0030.Library;


namespace R5T.S0030
{
    public static partial class IOperationExtensions
    {
        public static string[] DescribeImplementations(this IOperation _,
            IList<ProjectImplementationSuccessSet> implementations)
        {
            var count = implementations
                .SelectMany(x => x.ImplementationSuccesses
                    .Select(y => y.ImplementationSuccesses.Length))
                .Sum();

            var output = EnumerableHelper.From($"Service implementations - Count: {count}\n")
                .Concat(implementations
                    .SelectMany(x => x.ImplementationSuccesses
                        .SelectMany(y => y.ImplementationSuccesses
                            .SelectMany(z => _.DescribeImplementation(z.Result, z.Implementation, y.CodeFilePath)
                                .Append(String.Empty)))))
                .Now();

            return output;
        }

        public static string[] DescribeFailedCandidates(this IOperation _,
            IList<CodeFileCandidateUpgradeFailureSet> failedCandidates)
        {
            var count = failedCandidates
                .Select(x => x.UpgradeResults.Length)
                .Sum();

            var output = EnumerableHelper.From($"Failed service implementation candidates - Count: {count}\n")
                .Concat(failedCandidates
                    .SelectMany(x => x.UpgradeResults
                        .SelectMany(y => _.DescribeResult(
                            y,
                            x.CodeFilePath)
                        .Append(String.Empty))))
                .Now();

            return output;
        }

        public static string[] DescribeImplementations(this IOperation _,
            IEnumerable<(T001.Level01.UpgradeResult UpgradeResult, T001.Level02.ImplementationDescriptor ImplementationDescriptor)> upgradeResultPairs,
            string codeFilePath)
        {
            var lines = upgradeResultPairs.Any()
                ? upgradeResultPairs
                    .SelectMany(
                        xPair => _.DescribeImplementation(
                            xPair.UpgradeResult,
                            xPair.ImplementationDescriptor,
                            codeFilePath)
                            .Append(String.Empty))
                : EnumerableHelper.From($"No service implementations in code file:\n\t{codeFilePath}\n")
                ;

            var output = lines.Now();
            return output;
        }

        public static string[] DescribeFailedCandidateTypes(this IOperation _,
            IList<CodeFileTypeUpgradeFailureSet> failedCandidateTypes)
        {
            var count = failedCandidateTypes
                .Select(x => x.UpgradeResults.Length)
                .Sum();

            var output = EnumerableHelper.From($"Failed candidate types - Count: {count}\n")
                .Concat(failedCandidateTypes
                    .SelectMany(x => _.DescribeFailedCandidateTypes(
                        x.UpgradeResults,
                        x.CodeFilePath)
                        .Append(String.Empty)))
                .Now();

            return output;
        }

        public static string[] DescribeFailedCandidateTypes(this IOperation _,
            IEnumerable<T002.ImplementationCandidates.Level01.TypeUpgradeResult> failedCandidates,
            string codeFilePath)
        {
            var lines = failedCandidates.Any()
                ? failedCandidates.SelectMany(x => EnumerableHelper.From($"{x.NamespacedTypeName}\n\t{codeFilePath}\n")
                    .AppendRange(x.ValidationSuccess.ActionResults
                        .Select(x => $"\t{x}")))
                : EnumerableHelper.From($"No service implementation candidate type failures in code file:\n\t{codeFilePath}")
                ;

            var output = lines.Now();
            return output;
        }
    }
}
