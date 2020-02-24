using PointsOfInterest.Logger;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Register reg = new Register();
            reg.Show();
            this.Hide();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var email = txtemail.Text.Trim();
            var password = txtpassword.Password.Trim();

            using (var db = new PointsOfInterestContext())
            {
                var existUser = db.Users.SingleOrDefault(x => x.Email == email);

                if (existUser == null)
                {
                    MessageBox.Show("Invalid email or password");
                }
                else
                {
                    var checkPassword = BCrypt.Net.BCrypt.Verify(password, existUser.Password);
                    if (checkPassword)
                    {
                        UserLogger.Save(email,existUser.Name);
                        
                        ConfigurationManager.AppSettings["CurrentUser"] = existUser.Email;

                        var isAdmin = existUser.IsAdmin ?? false;
                        var adminMessage = "";
                        if (isAdmin)
                        {
                            adminMessage = "with admin permissions";
                        }
                        MessageBox.Show($"You have succesfully logged in with {existUser.Email} {adminMessage}");

                        var page = new Home();
                        page.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid email or password");
                    }
                }
            }
        }
    }
}
