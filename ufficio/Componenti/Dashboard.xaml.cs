using ComponentiGrafiche;
using System;
using System.Windows.Controls;

//TODO:pagina per modificare informazioni catena di negozi: nome, logo, ...

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
    }
}
