using Libreria.Controller;
using Libreria.Model;
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

namespace ufficio.Componenti.inventario
{
    /// <summary>
    /// Logica di interazione per PaginaInventarioAggiungi.xaml
    /// </summary>
    public partial class PaginaInventarioAggiungi : UserControl
    {
        private Libreria.DatabaseLibrary db;
        private object dashboard;
        ProdottiController prodottiController;
        PosizioneController posizioneController;
        public PaginaInventarioAggiungi(Libreria.DatabaseLibrary db, object dashboard)
        {
            this.db = db;
            this.dashboard = dashboard;
            InitializeComponent();

            prodottiController = new ProdottiController(db, "Prodotti");
            posizioneController = new PosizioneController(db, "Posizioni");

            cmbProdotto.ItemsSource = prodottiController.getAll();
            cmbProdotto.DisplayMemberPath = "Nome";
            cmbProdotto.SelectedValuePath = "Id";

            cmbPosizione.ItemsSource = posizioneController.GetAll();
            cmbPosizione.DisplayMemberPath = "Nome";
            cmbPosizione.SelectedValuePath = "Id";
        }

        private void btnSalva_Click(object sender, RoutedEventArgs e)
        {
            if(txtQuantita.Text == "")
            {
                MessageBox.Show("Inserire la quantità");
                return;
            }
            if(cmbPosizione.SelectedItem == null)
            {
                MessageBox.Show("Selezionare la posizione");
                return;
            }
            if(cmbProdotto.SelectedItem == null)
            {
                MessageBox.Show("Selezionare il prodotto");
                return;
            }
            Magazzino magazzino = new Magazzino();
            magazzino.Prodotto = (Prodotto)cmbProdotto.SelectedItem;
            magazzino.Quantita = Convert.ToInt32(txtQuantita.Text);
            magazzino.Posizione = (Posizione)cmbPosizione.SelectedItem;

            MagazzinoController magazzinoController = new MagazzinoController(db, "Magazzino");
            magazzinoController.Aggiungi(magazzino);

            PaginaInventario paginaInventario = new PaginaInventario(db, dashboard);
            this.Content = paginaInventario;

        }

        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {

            PaginaInventario paginaInventario = new PaginaInventario(db, dashboard);
            this.Content = paginaInventario;
        }
    }
}
