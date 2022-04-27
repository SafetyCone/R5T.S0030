using System;
using System.Collections.Generic;


namespace R5T.S0030
{
    public class ServiceImplementationProjectEvaluationResult
    {
        public List<CodeFileTypeUpgradeFailureSet> CandidateTypeFailures { get; } = new List<CodeFileTypeUpgradeFailureSet>();
        public List<CodeFileCandidateUpgradeFailureSet> UpgradeFailures { get; } = new List<CodeFileCandidateUpgradeFailureSet>();

        public ProjectImplementationSuccessSet ProjectImplementationSuccessSet { get; set; }

        public string ProjectFilePath => this.ProjectImplementationSuccessSet.ProjectFilePath;
    }
}
