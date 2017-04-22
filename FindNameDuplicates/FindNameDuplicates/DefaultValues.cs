using System;
using System.Runtime.InteropServices;

namespace DoenaSoft.DVDProfiler.FindNameDuplicates
{
    [ComVisible(false)]
    [Serializable()]
    public class DefaultValues
    {
        public Boolean CheckCreditedAs = true;

        public Boolean CheckMissingMiddleName = false;

        public Boolean ReloadAfterSavingProfile = true;

        public String CurrentVersion;
    }
}