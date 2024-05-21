using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Model
{
    public class Magazzino: Elemento
    {
        public Prodotto Prodotto { get; set; }
        public int Quantita { get; set; }
        public Posizione Posizione { get; set; }

        public Magazzino(): base()
        {
        }

        public Magazzino(int id, Prodotto prodotto, int quantita, Posizione posizione): base(id)
        {
            Prodotto = prodotto;
            Quantita = quantita;
            Posizione = posizione;
        }
    }
}
