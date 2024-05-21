using Libreria.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Controller
{
    public class ProdottiController
    {
        private DatabaseLibrary db;
        private string nomeTabella;
        private string tabellaPosizioni;
        private PosizioneController posizioneController;
        public ProdottiController(DatabaseLibrary db, string nomeTabella, string tabellaPosizioni = "Posizioni")
        {
            this.db = db;
            this.nomeTabella = nomeTabella;
            this.tabellaPosizioni = tabellaPosizioni;
            posizioneController = new PosizioneController(db, tabellaPosizioni);
        }

        public void Aggiungi(Prodotto prodotto)
        {
            try
            {
                string query = $"INSERT INTO {nomeTabella} (nome, prezzo, note) VALUES (@nome, @prezzo, @note)";
                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@nome", prodotto.Nome);
                sqlParameters[1] = new SqlParameter("@prezzo", prodotto.Prezzo);
                sqlParameters[2] = new SqlParameter("@note", prodotto.Note);
                db.EseguiQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Elimina(Prodotto prodotto)
        {
            if (prodotto != null)
            {
                try
                {
                    string query = $"DELETE FROM {nomeTabella} WHERE id = @id";
                    SqlParameter[] sqlParameters = new SqlParameter[1];
                    sqlParameters[0] = new SqlParameter("@id", prodotto.Id);
                    db.EseguiQuery(query, sqlParameters);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public IEnumerable getAll()
        {
            try
            {
                string query = $"SELECT * FROM {nomeTabella}";
                DataTable dt = db.EseguiQuery(query);
                ArrayList prodotti = new ArrayList();
                foreach (DataRow item in dt.Rows)
                {
                    prodotti.Add(new Prodotto()
                    {
                        Id = (int)item["id"],
                        Nome = item["nome"].ToString(),
                        Prezzo = (decimal)item["prezzo"],
                        Note = item["note"].ToString()
                    });
                }
                return prodotti;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Prodotto getProdottoById(int id)
        {
            try
            {
                string query = $"SELECT * FROM {nomeTabella} WHERE id={id}";
                DataTable dt = db.EseguiQuery(query);
                
                return new Prodotto()
                {
                    Id = (int)dt.Rows[0]["id"],
                    Nome = dt.Rows[0]["nome"].ToString(),
                    Prezzo = (decimal)dt.Rows[0]["prezzo"],
                    Note = dt.Rows[0]["note"].ToString()
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Modifica(Prodotto prodotto)
        {
            try
            {
                string query = $"UPDATE {nomeTabella} SET nome = @nome, prezzo = @prezzo, note = @note WHERE id = @id";
                SqlParameter[] sqlParameters = new SqlParameter[4];
                sqlParameters[0] = new SqlParameter("@nome", prodotto.Nome);
                sqlParameters[1] = new SqlParameter("@prezzo", prodotto.Prezzo);
                sqlParameters[2] = new SqlParameter("@note", prodotto.Note);
                sqlParameters[3] = new SqlParameter("@id", prodotto.Id);
                db.EseguiQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
