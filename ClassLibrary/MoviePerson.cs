using System;
using System.Text.RegularExpressions;
namespace ClassLibrary
{
    public enum Gender
    {
        Male,
        Female
    }
    public class MoviePerson
    {

        private static string namePat = @"^[A-Z][a-zA-Z]*(\s+[a-zA-Z]*)*$";

        private string firstName;
        private string lastName;
        private Gender Gender { get; set; }
        private DateTime DateT { get; set; }
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                string regex = @"[a-zA-Z]+[ \t][a-zA-Z]+$";
                if (!Regex.Match(value, regex).Success)
                {
                    throw new ArgumentException("Non valid name of director");
                }
                else
                    firstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return firstName;
            }
            set
            {
                string regex = @"[a-zA-Z]+[ \t][a-zA-Z]+$";
                if (!Regex.Match(value, regex).Success)
                {
                    throw new ArgumentException("Non valid name of director");
                }
                else
                    firstName = value;
            }
        }


        public MoviePerson(string fn, string ln, DateTime dT,Gender g)
        {
            firstName = fn;
            lastName = ln;
            DateT = dT;
            Gender = g;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public override bool Equals(object obj)
        {
            MoviePerson other = obj as MoviePerson;
            if (other == null)
            {
                throw new ArgumentException("Argument of Equals must be of type MoviePerson");
            }
            return FirstName.Equals(other.FirstName) &&
                LastName.Equals(other.LastName);
        }

        public override int GetHashCode()
        {
            return 1;
        }


    }
}
