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
    /// Interaction logic for PlaceWindow.xaml
    /// </summary>
    public partial class PlaceWindow : Window
    {
        public Place Plc { get; set; }
        public bool IsRated { get; set; }
        public string UserEmail { get; set; }

        public PlaceWindow()
        {
            InitializeComponent();
        }

        public PlaceWindow(string id)
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
                this.IsRated = db.Rates_Users_Places
                    .Any(x => x.PlaceId == this.Plc.Id && x.UserId == currentUser.Id);

                if (this.IsRated)
                {
                    PlaceRate.Visibility = Visibility.Hidden;
                    RateBtn.Visibility = Visibility.Hidden;
                    DeleteRateBtn.Visibility = Visibility.Visible;
                }
                else
                {

                    PlaceRate.Visibility = Visibility.Visible;
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
                    var place = db.Places.SingleOrDefault(x => x.Id == idToNumber);
                    
                    if (place == null)
                    {
                        throw new ArgumentNullException("Invalid place Id");
                    }
                    else
                    {
                        this.Plc = place;


                        var ratesPlace = db.Rates_Users_Places
                      .Where(x => x.PlaceId == this.Plc.Id).ToList();

                        var averageRate = 0.0m;
                        if (ratesPlace.Count > 0)
                        {
                            var fiveCount = ratesPlace.Count(x => x.Rate == 5);
                            var fourCount = ratesPlace.Count(x => x.Rate == 4);
                            var threeCount = ratesPlace.Count(x => x.Rate == 3);
                            var twoCount = ratesPlace.Count(x => x.Rate == 2);
                            var oneCount = ratesPlace.Count(x => x.Rate == 1);

                            averageRate = RateCalculator.Calculcate(fiveCount, fourCount, threeCount, twoCount, oneCount);
                        }

                        place.Rate = averageRate;


                        var currentUserRate = db.Rates_Users_Places
                       .SingleOrDefault(x => x.PlaceId == this.Plc.Id && x.UserId == currentUser.Id);
                        if (currentUserRate != null)
                        {
                            YourRateLabel.Content = "Your rate is : " + currentUserRate.Rate;
                        }
                        

                        PlRate.Value = (int) Math.Round(averageRate);
                        PlaceDes.Content = place.Descripiton;
                        PlaceName.Content = place.PlaceName;

                        var dir = System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString();
                        var path = System.IO.Path.GetDirectoryName(dir);
                        var imagePath = System.IO.Path.Combine(path + "/Images/Places/" + place.ImageUrl);
                        //Uri resourceUri = new Uri(imagePath, UriKind.Relative);
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(imagePath);
                        bitmap.EndInit();
                        PlaceImg.Source = bitmap;
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

            var parsedRate = Int32.TryParse(PlaceRate.Value.ToString(), out parsedRateNumber);
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
                            var ratePlace = new Rates_Users_Places
                            {
                                UserId = currentUser.Id,
                                PlaceId = this.Plc.Id,
                                Rate = parsedRateNumber
                            };

                            db.Rates_Users_Places.Add(ratePlace);
                            db.SaveChanges();
                        }
                    }

                    var page = new PlaceWindow(this.Plc.Id.ToString());
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

                    var place = db.Places.SingleOrDefault(x => x.Id == this.Plc.Id);
                    place.Comments.Add(comment);

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
                var place = db.Places.SingleOrDefault(x => x.Id == this.Plc.Id);
                comments = place.Comments.ToList();
            }
            var commentWindow = new CommentWindow(comments);
            commentWindow.Show();
            this.Close();
        }

        private void BackToPlaces_Button(object sender, RoutedEventArgs e)
        {
            var window = new Places();
            window.Show();
            this.Close();
        }

        private void DeleteRateBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new PointsOfInterestContext())
            {
                var currentUser = db.Users.SingleOrDefault(x => x.Email == this.UserEmail);
                var currentRate = db.Rates_Users_Places
                    .SingleOrDefault(x => x.UserId == currentUser.Id && x.PlaceId == this.Plc.Id);

                db.Rates_Users_Places.Remove(currentRate);
                db.SaveChanges();
            }

            var page = new PlaceWindow(this.Plc.Id.ToString());
            page.Show();
            this.Close();
        }
    }
}
