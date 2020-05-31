using ClassLibrary;
using ProcessEmployees;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ex2
{
    /// <summary>
    /// Interaction logic for Search_by_string.xaml
    /// </summary>
    public partial class Search_by_string : Window
    {
        private ObservableCollection<string> resultsToShow;
        private string whichSearch;
        private ObservableDictionary<KeyPair, Movie> movies;
        public Search_by_string(string whichSearch, ObservableDictionary<KeyPair, Movie> list)
        {
            InitializeComponent();
            tbValue.Text = $"Search by {whichSearch}";
            resultsToShow = new ObservableCollection<string>();
            lbSearchResults.ItemsSource = resultsToShow;
            movies = list;
            this.whichSearch = whichSearch;
            Show();
        }

        private void btnSearch_click(object sender, RoutedEventArgs e)
        {
            try
            {
                calculateSearchRes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void calculateSearchRes()
        {
            resultsToShow.Clear();
            string value = tbValue.Text.Trim();
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Can't search without input value");
            }
            switch (whichSearch)
            {
                case "name":
                    foreach (var mov in movies.Dict.Values)
                    {
                        if (mov.Title.StartsWith(value))
                        {
                            resultsToShow.Add($"{mov.Title} {mov.Year}");
                        }
                    }
                    break;
                case "director":
                    foreach (var mov in movies.Dict.Values)
                    {
                        string dirName = mov.Director.ToString();
                        if (dirName.StartsWith(value))
                        {
                            resultsToShow.Add($"{mov.Title} {mov.Year}");
                        }
                    }
                    break;
                case "actor":
                    foreach (var mov in movies.Dict.Values)
                    {
                        foreach (var act in mov.Actors)
                        {
                            if (act.StartsWith(value))
                            {
                                resultsToShow.Add($"{mov.Title} {mov.Year}");
                            }
                        }
                    }
                    break;
                case "year":
                    if (!int.TryParse(value, out int val))
                    {
                        throw new ArgumentException("Valid year between 1900-2020");
                    }
                    foreach (var mov in movies.Dict.Values)
                    {
                        if (mov.Year.Equals(val))
                        {
                            resultsToShow.Add($"{mov.Title} {mov.Year}");
                        }
                    }
                    break;

            }
            if (resultsToShow.Count == 0)
            {
                resultsToShow.Add("No match found");
            }
        }
    }
}
