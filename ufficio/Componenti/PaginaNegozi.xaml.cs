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
using Libreria;
using Libreria.Model;
using Libreria.Controller;
namespace ufficio.Componenti
{
    public partial class PaginaNegozi : UserControl
    {
        DatabaseLibrary db;
        object dashboard;

        public PaginaNegozi(DatabaseLibrary db, object dashboard)
        {
            InitializeComponent();
            this.db = db;
            this.dashboard = dashboard;
            caricaDgvNegozi();

        }
        private void caricaDgvNegozi()
        {
            NegozioController negozioController = new NegozioController(db, "Negozi");
            dgvNegozi.ItemsSource = negozioController.GetNegozi();
        }

        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            this.Content = dashboard;
        }

        private void btnSalva_Click(object sender, RoutedEventArgs e)
        {
            List<Negozio> negozi = dgvNegozi.ItemsSource.Cast<Negozio>().ToList();

            NegozioController negozioController = new NegozioController(db, "Negozi");
            negozioController.SalvaNegozi(negozi);
            MessageBox.Show("Impostazioni salvate correttamente");

        }
    }
}
