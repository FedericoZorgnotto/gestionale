using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Model
{
    public enum Tipo
    {
        Magazzino,
        Negozio
    }
    public class Posizione
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string Telefono { get; set; }
        public Tipo Tipo { get; set; }
        public string Note { get; set; }

        public Posizione() { }

        public Posizione(int id, string nome, string indirizzo, string citta, string telefono,Tipo tipo, string note)
        {
            Id = id;
            Nome = nome;
            Indirizzo = indirizzo;
            Citta = citta;
            Telefono = telefono;
            Tipo = tipo;
            Note = note;
        }
    }


}
