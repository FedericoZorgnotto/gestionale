using ComponentiGrafiche;
using System;
using System.Windows.Controls;

namespace ufficio
{
    /// <summary>
    /// Logica di interazione per UserControl1.xaml
    /// </summary>
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


            if(utente.Ruolo != Libreria.Model.Ruoli.Amministratore)
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
    }
}
