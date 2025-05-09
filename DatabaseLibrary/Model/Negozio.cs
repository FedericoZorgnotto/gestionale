﻿namespace Libreria.Model
{
    public class Negozio
    {
        public int IdNegozio { get; set; }
        public string NomeNegozio { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string Telefono { get; set; }
        public string Note { get; set; }

        public Negozio()
        {
        }

        public Negozio(int idNegozio, string nomeNegozio, string indirizzo, string citta, string telefono, string note)
        {
            IdNegozio = idNegozio;
            NomeNegozio = nomeNegozio;
            Indirizzo = indirizzo;
            Citta = citta;
            Telefono = telefono;
            Note = note;
        }
    }
}
