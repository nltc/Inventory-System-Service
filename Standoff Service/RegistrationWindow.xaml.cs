using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Standoff_Service
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void UsernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            textBox.Text = textBox.Text.Replace("Username:", string.Empty);
            textBox.Foreground = new SolidColorBrush(Colors.Black);

        }

        private void UsernameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
                textBox.Text = "Username:";
            }
        }

        private void FirstLastNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            textBox.Text = textBox.Text.Replace("First and last name:", string.Empty);
            textBox.Foreground = new SolidColorBrush(Colors.Black);

        }

        private void FirstLastNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
                textBox.Text = "First and last name:";
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordTextBox = (PasswordBox)sender;

            if (passwordTextBox.Password == "Password:")
            {
                passwordTextBox.Password = string.Empty;
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordTextBox = (PasswordBox)sender;

            if (string.IsNullOrEmpty(passwordTextBox.Password))
            {
                passwordTextBox.Password = "Password:";
            }
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            string loginUser = RegLoginField.Text;
            string firstLastName = FirstLastNameField.Text;
            string passwordUser = RegPasswordField.Password;
            string passwordUserRetry = RegPasswordFieldRetry.Password;

            string[] fields = { loginUser, passwordUser, passwordUserRetry, firstLastName };
            bool isAnyFieldEmpty = fields.Any(string.IsNullOrEmpty);

            if (isAnyFieldEmpty || fields.Any(field => field == "Username:" || field == "Password:" || field == "First and last name:"))
            {
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
                ErrorMessageTextBlock.Text = "Enter all details";
            }
            else if (passwordUser != passwordUserRetry)
            {
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
                ErrorMessageTextBlock.Text = "Password mismatch";
            }
            else
            {
                Database db = new Database();
                db.OpenConnection();
                bool registration = db.RegistrateUser(loginUser, passwordUser, firstLastName);
                db.CloseConnection();

                if (registration)
                {
                    ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
                    MessageBox.Show("Registration successful");
                    this.Close();
                }
            }
        }
    }
}
