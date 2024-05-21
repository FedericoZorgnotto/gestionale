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

namespace ufficio.Componenti.inventario
{
    /// <summary>
    /// Logica di interazione per PaginaInventario.xaml
    /// </summary>
    public partial class PaginaInventario : UserControl
    {
        object dashboard;
        DatabaseLibrary db;
        private Posizione posizione = null;
        private MagazzinoController MagazzinoController;
        public PaginaInventario(DatabaseLibrary db, object dashboard)  //, Tipo tipo
        {
            this.dashboard = dashboard;
            this.db = db;
            MagazzinoController = new MagazzinoController(db, "Magazzino");
            InitializeComponent();
            caricaDgvInventario();
        }

        public PaginaInventario(DatabaseLibrary db, object dashboard, Posizione posizione)
        {
            this.dashboard = dashboard;
            this.db = db;
            this.posizione = posizione;
            MagazzinoController = new MagazzinoController(db, "Magazzino");
            InitializeComponent();
            caricaDgvInventario();
        }

        private void caricaDgvInventario()
        {
            if (posizione != null)
            {
                dgvInventario.ItemsSource = MagazzinoController.GetByPosizione(posizione);
            }
            else
            {
                dgvInventario.ItemsSource = MagazzinoController.GetAll();
            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgvInventario.SelectedItem != null)
            {
                Magazzino magazzino = (Magazzino)dgvInventario.SelectedItem;
                if (magazzino != null)
                {
                    this.Content = new PaginaInventarioModifica(db, dashboard, magazzino);
                }
            }
        }


        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            this.Content = dashboard;
        }

        private void btnAggiungi_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new PaginaInventarioAggiungi(db, dashboard);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            while(dgvInventario.Columns.Count == 0)
                {
                continue;
            }

            //imposto la visualizzazione per la colonna prodotto
            DataGridTextColumn coProdotto = (DataGridTextColumn)dgvInventario.Columns[0];
            coProdotto.Binding = new Binding("Prodotto.Nome");

            //imposto la visualizzazione per la colonna posizione
            DataGridTextColumn colPosizione = (DataGridTextColumn)dgvInventario.Columns[2];
            colPosizione.Binding = new Binding("Posizione.Nome");

            //add edit button
            DataGridTemplateColumn colEdit = new DataGridTemplateColumn();
            colEdit.Header = "Modifica";
            FrameworkElementFactory btnEdit = new FrameworkElementFactory(typeof(Button));
            btnEdit.SetValue(Button.ContentProperty, "Modifica");
            btnEdit.AddHandler(Button.ClickEvent, new RoutedEventHandler(btnEdit_Click));
            DataTemplate cellEdit = new DataTemplate();
            cellEdit.VisualTree = btnEdit;
            colEdit.CellTemplate = cellEdit;
            dgvInventario.Columns.Add(colEdit);

            //set table readonly
            dgvInventario.IsReadOnly = true;
        }
    }
}
