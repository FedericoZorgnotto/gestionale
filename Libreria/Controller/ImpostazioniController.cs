using Libreria.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Controller
{
    public class ImpostazioniController
    {
        private DatabaseLibrary db;
        private string tabella;

        public ImpostazioniController(DatabaseLibrary db, string tabella)
        {
            this.db = db;
            this.tabella = tabella;
        }

        public void SalvaImpostazioni()
        {
            try
            {
                string query = $"UPDATE {tabella} SET NomeAzienda = '{Impostazioni.NomeAzienda}', LogoBase64 = '{Impostazioni.logoBase64}'";
                db.EseguiQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       public void CaricaImpostazioni()
        {
            try
            {
                string query = $"SELECT * FROM {tabella}";
                DataTable dt = db.EseguiQuery(query);
                if (dt.Rows.Count > 0)
                {
                    Impostazioni.NomeAzienda = dt.Rows[0]["NomeAzienda"].ToString();
                    Impostazioni.logoBase64 = dt.Rows[0]["LogoBase64"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
