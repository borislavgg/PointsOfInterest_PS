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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PointsOfInterest
{
    /// <summary>
    /// Interaction logic for Museum.xaml
    /// </summary>
    public partial class HotelWindow : Window
    {
        public Hotel Hotl { get; set; }
        public bool IsRated { get; set; }
        public string UserEmail { get; set; }


        public HotelWindow()
        {
            InitializeComponent();
            
        }

        public HotelWindow(string id)
        {
            this.UserEmail = ConfigurationManager.AppSettings["CurrentUser"];
            InitializeComponent();
            this.LoadData(id);
            this.LockButtons();
        }

        private void LockButtons()
        {
            using (var db = new PointsOfInterestContext())
            {
                var currentUser = db.Users.SingleOrDefault(x => x.Email == this.UserEmail);
                this.IsRated = db.Rates_Users_Hotels
                    .Any(x => x.HotelId == this.Hotl.Id && x.UserId == currentUser.Id);

                if (this.IsRated)
                {
                    HotelRate.Visibility = Visibility.Hidden;
                    RateBtn.Visibility = Visibility.Hidden;
                    DeleteRateBtn.Visibility = Visibility.Visible;
                }
                else
                {

                    HotelRate.Visibility = Visibility.Visible;
                    RateBtn.Visibility = Visibility.Visible;
                    DeleteRateBtn.Visibility = Visibility.Hidden;
                }
                
            }
        }

        private void LoadData(string id)
        {
            try
            {
                var idToNumber = int.Parse(id);
                using (var db = new PointsOfInterestContext())
                {
                    var currentUser = db.Users.SingleOrDefault(x => x.Email == this.UserEmail);
                    var hotel = db.Hotels.SingleOrDefault(x => x.Id == idToNumber); 
                    if (hotel == null)
                    {
                        throw new ArgumentNullException("Invalid hotel Id");
                    }
                    else
                    {
                        this.Hotl = hotel;

                        var ratesHotel = db.Rates_Users_Hotels
                       .Where(x => x.HotelId == this.Hotl.Id).ToList();

                        var averageRate = 0.0m;
                        if (ratesHotel.Count > 0)
                        {
                            var fiveCount = ratesHotel.Count(x => x.Rate == 5);
                            var fourCount = ratesHotel.Count(x => x.Rate == 4);
                            var threeCount = ratesHotel.Count(x => x.Rate == 3);
                            var twoCount = ratesHotel.Count(x => x.Rate == 2);
                            var oneCount = ratesHotel.Count(x => x.Rate == 1);

                            averageRate = RateCalculator.Calculcate(fiveCount, fourCount, threeCount, twoCount, oneCount);
                        }

                        hotel.Rate = averageRate;


                        var currentUserRate = db.Rates_Users_Hotels
                       .SingleOrDefault(x => x.HotelId == this.Hotl.Id && x.UserId == currentUser.Id);
                        if (currentUserRate != null)
                        {
                            YourRateLabel.Content = "Your rate is : " + currentUserRate.Rate;
                        }

                        AverageRate.Value = (int)Math.Round(averageRate);
                        
                        HotelDes.Content = hotel.Descripiton;
                        HotelName.Content = hotel.HotelName;
                        HotelPlace.Content = hotel.Place;
                        HotelPrice.Content = "Price: " + hotel.Price;

                        var dir = System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString();
                        var path = System.IO.Path.GetDirectoryName(dir);
                        var imagePath = System.IO.Path.Combine(path + "/Images/Hotels/" + hotel.ImageUrl);
                        //Uri resourceUri = new Uri(imagePath, UriKind.Relative);
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(imagePath);
                        bitmap.EndInit();
                        HotelImg.Source = bitmap;
                    }
                }
            }
            catch
            {

            }

        }

        private void AddRate_Button(object sender, RoutedEventArgs e)
        {
            var parsedRateNumber = 0;

            var parsedRate = Int32.TryParse(HotelRate.Value.ToString(), out parsedRateNumber);
            if (parsedRate)
            {
                if (parsedRateNumber < 1 || parsedRateNumber > 5)
                {
                    MessageBox.Show("rate must be between 1 and 5");
                }
                else
                {
                    using (var db = new PointsOfInterestContext())
                    {
                        var currentUser = db.Users.SingleOrDefault(x => x.Email == this.UserEmail);

                        if (!this.IsRated)
                        {
                            var rateHotel = new Rates_Users_Hotels
                            {
                                UserId = currentUser.Id,
                                HotelId = this.Hotl.Id,
                                Rate = parsedRateNumber
                            };

                            db.Rates_Users_Hotels.Add(rateHotel);
                            db.SaveChanges();
                        }
                    }

                    var page = new HotelWindow(this.Hotl.Id.ToString());
                    page.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("rate must be a number");
            }
            
        }

        private void AddComment_Button(object sender, RoutedEventArgs e)
        {
            var commentText = CommentVal.Text;

            if (!string.IsNullOrEmpty(commentText))
            {
                using (var db = new PointsOfInterestContext())
                {
                    var comment = new Comment();
                    comment.Name = commentText;
                    comment.UserEmail = this.UserEmail;

                    var hotel = db.Hotels.SingleOrDefault(x => x.Id == this.Hotl.Id);
                    hotel.Comments.Add(comment);

                    db.SaveChanges();
                }
            }

            CommentVal.Text = "";
        }

        private void ViewComments_Button(object sender, RoutedEventArgs e)
        {
            var comments = new List<Comment>();
            using (var db = new PointsOfInterestContext())
            {
                var hotel = db.Hotels.SingleOrDefault(x => x.Id == this.Hotl.Id);
                comments = hotel.Comments.ToList();
            }
                var commentWindow = new CommentWindow(comments);
            commentWindow.Show();
            this.Close();
        }

        private void BackToHotels_Button(object sender, RoutedEventArgs e)
        {
            var window = new HotelsWindow();
            window.Show();
            this.Close();
        }

        private void DeleteRateBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new PointsOfInterestContext())
            {
                var currentUser = db.Users.SingleOrDefault(x => x.Email == this.UserEmail);
                var currentRate = db.Rates_Users_Hotels
                    .SingleOrDefault(x => x.UserId == currentUser.Id && x.HotelId == this.Hotl.Id);

                db.Rates_Users_Hotels.Remove(currentRate);
                db.SaveChanges();
            }

            var page = new HotelWindow(this.Hotl.Id.ToString());
            page.Show();
            this.Close();
        }
    }
}
