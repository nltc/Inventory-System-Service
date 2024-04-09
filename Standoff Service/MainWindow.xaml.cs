using Serilog;
using System.Data;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Standoff_Service
{
    public partial class MainWindow : Window
    {
        public string rigth { get; private set; }

        public MainWindow(string username, string rigths)
        {
            rigth = rigths;
            InitializeComponent();
            User_Text.Text = username;
            Main_Text.Visibility = Visibility.Visible;
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllWindows();
            Main_Text.Text = "Nuclear Materials Inventory System";
            Main_Text.Visibility = Visibility.Visible;
        }

        private void Persons_Button_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllWindows();

            if (rigth == "Employee")
            {
                Main_Text.Text = "You do not have enough rights to view this page";
                Main_Text.Visibility = Visibility.Visible;
            }
            else
            {
                Load_Persons_Table();
            }
        }

        private void Materials_Button_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllWindows();
            Load_Materials_Table();
        }

        private void History_Button_Click(object sender, RoutedEventArgs e)
        {
            CollapseAllWindows();

            if (rigth == "Employee")
            {
                Main_Text.Text = "You do not have enough rights to view this page";
                Main_Text.Visibility = Visibility.Visible;
            }
            else
            {
                Load_History_Table();
            }
        }

        private void Find_Window_Click(object sender, RoutedEventArgs e)
        {
            FindWindow findWindow = new FindWindow(Materials_Table, User_Text.Text);
            findWindow.Show();
        }

        private void Delete_Window_Click(object sender, RoutedEventArgs e)
        {
            if (rigth == "Employee" || rigth == "Manager")
            {
                MessageBox.Show("There are not enough rights for this action");
            }
            else
            {
                DeleteWindow deleteWindow = new DeleteWindow(Materials_Table, User_Text.Text);
                deleteWindow.Show();
            }
        }

        private void Add_Window_Click(object sender, RoutedEventArgs e)
        {
            if (rigth == "Employee" || rigth == "Manager")
            {
                MessageBox.Show("There are not enough rights for this action");
            }
            else
            {
                AddWindow AddWindow = new AddWindow(Materials_Table, User_Text.Text);
                AddWindow.Show();
            }
        }

        private void CollapseAllWindows()
        {
            Main_Text.Visibility = Visibility.Collapsed;
            Materials_Table.Visibility = Visibility.Collapsed;
            Persons_Table.Visibility = Visibility.Collapsed;
            History_Table.Visibility = Visibility.Collapsed;
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

        private void Load_History_Table()
        {
            Database db = new Database();
            db.OpenConnection();
            DataTable table = db.ShowHistory();
            db.CloseConnection();
            History_Table.Visibility = Visibility.Visible;
            History_Table.ItemsSource = table.DefaultView;
        }
    }
}
