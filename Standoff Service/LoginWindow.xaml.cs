﻿using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Serilog;

namespace Standoff_Service
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
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

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string loginUser = LoginField.Text;
            string passwordUser = PasswordField.Password;

            Database db = new Database();
            db.OpenConnection();
            DataTable table = db.SelectUser(loginUser, passwordUser);
            db.CloseConnection();

            if (table.Rows.Count > 0)
            {
                string rights = table.Rows[0]["Rights"].ToString();
                MainWindow mainWindow = new MainWindow(LoginField.Text, rights);
                mainWindow.Show();
                Log.Information($"{loginUser} logged in app");
                this.Close();
            }
            else
            {
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
                ErrorMessageTextBlock.Text = "Wrong username or password";
            }
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
        }
    }
}
