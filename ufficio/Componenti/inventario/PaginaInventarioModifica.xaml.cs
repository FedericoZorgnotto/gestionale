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
    /// Logica di interazione per PaginaInventarioModifica.xaml
    /// </summary>
    public partial class PaginaInventarioModifica : UserControl
    {
        private Libreria.DatabaseLibrary db;
        private object dashboard;
        ProdottiController prodottiController;
        PosizioneController posizioneController;
        Magazzino magazzino;
        public PaginaInventarioModifica(Libreria.DatabaseLibrary db, object dashboard, Libreria.Model.Magazzino magazzino)
        {
            this.db = db;
            this.dashboard = dashboard;
            this.magazzino = magazzino;
            InitializeComponent();
            txtQuantita.Text = magazzino.Quantita.ToString();

            prodottiController = new ProdottiController(db, "Prodotti");
            posizioneController = new PosizioneController(db, "Posizioni");

            cmbProdotto.ItemsSource = prodottiController.getAll();
            cmbProdotto.DisplayMemberPath = "Nome";
            cmbProdotto.SelectedValuePath = "Id";
            cmbProdotto.SelectedValue = magazzino.Prodotto.Id;

            cmbPosizione.ItemsSource = posizioneController.GetAll();
            cmbPosizione.DisplayMemberPath = "Nome";
            cmbPosizione.SelectedValuePath = "Id";
            cmbPosizione.SelectedValue = magazzino.Posizione.Id;
        }

        private void btnSalva_Click(object sender, RoutedEventArgs e)
        {
            magazzino.Quantita = int.Parse(txtQuantita.Text);
            magazzino.Prodotto = (Prodotto)cmbProdotto.SelectedItem;
            magazzino.Posizione = (Posizione)cmbPosizione.SelectedItem;
            MagazzinoController magazzinoController = new MagazzinoController(db, "Magazzino");
            magazzinoController.Modifica(magazzino);

            PaginaInventario paginaInventario = new PaginaInventario(db,dashboard);
            this.Content = paginaInventario;
        }

        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            PaginaInventario paginaInventario = new PaginaInventario(db, dashboard);
            this.Content = paginaInventario;
        }
    }
}
