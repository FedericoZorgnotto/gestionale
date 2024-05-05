using Libreria.Model;
using System;
using System.Collections;
using System.Collections.Generic;
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

        public void EliminaNegozio(Negozio negozio)
        {
            if (negozio != null)
            {
                try
                {
                    string query = $"DELETE FROM {nomeTabella} WHERE id = @id";
                    SqlParameter[] sqlParameters = new SqlParameter[1];
                    sqlParameters[0] = new SqlParameter("@id", negozio.Id);
                    db.EseguiQuery(query, sqlParameters);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
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
                        Id = Convert.ToInt32(item["id"]),
                        nome = item["nome"].ToString(),
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
        public void SalvaNegozi(List<Negozio> negozi)
        {
            try
            {
                foreach (Negozio item in negozi)
                {
                    string query = $"SELECT * FROM {nomeTabella} WHERE id = @id";
                    SqlParameter[] sqlParameters = new SqlParameter[1];
                    sqlParameters[0] = new SqlParameter("@id", item.Id);
                    DataTable dt = db.EseguiQuery(query, sqlParameters);
                    if (dt.Rows.Count == 0)
                        query = $"INSERT INTO {nomeTabella} VALUES (@id, @nome, @indirizzo, @citta, @telefono, @note)";
                    else
                        query = $"UPDATE {nomeTabella} SET nome = @nome, indirizzo = @indirizzo, citta = @citta, telefono = @telefono, note = @note WHERE id = @id";
                    sqlParameters = new SqlParameter[6];
                    sqlParameters[0] = new SqlParameter("@id", item.Id);
                    sqlParameters[1] = new SqlParameter("@nome", item.nome);
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
                return new Negozio()
                {
                    Id = Convert.ToInt32(dt.Rows[0]["id"]),
                    nome = dt.Rows[0]["nome"].ToString(),
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
