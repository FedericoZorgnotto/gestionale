using Libreria.Model;
using Libreria.Utilities;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Libreria.Controller
{
    public class NegozioController
    {
        private DatabaseLibrary db;
        private string nomeTabella;

        public NegozioController(DatabaseLibrary db, string nomeTabella)
        {
            this.db = db;
            this.nomeTabella = nomeTabella;
        }

        public IEnumerable GetNegozi()
        {
            try
            {
                string query = $"SELECT * FROM {nomeTabella}";
                DataTable dt = db.EseguiQuery(query);
                ArrayList negozi = new ArrayList();
                foreach (DataRow item in dt.Rows)
                {
                    negozi.Add(new Negozio()
                    {
                        IdNegozio = Convert.ToInt32(item["idNegozio"]),
                        NomeNegozio = item["nomeNegozio"].ToString(),
                        Indirizzo = item["indirizzo"].ToString(),
                        Citta = item["citta"].ToString(),
                        Telefono = item["telefono"].ToString(),
                        Note = item["note"].ToString()
                    });
                }
                return negozi;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void SalvaNegozi(System.Collections.Generic.List<Utente> utenti)
        {
            try
            {
                string query = $"DELETE FROM {nomeTabella}";
                db.EseguiQuery(query);
                foreach (Utente item in utenti)
                {
                    query = $"INSERT INTO {nomeTabella} VALUES (@id, @nome, @indirizzo, @citta, @telefono, @note)";
                    SqlParameter[] sqlParameters = new SqlParameter[6];
                    sqlParameters[0] = new SqlParameter("@id", item.id);
                    sqlParameters[1] = new SqlParameter("@nome", item.Nome);
                    sqlParameters[2] = new SqlParameter("@indirizzo", item.Indirizzo);
                    sqlParameters[3] = new SqlParameter("@citta", item.Citta);
                    sqlParameters[4] = new SqlParameter("@telefono", item.Telefono);
                    sqlParameters[5] = new SqlParameter("@note", item.Note);
                    db.EseguiQuery(query, sqlParameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal Negozio GetNegozio(int id)
        {
            try
            {
                string query = $"SELECT * FROM {nomeTabella} WHERE idNegozio = @id";
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@id", id);
                DataTable dt = db.EseguiQuery(query, sqlParameters);
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                return new Negozio()
                {
                    IdNegozio = Convert.ToInt32(dt.Rows[0]["idNegozio"]),
                    NomeNegozio = dt.Rows[0]["nomeNegozio"].ToString(),
                    Indirizzo = dt.Rows[0]["indirizzo"].ToString(),
                    Citta = dt.Rows[0]["citta"].ToString(),
                    Telefono = dt.Rows[0]["telefono"].ToString(),
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
