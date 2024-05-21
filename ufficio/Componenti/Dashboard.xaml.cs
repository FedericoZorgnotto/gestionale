using System;
using System.Windows.Controls;

namespace ufficio.Componenti
{
    public partial class Dashboard : UserControl
    {
        private Libreria.DatabaseLibrary db;
        private Libreria.Model.Utente utente;

        public Action logout { get; internal set; }

        public Dashboard(Libreria.DatabaseLibrary db, Libreria.Model.Utente utente)
        {
            InitializeComponent();
            this.db = db;
            this.utente = utente;


            if (utente.Ruolo != Libreria.Model.Ruoli.Amministratore)
            {
                btnUtenti.Visibility = System.Windows.Visibility.Hidden;
                btnImpostazioni.Visibility = System.Windows.Visibility.Hidden;

            }
        }

        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            logout?.Invoke();
        }

        private void btnUtenti_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            object dashboard = this.Content;
            this.Content = new utenti.PaginaUtenti(db, dashboard);
        }

        private void btnPosizioni_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            object dashboard = this.Content;
            this.Content = new posizioni.PaginaPosizioni(db, dashboard);
        }


        private void btnImpostazioni_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            object dashboard = this.Content;
            this.Content = new PaginaImpostazioni(db, dashboard);
        }

        private void btnProdotti_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            object dashboard = this.Content;
            Content = new prodotti.PaginaProdotti(db, dashboard);
        }
    }
}
