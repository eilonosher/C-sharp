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
    /// Interaction logic for Search_by_score.xaml
    /// </summary>
    public partial class Search_by_score : Window
    {
        private ObservableCollection<string> resultsToShow;
        private string whichSearch;
        private ObservableDictionary<KeyPair, Movie> movies;
        public Search_by_score(string whichSearch, ObservableDictionary<KeyPair, Movie> list)
        {
            InitializeComponent();
            resultsToShow = new ObservableCollection<string>();
            SearchResults.ItemsSource = resultsToShow;
            movies = list;
            this.whichSearch = whichSearch;
            Show();
        }

            private void BtnGT_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    calculateSearchRes("GT", whichSearch, tbValue.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void btnLT_Click(object sender, RoutedEventArgs e)
            {
            try
            {
                calculateSearchRes("LT", whichSearch, tbValue.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            }

            private void btnGET_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    calculateSearchRes("GET", whichSearch, tbValue.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void btnLET_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    calculateSearchRes("LET", whichSearch, tbValue.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void calculateSearchRes(string op, string which, string input)
            {
                resultsToShow.Clear();
                switch (whichSearch)
                {
                    case "Rotten":
                        if (!int.TryParse(input, out int intVal))
                        {
                            throw new ArgumentException("Valid Rotten Tomatoes search value is natural number between 0-100");
                        }
                        if (intVal > 100 || intVal < 0)
                        {
                            throw new ArgumentException("Valid Rotten Tomatoes search value is natural number between 0-100");
                        }
                        switch (op)
                        {
                            case "LT":
                                foreach (var mov in movies.Dict.Values)
                                    if (mov.RotTomScore < intVal)
                                    {
                                        resultsToShow.Add($"{mov.Title}, Rotten score: {mov.RotTomScore}");
                                    }
                                break;
                            case "GT":
                                foreach (var mov in movies.Dict.Values)
                                    if (mov.RotTomScore > intVal)
                                    {
                                        resultsToShow.Add($"{mov.Title}, Rotten score: {mov.RotTomScore}");
                                    }
                                break;
                            case "LET":
                                foreach (var mov in movies.Dict.Values)
                                    if (mov.RotTomScore <= intVal)
                                    {
                                        resultsToShow.Add($"{mov.Title}, Rotten score: {mov.RotTomScore}");
                                    }
                                break;
                            case "GET":
                                foreach (var mov in movies.Dict.Values)
                                    if (mov.RotTomScore >= intVal)
                                    {
                                        resultsToShow.Add($"{mov.Title}, Rotten score: {mov.RotTomScore}");
                                    }
                                break;
                        }
                        if (resultsToShow.Count == 0)
                        {
                            resultsToShow.Add("No matches found");
                        }
                        break;
                    case "IMDB":
                        if (!decimal.TryParse(input, out decimal decVal))
                        {
                            throw new ArgumentException("Valid IMDB search value is decimal number between 0-10");
                        }
                        if (decVal > 10 || decVal < 0)
                        {
                            throw new ArgumentException("Valid IMDB search value is decimal number between 0-10");
                        }
                        switch (op)
                        {
                            case "LT":
                                foreach (var mov in movies.Dict.Values)
                                    if (mov.imdbScore < decVal)
                                    {
                                        resultsToShow.Add($"{mov.Title}, IMDB score: {mov.imdbScore}");
                                    }
                                break;
                            case "GT":
                                foreach (var mov in movies.Dict.Values)
                                    if (mov.imdbScore > decVal)
                                    {
                                        resultsToShow.Add($"{mov.Title}, IMDB score: {mov.imdbScore}");
                                    }
                                break;
                            case "LET":
                                foreach (var mov in movies.Dict.Values)
                                    if (mov.imdbScore <= decVal)
                                    {
                                        resultsToShow.Add($"{mov.Title}, IMDB score: {mov.imdbScore}");
                                    }
                                break;
                            case "GET":
                                foreach (var mov in movies.Dict.Values)
                                    if (mov.imdbScore >= decVal)
                                    {
                                        resultsToShow.Add($"{mov.Title}, IMDB score: {mov.imdbScore}");
                                    }
                                break;

                        }
                        break;
                }
                if (resultsToShow.Count == 0)
                {
                    resultsToShow.Add("No matches found");
                }
            }

        }
    }
