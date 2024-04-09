using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Serilog;

namespace Standoff_Service
{
    public partial class DeleteWindow : Window
    {
        public string username { get; private set; }

        public DeleteWindow(DataGrid grid, string user)
        {
            InitializeComponent();
            username = user;
        }

        private void DeleteBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = textBox.Text.Replace("Name:", string.Empty);
            textBox.Foreground = Brushes.Black;
        }

        private void DeleteBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Foreground = Brushes.Gray;
                textBox.Text = "Name:";
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string historyText;
            string deleteFieldText = DeleteField.Text;
            bool deleteTable;

            Database db = new Database();
            db.OpenConnection();
            DataTable findTable = db.FindMaterials(deleteFieldText);
            db.CloseConnection();

            if (findTable.Rows.Count > 0)
            {
                db.OpenConnection();
                deleteTable = db.DeleteMaterial(deleteFieldText);
                historyText = $"Deleted from database material: {deleteFieldText}";

                if (deleteTable)
                {
                    db.AddHistory(username, historyText);

                    Log.Information($"{username} {historyText}");

                    MessageBox.Show("Material deleted successfully");
                }

                db.CloseConnection();
            }
            else
            {
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
                ErrorMessageTextBlock.Text = "No such material";
            }
        }
    }
}
