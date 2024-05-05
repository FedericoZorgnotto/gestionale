using Libreria;
using Libreria.Controller;
using Libreria.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            dgvMagazini.Columns.Clear();
            MagazzinoController magazzinoController = new MagazzinoController(db, "Magazzini");
            dgvMagazini.ItemsSource = magazzinoController.GetMagazzini();

            DataGridTemplateColumn colonnaElimina = new DataGridTemplateColumn();
            colonnaElimina.Header = "Elimina";

            FrameworkElementFactory buttonFactory = new FrameworkElementFactory(typeof(Button));
            buttonFactory.SetValue(Button.ContentProperty, "Elimina");
            buttonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(DeleteButton_Click));
            DataTemplate cellTemplate = new DataTemplate { VisualTree = buttonFactory };

            colonnaElimina.CellTemplate = cellTemplate;
            dgvMagazini.Columns.Add(colonnaElimina);
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
            MessageBox.Show("Impostazioni salvate correttamente");

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Magazzino magazzino = (Magazzino)dgvMagazini.SelectedItem;
            if (magazzino != null)
            {
                MagazzinoController magazzinoController = new MagazzinoController(db, "Magazzini");
                magazzinoController.EliminaMagazzino(magazzino);
                caricaDgvMagazzini();
            }
        }
    }
}
