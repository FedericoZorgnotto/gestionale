using ComponentiGrafiche;
using System;
using System.Windows.Controls;

//TODO: pagina prodotti: possibilità di aggiungere un'ordine, visualizzazione per ordini,
//modificare il prezzo dei prodotti, chiederne lo spostamento e
//stabilire quanti prodotti devono esser presenti per ogni punto vendita

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
            var dashboard = this.Content;
            this.Content = new PaginaUtenti(db, dashboard);
        }

        private void btnMagazini_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var dashboard = this.Content;
            this.Content = new PaginaMagazzini(db, dashboard);
        }

        private void btnNegozi_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var dashboard = this.Content;
            this.Content = new PaginaNegozi(db, dashboard);
        }

        private void btnImpostazioni_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var dashboard = this.Content;
            this.Content = new PaginaImpostazioni(db, dashboard);
        }
    }
}
