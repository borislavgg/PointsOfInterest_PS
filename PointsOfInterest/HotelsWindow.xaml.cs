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
    /// Interaction logic for HotelsWindow.xaml
    /// </summary>
    public partial class HotelsWindow : Window
    {
        public HotelsWindow()
        {
            InitializeComponent();

            var isAdmin = this.IsAdmin();
            if (!isAdmin)
            {
                this.HideLabels();
            }

            hotels.ItemsSource = LoadCollectionData();
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
            HotelName.Visibility = Visibility.Hidden;
            HotelDes.Visibility = Visibility.Hidden;
            HotelImageName.Visibility = Visibility.Hidden;
            AddHotel.Visibility = Visibility.Hidden;
            NameLabel.Visibility = Visibility.Hidden;
            DesLabel.Visibility = Visibility.Hidden;
            BrowseButton.Visibility = Visibility.Hidden;
            DeleteHotel.Visibility = Visibility.Hidden;
            PlaceLabel.Visibility = Visibility.Hidden;
            PriceLabel.Visibility = Visibility.Hidden;
            HotelPlace.Visibility = Visibility.Hidden;
            HotelPrice.Visibility = Visibility.Hidden;
            SeedFromFile.Visibility = Visibility.Hidden;
        }

        private List<Hotel> LoadCollectionData()
        {
            var hotelsToReturn = new List<Hotel>();
            using (var db = new PointsOfInterestContext())
            {
                hotelsToReturn = db.Hotels
                    .Where(x => x.IsDeleted == false || x.IsDeleted == null)
                    .ToList();

                foreach (var item in hotelsToReturn)
                {
                    var ratesHotel = db.Rates_Users_Hotels
                       .Where(x => x.HotelId == item.Id).ToList();

                    item.Rate = this.CalculateAverageRate(ratesHotel);
                }
            }

            return hotelsToReturn;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = hotels.SelectedItem as Hotel;

            var page = new HotelWindow(selectedItem.Id.ToString());
            page.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var name = HotelName.Text;
            var des = HotelDes.Text;
            var imagePath = HotelImageName.Text;
            var place = HotelPlace.Text;
            var price = HotelPrice.Text;

            var parsedPriceNumber = 0.00m;

            if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(des)
                && !String.IsNullOrEmpty(imagePath) && !String.IsNullOrEmpty(place)
                && !String.IsNullOrEmpty(price))
            {
                var parsedPrice = Decimal.TryParse(price, out parsedPriceNumber);

                if (parsedPrice)
                {
                    if (parsedPriceNumber < 0)
                    {
                        ErrrorMessage.Content="Price cannot be a negative number";
                    }
                    else
                    {
                        var imageName=ImageSaver.Save("Hotels", imagePath);
                        var hotel = new Hotel();
                        hotel.HotelName = name;
                        hotel.Descripiton = des;
                        hotel.ImageUrl = imageName;
                        hotel.Place = place;
                        hotel.Price = parsedPriceNumber;

                        using (var db = new PointsOfInterestContext())
                        {
                            db.Hotels.Add(hotel);

                            db.SaveChanges();
                        }

                        HotelName.Text = "";
                        HotelDes.Text = "";
                        HotelImageName.Text = "";
                        HotelPlace.Text = "";
                        HotelPrice.Text = "";

                        hotels.ItemsSource = this.LoadCollectionData();
                        hotels.Items.Refresh();

                        ErrrorMessage.Content = "";
                    }

                }
                else
                {
                    ErrrorMessage.Content="Price must be a number";
                }
            }
            else
            {
                ErrrorMessage.Content="Name, Description, Image Name, Place and Price cannot be empty";
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = hotels.SelectedItem as Hotel;
                using (var db = new PointsOfInterestContext())
                {
                    var deletedHotel = db.Hotels.SingleOrDefault(x => x.Id == selectedItem.Id);
                    deletedHotel.IsDeleted = true;

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

            var placesToReturn = new List<Hotel>();

            using (var db = new PointsOfInterestContext())
            {
                placesToReturn = db.Hotels
                    .Where(x => (x.IsDeleted == false || x.IsDeleted == null)
                    && x.HotelName.ToLower().Contains(searchTerm))
                    .ToList();

                foreach (var item in placesToReturn)
                {
                    var ratesHotel = db.Rates_Users_Hotels
                       .Where(x => x.HotelId == item.Id).ToList();

                    item.Rate = this.CalculateAverageRate(ratesHotel);
                }
            }

            hotels.ItemsSource = placesToReturn;
            hotels.Items.Refresh();

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var window = new Home();
            window.Show();
            this.Close();
        }


        private decimal CalculateAverageRate(List<Rates_Users_Hotels> rates)
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
            var combinePath = System.IO.Path.Combine(path + "/Files/Hotels" + ".txt");
            var lines = System.IO.File.ReadAllLines(combinePath);

            var items = new List<Hotel>();
            foreach (var line in lines)
            {
                var splitLine = line.Split(';');
                var name = splitLine[0];
                var img = splitLine[1];
                var des = splitLine[2];
                var price = decimal.Parse(splitLine[3]);
                var place = splitLine[4];

                var item = new Hotel
                {
                    HotelName = name,
                    ImageUrl = img,
                    Price = price,
                    Place = place,
                    Descripiton = des
                };
                items.Add(item);
            }

            using (var db = new PointsOfInterestContext())
            {
                foreach (var item in items)
                {
                    var existItem = db.Hotels.SingleOrDefault(x => x.HotelName == item.HotelName);

                    if (existItem == null)
                    {
                        db.Hotels.Add(item);
                    }
                }

                db.SaveChanges();
            }

            var itemsToReturn = new List<Hotel>();
            using (var db = new PointsOfInterestContext())
            {
                itemsToReturn = db.Hotels
                  .Where(x => (x.IsDeleted == false || x.IsDeleted == null))
                  .ToList();

                foreach (var item in itemsToReturn)
                {
                    var ratesHotel = db.Rates_Users_Hotels
                       .Where(x => x.HotelId == item.Id).ToList();

                    item.Rate = this.CalculateAverageRate(ratesHotel);
                }
            }

            hotels.ItemsSource = itemsToReturn;
            hotels.Items.Refresh();
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
                HotelImageName.Text = selectedFileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
            }
        }
    }
}
