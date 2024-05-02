using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ufficio.Componenti
{
    public partial class PaginaUtenti : UserControl
    {
        Libreria.DatabaseLibrary db;
        object dashboard;

        public PaginaUtenti(Libreria.DatabaseLibrary db, object dashboard)
        {
            InitializeComponent();
            this.db = db;
            this.dashboard = dashboard;
            caricaDgvUtenti();

        }

        private void caricaDgvUtenti()
        {
            Libreria.Controller.UtenteController utenteController = new Libreria.Controller.UtenteController(db, "Utenti");
            dgvUtenti.ItemsSource = utenteController.GetUtenti();
        }

        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            this.Content = dashboard;
        }

        private void btnSalva_Click(object sender, RoutedEventArgs e)
        {
            List<Libreria.Model.Utente> utenti = dgvUtenti.ItemsSource.Cast<Libreria.Model.Utente>().ToList();

            Libreria.Controller.UtenteController utenteController = new Libreria.Controller.UtenteController(db, "Utenti");
            utenteController.SalvaUtenti(utenti);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            dgvUtenti.Columns[0].Visibility = Visibility.Hidden;
            dgvUtenti.Columns[2].Visibility = Visibility.Hidden;
        }
    }
}
