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
        private PosizioneController posizioneController;

        public UtenteController(DatabaseLibrary db, string tabellaUtente = "Utenti", string tabellaPosizioni = "Posizioni")
        {
            this.db = db;
            this.tabellaUtenti = tabellaUtente;
            posizioneController = new PosizioneController(db, tabellaPosizioni);
        }

        public void Aggiungi(Utente utente)
        {
            try
            {
                string query = $"INSERT INTO {tabellaUtenti} VALUES (@id, @username, @password, @nome, @cognome, @email, @telefono, @indirizzo, @citta, @ruolo, @posizione, @note)";
                SqlParameter[] sqlParameters = new SqlParameter[12];
                sqlParameters[0] = new SqlParameter("@id", utente.Id);
                sqlParameters[1] = new SqlParameter("@username", utente.Username);
                sqlParameters[2] = new SqlParameter("@password", utente.Password);
                sqlParameters[3] = new SqlParameter("@nome", utente.Nome);
                sqlParameters[4] = new SqlParameter("@cognome", utente.Cognome);
                sqlParameters[5] = new SqlParameter("@email", utente.Email);
                sqlParameters[6] = new SqlParameter("@telefono", utente.Telefono);
                sqlParameters[7] = new SqlParameter("@indirizzo", utente.Indirizzo);
                sqlParameters[8] = new SqlParameter("@citta", utente.Citta);
                sqlParameters[9] = new SqlParameter("@ruolo", utente.Ruolo);
                sqlParameters[10] = new SqlParameter("@posizione", utente.Posizione == null ? (object)DBNull.Value : utente.Posizione.Id);
                sqlParameters[11] = new SqlParameter("@note", utente.Note);
                db.EseguiQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Elimina(Utente utente)
        {
            if (utente != null)
            {
                try
                {
                    string query = $"DELETE FROM {tabellaUtenti} WHERE id = @id";
                    SqlParameter[] sqlParameters = new SqlParameter[1];
                    sqlParameters[0] = new SqlParameter("@id", utente.Id);
                    db.EseguiQuery(query, sqlParameters);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
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
                    int idPos;
                    int.TryParse(item["posizione"].ToString(), out idPos);
                    utenti.Add(new Utente()
                    {
                        Id = (int)item["id"],
                        Username = item["username"].ToString(),
                        Password = item["password"].ToString(),
                        Nome = item["nome"].ToString(),
                        Cognome = item["cognome"].ToString(),
                        Email = item["email"].ToString(),
                        Telefono = item["telefono"].ToString(),
                        Indirizzo = item["indirizzo"].ToString(),
                        Citta = item["citta"].ToString(),
                        Ruolo = (Ruoli)Enum.Parse(typeof(Ruoli), item["ruolo"].ToString()),
                        Posizione = posizioneController.GetById(idPos),
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
                    Id = (int)dt.Rows[0]["id"],
                    Username = dt.Rows[0]["username"].ToString(),
                    Password = dt.Rows[0]["password"].ToString(),
                    Nome = dt.Rows[0]["nome"].ToString(),
                    Cognome = dt.Rows[0]["cognome"].ToString(),
                    Email = dt.Rows[0]["email"].ToString(),
                    Telefono = dt.Rows[0]["telefono"].ToString(),
                    Indirizzo = dt.Rows[0]["indirizzo"].ToString(),
                    Citta = dt.Rows[0]["citta"].ToString(),
                    Ruolo = (Ruoli)Enum.Parse(typeof(Ruoli), dt.Rows[0]["ruolo"].ToString()),
                    Posizione = posizioneController.GetById(int.TryParse(dt.Rows[0]["posizione"].ToString(), out int valoreNegozio) ? Convert.ToInt32(dt.Rows[0]["posizione"]) : -1),
                    Note = dt.Rows[0]["note"].ToString()
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Modifica(Utente utente)
        {
            try
            {
                string query = $"UPDATE {tabellaUtenti} SET username = @username, password = @password, nome = @nome, cognome = @cognome, email = @email, telefono = @telefono, indirizzo = @indirizzo, citta = @citta, ruolo = @ruolo, posizione = @posizione, note = @note WHERE id = @id";
                SqlParameter[] sqlParameters = new SqlParameter[12];
                sqlParameters[0] = new SqlParameter("@id", utente.Id);
                sqlParameters[1] = new SqlParameter("@username", utente.Username);
                sqlParameters[2] = new SqlParameter("@password", utente.Password);
                sqlParameters[3] = new SqlParameter("@nome", utente.Nome);
                sqlParameters[4] = new SqlParameter("@cognome", utente.Cognome);
                sqlParameters[5] = new SqlParameter("@email", utente.Email);
                sqlParameters[6] = new SqlParameter("@telefono", utente.Telefono);
                sqlParameters[7] = new SqlParameter("@indirizzo", utente.Indirizzo);
                sqlParameters[8] = new SqlParameter("@citta", utente.Citta);
                sqlParameters[9] = new SqlParameter("@ruolo", utente.Ruolo);
                sqlParameters[10] = new SqlParameter("@posizione", utente.Posizione == null ? (object)DBNull.Value : utente.Posizione.Id);
                sqlParameters[11] = new SqlParameter("@note", utente.Note);
                db.EseguiQuery(query, sqlParameters);
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
                foreach (Utente item in utenti)
                {
                    string query = $"SELECT * FROM {tabellaUtenti} WHERE id = @id";
                    SqlParameter[] sqlParameters = new SqlParameter[1];
                    sqlParameters[0] = new SqlParameter("@id", item.Id);
                    DataTable dt = db.EseguiQuery(query, sqlParameters);
                    if (dt.Rows.Count == 0)
                        query = $"INSERT INTO {tabellaUtenti} VALUES (@id, @username, @password, @nome, @cognome, @email, @telefono, @indirizzo, @citta, @ruolo, @posizione, @note)";
                    else
                        query = $"UPDATE {tabellaUtenti} SET username = @username, password = @password, nome = @nome, cognome = @cognome, email = @email, telefono = @telefono, indirizzo = @indirizzo, citta = @citta, ruolo = @ruolo, posizione = @posizione, note = @note WHERE id = @id";
                    sqlParameters = new SqlParameter[12];
                    sqlParameters[0] = new SqlParameter("@id", item.Id);
                    sqlParameters[1] = new SqlParameter("@username", item.Username);
                    sqlParameters[2] = new SqlParameter("@password", item.Password);
                    sqlParameters[3] = new SqlParameter("@nome", item.Nome);
                    sqlParameters[4] = new SqlParameter("@cognome", item.Cognome);
                    sqlParameters[5] = new SqlParameter("@email", item.Email);
                    sqlParameters[6] = new SqlParameter("@telefono", item.Telefono);
                    sqlParameters[7] = new SqlParameter("@indirizzo", item.Indirizzo);
                    sqlParameters[8] = new SqlParameter("@citta", item.Citta);
                    sqlParameters[9] = new SqlParameter("@ruolo", item.Ruolo);
                    sqlParameters[10] = new SqlParameter("@posizione", item.Posizione == null ? (object)DBNull.Value : item.Posizione.Id);
                    sqlParameters[11] = new SqlParameter("@note", item.Note);
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
