using MySqlX.XDevAPI.Relational;
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
            CollapseAllWindows();
            Main_Text.Visibility = Visibility.Visible;
        }

        private void Persons_Button_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllWindows();
            Load_Persons_Table();
        }

        private void Materials_Button_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllWindows();
            Load_Materials_Table();
        }

        private void History_Button_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllWindows();
        }

        private void Find_Window_Click(object sender, RoutedEventArgs e)
        {
            FindWindow findWindow = new FindWindow(Materials_Table);
            findWindow.Show();
        }

        private void Delete_Window_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Add_Window_Click(object sender, RoutedEventArgs e)
        {
            AddWindow AddWindow = new AddWindow(Materials_Table);
            AddWindow.Show();
        }

        private void CollapseAllWindows()
        {
            Main_Text.Visibility = Visibility.Collapsed;
            Materials_Table.Visibility = Visibility.Collapsed;
            Persons_Table.Visibility = Visibility.Collapsed;
            Delete_Button.Visibility = Visibility.Collapsed;
            Find_Button.Visibility = Visibility.Collapsed;
            Add_Button.Visibility = Visibility.Collapsed;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void Load_Materials_Table()
        {
            Database db = new Database();
            db.OpenConnection();
            DataTable table = db.ShowMaterials();
            db.CloseConnection();
            Materials_Table.Visibility = Visibility.Visible;
            Add_Button.Visibility = Visibility.Visible;
            Delete_Button.Visibility = Visibility.Visible;
            Find_Button.Visibility = Visibility.Visible;
            Materials_Table.ItemsSource = table.DefaultView;
        }

        private void Load_Persons_Table()
        {
            Database db = new Database();
            db.OpenConnection();
            DataTable table = db.ShowPersons();
            db.CloseConnection();
            Persons_Table.Visibility = Visibility.Visible;
            Persons_Table.ItemsSource = table.DefaultView;
        }
    }
}
