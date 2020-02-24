using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Windows;
using PointsOfInterest.Helpers;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace PointsOfInterest
{
    /// <summary>
    /// Interaction logic for Museums.xaml
    /// </summary>
    public partial class Museums : Window
    {
        public Museums()
        {
            InitializeComponent();

            var isAdmin = this.IsAdmin();
            if (!isAdmin)
            {
                this.HideLabels();
            }

            museums.ItemsSource = LoadCollectionData();

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
            MusName.Visibility = Visibility.Hidden;
            MusDes.Visibility = Visibility.Hidden;
            MusImageName.Visibility = Visibility.Hidden;
            AddMus.Visibility = Visibility.Hidden;
            NameLabel.Visibility = Visibility.Hidden;
            DesLabel.Visibility = Visibility.Hidden;
            BrowseButton.Visibility = Visibility.Hidden;
            DeleteMus.Visibility = Visibility.Hidden;
            SeedFromFile.Visibility = Visibility.Hidden;
        }

        private List<Museum> LoadCollectionData()
        {
            var museumsToReturn = new List<Museum>();
            using (var db = new PointsOfInterestContext())
            {
                museumsToReturn = db.Museums
                    .Where(x => x.IsDeleted == false || x.IsDeleted == null)
                    .ToList();

                foreach (var item in museumsToReturn)
                {
                    var ratesMuseum = db.Rates_Users_Museums
                     .Where(x => x.MuseumId == item.Id).ToList();

                    item.AverageRate = CalculateAverageRate(ratesMuseum);
                }
            }

            return museumsToReturn;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = museums.SelectedItem as Museum;

            var page = new MuseumWindow(selectedItem.Id.ToString());
            page.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var musName = MusName.Text;
            var musDes = MusDes.Text;
            var imagePath = MusImageName.Text;

            if (!String.IsNullOrEmpty(musName) && !String.IsNullOrEmpty(musDes)
                && !String.IsNullOrEmpty(imagePath))
            {
                var imageName = ImageSaver.Save("Museums", imagePath);

                var museum = new Museum();
                museum.MuseumName = musName;
                museum.Descripiton = musDes;
                museum.ImageUrl = imageName;

                using (var db = new PointsOfInterestContext())
                {
                    db.Museums.Add(museum);

                    db.SaveChanges();
                }

                MusName.Text = "";
                MusDes.Text = "";
                MusImageName.Text = "";

                museums.ItemsSource = this.LoadCollectionData();
                museums.Items.Refresh();

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
                var selectedItem = museums.SelectedItem as Museum;
                using (var db = new PointsOfInterestContext())
                {
                    var deletedMuseum = db.Museums.SingleOrDefault(x => x.Id == selectedItem.Id);
                    deletedMuseum.IsDeleted = true;

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

            var placesToReturn = new List<Museum>();

            using (var db = new PointsOfInterestContext())
            {
                placesToReturn = db.Museums
                    .Where(x => (x.IsDeleted == false || x.IsDeleted == null)
                    && x.MuseumName.ToLower().Contains(searchTerm))
                    .ToList();

                foreach (var item in placesToReturn)
                {
                    var ratesMuseum = db.Rates_Users_Museums
                     .Where(x => x.MuseumId == item.Id).ToList();
                    
                    item.AverageRate = CalculateAverageRate(ratesMuseum);
                }
            }

            museums.ItemsSource = placesToReturn;
            museums.Items.Refresh();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var window = new Home();
            window.Show();
            this.Close();
        }

        private decimal CalculateAverageRate(List<Rates_Users_Museums> rates)
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
            var combinePath = System.IO.Path.Combine(path + "/Files/Museums" + ".txt");
            var lines = System.IO.File.ReadAllLines(combinePath);

            var items = new List<Museum>();
            foreach (var line in lines)
            {
                var splitLine = line.Split(';');
                var name = splitLine[0];
                var img = splitLine[1];
                var des = splitLine[2];

                var item = new Museum
                {
                    MuseumName = name,
                    ImageUrl = img,
                    Descripiton = des
                };
                items.Add(item);
            }

            using (var db = new PointsOfInterestContext())
            {
                foreach (var item in items)
                {
                    var existItem = db.Museums.SingleOrDefault(x => x.MuseumName == item.MuseumName);

                    if (existItem == null)
                    {
                        db.Museums.Add(item);
                    }
                }

                db.SaveChanges();
            }

            var itemsToReturn = new List<Museum>();
            using (var db = new PointsOfInterestContext())
            {
                itemsToReturn = db.Museums
                  .Where(x => (x.IsDeleted == false || x.IsDeleted == null))
                  .ToList();

                foreach (var item in itemsToReturn)
                {
                    var ratesMuseums= db.Rates_Users_Museums
                       .Where(x => x.MuseumId == item.Id).ToList();

                    item.AverageRate = this.CalculateAverageRate(ratesMuseums);
                }
            }

            museums.ItemsSource = itemsToReturn;
            museums.Items.Refresh();
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
                MusImageName.Text = selectedFileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
            }
        }
    }
}
