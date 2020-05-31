using ClassLibrary;
using ProcessEmployees;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Ex2.MainWindow;

namespace Ex2
{
    /// <summary>
    /// Interaction logic for Add_movie.xaml
    /// </summary>
    public partial class Add_movie : Window
    {
        #region proprety
        private MainWindow mainWindow;
        private ObservableDictionary<KeyPair, Movie> movies;
        List<string> actorsChecked = new List<string>();
        #endregion
        public Add_movie()
        {
            InitializeComponent();
            Show();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            string title = tbTitle.Text.ToString();
            string imdbScore = tbIMDBScore.Text.ToString();
            string year = tbYear.Text.ToString();
            string rotTomScore = tbRotTomScore.Text.ToString();
            string director = directorList.SelectedItem.ToString();
            Movie movie = new Movie(title,int.Parse(year), director, float.Parse(imdbScore), actorsChecked, int.Parse(rotTomScore));
            movies.Add(new KeyPair(movie.Title,movie.Year),movie);
        }

        private List<string> getActors()
        {
            List<string> actors = new List<string>();
            var lst = directorList.SelectedItems.Cast<MoviePerson>();
            foreach (var item in lst)
            {
                actors.Add(item.ToString());
            }
            return actors;
        }

        internal void setLists(ObservableCollection<MoviePerson> director, ObservableCollection<MoviePerson> actors, ObservableDictionary<KeyPair, Movie> m)
        {
             directorList.ItemsSource= director;
             actorsList.ItemsSource = actors;
             movies = m;
        }

        private void addDirector_Click(object sender, RoutedEventArgs e)
        {
            if (directorList.SelectedItems.Count == 0)
            {
                //To do meesgae box
            }
            else
                tbDirector.Text = directorList.SelectedItem.ToString();
        }

        private void addActor_Click(object sender, RoutedEventArgs e)
        {
            if(actorsList.SelectedItems.Count == 0)
            {
                //To do meesgae box
            }
            else
            {
                string a = actorsList.SelectedItem.ToString();
                actorsChecked.Add(a);
            }
                 
        }
    }
}
