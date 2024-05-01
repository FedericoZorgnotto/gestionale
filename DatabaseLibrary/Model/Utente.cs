namespace Libreria.Model
{
    public class Utente
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public Ruoli Ruolo { get; set; }
        public string Note { get; set; }
    }
}
