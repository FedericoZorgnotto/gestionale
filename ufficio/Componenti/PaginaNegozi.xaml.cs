using Libreria;
using Libreria.Controller;
using Libreria.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace ufficio.Componenti
{
    public partial class PaginaNegozi : UserControl
    {
        DatabaseLibrary db;
        object dashboard;
        const string tabella = "Posizioni";
        public PaginaNegozi(DatabaseLibrary db, object dashboard)
        {
            InitializeComponent();
            this.db = db;
            this.dashboard = dashboard;
            caricaDgvNegozi();

        }
        private void caricaDgvNegozi()
        {
            dgvNegozi.Columns.Clear();
            PosizioneController posizioneController = new PosizioneController(db, tabella);
            dgvNegozi.ItemsSource = posizioneController.GetTipo(Tipo.Negozio);

            DataGridTemplateColumn colonnaElimina = new DataGridTemplateColumn();
            colonnaElimina.Header = "Elimina";

            FrameworkElementFactory buttonFactory = new FrameworkElementFactory(typeof(Button));
            buttonFactory.SetValue(Button.ContentProperty, "Elimina");
            buttonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(DeleteButton_Click));
            DataTemplate cellTemplate = new DataTemplate { VisualTree = buttonFactory };

            colonnaElimina.CellTemplate = cellTemplate;
            dgvNegozi.Columns.Add(colonnaElimina);
        }

        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            this.Content = dashboard;
        }

        private void btnSalva_Click(object sender, RoutedEventArgs e)
        {
            List<Posizione> negozi = dgvNegozi.ItemsSource.Cast<Posizione>().ToList();

            PosizioneController posizioneController = new PosizioneController(db, tabella);
            posizioneController.SalvaPosizioni(negozi);
            MessageBox.Show("Impostazioni salvate correttamente");

        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Posizione negozio = (Posizione)dgvNegozi.SelectedItem;
            if (negozio != null)
            {
                PosizioneController posizioneController = new PosizioneController(db, tabella);
                posizioneController.Elimina(negozio);
                caricaDgvNegozi();
            }
        }
    }
}
