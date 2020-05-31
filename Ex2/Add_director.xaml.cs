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
using static Ex2.MainWindow;

namespace Ex2
{
    /// <summary>
    /// Interaction logic for Add_director.xaml
    /// </summary>
    public partial class Add_director : Window
    {
        private ObservableCollection<MoviePerson> directorList;

        public Add_director()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            string firstName = tbFName.Text.ToString();
            string lastName = tbLName.Text.ToString();
            string genderValue = gender.Text.ToString();
            string date = datePicker.Text.ToString();
            Gender g = genderValue.Equals("Female") ? Gender.Female : Gender.Male;
            MoviePerson person = new MoviePerson(firstName, lastName, new DateTime(2020, 1, 1), g);
            if (!directorList.Contains(person))
                directorList.Add(person);
            else
                MessageBox.Show("The director already exist, please try again!");
        }
        public override string ToString()
        {
            return Name;
        }
        internal void setLists(ObservableCollection<MoviePerson> director, ObservableCollection<MoviePerson> actors, ObservableDictionary<KeyPair, Movie> m)
        {
            directorList = director;
        }
    }
}
