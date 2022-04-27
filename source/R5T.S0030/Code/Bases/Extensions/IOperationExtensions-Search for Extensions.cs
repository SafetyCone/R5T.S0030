using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0098;

using R5T.S0030.F003;


namespace R5T.S0030
{
    public static partial class IOperationExtensions
    {
        public static async Task<ProjectResult> SearchForAddXActionMethods(this IOperation _,
            string projectFilePath)
        {
            // Really, searching for AddX() methods is searching for IServiceCollection extensions.
            var output = await Instances.Operation.SearchForExtensions(
                projectFilePath,
                Instances.ProjectPathsOperator.GetExtensionsDirectoryFilePaths,
                Instances.NamespacedTypeName.R5T_T0062_IServiceAction_Base());

            return output;
        }

        public static async Task<ProjectResult> SearchForAddXMethods(this IOperation _,
            string projectFilePath)
        {
            // Really, searching for AddX() methods is searching for IServiceCollection extensions.
            var output = await Instances.Operation.SearchForExtensions(
                projectFilePath,
                Instances.ProjectPathsOperator.GetExtensionsDirectoryFilePaths,
                Instances.NamespacedTypeName.IServiceCollection());

            return output;
        }
    }
}
