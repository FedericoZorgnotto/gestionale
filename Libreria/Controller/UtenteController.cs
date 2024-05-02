using Libreria.Model;
using Libreria.Utilities;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Libreria.Controller
{
    public class UtenteController
    {
        private DatabaseLibrary db;
        private string tabellaUtenti;
        private NegozioController negozioController;
        private MagazzinoController magazzinoController;

        public UtenteController(DatabaseLibrary db, string tabellaUtente = "Utenti", string tabellaNegozio = "Negozi", string tabellaMagazzino="Magazzini")
        {
            this.db = db;
            this.tabellaUtenti = tabellaUtente;
            negozioController = new NegozioController(db, tabellaNegozio);
            magazzinoController = new MagazzinoController(db, tabellaMagazzino);
        }

        public IEnumerable GetUtenti()
        {
            try
            {
                string query = $"SELECT * FROM {tabellaUtenti}";
                DataTable dt = db.EseguiQuery(query);
                ArrayList utenti = new ArrayList();
                foreach (DataRow item in dt.Rows)
                {
                    utenti.Add(new Utente()
                    {
                        id = (int)item["id"],
                        Username = item["username"].ToString(),
                        Password = item["password"].ToString(),
                        Nome = item["nome"].ToString(),
                        Cognome = item["cognome"].ToString(),
                        Email = item["email"].ToString(),
                        Telefono = item["telefono"].ToString(),
                        Indirizzo = item["indirizzo"].ToString(),
                        Citta = item["citta"].ToString(),
                        Ruolo = (Ruoli)Enum.Parse(typeof(Ruoli), item["ruolo"].ToString()),
                        Negozio = negozioController.GetNegozio(int.TryParse(dt.Rows[0]["negozio"].ToString(), out int valoreNegozio) ? Convert.ToInt32(dt.Rows[0]["negozio"]) : -1),
                        Magazzino = magazzinoController.GetMagazzino(int.TryParse(dt.Rows[0]["magazzino"].ToString(), out int valoreMagazzino) ? Convert.ToInt32(dt.Rows[0]["magazzino"]) : -1),
                        Note = item["note"].ToString()
                    });
                }
                return utenti;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Utente Login(string username, string password)
        {
            password = HashUtility.CalcoloSHA1(password);
            try
            {
                string query = $"SELECT * FROM {tabellaUtenti} WHERE username = @username AND password = @password";
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@username", username);
                sqlParameters[1] = new SqlParameter("@password", password);
                DataTable dt = db.EseguiQuery(query, sqlParameters);
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                //errore qui
                return new Utente()
                {
                    id = (int)dt.Rows[0]["id"],
                    Username = dt.Rows[0]["username"].ToString(),
                    Password = dt.Rows[0]["password"].ToString(),
                    Nome = dt.Rows[0]["nome"].ToString(),
                    Cognome = dt.Rows[0]["cognome"].ToString(),
                    Email = dt.Rows[0]["email"].ToString(),
                    Telefono = dt.Rows[0]["telefono"].ToString(),
                    Indirizzo = dt.Rows[0]["indirizzo"].ToString(),
                    Citta = dt.Rows[0]["citta"].ToString(),
                    Ruolo = (Ruoli)Enum.Parse(typeof(Ruoli), dt.Rows[0]["ruolo"].ToString()),
                    Negozio = negozioController.GetNegozio(int.TryParse(dt.Rows[0]["negozio"].ToString(), out int valoreNegozio) ? Convert.ToInt32(dt.Rows[0]["negozio"]):-1),
                    Magazzino = magazzinoController.GetMagazzino(int.TryParse(dt.Rows[0]["magazzino"].ToString(), out int valoreMagazzino) ? Convert.ToInt32(dt.Rows[0]["magazzino"]) : -1),
                    Note = dt.Rows[0]["note"].ToString()
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SalvaUtenti(System.Collections.Generic.List<Utente> utenti)
        {
            try
            {
                string query = $"DELETE FROM {tabellaUtenti}";
                db.EseguiQuery(query);

                foreach (Utente item in utenti)
                {
                    query = $"INSERT INTO {tabellaUtenti} (id, username, password, nome, cognome, email, telefono, indirizzo, citta, ruolo, negozio, magazzino, note) " +
                        $"VALUES (@id, @username, @password, @nome, @cognome, @email, @telefono, @indirizzo, @citta, @ruolo, @negozio, @magazzino, @note)";
                    SqlParameter[] sqlParameters = new SqlParameter[12];
                    sqlParameters[0] = new SqlParameter("@id", Guid.NewGuid().ToString());
                    sqlParameters[1] = new SqlParameter("@username", item.Username);
                    sqlParameters[2] = new SqlParameter("@password", item.Password);
                    sqlParameters[3] = new SqlParameter("@nome", item.Nome);
                    sqlParameters[4] = new SqlParameter("@cognome", item.Cognome);
                    sqlParameters[5] = new SqlParameter("@email", item.Email);
                    sqlParameters[6] = new SqlParameter("@telefono", item.Telefono);
                    sqlParameters[7] = new SqlParameter("@indirizzo", item.Indirizzo);
                    sqlParameters[8] = new SqlParameter("@citta", item.Citta);
                    sqlParameters[9] = new SqlParameter("@ruolo", ((int)item.Ruolo));
                    sqlParameters[10] = new SqlParameter("@negozio", item.Negozio.Id);
                    sqlParameters[11] = new SqlParameter("@magazzino", item.Magazzino.Id);
                    sqlParameters[12] = new SqlParameter("@note", item.Note);
                    db.EseguiQuery(query, sqlParameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
