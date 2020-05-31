using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ClassLibrary;
namespace ClassLibrary
{
    public class Movie
    {
        #region varible
        private string title;
        private int year;
        private string director;
        private float iMDBScore;
        private List<string> actors;
        private int rotTomScore;
        private static int H_YEAR = 2020;
        private static int L_YEAR = 1900;
        public decimal imdbScore;

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                string regex = @"[a-zA-Z0-9.!?\\-]+$";
                if (!Regex.Match(value, regex).Success)
                {
                    throw new ArgumentException("Non valid title");
                }
                else
                    title = value;

            }
        }
        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                if (value > H_YEAR || value < L_YEAR)
                {
                    throw new ArgumentException("Non valid year");
                }
                else
                    year = value;
            }
        }
        public string Director
        {
            get
            {
                return Director.ToString();
            }
            set
            {
                    director = value;
            }
        }
        public float IMDBScore
        {
            get
            {
                return iMDBScore;
            }
            set
            {
                String floatToString = value.ToString();
                string regex = @"^[0-9](\.\d)?|10$";
                if (!Regex.Match(floatToString, regex).Success || value > 10 || value < 0)
                {
                    throw new ArgumentException("Non vsalid IMDB Score");
                }
                else
                    iMDBScore = value;
            }
        }

        public int RotTomScore
        {
            get
            {
                return rotTomScore;
            }
            set
            {
                if (value > 100 || value < 0)
                {
                    throw new ArgumentException("Non valid Rotten Tomatoes Score");

                }
                else
                    rotTomScore = value;
            }
        }
        public List<string> Actors
        {
            get
            {
                return actors;
            }
            set
            {
                value.ForEach(actor =>
                {
                    string regex = @"[a-zA-Z]+[ \t][a-zA-Z]+$";
                    if (!Regex.Match(actor, regex).Success)
                    {
                        throw new ArgumentException("Non valid actors");
                    }
                });
                actors = value;
            }
        }
        #endregion
        public Movie(string _title, int _year, string _director, float _iMDBScore, List<string> _actors, int _rotTomScore)
        {
            Title = _title;
            Year = _year;
            Director = _director;
            IMDBScore = _iMDBScore;
            Actors = _actors;
            RotTomScore = _rotTomScore;
        }

        public override bool Equals(object obj)
        {
            return title.Equals(((Movie)obj).Title) && year == (((Movie)obj).Year);
        }

        public override string ToString()
        {
            return Title + "," + Year + ", Actors: " + String.Join(", ", actors.ToArray()) + ", Directors: " + director + ";";
        }

    }
}
 