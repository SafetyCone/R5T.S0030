using System;
using System.Linq;

using R5T.Magyar.Results;

using R5T.T0098;

using R5T.S0030.T001.Level01;

using Level02 = R5T.S0030.T001.Level02;


namespace R5T.S0030.Library
{
    public static partial class IOperationExtensions
    {
        public static string[] DescribeImplementation(this IOperation _,
            UpgradeResult upgradeResult,
            Level02.ImplementationDescriptor implementationDescriptor,
            string codeFilePath)
        {
            var contentLines = _.DescribeImplementation_Content(
                upgradeResult,
                implementationDescriptor);

            var implementationNamespacedTypeNameDescription = Instances.NamespacedTypeNameOperator.GetDescribedNamespacedTypeName(
                upgradeResult.NamespacedTypeName);

            var lines = EnumerableHelper.From(
                $"{implementationNamespacedTypeNameDescription}\n\t{codeFilePath}\n")
                .AppendRange(contentLines)
                .Now();

            return lines;
        }

        public static string[] DescribeImplementation_Content(this IOperation _,
            UpgradeResult upgradeResult,
            Level02.ImplementationDescriptor implementationDescriptor)
        {
            var validationSuccess = upgradeResult.ValidationSuccess.Succeeded();

            var upgradedDescriptorLines = validationSuccess
                ? Instances.Operation.Describe(implementationDescriptor)
                : EnumerableHelper.Empty<string>();
            ;

            var upgradedResultLines = validationSuccess
                ? Instances.Operation.DescribeSelectionProcessesOnly(upgradeResult)
                : Enumerable.Empty<string>()
                ;

            var validationSuccessLines = upgradeResult.ValidationSuccess.ActionResults
                .OrderByDescending(
                    x => x,
                    ActionResultSeverityLevelComparer.Instance)
                .Select(x => $"\t{x}");

            var lines = Enumerable.Empty<string>()
                .Concat(upgradedDescriptorLines)
                .Concat(upgradedResultLines)
                .Select(x => $"\t{x}")
                .Append(String.Empty)
                .Concat(validationSuccessLines)
                .Now()
                ;

            return lines;
        }

        public static string[] DescribeResult(this IOperation _,
            UpgradeResult upgradeResult,
            string codeFilePath)
        {
            var contentLines = _.DescribeResult(
                upgradeResult);

            var implementationNamespacedTypeNameDescription = Instances.NamespacedTypeNameOperator.GetDescribedNamespacedTypeName(
                upgradeResult.NamespacedTypeName);

            var lines = EnumerableHelper.From(
                $"{implementationNamespacedTypeNameDescription}\n\t{codeFilePath}\n")
                .AppendRange(contentLines)
                .Now();

            return lines;
        }

        public static string[] DescribeResult(this IOperation _,
            UpgradeResult upgradeResult)
        {
            var validationSuccess = upgradeResult.ValidationSuccess.Succeeded();

            var validationSuccessLines = upgradeResult.ValidationSuccess.ActionResults
                .OrderByDescending(
                    x => x,
                    ActionResultSeverityLevelComparer.Instance)
                .Select(x => $"\t{x}");

            var lines = validationSuccessLines
                .Now();

            return lines;
        }
    }
}
