﻿using System;
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
    public partial class FindWindow : Window
    {
        public DataTable resultTable { get; private set; }
        public DataGrid input_grid { get; private set; }

        public FindWindow(DataGrid grid)
        {
            InitializeComponent();
            input_grid = grid;
        }
        private void FindBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            textBox.Text = textBox.Text.Replace("Название:", string.Empty);
            textBox.Foreground = new SolidColorBrush(Colors.Black);

        }

        private void FindBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
                textBox.Text = "Название:";
            }
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            string FindFieldText = FindField.Text;

            Database db = new Database();
            db.OpenConnection();
            DataTable table = db.FindMaterials(FindFieldText);
            db.CloseConnection();

            if (table.Rows.Count > 0)
            {
                input_grid.ItemsSource = table.DefaultView;
                this.Close();
            }
            else
            {
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
                ErrorMessageTextBlock.Text = "Нет такого материала";
            }
        }
    }
}
