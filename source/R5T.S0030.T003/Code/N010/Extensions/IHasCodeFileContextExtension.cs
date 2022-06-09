using System;
using System.Threading.Tasks;

using R5T.S0030.T003.N010;


namespace System
{
    public static class IHasCodeFileContextExtension
    {
        public static Task Modify(this IHasCodeFileContext hasContext,
            Func<ICodeFileContext, Task> codeFileModifierAction)
        {
            return codeFileModifierAction(hasContext.CodeFileContext_N010);
        }
    }
}
