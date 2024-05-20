namespace Libreria.Model
{
    public enum Ruoli
    {
        Amministratore,
        Commesso
    }
    public class Utente:Elemento
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
        public Posizione Posizione{ get; set; }
        public string Note { get; set; }
        
        public Utente():base() { }

        public Utente(int id, string username, string password, string nome, string cognome, string email, string telefono, string indirizzo, string citta, Ruoli ruolo, Posizione posizione, string note):base(id)
        {
            Username = username;
            Password = password;
            Nome = nome;
            Cognome = cognome;
            Email = email;
            Telefono = telefono;
            Indirizzo = indirizzo;
            Citta = citta;
            Ruolo = ruolo;
            Posizione = posizione;
            Note = note;
        }
    }

}
