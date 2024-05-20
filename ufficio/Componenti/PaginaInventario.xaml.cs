using Libreria;
using Libreria.Controller;
using Libreria.Model;
using System;
using System.Collections;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ufficio.Componenti
{
    /// <summary>
    /// Logica di interazione per PaginaInventario.xaml
    /// </summary>
    public partial class PaginaInventario : UserControl
    {
        object dashboard;
        DatabaseLibrary db;
        private Posizione negozio = null;
        private ProdottiController ProdottiController;
        public PaginaInventario(DatabaseLibrary db, object dashboard)  //, Tipo tipo
        {
            this.dashboard = dashboard;
            this.db = db;
            ProdottiController = new ProdottiController(db, "Prodotti");
            InitializeComponent();
            caricaDgvInventario();
        }

        public PaginaInventario(DatabaseLibrary db, object dashboard, Posizione negozio) : this(db, dashboard)
        {
            this.dashboard = dashboard;
            this.db = db;
            this.negozio = negozio;
            ProdottiController = new ProdottiController(db, "Prodotti");
            InitializeComponent();
            caricaDgvInventario();
        }

        private void caricaDgvInventario()
        {
            if (negozio != null)
            {
                dgvInventario.ItemsSource = ProdottiController.getProdotti(negozio);
            }
            else
            {
                dgvInventario.ItemsSource = ProdottiController.getProdotti();
            }
        }


        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            this.Content = dashboard;
        }

        private void btnSalva_Click(object sender, RoutedEventArgs e)
        {
            List<Posizione> negozi = dgvInventario.ItemsSource.Cast<Posizione>().ToList();

            PosizioneController posizioneController = new PosizioneController(db, "Negozi");
            posizioneController.SalvaPosizioni(negozi);
            MessageBox.Show("Impostazioni salvate correttamente");

        }
    }
}
