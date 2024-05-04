namespace Libreria.Model
{
    public class Negozio
    {
        public int Id { get; set; }
        public string nome { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string Telefono { get; set; }
        public string Note { get; set; }

        public Negozio()
        {   
        }

        public Negozio(int idNegozio, string nomeNegozio, string indirizzo, string citta, string telefono, string note)
        {
            Id = idNegozio;
            nome = nomeNegozio;
            Indirizzo = indirizzo;
            Citta = citta;
            Telefono = telefono;
            Note = note;
        }
    }
}
