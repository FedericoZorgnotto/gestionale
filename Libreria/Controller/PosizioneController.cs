using Libreria.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Controller
{
    public class PosizioneController
    {
        private DatabaseLibrary db;
        private string nomeTabella;

        public PosizioneController(DatabaseLibrary db, string nomeTabella)
        {
            this.db = db;
            this.nomeTabella = nomeTabella;
        }

        public void Elimina(Posizione posizione)
        {
            if (posizione != null)
            {
                try
                {
                    string query = $"DELETE FROM {nomeTabella} WHERE id = @id";
                    SqlParameter[] sqlParameters = new SqlParameter[1];
                    sqlParameters[0] = new SqlParameter("@id", posizione.Id);
                    db.EseguiQuery(query, sqlParameters);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public IEnumerable GetAll()
        {
            try
            {
                string query = $"SELECT * FROM {nomeTabella}";
                DataTable dt = db.EseguiQuery(query);
                ArrayList posizioni = new ArrayList();
                foreach (DataRow item in dt.Rows)
                {
                    posizioni.Add(new Posizione()
                    {
                        Id = Convert.ToInt32(item["id"]),
                        Nome = item["nome"].ToString(),
                        Indirizzo = item["indirizzo"].ToString(),
                        Citta = item["citta"].ToString(),
                        Telefono = item["telefono"].ToString(),
                        Tipo = (Tipo)item["tipo"],
                        Note = item["note"].ToString()
                    });
                }
                return posizioni;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SalvaPosizioni(List<Posizione> posizioni)
        {
            try
            {
                foreach (Posizione item in posizioni)
                {
                    string query = $"SELECT * FROM {nomeTabella} WHERE id = @id";
                    SqlParameter[] sqlParameters = new SqlParameter[1];
                    sqlParameters[0] = new SqlParameter("@id", item.Id);
                    DataTable dt = db.EseguiQuery(query, sqlParameters);
                    if (dt.Rows.Count == 0)
                        query = $"INSERT INTO {nomeTabella} VALUES (@id, @nome, @indirizzo, @citta, @telefono, @tipo, @note)";
                    else
                        query = $"UPDATE {nomeTabella} SET nome = @nome, indirizzo = @indirizzo, citta = @citta, telefono = @telefono, tipo = @tipo, note = @note WHERE id = @id";
                    sqlParameters = new SqlParameter[6];
                    sqlParameters[0] = new SqlParameter("@id", item.Id);
                    sqlParameters[1] = new SqlParameter("@nome", item.Nome);
                    sqlParameters[2] = new SqlParameter("@indirizzo", item.Indirizzo);
                    sqlParameters[3] = new SqlParameter("@citta", item.Citta);
                    sqlParameters[4] = new SqlParameter("@telefono", item.Telefono);
                    sqlParameters[5] = new SqlParameter("@tipo", (int)item.Tipo);
                    sqlParameters[6] = new SqlParameter("@note", item.Note);
                    db.EseguiQuery(query, sqlParameters);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Posizione GetPosizione(int id)
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
                return new Posizione()
                {
                    Id = Convert.ToInt32(dt.Rows[0]["id"]),
                    Nome = dt.Rows[0]["nome"].ToString(),
                    Indirizzo = dt.Rows[0]["indirizzo"].ToString(),
                    Citta = dt.Rows[0]["citta"].ToString(),
                    Telefono = dt.Rows[0]["telefono"].ToString(),
                    Tipo = (Tipo)dt.Rows[0]["tipo"],
                    Note = dt.Rows[0]["note"].ToString()
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable GetByTipo(Tipo tipo)
        {
            try
            {
                string query = $"SELECT * FROM {nomeTabella} WHERE tipo = @tipo";
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("tipo", tipo);
                DataTable dt = db.EseguiQuery(query, sqlParameters);
                ArrayList posizioni = new ArrayList();
                foreach (DataRow item in dt.Rows)
                {
                    posizioni.Add(new Posizione()
                    {
                        Id = Convert.ToInt32(item["id"]),
                        Nome = item["nome"].ToString(),
                        Indirizzo = item["indirizzo"].ToString(),
                        Citta = item["citta"].ToString(),
                        Telefono = item["telefono"].ToString(),
                        Tipo = (Tipo)item["tipo"],
                        Note = item["note"].ToString()
                    });
                }
                return posizioni;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void Modifica(Posizione posizione)
        {
            try
            {
                string query = $"UPDATE {nomeTabella} SET nome = @nome, indirizzo = @indirizzo, citta = @citta, telefono = @telefono, tipo = @tipo, note = @note WHERE id = @id";
                SqlParameter[] sqlParameters = new SqlParameter[7];
                sqlParameters[0] = new SqlParameter("@id", posizione.Id);
                sqlParameters[1] = new SqlParameter("@nome", posizione.Nome);
                sqlParameters[2] = new SqlParameter("@indirizzo", posizione.Indirizzo);
                sqlParameters[3] = new SqlParameter("@citta", posizione.Citta);
                sqlParameters[4] = new SqlParameter("@telefono", posizione.Telefono);
                sqlParameters[5] = new SqlParameter("@tipo", (int)posizione.Tipo);
                sqlParameters[6] = new SqlParameter("@note", posizione.Note);
                db.EseguiQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Aggiungi(Posizione posizione)
        {
            try
            {
                string query = $"INSERT INTO {nomeTabella} VALUES (@id, @nome, @indirizzo, @citta, @telefono, @tipo, @note)";
                SqlParameter[] sqlParameters = new SqlParameter[7];
                sqlParameters[0] = new SqlParameter("@id", posizione.Id);
                sqlParameters[1] = new SqlParameter("@nome", posizione.Nome);
                sqlParameters[2] = new SqlParameter("@indirizzo", posizione.Indirizzo);
                sqlParameters[3] = new SqlParameter("@citta", posizione.Citta);
                sqlParameters[4] = new SqlParameter("@telefono", posizione.Telefono);
                sqlParameters[5] = new SqlParameter("@tipo", (int)posizione.Tipo);
                sqlParameters[6] = new SqlParameter("@note", posizione.Note);
                db.EseguiQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
