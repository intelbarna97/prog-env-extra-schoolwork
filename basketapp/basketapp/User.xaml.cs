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

namespace basketapp
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : Window
    {
        Window mainwin;
        public User(Window mainwin)
        {
            this.mainwin = mainwin;
            InitializeComponent();
            mainwin.Hide();
        }

        private void Submit_button_Click(object sender, RoutedEventArgs e)
        {
            if(user_TextBox.Text!=null || user_TextBox.Text!="")
            {
                MainWindow.activeUser = user_TextBox.Text;
                MessageBox.Show("Welcome " + MainWindow.activeUser + "!");
                mainwin.Show();
                this.Close();
            }
        }
    }
}
