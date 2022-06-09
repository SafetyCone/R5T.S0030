using System;


namespace R5T.S0030.T003.N010
{
    public class CodeFileContext : ICodeFileContext
    {
        public string CodeFilePath { get; set; }

        public ICodeFileContext CodeFileContext_N010 => this;
    }
}
