using System;
using System.Collections.Generic;
using System.Text;
using DoenaSoft.DVDProfiler.DVDProfilerXML;

namespace DoenaSoft.DVDProfiler.FindNameDuplicates
{
    internal class Person
    {
        protected String FullName;

        private String ComparisonName;

        protected Person()
        {
        }

        internal Person(IPerson xmlPerson)
        {
            StringBuilder sb;

            sb = new StringBuilder();
            if(String.IsNullOrEmpty(xmlPerson.FirstName) == false)
            {
                sb.Append(xmlPerson.FirstName);
                sb.Append(" ");
            }
            if(String.IsNullOrEmpty(xmlPerson.MiddleName) == false)
            {
                sb.Append(xmlPerson.MiddleName);
                sb.Append(" ");
            }
            if(String.IsNullOrEmpty(xmlPerson.LastName) == false)
            {
                sb.Append(xmlPerson.LastName);
            }
            this.FullName = sb.ToString().Trim();
            this.CreateComparisonName();
        }

        protected void CreateComparisonName()
        {
            StringBuilder sb;
            Char[] chars;

            chars = this.FullName.ToLower().ToCharArray();
            sb = new StringBuilder();
            for(Int32 i = 0; i < chars.Length; i++)
            {
                if(Char.IsLetter(chars[i]))
                {
                    sb.Append(chars[i]);
                }
            }
            this.ComparisonName = sb.ToString();
        }

        public sealed override String ToString()
        {
            return (this.FullName);
        }

        public override Int32 GetHashCode()
        {
            return (this.ComparisonName.GetHashCode());
        }

        public override Boolean Equals(Object obj)
        {
            Person other;

            other = obj as Person;
            if(other == null)
            {
                return (false);
            }
            else
            {
                return (this.ComparisonName == other.ComparisonName);
            }
        }
    }

    internal class CompletePerson : Person
    {
        internal IPerson XmlPerson;
        private Int32 HashCode;

        internal CompletePerson(IPerson xmlPerson)
        {
            StringBuilder sb;
            Int32 firstNameHash;
            Int32 middleNameHash;
            Int32 lastNameHash;
            Int32 birthYearHash;

            this.XmlPerson = xmlPerson;
            sb = new StringBuilder();
            if(String.IsNullOrEmpty(xmlPerson.FirstName) == false)
            {
                sb.Append("<");
                sb.Append(xmlPerson.FirstName);
                sb.Append("> ");
            }
            if(String.IsNullOrEmpty(xmlPerson.MiddleName) == false)
            {
                sb.Append("{");
                sb.Append(xmlPerson.MiddleName);
                sb.Append("} ");
            }
            if(String.IsNullOrEmpty(xmlPerson.LastName) == false)
            {
                sb.Append("[");
                sb.Append(xmlPerson.LastName);
                sb.Append("] ");
            }
            if(xmlPerson.BirthYear != 0)
            {
                sb.Append("(");
                sb.Append(xmlPerson.BirthYear);
                sb.Append(")");
            }
            //if (String.IsNullOrEmpty(xmlPerson.CreditedAs) == false)
            //{
            //    sb.Append(" as >");
            //    sb.Append(xmlPerson.CreditedAs);
            //    sb.Append("<");
            //}
            this.FullName = sb.ToString().Trim();
            //this.ComparisonName = this.FullName;
            if(String.IsNullOrEmpty(this.XmlPerson.FirstName) == false)
            {
                firstNameHash = this.XmlPerson.FirstName.GetHashCode();
            }
            else
            {
                firstNameHash = 0;
            }
            if(String.IsNullOrEmpty(this.XmlPerson.MiddleName) == false)
            {
                middleNameHash = this.XmlPerson.MiddleName.GetHashCode();
            }
            else
            {
                middleNameHash = 0;
            }
            if(String.IsNullOrEmpty(this.XmlPerson.LastName) == false)
            {
                lastNameHash = this.XmlPerson.LastName.GetHashCode();
            }
            else
            {
                lastNameHash = 0;
            }
            birthYearHash = this.XmlPerson.BirthYear.GetHashCode();
            this.HashCode = firstNameHash / 4 + middleNameHash / 4 + lastNameHash / 4 + birthYearHash / 4;
        }

        public override Int32 GetHashCode()
        {
            return (this.HashCode);
        }

        public override bool Equals(Object obj)
        {
            CompletePerson other;

            other = obj as CompletePerson;
            if(other == null)
            {
                return (false);
            }
            else
            {
                return ((this.XmlPerson.FirstName == other.XmlPerson.FirstName)
                    && (this.XmlPerson.MiddleName == other.XmlPerson.MiddleName)
                    && (this.XmlPerson.LastName == other.XmlPerson.LastName)
                    && (this.XmlPerson.BirthYear == other.XmlPerson.BirthYear));
            }
        }
    }

    internal class CreditedAsPerson : Person
    {
        internal CreditedAsPerson(IPerson xmlPerson)
        {
            this.FullName = xmlPerson.CreditedAs;
            this.CreateComparisonName();
        }

    }

    //internal class CompleteCreditedAsPerson : CompletePerson
    //{
    //    internal CompleteCreditedAsPerson(IPerson xmlPerson)
    //        : base(xmlPerson)
    //    {
    //        StringBuilder sb;

    //        sb = new StringBuilder();
    //        sb.Append(this.FullName);
    //        sb.Append(" as >");
    //        sb.Append(xmlPerson.CreditedAs);
    //        sb.Append("<");
    //        this.FullName = sb.ToString().Trim();
    //        this.ComparisonName = this.FullName;
    //    }
    //}

    internal class NoMiddleNamePerson : Person
    {
        internal NoMiddleNamePerson(IPerson xmlPerson)
        {
            StringBuilder sb;

            sb = new StringBuilder();
            if(String.IsNullOrEmpty(xmlPerson.FirstName) == false)
            {
                sb.Append(xmlPerson.FirstName);
                sb.Append(" ");
            }
            if(String.IsNullOrEmpty(xmlPerson.LastName) == false)
            {
                sb.Append(xmlPerson.LastName);
            }
            this.FullName = sb.ToString().Trim();
            this.CreateComparisonName();
        }
    }

    internal class InvertedNamePerson : Person
    {
        internal InvertedNamePerson(IPerson xmlPerson)
        {
            StringBuilder sb;

            sb = new StringBuilder();
            if(String.IsNullOrEmpty(xmlPerson.MiddleName) == false)
            {
                sb.Append(xmlPerson.MiddleName);
                sb.Append(" ");
            }
            if(String.IsNullOrEmpty(xmlPerson.LastName) == false)
            {
                sb.Append(xmlPerson.LastName);
                sb.Append(" ");
            }
            if(String.IsNullOrEmpty(xmlPerson.FirstName) == false)
            {
                sb.Append(xmlPerson.FirstName);
            }
            this.FullName = sb.ToString().Trim();
            this.CreateComparisonName();
        }
    }
}