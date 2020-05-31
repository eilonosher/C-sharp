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
    /// Interaction logic for Add_actor.xaml
    /// </summary>
    public partial class Add_actor : Window
    {
        private ObservableCollection<MoviePerson> actorsList;
        private string FirstName { get; set; }
        private string LastName { get; set; }
        public Add_actor()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            FirstName = tbFName.Text.ToString();
            LastName = tbLName.Text.ToString();
            string genderValue = gender.Text.ToString();
            string date = datePicker.Text.ToString();
            Gender g = genderValue.Equals("Female") ? Gender.Female : Gender.Male;
            MoviePerson person = new MoviePerson(FirstName , LastName, new DateTime(2020, 1, 1), g) ;
            if(!actorsList.Contains(person))
                actorsList.Add(person);
            else
            {
                MessageBox.Show("The actor already exist, please try again!");
            }

        }

        internal void setLists(ObservableCollection<MoviePerson> director, ObservableCollection<MoviePerson> actors, ObservableDictionary<KeyPair, Movie> m)
        {
            actorsList = actors;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
