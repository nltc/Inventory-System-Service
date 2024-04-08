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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Standoff_Service
{
    public partial class DeleteWindow : Window
    {
        public DataTable resultTable { get; private set; }
        public DataGrid input_grid { get; private set; }
        public string username { get; private set; }

        public DeleteWindow(DataGrid grid, string user)
        {
            InitializeComponent();
            input_grid = grid;
            username = user;
        }
        private void DeleteBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            textBox.Text = textBox.Text.Replace("Name:", string.Empty);
            textBox.Foreground = new SolidColorBrush(Colors.Black);

        }

        private void DeleteBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
                textBox.Text = "Name:";
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string historyText;
            string DeleteFieldText = DeleteField.Text;
            bool deletetable;

            Database db = new Database();
            db.OpenConnection();
            DataTable findtable = db.FindMaterials(DeleteFieldText);
            db.CloseConnection();

            if (findtable.Rows.Count > 0)
            {
                db.OpenConnection();
                deletetable = db.DeleteMaterial(DeleteFieldText);
                historyText = $"Deleted from database material: {DeleteFieldText}";

                if (deletetable)
                {
                    db.AddHistory(username, historyText);
                    MessageBox.Show("Material deleted successful");
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
