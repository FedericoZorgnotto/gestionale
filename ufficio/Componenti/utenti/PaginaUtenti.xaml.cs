using Libreria.Controller;
using Libreria.Converters;
using Libreria.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ufficio.Componenti.utenti
{
    public partial class PaginaUtenti : UserControl
    {
        Libreria.DatabaseLibrary db;
        const string tabellaPosizioni = "Posizioni";
        object dashboard;

        public PaginaUtenti(Libreria.DatabaseLibrary db, object dashboard)
        {
            InitializeComponent();
            this.db = db;
            this.dashboard = dashboard;
            caricaDgvUtenti();
            
        }

        private void caricaDgvUtenti()
        {
            PosizioneController posizioneController = new PosizioneController(db, tabellaPosizioni);
            Libreria.Controller.UtenteController utenteController = new Libreria.Controller.UtenteController(db, "Utenti");

            dgvUtenti.ItemsSource = new ObservableCollection<Utente>(utenteController.GetUtenti().Cast<Utente>().ToList());
            
        }

        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
           //apri paginamodificautente con l'utente selezionato come parametro referenziale
           Utente utente = dgvUtenti.SelectedItem as Utente;
            if (utente != null)
            {
                PaginaModificaUtente paginaModificaUtente = new PaginaModificaUtente(db, utente, dashboard);
                this.Content = paginaModificaUtente;
            }
        }

        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            this.Content = dashboard;
        }

        private void btnAggiungi_Click(object sender, RoutedEventArgs e)
        {
            PaginaAggiungiUtente paginaAggiungiUtente = new PaginaAggiungiUtente(db, dashboard);
            this.Content = paginaAggiungiUtente;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //imposto la visualizzazione per la colonna posizione
            DataGridTextColumn colPosizione = (DataGridTextColumn)dgvUtenti.Columns[9];
            colPosizione.Binding = new Binding("Posizione.Nome");
            
            //remove password column
            dgvUtenti.Columns.RemoveAt(1);
            //add edit button column
            DataGridTemplateColumn colonnaModifica = new DataGridTemplateColumn();
            colonnaModifica.Header = "Modifica";
            FrameworkElementFactory btnModifica = new FrameworkElementFactory(typeof(Button));
            btnModifica.SetValue(Button.ContentProperty, "Modifica");
            btnModifica.AddHandler(Button.ClickEvent, new RoutedEventHandler(btnModifica_Click));
            DataTemplate dt = new DataTemplate();
            dt.VisualTree = btnModifica;
            colonnaModifica.CellTemplate = dt;
            dgvUtenti.Columns.Add(colonnaModifica);
            //add delete button column
            DataGridTemplateColumn colonnaElimina = new DataGridTemplateColumn();
            colonnaElimina.Header = "Elimina";
            FrameworkElementFactory btnElimina = new FrameworkElementFactory(typeof(Button));
            btnElimina.SetValue(Button.ContentProperty, "Elimina");
            btnElimina.AddHandler(Button.ClickEvent, new RoutedEventHandler(DeleteButton_Click));
            DataTemplate dt2 = new DataTemplate();
            dt2.VisualTree = btnElimina;
            colonnaElimina.CellTemplate = dt2;
            dgvUtenti.Columns.Add(colonnaElimina);
            //set readonly 
            dgvUtenti.IsReadOnly = true;


        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Utente utente = dgvUtenti.SelectedItem as Utente;
            if (utente != null)
            {
                Libreria.Controller.UtenteController utenteController = new Libreria.Controller.UtenteController(db, "Utenti");
                utenteController.Elimina(utente);
                caricaDgvUtenti();
            }
        }
    }
}
