﻿using Libreria.Model;
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
        private string tabella;
        private NegozioController negozioController;

        public UtenteController(DatabaseLibrary db, string tabellaUtente="Utente", string tabellaNegozio = "Negozio")
        {
            this.db = db;
            this.tabella = tabellaUtente;
            negozioController = new NegozioController(db, tabellaNegozio);
        }

        public IEnumerable GetUtenti()
        {
            try
            {
                string query = $"SELECT * FROM {tabella}";
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
                        Negozio = negozioController.GetNegozio(Convert.ToInt32(item["negozio"])),
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
                    Negozio = negozioController.GetNegozio(Convert.ToInt32(dt.Rows[0]["negozio"])),
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
                string query = $"DELETE FROM {tabella}";
                db.EseguiQuery(query);

                foreach (Utente item in utenti)
                {
                    query = $"INSERT INTO {tabella} (id, username, password, nome, cognome, email, telefono, indirizzo, citta, ruolo, negozio, note) " +
                        $"VALUES (@id, @username, @password, @nome, @cognome, @email, @telefono, @indirizzo, @citta, @ruolo, @negozio, @note)";
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
                    sqlParameters[10] = new SqlParameter("@negozio", item.Negozio.IdNegozio);
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
