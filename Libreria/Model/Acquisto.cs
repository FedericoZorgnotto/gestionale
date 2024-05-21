using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Model
{
    public class Acquisto:Elemento
    {
        private DateTime data;
        private Prodotto prodotto;

        public DateTime Data { get => data; set => data = value; }
        public Prodotto Prodotto { get => prodotto; set => prodotto = value; }

        public Acquisto() : base()
        {
        }

        public Acquisto(int id, DateTime data, Prodotto prodotto) : base(id)
        {
            this.Data = data;
            this.Prodotto = prodotto;
        }
    }
}
