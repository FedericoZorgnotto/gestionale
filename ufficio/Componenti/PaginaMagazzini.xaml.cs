using Libreria.Controller;
using Libreria.Model;
using Libreria;
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

    public partial class PaginaMagazzini : UserControl
    {
        DatabaseLibrary db;
        object dashboard;

        public PaginaMagazzini(DatabaseLibrary db, object dashboard)
        {
            InitializeComponent();
            this.db = db;
            this.dashboard = dashboard;
            caricaDgvMagazzini();

        }
        private void caricaDgvMagazzini()
        {
            MagazzinoController magazzinoController = new MagazzinoController(db, "Magazzini");
            dgvMagazini.ItemsSource = magazzinoController.GetMagazzini();
        }

        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            this.Content = dashboard;
        }

        private void btnSalva_Click(object sender, RoutedEventArgs e)
        {
            List<Magazzino> magazzini = dgvMagazini.ItemsSource.Cast<Magazzino>().ToList();

            MagazzinoController magazzinoController = new MagazzinoController(db, "Magazzini");
            magazzinoController.SalvaMagazzini(magazzini);
        }
    }
}
