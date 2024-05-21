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
    public class MagazzinoController
    {
        private DatabaseLibrary db;
        private string table;

        public MagazzinoController(DatabaseLibrary db, string table = "magazzino")
        {
            this.db = db;
            this.table = table;
        }

        public void Aggiungi(Magazzino magazzino)
        {
            try
            {
                string query = "INSERT INTO "+table+" (idPosizione, quantita, idProdotto) VALUES (@idPosizione, @quantita, @idProdotto)";
                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@idPosizione", magazzino.Posizione.Id);
                sqlParameters[1] = new SqlParameter("@quantita", magazzino.Quantita);
                sqlParameters[2] = new SqlParameter("@idProdotto", magazzino.Prodotto.Id);
                db.EseguiQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public IEnumerable GetAll()
        {
            try
            {
                string query = "SELECT * FROM " + table;
                DataTable data = db.EseguiQuery(query);
                List<Magazzino> magazzini = new List<Magazzino>();
                foreach (DataRow riga in data.Rows)
                {
                    Magazzino magazzino = new Magazzino();
                    magazzino.Id = Convert.ToInt32(riga["id"]);
                    magazzino.Posizione = new PosizioneController(db, "Posizioni").GetById(Convert.ToInt32(riga["idPosizione"]));
                    magazzino.Quantita = Convert.ToInt32(riga["quantita"]);
                    magazzino.Prodotto = new ProdottiController(db, "Prodotti").getProdottoById(Convert.ToInt32(riga["idProdotto"]));
                    magazzini.Add(magazzino);
                }
                return magazzini;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }

        public IEnumerable GetByPosizione(Posizione posizione)
        {
            try
            {
                string query = "SELECT * FROM "+table+" WHERE idPosizione = @idPosizione";
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@idPosizione", posizione.Id);
                DataTable data = db.EseguiQuery(query, sqlParameters);
                List<Magazzino> magazzini = new List<Magazzino>();
                foreach (DataRow riga in data.Rows)
                {
                    Magazzino magazzino = new Magazzino();
                    magazzino.Id = Convert.ToInt32(riga["id"]);
                    magazzino.Prodotto = new ProdottiController(db, "Prodotti").getProdottoById(Convert.ToInt32(riga["idProdotto"]));
                    magazzino.Posizione = new PosizioneController(db, "Posizioni").GetById(Convert.ToInt32(riga["idPosizione"]));
                    magazzino.Quantita = Convert.ToInt32(riga["quantita"]);
                    magazzini.Add(magazzino);
                }
                return magazzini;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public void Modifica(Magazzino magazzino)
        {
            try
            {
                string query = "UPDATE "+table+" SET idPosizione = @idPosizione, quantita = @quantita, idProdotto = @idProdotto WHERE id = @id";
                SqlParameter[] sqlParameters = new SqlParameter[4];
                sqlParameters[0] = new SqlParameter("@idPosizione", magazzino.Posizione.Id);
                sqlParameters[1] = new SqlParameter("@quantita", magazzino.Quantita);
                sqlParameters[2] = new SqlParameter("@idProdotto", magazzino.Prodotto.Id);
                sqlParameters[3] = new SqlParameter("@id", magazzino.Id);
                db.EseguiQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
