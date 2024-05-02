using Libreria.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Libreria.Controller
{
    public class MagazzinoController
    {
        private Libreria.DatabaseLibrary db;
        private string nomeTabella;

        public MagazzinoController(Libreria.DatabaseLibrary db, string nomeTabella)
        {
            this.db = db;
            this.nomeTabella = nomeTabella;
        }

        public IEnumerable GetMagazzini()
        {
            try
            {
                string query = $"SELECT * FROM {nomeTabella}";
                DataTable dt = db.EseguiQuery(query);
                ArrayList magazzini = new ArrayList();
                foreach (DataRow item in dt.Rows)
                {
                    magazzini.Add(new Magazzino()
                    {
                        Id = Convert.ToInt32(item["id"]),
                        Nome = item["nome"].ToString(),
                        Indirizzo = item["indirizzo"].ToString(),
                        Citta = item["citta"].ToString(),
                        Telefono = item["telefono"].ToString(),
                        Note = item["note"].ToString()
                    });
                }
                return magazzini;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void SalvaMagazzini(List<Magazzino> magazzini)
        {
            try
            {
                string query = $"DELETE FROM {nomeTabella}";
                db.EseguiQuery(query);
                foreach (Magazzino item in magazzini)
                {
                    query = $"INSERT INTO {nomeTabella} VALUES (@id, @nome, @indirizzo, @citta, @telefono, @note)";
                    SqlParameter[] sqlParameters = new SqlParameter[6];
                    sqlParameters[0] = new SqlParameter("@id", item.Id);
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

        internal Magazzino GetMagazzino(int id)
        {
            if (id == -1)
            {
                return null;
            }
            try
            {
                string query = $"SELECT * FROM {nomeTabella} WHERE id = @id";
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@id", id);
                DataTable dt = db.EseguiQuery(query, sqlParameters);
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                return new Magazzino()
                {
                    Id = Convert.ToInt32(dt.Rows[0]["idNegozio"]),
                    Nome = dt.Rows[0]["nomeNegozio"].ToString(),
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
