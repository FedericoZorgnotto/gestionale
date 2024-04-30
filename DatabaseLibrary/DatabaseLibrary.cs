using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DatabaseLibrary
{
    public class DatabaseLibrary : DatabaseInterface
    {
        private SqlConnection _sqlConnection;
        private MySqlConnection _mySqlConnection;
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// costruttore della classe per la connessione al file di database
        /// </summary>
        /// <param name="database"></param>
        /// <param name="location"></param>
        public DatabaseLibrary(string database, string location)
        {
            _sqlConnection = new SqlConnection();
            _sqlConnection.ConnectionString = $"Data Source={database};AttachDbFilename={location};Integrated Security=True";
            ErrorMessage = null;
        }

        /// <summary>
        /// costuttore della classe per la connessione al database MySql
        /// </summary>
        /// <param name="server"></param>"
        /// <param name="database"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public DatabaseLibrary(string server, string port, string database, string username, string password)
        {
            _mySqlConnection = new MySqlConnection();
            _mySqlConnection.ConnectionString = $"Server={server};Port={port};Database={database};Uid={username};Pwd={password}";
            ErrorMessage = null;
        }

        public bool Connetti()
        {
            if(_sqlConnection != null)
            {
                try
                {
                    if (_sqlConnection.State == System.Data.ConnectionState.Closed)
                    {
                        _sqlConnection.Open();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Errore nell'apertura della connessione: {ex.Message}";
                    return false;
                }
            }
            else
            {
                try
                {
                    if (_mySqlConnection.State == System.Data.ConnectionState.Closed)
                    {
                        _mySqlConnection.Open();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Errore nell'apertura della connessione: {ex.Message}";
                    return false;
                }
            }
        }
        public void Disconnetti()
        {
            if (_sqlConnection != null)
            {
                if (_sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    _sqlConnection.Close();
                }
            }
            else
            {
                if (_mySqlConnection.State == System.Data.ConnectionState.Open)
                {
                    _mySqlConnection.Close();
                }
            }
        }


        public DataTable EseguiQuery(string query, SqlParameter[] parameters = null)
        {
            Connetti();
            DataTable dataTable = new DataTable();
            
            if (_mySqlConnection != null)
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, _sqlConnection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Errore nell'esecuzione della query: {ex.Message}";
                }
                finally
                {
                    Disconnetti();
                }
            }
            else
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand(query, _mySqlConnection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Errore nell'esecuzione della query: {ex.Message}";
                }
                finally
                {
                    Disconnetti();
                }
            }

            return dataTable;
        }
    }
}
