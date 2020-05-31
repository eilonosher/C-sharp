using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class KeyPair : IComparable
    {
        private string movieName { get; set; }
        private int movieYear { get; set; }
        public KeyPair(string name , int year)
        {
            movieName = name;
            movieYear = year;
        }
        public int CompareTo(object obj)
        {
            if (!(obj is KeyPair m))
            {
                throw new ArgumentException("compareTo must get KeyPair");
            }
            int ret = this.movieYear.CompareTo(m.movieYear);
            if (ret != 0) return ret;
            return this.movieName.CompareTo(m.movieName);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is KeyPair mp))
            {
                throw new Exception("Equals must get mtKeyPair argument");
            }
            return this.movieName.Equals(mp.movieName) && movieYear.Equals(mp.movieYear);
        }

        public override int GetHashCode()
        {
            return 1;
        }

    }
}
