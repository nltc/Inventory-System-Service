using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Serilog;

namespace Standoff_Service
{
    public partial class FindWindow : Window
    {
        public DataTable resultTable { get; private set; }
        public DataGrid input_grid { get; private set; }
        public string username { get; private set; }

        public FindWindow(DataGrid grid, string user)
        {
            InitializeComponent();
            input_grid = grid;
            username = user;
        }

        private void FindBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            textBox.Text = textBox.Text.Replace("Name:", string.Empty);
            textBox.Foreground = new SolidColorBrush(Colors.Black);

        }

        private void FindBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
                textBox.Text = "Name:";
            }
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            string FindFieldText = FindField.Text;

            Database db = new Database();
            db.OpenConnection();
            DataTable table = db.FindMaterials(FindFieldText);
            Log.Information($"{username} tried to find the material: {FindFieldText}");
            db.CloseConnection();

            if (table.Rows.Count > 0)
            {
                input_grid.ItemsSource = table.DefaultView;
                this.Close();
            }
            else
            {
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
                ErrorMessageTextBlock.Text = "No such material";
            }
        }
    }
}
