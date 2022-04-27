using System;


namespace R5T.S0030
{
    public class CodeFileCandidateUpgradeFailureSet
    {
        public string CodeFilePath { get; set; }
        public T001.Level01.UpgradeResult[] UpgradeResults { get; set; }
    }
}
