using System;

using R5T.Lombardy;

using R5T.D0079;


namespace R5T.S0030.T003.N009
{
    public class ProjectContext : IProjectContext
    {
        public IStringlyTypedPathOperator StringlyTypedPathOperator { get; set; }
        public IVisualStudioProjectFileOperator VisualStudioProjectFileOperator { get; set; }

        public string ProjectFilePath { get; set; }

        public IProjectContext ProjectContext_N009 => this;
    }
}
