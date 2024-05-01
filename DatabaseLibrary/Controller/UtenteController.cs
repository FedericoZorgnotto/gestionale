using Libreria.Model;
using Libreria.Utilities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Libreria.Controller
{
    public class UtenteController
    {
        private DatabaseLibrary db;
        private string tabella;

        public UtenteController(DatabaseLibrary db, string tabella)
        {
            this.db = db;
            this.tabella = tabella;
        }

        public Utente Login(string username, string password)
        {
            password = HashUtility.calcoloSHA1(password);
            try
            {
                string query = $"SELECT * FROM {tabella} WHERE username = @username AND password = @password";
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@username", username);
                sqlParameters[1] = new SqlParameter("@password", password);
                DataTable dt = db.EseguiQuery(query, sqlParameters);
                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                return new Utente()
                {
                    Username = dt.Rows[0]["username"].ToString(),
                    Password = dt.Rows[0]["password"].ToString(),
                    Nome = dt.Rows[0]["nome"].ToString(),
                    Cognome = dt.Rows[0]["cognome"].ToString(),
                    Email = dt.Rows[0]["email"].ToString(),
                    Telefono = dt.Rows[0]["telefono"].ToString(),
                    Indirizzo = dt.Rows[0]["indirizzo"].ToString(),
                    Citta = dt.Rows[0]["citta"].ToString(),
                    Ruolo = (Ruoli)Enum.Parse(typeof(Ruoli), dt.Rows[0]["ruolo"].ToString()),
                    Note = dt.Rows[0]["note"].ToString()
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
