using System;


namespace R5T.S0030
{
    public class CodeFileTypeUpgradeFailureSet
    {
        public string CodeFilePath { get; set; }
        public T002.ImplementationCandidates.Level01.TypeUpgradeResult[] UpgradeResults { get; set; }
    }
}
