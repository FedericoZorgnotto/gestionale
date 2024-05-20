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

namespace ufficio.Componenti.posizioni
{
    /// <summary>
    /// Logica di interazione per PaginaAggiungiPosizione.xaml
    /// </summary>
    public partial class PaginaAggiungiPosizione : UserControl
    {
        Libreria.DatabaseLibrary db;
        object dashboard;
        PosizioneController PosizioneController;
        public PaginaAggiungiPosizione(Libreria.DatabaseLibrary db, object dashboard)
        {
            InitializeComponent();
            this.db = db;
            this.dashboard = dashboard;

            cmbTipo.ItemsSource = Enum.GetValues(typeof(Libreria.Model.Tipo));

            PosizioneController = new PosizioneController(db, "Posizioni");

        }

        private void btnSalva_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PosizioneController.Aggiungi(new Posizione()
                {
                    Nome = txtNome.Text,
                    Indirizzo = txtIndirizzo.Text,
                    Citta = txtCitta.Text,
                    Telefono = txtTelefono.Text,
                    Tipo = (Tipo)cmbTipo.SelectedItem,
                    Note = txtNote.Text
                });
                MessageBox.Show("Posizione aggiunta con successo");
                PaginaPosizioni paginaPosizioni = new PaginaPosizioni(db, dashboard);
                this.Content = paginaPosizioni;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            PaginaPosizioni paginaPosizioni = new PaginaPosizioni(db, dashboard);
            this.Content = paginaPosizioni;
        }
    }
}
