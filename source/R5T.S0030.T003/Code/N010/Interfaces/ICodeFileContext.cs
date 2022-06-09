using System;


namespace R5T.S0030.T003.N010
{
    public interface ICodeFileContext : IHasCodeFileContext
    {
        string CodeFilePath { get; }
    }
}
