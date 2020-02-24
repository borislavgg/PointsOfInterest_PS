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
    public partial class MuseumWindow : Window
    {
        public Museum Mus { get; set; }
        public bool IsRated { get; set; }
        public string UserEmail { get; set; }

        public MuseumWindow()
        {
            InitializeComponent();
        }

        public MuseumWindow(string id)
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
                this.IsRated = db.Rates_Users_Museums
                    .Any(x => x.MuseumId == this.Mus.Id && x.UserId == currentUser.Id);

                if (this.IsRated)
                {
                    MusRate.Visibility = Visibility.Hidden;
                    RateBtn.Visibility = Visibility.Hidden;
                    DeleteRateBtn.Visibility = Visibility.Visible;
                }
                else
                {

                    MusRate.Visibility = Visibility.Visible;
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
                    var museum = db.Museums.SingleOrDefault(x => x.Id == idToNumber);
                    var currentUser = db.Users.SingleOrDefault(x => x.Email == this.UserEmail);

                    if (museum == null)
                    {
                        throw new ArgumentNullException("Invalid museum Id");
                    }
                    else
                    {
                        this.Mus = museum;

                        var ratesMus = db.Rates_Users_Museums
                       .Where(x => x.MuseumId == this.Mus.Id).ToList();

                        var averageRate = 0.0m;
                        if (ratesMus.Count > 0)
                        {
                            var fiveCount = ratesMus.Count(x => x.Rate == 5);
                            var fourCount = ratesMus.Count(x => x.Rate == 4);
                            var threeCount = ratesMus.Count(x => x.Rate == 3);
                            var twoCount = ratesMus.Count(x => x.Rate == 2);
                            var oneCount = ratesMus.Count(x => x.Rate == 1);

                            averageRate = RateCalculator.Calculcate(fiveCount, fourCount, threeCount, twoCount, oneCount);
                        }

                        museum.AverageRate = averageRate;


                        var currentUserRate = db.Rates_Users_Museums
                       .SingleOrDefault(x => x.MuseumId == this.Mus.Id && x.UserId == currentUser.Id);
                        if (currentUserRate != null)
                        {
                            YourRateLabel.Content = "Your rate is : " + currentUserRate.Rate;
                        }

                        AverageRate.Value = (int)Math.Round(averageRate);
                        

                        MusDes.Content = museum.Descripiton;
                        MusName.Content = museum.MuseumName;

                        var dir = System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString();
                        var path = System.IO.Path.GetDirectoryName(dir);
                        var imagePath = System.IO.Path.Combine(path + "/Images/Museums/" + museum.ImageUrl);
                        //Uri resourceUri = new Uri(imagePath, UriKind.Relative);
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(imagePath);
                        bitmap.EndInit();
                        MusImg.Source = bitmap;
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

            var parsedRate = Int32.TryParse(MusRate.Value.ToString(), out parsedRateNumber);
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
                            var rateMuseum = new Rates_Users_Museums
                            {
                                UserId = currentUser.Id,
                                MuseumId = this.Mus.Id,
                                Rate = parsedRateNumber
                            };

                            db.Rates_Users_Museums.Add(rateMuseum);
                            db.SaveChanges();
                        }
                    }

                    var page = new MuseumWindow(this.Mus.Id.ToString());
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

                    var museum = db.Museums.SingleOrDefault(x => x.Id == this.Mus.Id);
                    museum.Comments.Add(comment);

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
                var museum = db.Museums.SingleOrDefault(x => x.Id == this.Mus.Id);
                comments = museum.Comments.ToList();
            }
            var commentWindow = new CommentWindow(comments);
            commentWindow.Show();
            this.Close();
        }

        private void BackToMuseums_Button(object sender, RoutedEventArgs e)
        {
            var window = new Museums();
            window.Show();
            this.Close();
        }

        private void DeleteRateBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new PointsOfInterestContext())
            {
                var currentUser = db.Users.SingleOrDefault(x => x.Email == this.UserEmail);
                var currentRate = db.Rates_Users_Museums
                    .SingleOrDefault(x => x.UserId == currentUser.Id && x.MuseumId == this.Mus.Id);

                db.Rates_Users_Museums.Remove(currentRate);
                db.SaveChanges();
            }

            var page = new MuseumWindow(this.Mus.Id.ToString());
            page.Show();
            this.Close();
        }
    }
}
