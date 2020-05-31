
using ClassLibrary;
using ProcessEmployees;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using Microsoft.VisualBasic.FileIO;
using System.IO;

namespace Ex2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
   
    public partial class MainWindow : Window
    {
        private delegate void sendItemsToAnotherWindow(ObservableCollection<MoviePerson> director, ObservableCollection<MoviePerson> actors, ObservableDictionary<KeyPair, Movie> movies);
        private event sendItemsToAnotherWindow sendToAnotherWindow;
        private ObservableCollection<MoviePerson> directors = new ObservableCollection<MoviePerson>();
        private ObservableCollection<MoviePerson> actors = new ObservableCollection<MoviePerson>();
        private ObservableDictionary<KeyPair, Movie> movies = new ObservableDictionary<KeyPair, Movie>();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = movies;
            sortAllLists();
        }

        private void sortAllLists()
        {
            actors.OrderBy(act => act.FirstName).ThenBy(act => act.LastName);
            directors.OrderBy(dir => dir.FirstName).ThenBy(dir => dir.LastName);
            movies.sortDict();
        }

        private void movieClicked(object sender, RoutedEventArgs e)
        {
            int countDirectors = directors.Where(item => item.FirstName != null).Count();
            int countActor = actors.Where(item => item.FirstName != null).Count();
            if(countActor == 0 || countDirectors == 0)
            {
                MessageBox.Show("You need to add actor or director before you add movie");
            }
            else
            {
                Add_movie window = new Add_movie();
                sendToAnotherWindow += window.setLists;
                sendToAnotherWindow(directors, actors, movies);
                window.Show();
            }
            
   
        }


        private void actorClicked(object sender, RoutedEventArgs e)
        {
            Add_actor window = new Add_actor();
            sendToAnotherWindow += window.setLists;
            sendToAnotherWindow(directors, actors, movies);
            window.Show();
        }

        private void directorClicked(object sender, RoutedEventArgs e)
        {
            Add_director window = new Add_director();
            sendToAnotherWindow += window.setLists;
            sendToAnotherWindow(directors, actors, movies);
            window.Show();
        }

        public void writeMovieToScreen(Movie movie)
        {
            listMovie.Items.Add(movie);
        }

        private void byDirectorNameClicked(object sender, RoutedEventArgs e)
        {
            if (movies.Dict.Count == 0)
            {
                MessageBox.Show("No movies entered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Search_by_string sbs = new Search_by_string("director", movies);
        }

        private void byActorNameClicked(object sender, RoutedEventArgs e)
        {
            if (movies.Dict.Count == 0)
            {
                MessageBox.Show("No movies entered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Search_by_string sbs = new Search_by_string("actor", movies);
        }

        private void byYearClicked(object sender, RoutedEventArgs e)
        {
            if (movies.Dict.Count == 0)
            {
                MessageBox.Show("No movies entered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Search_by_string sbs = new Search_by_string("year", movies);
        }

        private void byMovieNameClicked(object sender, RoutedEventArgs e)
        {
            if (movies.Dict.Count == 0)
            {
                MessageBox.Show("No movies entered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Search_by_string sbs = new Search_by_string("name", movies);
        }
        private void byIMDBClicked(object sender, RoutedEventArgs e)
        {
            if (movies.Dict.Count == 0)
            {
                MessageBox.Show("No movies entered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Search_by_score sbs = new Search_by_score("IMDB", movies);
        }

        private void byRTClicked(object sender, RoutedEventArgs e)
        {
            if (movies.Dict.Count == 0)
            {
                MessageBox.Show("No movies entered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Search_by_score sbs = new Search_by_score("Rotten", movies);
        }
        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (movies.Dict.Count == 0)
            {
                MessageBox.Show("No movies entered yet", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (listMovie.SelectedItem == null)
            {
                MessageBox.Show("Must pick movie to delete", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var selDel = listMovie.SelectedItem.ToString();
            var stL = selDel.Split(',');
            string title = stL[1].Trim();
            stL[2] = stL[2].Substring(0, stL[2].Length);
            int year = int.Parse(stL[2].Trim());
            KeyPair keyDel = new KeyPair(title, year);
            MessageBoxResult res = MessageBox.Show($"Are you sure you want to delete '{title}'?", "Confirm pick", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
            {
                return;
            }
            foreach (var mov in movies.Dict)
            {
                if (mov.Key.Equals(keyDel))
                {
                    keyDel = mov.Key;
                }
            }
            movies.Remove(keyDel);
            return;
        }
        public class DataProgram
        {
            private static DataProgram instance = null;
            private static readonly object padlock = new object();
            private ObservableDictionary<KeyPair, Movie> movieList;
            public List<MoviePerson> ActorList { get; set; }
            public List<MoviePerson> DirectorList { get; set; }
            DataProgram()
            {
                ActorList = new List<MoviePerson>();
                DirectorList = new List<MoviePerson>();
                movieList = new ObservableDictionary<KeyPair, Movie>();
            }

            public static DataProgram Instance
            {
                get
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new DataProgram();
                        }
                        return instance;
                    }
                }
            }

        }

        private void listMovie_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string selectedMovie = listMovie.SelectedItem.ToString();
            string [] splitByComma = selectedMovie.Split(",");
            string title = splitByComma[1].Trim();
            string year = splitByComma[2];
            MessageBoxResult res = MessageBox.Show($"Are you sure you want to delete '{title}'?", "Confirm pick", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                movies.Remove(new KeyPair(title, int.Parse(year)));
            }
            
        }
    }

    
}
