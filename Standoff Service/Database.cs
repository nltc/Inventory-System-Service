using MySql.Data.MySqlClient;
using Serilog;
using System;
using System.Data;

namespace Standoff_Service
{
    public class Database
    {
        private readonly MySqlConnection _connection;

        public Database()
        {
            _connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=localtest");
        }

        public void OpenConnection()
        {
            try
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
            }
            catch (Exception ex)
            {
                Log.Error("Error when opening a connection to the database: " + ex.Message);
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
            catch (Exception ex)
            {
                Log.Error("Error when closing database connection: " + ex.Message);
            }
        }

        public MySqlConnection GetConnection() => _connection;

        public DataTable SelectUser(string username, string password)
        {
            DataTable table = new DataTable();
            try
            {
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE login = @username AND password = @password", GetConnection()))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error when executing login request: " + ex.Message);
            }
            return table;
        }

        public bool RegistrateUser(string username, string password, string name)
        {
            string registrationDate = DateTime.UtcNow.ToString("dd.MM.yyyy");
            try
            {
                using (MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`Login`, `Password`, `Rights`, `Name`, `Registration_Date`) VALUES (@username, @password, 'Employee', @name, @registrationDate)", GetConnection()))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@registrationDate", registrationDate);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error when executing registration request: " + ex.Message);
                return false;
            }
        }

        public DataTable ShowMaterials()
        {
            DataTable table = new DataTable();
            try
            {
                using (MySqlCommand command = new MySqlCommand("SELECT Name, Description, Quantity, Location, Production_Date, Expiration_Date FROM nuclearmaterials", GetConnection()))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error when retrieving the material database: " + ex.Message);
            }
            return table;
        }

        public DataTable FindMaterials(string name)
        {
            DataTable table = new DataTable();
            try
            {
                using (MySqlCommand command = new MySqlCommand($"SELECT Name, Description, Quantity, Location, Production_Date, Expiration_Date FROM nuclearmaterials WHERE `Name` = '{name}'", GetConnection()))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error when receiving search material: " + ex.Message);
            }

            return table;
        }

        public bool AddMaterial(string name, string description, string quantity, string location, string production_date, string expiration_date)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("INSERT INTO `nuclearmaterials` (`Name`, `Description`, `Quantity`, `Location`, `Production_Date`, `Expiration_Date`) VALUES (@name, @description, @quantity, @location, @production_date, @expiration_date)", GetConnection()))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@location", location);
                    command.Parameters.AddWithValue("@production_date", production_date);
                    command.Parameters.AddWithValue("@expiration_date", expiration_date);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error adding material: " + ex.Message);
                return false;
            }
        }

        public bool DeleteMaterial(string name)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("DELETE FROM nuclearmaterials WHERE `Name` = @name", GetConnection()))
                {
                    command.Parameters.AddWithValue("@name", name);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error when deleting material: " + ex.Message);
                return false;
            }
        }

        public bool AddHistory(string user, string action)
        {
            string changeTime = DateTime.UtcNow.ToString("dd.MM.yyyy HH:mm:ss");
            try
            {
                using (MySqlCommand command = new MySqlCommand("INSERT INTO `history` (`User`, `Time`, `Action`) Values (@user, @changeTime, @action)", GetConnection()))
                {
                    command.Parameters.AddWithValue("@user", user);
                    command.Parameters.AddWithValue("@changeTime", changeTime);
                    command.Parameters.AddWithValue("@action", action);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error when changing change history: " + ex.Message);
                return false;
            }
        }

        public DataTable ShowPersons()
        {
            DataTable table = new DataTable();
            try
            {
                using (MySqlCommand command = new MySqlCommand("SELECT `Name`, `Rights`, `Registration_Date` FROM users", GetConnection()))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error in getting list of employees: " + ex.Message);
            }
            return table;
        }

        public DataTable ShowHistory()
        {
            DataTable table = new DataTable();
            try
            {
                using (MySqlCommand command = new MySqlCommand("SELECT `User`, `Time`, `Action` FROM history", GetConnection()))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error getting change history: " + ex.Message);
            }
            return table;
        }
    }
}
