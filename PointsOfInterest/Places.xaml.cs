using PointsOfInterest.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PointsOfInterest
{
    /// <summary>
    /// Interaction logic for Places.xaml
    /// </summary>
    public partial class Places : Window
    {
        public Places()
        {
            InitializeComponent();

            var isAdmin = this.IsAdmin();
            if (!isAdmin)
            {
                this.HideLabels();
            }

            places.ItemsSource = LoadCollectionData();
        }

        private bool IsAdmin()
        {
            try
            {
                string currentUserEmail = ConfigurationManager.AppSettings["CurrentUser"];

                using (var db = new PointsOfInterestContext())
                {
                    var currentUser = db.Users.SingleOrDefault(x => x.Email == currentUserEmail);
                    var isAdmin = currentUser.IsAdmin ?? false;

                    if (isAdmin)
                    {
                        return true;
                    }
                }
            }
            catch
            {

            }


            return false;
        }

        private void HideLabels()
        {
            PlaceName.Visibility = Visibility.Hidden;
            PlaceDes.Visibility = Visibility.Hidden;
            PlaceImageName.Visibility = Visibility.Hidden;
            AddPlace.Visibility = Visibility.Hidden;
            NameLabel.Visibility = Visibility.Hidden;
            DesLabel.Visibility = Visibility.Hidden;
            PlaceImageName.Visibility = Visibility.Hidden;
            DeletePlace.Visibility = Visibility.Hidden;
            SeedFromFile.Visibility = Visibility.Hidden;
        }

        private List<Place> LoadCollectionData()
        {
            var placesToReturn = new List<Place>();
            using (var db = new PointsOfInterestContext())
            {
                placesToReturn = db.Places
                    .Where(x => x.IsDeleted == false || x.IsDeleted == null)
                    .ToList();

                foreach (var item in placesToReturn)
                {
                    var ratesPlaces = db.Rates_Users_Places
                     .Where(x => x.PlaceId == item.Id).ToList();

                    item.Rate = CalculateAverageRate(ratesPlaces);
                }
            }

            return placesToReturn;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = places.SelectedItem as Place;
            
            var page = new PlaceWindow(selectedItem.Id.ToString());
            page.Show();

            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var placeName = PlaceName.Text;
            var placeDes = PlaceDes.Text;
            var imagePath = PlaceImageName.Text;

            if (!String.IsNullOrEmpty(placeName) && !String.IsNullOrEmpty(placeDes)
                && !String.IsNullOrEmpty(imagePath))
            {
                var imageName = ImageSaver.Save("Places", imagePath);
                var place= new Place();
                place.PlaceName = placeName;
                place.Descripiton = placeDes;
                place.ImageUrl = imageName;

                using (var db = new PointsOfInterestContext())
                {
                    db.Places.Add(place);

                    db.SaveChanges();
                }

                PlaceName.Text = "";
                PlaceDes.Text = "";
                PlaceImageName.Text = "";

                places.ItemsSource = this.LoadCollectionData();
                places.Items.Refresh();

                ErrrorMessage.Content = "";
            }
            else
            {
                ErrrorMessage.Content="Name, Description, Image Name cannot be empty";
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = places.SelectedItem as Place;
                using (var db = new PointsOfInterestContext())
                {
                    var deletedItem = db.Places.SingleOrDefault(x => x.Id == selectedItem.Id);
                    deletedItem.IsDeleted = true;

                    db.SaveChanges();
                }
            }
            catch
            {

            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var searchTerm = SearchTerm.Text.ToLower();

            var placesToReturn = new List<Place>();

            using (var db = new PointsOfInterestContext())
            {
                placesToReturn = db.Places
                    .Where(x => (x.IsDeleted == false || x.IsDeleted == null)
                    && x.PlaceName.ToLower().Contains(searchTerm))
                    .ToList();

                foreach (var item in placesToReturn)
                {
                    var ratesPlaces = db.Rates_Users_Places
                    .Where(x => x.PlaceId == item.Id).ToList();

                    item.Rate = CalculateAverageRate(ratesPlaces);
                }
            }

            places.ItemsSource = placesToReturn;
            places.Items.Refresh();

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var window = new Home();
            window.Show();
            this.Close();
        }

        private decimal CalculateAverageRate(List<Rates_Users_Places> rates)
        {
            var averageRate = 0.00m;
            if (rates.Count > 0)
            {
                var fiveCount = rates.Count(x => x.Rate == 5);
                var fourCount = rates.Count(x => x.Rate == 4);
                var threeCount = rates.Count(x => x.Rate == 3);
                var twoCount = rates.Count(x => x.Rate == 2);
                var oneCount = rates.Count(x => x.Rate == 1);

                averageRate = RateCalculator.Calculcate(fiveCount, fourCount, threeCount, twoCount, oneCount);
            }

            return averageRate;
        }

        private void SeedFromFile_Click(object sender, RoutedEventArgs e)
        {
            var dir = System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString();
            var path = System.IO.Path.GetDirectoryName(dir);
            var combinePath = System.IO.Path.Combine(path + "/Files/PLaces" + ".txt");
            var lines = System.IO.File.ReadAllLines(combinePath);

            var items = new List<Place>();
            foreach (var line in lines)
            {
                var splitLine = line.Split(';');
                var name = splitLine[0];
                var img = splitLine[1];
                var des = splitLine[2];

                var item = new Place
                {
                    PlaceName = name,
                    ImageUrl = img,
                    Descripiton = des
                };
                items.Add(item);
            }

            using (var db = new PointsOfInterestContext())
            {
                foreach (var item in items)
                {
                    var existItem = db.Places.SingleOrDefault(x => x.PlaceName == item.PlaceName);

                    if (existItem == null)
                    {
                        db.Places.Add(item);
                    }
                }

                db.SaveChanges();
            }

            var itemsToReturn = new List<Place>();
            using (var db = new PointsOfInterestContext())
            {
                itemsToReturn = db.Places
                  .Where(x => (x.IsDeleted == false || x.IsDeleted == null))
                  .ToList();

                foreach (var item in itemsToReturn)
                {
                    var ratesPlaces = db.Rates_Users_Places
                       .Where(x => x.PlaceId == item.Id).ToList();

                    item.Rate = this.CalculateAverageRate(ratesPlaces);
                }
            }

            places.ItemsSource = itemsToReturn;
            places.Items.Refresh();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFileName = dlg.FileName;
                PlaceImageName.Text = selectedFileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
            }
        }
    }
}
