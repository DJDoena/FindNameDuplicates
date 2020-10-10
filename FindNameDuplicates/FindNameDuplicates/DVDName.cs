using System;
using System.Diagnostics;
using System.Text;
using DoenaSoft.DVDProfiler.DVDProfilerXML.Version400;

namespace DoenaSoft.DVDProfiler.FindNameDuplicates
{
    internal class DVDName
    {
        private String FullName;

        private String m_Id;

        internal String Id
        {
            [DebuggerStepThrough()]
            get
            {
                return (this.m_Id);
            }
        }

        internal DVDName(DVDName oldDVDName, String addendum)
        {
            this.m_Id = oldDVDName.Id;
            this.FullName = oldDVDName.FullName + " " + addendum;
        }

        internal DVDName(DVD dvd, Boolean isCast)
        {
            StringBuilder sb;

            this.m_Id = dvd.ID;
            sb = new StringBuilder();
            //if (isCast)
            //{
            //    sb.Append("(Cast) ");
            //}
            //else
            //{
            //    sb.Append("(Crew) ");
            //}
            sb.Append(dvd.Title);
            if (String.IsNullOrEmpty(dvd.Edition) == false)
            {
                sb.Append(": ");
                sb.Append(dvd.Edition);
            }
            this.FullName = sb.ToString().Trim();
        }

        public override String ToString()
        {
            return (this.FullName);
        }

        public override Int32 GetHashCode()
        {
            return (this.Id.GetHashCode());
        }

        public override Boolean Equals(Object obj)
        {
            DVDName other;

            other = obj as DVDName;
            if (other == null)
            {
                return (false);
            }
            else
            {
                return (this.Id == other.Id);
            }
        }
    }
}
