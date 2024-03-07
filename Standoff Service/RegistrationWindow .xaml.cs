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

            textBox.Text = textBox.Text.Replace("Логин:", string.Empty);
            textBox.Foreground = new SolidColorBrush(Colors.Black);

        }

        private void UsernameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
                textBox.Text = "Логин:";
            }
        }


        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordTextBox = (PasswordBox)sender;

            if (passwordTextBox.Password == "Пароль:")
            {
                passwordTextBox.Password = string.Empty;
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordTextBox = (PasswordBox)sender;

            if (string.IsNullOrEmpty(passwordTextBox.Password))
            {
                passwordTextBox.Password = "Пароль:";
            }
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            string loginUser = RegLoginField.Text;
            string passwordUser = RegPasswordField.Password;
            string passwordUserRetry = RegPasswordFieldRetry.Password;

            string[] fields = { loginUser, passwordUser, passwordUserRetry };
            bool isAnyFieldEmpty = fields.Any(string.IsNullOrEmpty);

            if (isAnyFieldEmpty || fields.Any(field => field == "Логин:" || field == "Пароль:"))
            {
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
                ErrorMessageTextBlock.Text = "Введите все данные";
            }
            else if (passwordUser != passwordUserRetry)
            {
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
                ErrorMessageTextBlock.Text = "Пароли не совпадают";
            }
            else
            {
                Database db = new Database();
                db.OpenConnection();
                bool registration = db.RegistrateUser(loginUser, passwordUser);
                db.CloseConnection();

                if (registration)
                {
                    ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
                    MessageBox.Show("Регистрация прошла успешно");
                    this.Close();
                }
            }
        }
    }
}
