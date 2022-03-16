using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace R5T.S0030
{
    /// <summary>
    /// The file context is fundamentally asynchronous since it represents the remote data store.
    /// </summary>
    public abstract class FileContext
    {
        /// <summary>
        /// Perform asychronous operations to get JSON file paths for file sets, then construct the file context's file sets.
        /// </summary>
        protected abstract Task Configure();
        /// <summary>
        /// Gets all file sets so that they can be saved.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<FileSet> GetAllFileSets();

        protected async Task Save()
        {
            var fileSets = this.GetAllFileSets();
            foreach (var fileSet in fileSets)
            {
                await fileSet.Save();
            }
        }
    }
}
