using System.Windows.Converters;

namespace Libreria.Model
{
    public class Prodotto:Elemento
    {
        public string Nome { get; set; }
        public decimal Prezzo { get; set; }
        public string Note { get; set; }

        public Prodotto():base() { }

        public Prodotto(int id, string nome, decimal prezzo, string note):base(id)
        {
            Nome = nome;
            Prezzo = prezzo;
            Note = note;
        }
    }
}
