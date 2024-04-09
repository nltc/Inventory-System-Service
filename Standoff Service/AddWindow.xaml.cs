using Serilog;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Serilog;

namespace Standoff_Service
{
    public partial class AddWindow : Window
    {
        public DataTable resultTable { get; private set; }
        public DataGrid input_grid { get; private set; }
        public string username { get; private set; }

        public AddWindow(DataGrid grid, string user)
        {
            InitializeComponent();
            username = user;
            input_grid = grid;
        }

        private void NameField_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            textBox.Text = textBox.Text.Replace("Name:", string.Empty);
            textBox.Foreground = new SolidColorBrush(Colors.Black);

        }

        private void NameField_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
                textBox.Text = "Name:";
            }
        }

        private void DescriptionField_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            textBox.Text = textBox.Text.Replace("Description:", string.Empty);
            textBox.Foreground = new SolidColorBrush(Colors.Black);

        }

        private void DescriptionField_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
                textBox.Text = "Description:";
            }
        }

        private void QuantityField_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            textBox.Text = textBox.Text.Replace("Quantity:", string.Empty);
            textBox.Foreground = new SolidColorBrush(Colors.Black);

        }

        private void QuantityField_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
                textBox.Text = "Quantity:";
            }
        }
        private void LocationField_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            textBox.Text = textBox.Text.Replace("Location:", string.Empty);
            textBox.Foreground = new SolidColorBrush(Colors.Black);

        }

        private void LocationField_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
                textBox.Text = "Location:";
            }
        }
        private void ProductionDateField_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            textBox.Text = textBox.Text.Replace("Production Date:", string.Empty);
            textBox.Foreground = new SolidColorBrush(Colors.Black);

        }

        private void ProductionDateField_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
                textBox.Text = "Production Date:";
            }
        }

        private void ExpirationDateField_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            textBox.Text = textBox.Text.Replace("Expiration Date:", string.Empty);
            textBox.Foreground = new SolidColorBrush(Colors.Black);

        }

        private void ExpirationDateField_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
                textBox.Text = "Expiration Date:";
            }
        }
        private void AddWindow_Click(object sender, RoutedEventArgs e)
        {
            string historyText;
            string[] fields = { NameField.Text, DescriptionField.Text, QuantityField.Text, LocationField.Text, ProductionDateField.Text, ExpirationDateField.Text };

            bool isAnyFieldEmpty = fields.Any(string.IsNullOrEmpty);

            if (isAnyFieldEmpty || fields.Any(field => field == "Name:" || field == "Description:" || field == "Quantity:" || field == "Location:" || field == "Production Date:" || field == "Expiration Date:"))
            {
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
                ErrorMessageTextBlock.Text = "Enter all details";
            }
            else
            {
                DateTime parsedProductionDate, parsedExpirationDate;

                try
                {
                    parsedProductionDate = DateTime.Parse(ProductionDateField.Text);
                    parsedExpirationDate = DateTime.Parse(ExpirationDateField.Text);
                }
                catch (Exception ex)
                {
                    ErrorMessageTextBlock.Visibility = Visibility.Visible;
                    ErrorMessageTextBlock.Text = "Wrong datetime";

                    return;
                }

                string productionDateParsed = parsedProductionDate.ToString("dd-MM-yyyy");
                string expirationDateParsed = parsedExpirationDate.ToString("dd-MM-yyyy");

                Database db = new Database();
                db.OpenConnection();
                DataTable findTable = db.FindMaterials(NameField.Text);

                if (!(findTable.Rows.Count > 0))
                {
                    bool added = db.AddMaterial(NameField.Text, DescriptionField.Text, QuantityField.Text, LocationField.Text, productionDateParsed, expirationDateParsed);

                    if (added)
                    {
                        historyText = $"Added to database material: {NameField.Text}";
                        db.AddHistory(username, historyText);

                        Log.Information($"{username} {historyText}");

                        ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
                        MessageBox.Show("Material added successfully");
                    }
                }
                else
                {
                    ErrorMessageTextBlock.Visibility = Visibility.Visible;
                    ErrorMessageTextBlock.Text = "Material already exists";
                }

                db.CloseConnection();
            }
        }
    }
}
