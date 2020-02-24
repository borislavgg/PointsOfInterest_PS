using System;
using System.Collections.Generic;
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
    /// Interaction logic for CommentWindow.xaml
    /// </summary>
    public partial class CommentWindow : Window
    {
        public CommentWindow()
        {
            InitializeComponent();
        }

        public CommentWindow(List<Comment> comments)
        {
            InitializeComponent();
            
            this.LoadComments(comments);
            
        }

        private void LoadComments(List<Comment> comments)
        {
            ListOfComments.ItemsSource = comments;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new Home();
            window.Show();
            this.Close();
        }
    }
}
