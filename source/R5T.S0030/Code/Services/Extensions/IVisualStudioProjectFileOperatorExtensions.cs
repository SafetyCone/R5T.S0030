using System;
using System.Threading.Tasks;

using R5T.D0079;
using R5T.D0083;


namespace System
{
    public static class IVisualStudioProjectFileOperatorExtensions
    {
        /// <summary>
        /// Ensures that the reference project is a project reference, either directly or recursively, and if not, adds it as a direct project reference.
        /// </summary>
        public static async Task EnsureHasProjectReferenceRecursivelyElseAddDirect(this IVisualStudioProjectFileOperator visualStudioProjectFileOperator,
            string projectFilePath,
            string referenceProjectFilePath,
            IVisualStudioProjectFileReferencesProvider visualStudioProjectFileReferencesProvider)
        {
            var hasProjectReference = await visualStudioProjectFileReferencesProvider.HasRecursiveProjectReference(
                projectFilePath,
                referenceProjectFilePath);

            if (!hasProjectReference)
            {
                await visualStudioProjectFileOperator.AddProjectReference(
                    projectFilePath,
                    referenceProjectFilePath);
            }
        }
    }
}
