using System;
using System.Threading.Tasks;


namespace R5T.S0030
{
    public class FileContextBasedRepositoryBase<TFileContext>
        where TFileContext : FileContext
    {
        protected IFileContextProvider<TFileContext> FileContextProvider { get; }


        public FileContextBasedRepositoryBase(IFileContextProvider<TFileContext> fileContextProvider)
        {
            this.FileContextProvider = fileContextProvider;
        }

        protected async Task ExecuteInContext(Func<TFileContext, Task> action)
        {
            var fileContext = await this.FileContextProvider.GetFileContext();

            await action(fileContext);
        }

        protected async Task<TOutput> ExecuteInContext<TOutput>(Func<TFileContext, Task<TOutput>> function)
        {
            var fileContext = await this.FileContextProvider.GetFileContext();

            var output = await function(fileContext);
            return output;
        }
    }
}
