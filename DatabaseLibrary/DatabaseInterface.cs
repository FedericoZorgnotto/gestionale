using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary
{
    internal interface DatabaseInterface
    {
        bool Connetti();
        void Disconnetti();
        /// <summary>
        /// esegue una query passata in input con i parametri passati in input 
        /// risolve problemi di sql injection
        /// </summary>
        /// <param name="query">query da eseguire</param>
        /// <param name="parameters">parametri della query</param>
        /// <returns>tabella con il risultato della query</returns>
        DataTable EseguiQuery(string query, SqlParameter[] parameters = null);
    }
}
