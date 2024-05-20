using Libreria.Controller;
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
    /// Logica di interazione per PaginaModificaPosizione.xaml
    /// </summary>
    public partial class PaginaModificaPosizione : UserControl
    {
        Libreria.DatabaseLibrary db;
        object dashboard;
        Libreria.Model.Posizione posizione;
        PosizioneController PosizioneController;
        public PaginaModificaPosizione(Libreria.DatabaseLibrary db, object dashboard, Libreria.Model.Posizione posizione)
        {
            InitializeComponent();
            this.db = db;
            this.dashboard = dashboard;
            this.posizione = posizione;

            txtNome.Text = posizione.Nome;
            txtIndirizzo.Text = posizione.Indirizzo;
            txtCitta.Text = posizione.Citta;
            txtTelefono.Text = posizione.Telefono;
            txtNote.Text = posizione.Note;

            cmbTipo.ItemsSource = Enum.GetValues(typeof(Libreria.Model.Tipo));
            cmbTipo.SelectedItem = posizione.Tipo;

            PosizioneController = new PosizioneController(db, "Posizioni");
        }
        private void btnSalva_Click(object sender, RoutedEventArgs e)
        {
            PosizioneController.Modifica(new Libreria.Model.Posizione()
            {
                Id = posizione.Id,
                Nome = txtNome.Text,
                Indirizzo = txtIndirizzo.Text,
                Citta = txtCitta.Text,
                Telefono = txtTelefono.Text,
                Tipo = (Libreria.Model.Tipo)cmbTipo.SelectedItem,
                Note = txtNote.Text
            });
            MessageBox.Show("Posizione modificata con successo");
            PaginaPosizioni paginaPosizioni = new PaginaPosizioni(db, dashboard);
            this.Content = paginaPosizioni;
        }
        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            PaginaPosizioni paginaPosizioni = new PaginaPosizioni(db, dashboard);
            this.Content = paginaPosizioni;
        }
    }
}
