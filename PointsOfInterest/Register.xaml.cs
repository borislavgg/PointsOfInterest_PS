using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using BCrypt.Net;

namespace PointsOfInterest
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var email = txtemail.Text.Trim();
            var name = txtname.Text.Trim();
            var password = txtpassword.Password.Trim();

            var errorMessage = "";
            if (email == "" || name == "" || password == "")
            {
                errorMessage = "Please fill all details";
                MessageBox.Show(errorMessage);
                return;
            }

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                errorMessage = "Invalid email address";
                MessageBox.Show(errorMessage);
                return;
            }

            if (!string.IsNullOrEmpty(name) && !Char.IsUpper(name[0]))
            {
                errorMessage = "Name must start with UpperCase";
                MessageBox.Show(errorMessage);
                return;
            }

            if (name.Length <= 3)
            {
                errorMessage = "Name must be more than 3 symbols!";
                MessageBox.Show(errorMessage);
                return;
            }

            Regex regexPass = new Regex(@"^(?=.{6,})(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+*!=]).*$");
            Match matchPass = regexPass.Match(password);
            if (!matchPass.Success)
            {
                errorMessage = "Password must have atleast 6 symbols and contains one upper case, one lower case and one special symbol(@#$%^&+*!=)";
                MessageBox.Show(errorMessage);
                return;
            }

            try
            {
                using (var db = new PointsOfInterestContext())
                {

                    var user = db.Users.SingleOrDefault(x => x.Email == email);

                    if (user == null)
                    {
                        user = new User();
                        user.Email = email;
                        user.Name = name;

                        string passwordHashed = BCrypt.Net.BCrypt.HashPassword(password,
                            BCrypt.Net.BCrypt.GenerateSalt());
                        user.Password = passwordHashed;
                        user.IsAdmin = false;

                        db.Users.Add(user);
                        db.SaveChanges();

                        MessageBox.Show("Registration Successfull.");
                        Login log = new Login();
                        log.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("This email is already register");
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
