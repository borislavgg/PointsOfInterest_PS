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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var page = new Museums();
            page.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var page = new HotelsWindow();
            page.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var page = new Places();
            page.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ConfigurationManager.AppSettings["CurrentUser"] = "";
            MessageBox.Show($"You have succesfully logout");

            var page = new MainWindow();
            page.Show();
            this.Close();
        }
    }
}
