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

        public void elimina(Prodotto prodotto)
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

        public IEnumerable getProdotti()
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
                        Quantita = (int)item["quantita"],
                        Posizione = posizioneController.GetPosizione((int)item["posizione"]),
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

        public IEnumerable getProdotti(Posizione posizione)
        {
            try
            {
                string query = $"SELECT * FROM {nomeTabella} WHERE posizione={posizione.Id.ToString()}";
                DataTable dt = db.EseguiQuery(query);
                ArrayList prodotti = new ArrayList();
                foreach (DataRow item in dt.Rows)
                {
                    prodotti.Add(new Prodotto()
                    {
                        Id = (int)item["id"],
                        Nome = item["nome"].ToString(),
                        Prezzo = (decimal)item["prezzo"],
                        Quantita = (int)item["quantita"],
                        Posizione = posizioneController.GetPosizione((int)item["posizione"]),
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

        public IEnumerable getProdottoById(int id)
        {
            try
            {
                string query = $"SELECT * FROM {nomeTabella} WHERE id={id}";
                DataTable dt = db.EseguiQuery(query);
                ArrayList prodotti = new ArrayList();
                foreach (DataRow item in dt.Rows)
                {
                    prodotti.Add(new Prodotto()
                    {
                        Id = (int)item["id"],
                        Nome = item["nome"].ToString(),
                        Prezzo = (decimal)item["prezzo"],
                        Quantita = (int)item["quantita"],
                        Posizione = posizioneController.GetPosizione((int)item["posizione"]),
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
    }
}
