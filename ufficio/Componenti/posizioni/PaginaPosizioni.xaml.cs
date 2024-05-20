using Libreria;
using Libreria.Controller;
using Libreria.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace ufficio.Componenti.posizioni
{
    public partial class PaginaPosizioni : UserControl
    {
        DatabaseLibrary db;
        object dashboard;
        const string tabella = "Posizioni";
        PosizioneController posizioneController;
        public PaginaPosizioni(DatabaseLibrary db, object dashboard)
        {
            this.db = db;
            this.dashboard = dashboard;
            posizioneController = new PosizioneController(db, tabella);
            InitializeComponent();

        }
        private void caricaDgvPosizioni()
        {
            dgvPosizioni.Columns.Clear();
            if (cbNegozi.IsChecked == true && cbMagazzini.IsChecked == true)
                dgvPosizioni.ItemsSource = posizioneController.GetAll();
            else if (cbNegozi.IsChecked == true)
                dgvPosizioni.ItemsSource = posizioneController.GetByTipo(Tipo.Negozio);
            else if (cbMagazzini.IsChecked == true)
                dgvPosizioni.ItemsSource = posizioneController.GetByTipo(Tipo.Magazzino);
            else
                dgvPosizioni.ItemsSource = null;

            //add modifica button
            DataGridTemplateColumn modificaButton = new DataGridTemplateColumn();
            modificaButton.Header = "Modifica";
            FrameworkElementFactory modificaButtonFactory = new FrameworkElementFactory(typeof(Button));
            modificaButtonFactory.SetValue(Button.ContentProperty, "Modifica");
            modificaButtonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(ModificaButton_Click));
            modificaButton.CellTemplate = new DataTemplate() { VisualTree = modificaButtonFactory };
            dgvPosizioni.Columns.Add(modificaButton);

            //add elimina button
            DataGridTemplateColumn deleteButton = new DataGridTemplateColumn();
            deleteButton.Header = "Elimina";
            FrameworkElementFactory deleteButtonFactory = new FrameworkElementFactory(typeof(Button));
            deleteButtonFactory.SetValue(Button.ContentProperty, "Elimina");
            deleteButtonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(DeleteButton_Click));
            deleteButton.CellTemplate = new DataTemplate() { VisualTree = deleteButtonFactory };
            dgvPosizioni.Columns.Add(deleteButton);

            //add inventario button
            DataGridTemplateColumn inventarioButton = new DataGridTemplateColumn();
            inventarioButton.Header = "Inventario";
            FrameworkElementFactory inventarioButtonFactory = new FrameworkElementFactory(typeof(Button));
            inventarioButtonFactory.SetValue(Button.ContentProperty, "Inventario");
            inventarioButtonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(InventarioButton_Click));
            inventarioButton.CellTemplate = new DataTemplate() { VisualTree = inventarioButtonFactory };
            dgvPosizioni.Columns.Add(inventarioButton);

            //set datagrid readonly
            dgvPosizioni.IsReadOnly = true;
        }

        private void ModificaButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgvPosizioni.SelectedItem == null)
            {
                MessageBox.Show("Selezionare una posizione");
                return;
            }
            Posizione posizione = (Posizione)dgvPosizioni.SelectedItem;
            if (posizione != null)
            {
                PaginaModificaPosizione paginaModificaPosizione = new PaginaModificaPosizione(db, dashboard, posizione);
                this.Content = paginaModificaPosizione;
                return;
            }
        }

        private void InventarioButton_Click(object sender, RoutedEventArgs e)
        {

            if (dgvPosizioni.SelectedItem == null)
            {
                MessageBox.Show("Selezionare una posizione");
                return;
            }
            Posizione posizione = (Posizione)dgvPosizioni.SelectedItem;
            if (posizione != null)
            {
                PaginaInventario paginaInventario = new PaginaInventario(db, this.Content, posizione);
                this.Content = paginaInventario;
                return;
            }
        }

        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            this.Content = dashboard;
        }

        private void btnAggiungi_Click(object sender, RoutedEventArgs e)
        {
            PaginaAggiungiPosizione paginaAggiungiPosizione = new PaginaAggiungiPosizione(db, dashboard);
            this.Content = paginaAggiungiPosizione;


        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            if (dgvPosizioni.SelectedItem == null)
            {
                MessageBox.Show("Selezionare una posizione");  
                return;
            }
            Posizione posizione = (Posizione)dgvPosizioni.SelectedItem;
            if (posizione != null)
            {
                PosizioneController posizioneController = new PosizioneController(db, tabella);
                posizioneController.Elimina(posizione);
                caricaDgvPosizioni();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            caricaDgvPosizioni();
        }

        private void Combo_Click(object sender, RoutedEventArgs e)
        {
            caricaDgvPosizioni();
        }

    }
}
