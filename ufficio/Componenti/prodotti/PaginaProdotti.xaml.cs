using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

//TODO: IMPLEMENTARE FORNITORI E ORDINI

namespace ufficio.Componenti.prodotti
{
    /// <summary>
    /// Logica di interazione per PaginaProdotti.xaml
    /// </summary>
    public partial class PaginaProdotti : UserControl
    {
        Libreria.DatabaseLibrary db;
        object dashboard;
        Libreria.Controller.ProdottiController prodottiController;
        public PaginaProdotti(Libreria.DatabaseLibrary db, object dashboard)
        {
            InitializeComponent();
            this.db = db;
            this.dashboard = dashboard;
            prodottiController = new Libreria.Controller.ProdottiController(db, "Prodotti");
            caricaDgvProdotti();
        }

        private void caricaDgvProdotti()
        {
            dgvProdotti.Columns.Clear();
            dgvProdotti.ItemsSource = prodottiController.getAll();

            //add modifica button
            DataGridTemplateColumn modificaButton = new DataGridTemplateColumn();
            DataTemplate modificaButtonTemplate = new DataTemplate();
            FrameworkElementFactory modificaButtonFactory = new FrameworkElementFactory(typeof(Button));
            modificaButtonFactory.SetValue(Button.ContentProperty, "Modifica");
            modificaButtonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(btnModifica_Click));
            modificaButtonTemplate.VisualTree = modificaButtonFactory;
            modificaButton.CellTemplate = modificaButtonTemplate;
            dgvProdotti.Columns.Add(modificaButton);

            //add elimina button    
            DataGridTemplateColumn eliminaButton = new DataGridTemplateColumn();
            DataTemplate eliminaButtonTemplate = new DataTemplate();
            FrameworkElementFactory eliminaButtonFactory = new FrameworkElementFactory(typeof(Button));
            eliminaButtonFactory.SetValue(Button.ContentProperty, "Elimina");
            eliminaButtonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(btnElimina_Click));
            eliminaButtonTemplate.VisualTree = eliminaButtonFactory;
            eliminaButton.CellTemplate = eliminaButtonTemplate;
            dgvProdotti.Columns.Add(eliminaButton);

            //set table readonly
            dgvProdotti.IsReadOnly = true;
        }

        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            Libreria.Model.Prodotto prodotto = (Libreria.Model.Prodotto)dgvProdotti.SelectedItem;
            this.Content = new PaginaModificaProdotto(db, dashboard, prodotto);
        }

        private void btnElimina_Click(object sender, RoutedEventArgs e)
        {
            prodottiController.Elimina((Libreria.Model.Prodotto)dgvProdotti.SelectedItem);
            caricaDgvProdotti();
        }

        private void btnAggiungi_Click(object sender, RoutedEventArgs e)
        {
            PaginaAggiungiProdotto paginaAggiungiProdotto = new PaginaAggiungiProdotto(db, dashboard);
            this.Content = paginaAggiungiProdotto;

        }

        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            this.Content = dashboard;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
