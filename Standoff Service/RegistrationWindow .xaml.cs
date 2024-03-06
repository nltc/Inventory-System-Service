using MySql.Data.MySqlClient;
using System;
using System.Data;
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

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string loginUser = LoginField.Text;
            string passwordUser = PasswordField.Password;

            Database database = new Database();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            
            MySqlCommand command = new MySqlCommand($"SELECT * FROM users WHERE login = '{loginUser}' AND password = '{passwordUser}'", database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
                ErrorMessageTextBlock.Text = "Ебал я это восстанавливать";
            }
            else
            {
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
                ErrorMessageTextBlock.Text = "Ошибка йопта";
            }
        }
        private void Registration_Click(object sender, RoutedEventArgs e)
        { }
    }
}
