using System;
using System.Collections.Generic;
using System.Data;
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

namespace Standoff_Service
{
    public partial class MainWindow : Window
    {
        public MainWindow(string username)
        {
            InitializeComponent();
            User_Text.Text = username;
            Main_Text.Visibility = Visibility.Visible;
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            Main_Text.Visibility = Visibility.Visible;
            SQL_Table.Visibility = Visibility.Collapsed;
        }

        private void Persons_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Text.Visibility = Visibility.Collapsed;
            SQL_Table.Visibility = Visibility.Collapsed;
        }

        private void Materials_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Text.Visibility = Visibility.Collapsed;
            Load_SQL_Table_();
        }

        private void History_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void Load_SQL_Table_()
        {
            Database db = new Database();
            db.OpenConnection();
            DataTable table = db.ShowMaterials();
            db.CloseConnection();
            SQL_Table.Visibility = Visibility.Visible;
            SQL_Table.ItemsSource = table.DefaultView;
        }
    }
}
