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
    public partial class AddWindow : Window
    {
        public DataTable resultTable { get; private set; }
        public DataGrid input_grid { get; private set; }

        public AddWindow(DataGrid grid)
        {
            InitializeComponent();
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
            string NameFieldText = NameField.Text;
            string DescriptionFieldText = DescriptionField.Text;
            string QuantityFieldText = QuantityField.Text;
            string LocationFieldText = LocationField.Text;
            string ProductionDateFieldText = ProductionDateField.Text;
            string ExpirationDateFieldText = ExpirationDateField.Text;
            string ProductionDateFieldParsed = "";
            string ExpirationDateFieldParsed = "";
            DateTime parsedProductionDate;
            DateTime parsedExpirationDate;

            string[] fields = { NameFieldText, DescriptionFieldText, QuantityFieldText, LocationFieldText, ProductionDateFieldText, ExpirationDateFieldText };
            bool isAnyFieldEmpty = fields.Any(string.IsNullOrEmpty);

            if (isAnyFieldEmpty || fields.Any(field => field == "Name:" || field == "Description:" || field == "Quantity:" || field == "Location:" || field == "Production Date:" || field == "Expiration Date:"))
            {
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
                ErrorMessageTextBlock.Text = "Enter all details";
            }
            else
            {
                try
                {
                    parsedProductionDate = DateTime.Parse(ProductionDateFieldText);
                    ProductionDateFieldParsed = parsedProductionDate.ToString("dd-MM-yyyy");

                    parsedExpirationDate = DateTime.Parse(ExpirationDateFieldText);
                    ExpirationDateFieldParsed = parsedExpirationDate.ToString("dd-MM-yyyy");
                }
                catch (Exception ex)
                {
                    ErrorMessageTextBlock.Visibility = Visibility.Visible;
                    ErrorMessageTextBlock.Text = "Wrong datetime";
                }

                if (!(string.IsNullOrEmpty(ProductionDateFieldParsed) || string.IsNullOrEmpty(ExpirationDateFieldParsed)))
                {
                    Database db = new Database();
                    db.OpenConnection();
                    bool added = db.AddMaterial(NameFieldText, DescriptionFieldText, QuantityFieldText, LocationFieldText, ProductionDateFieldParsed, ExpirationDateFieldParsed);
                    db.CloseConnection();

                    if (added)
                    {
                        ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
                        MessageBox.Show("Material added successful");
                        this.Close();
                    }
                }
            }
        }
    }
}
