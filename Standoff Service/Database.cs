using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Standoff_Service
{
    class Database
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=localtest");

        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public MySqlConnection GetConnection() { return connection; }

        public DataTable SelectUser(string username, string password)
        {
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand($"SELECT * FROM users WHERE login = '{username}' AND password = '{password}'", GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }


        public bool RegistrateUser(string username, string password, string name)
        {
            MySqlCommand command = new MySqlCommand($"INSERT INTO `users` (`Login`, `Password`, `Rights`, `Name`, `Registration_Date`) VALUES ('{username}', '{password}', 'Employee', '{name}', NOW());", GetConnection());
            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }

        public DataTable ShowMaterials()
        {
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM nuclearmaterials", GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        public DataTable FindMaterials(string name)
        {
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand($"SELECT * FROM nuclearmaterials WHERE `Name` = '{name}'", GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        public bool AddMaterial(string name, string description, string quantity, string location, string production_date, string expiration_date)
        {

            MySqlCommand command = new MySqlCommand(
                $"INSERT INTO `nuclearmaterials` (`Name`, `Description`, `Quantity`, `Location`, `Production_Date`, `Expiration_Date`) VALUES ('{name}', '{description}', '{quantity}', '{location}', '{production_date}', '{expiration_date}');", GetConnection());
            int rowsAffected = command.ExecuteNonQuery();


            return rowsAffected > 0;
        }

        public DataTable ShowPersons()
        {
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT `Name`, `Rights`, `Registration_Date` FROM users", GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }
    }
}
