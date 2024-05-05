using Libreria.Controller;
using Libreria.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

//TODO: comprendere perché non vengono visualizzati i negozi e i magazzini all'avvio nelle combobox
namespace ufficio.Componenti
{
    public partial class PaginaUtenti : UserControl
    {
        Libreria.DatabaseLibrary db;
        NegozioController negozioController;
        MagazzinoController magazzinoController;
        object dashboard;

        public PaginaUtenti(Libreria.DatabaseLibrary db, object dashboard)
        {
            InitializeComponent();
            this.db = db;
            this.dashboard = dashboard;
            negozioController = new NegozioController(db, "Negozi");
            magazzinoController = new MagazzinoController(db, "Magazzini");
            caricaDgvUtenti();

        }

        private void caricaDgvUtenti()
        {
            dgvUtenti.Columns.Clear();
            Libreria.Controller.UtenteController utenteController = new Libreria.Controller.UtenteController(db, "Utenti");
            dgvUtenti.ItemsSource = utenteController.GetUtenti();

            ObservableCollection<DataGridColumn> colonne = dgvUtenti.Columns;
            int lunghezza = colonne.Count;

            DataGridTextColumn colonnaNuovaPassword = new DataGridTextColumn();
            colonnaNuovaPassword.Header = "Nuova Password";
            colonne.Add(colonnaNuovaPassword);

            DataGridTemplateColumn colonnaElimina = new DataGridTemplateColumn();
            colonnaElimina.Header = "Elimina";

            FrameworkElementFactory buttonFactory = new FrameworkElementFactory(typeof(Button));
            buttonFactory.SetValue(Button.ContentProperty, "Elimina");
            buttonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(DeleteButton_Click));
            DataTemplate cellTemplate = new DataTemplate { VisualTree = buttonFactory };

            colonnaElimina.CellTemplate = cellTemplate;
            dgvUtenti.Columns.Add(colonnaElimina);

            for (int i = 0; i < lunghezza; i++)
            {
                if (colonne[i].Header.ToString() == "Password" || colonne[i].Header.ToString() == "id")
                    colonne[i].Visibility = Visibility.Hidden;

                if (colonne[i].Header.ToString() == "Negozio")
                {
                    DataGridComboBoxColumn colonnaNegozio = new DataGridComboBoxColumn();
                    colonnaNegozio.Header = "Negozio";
                    colonnaNegozio.DisplayMemberPath = "nome";
                    colonnaNegozio.SelectedValuePath = "Id";
                    colonnaNegozio.ItemsSource = negozioController.GetNegozi();
                    colonne[i] = colonnaNegozio;
                }
                if (colonne[i].Header.ToString() == "Magazzino")
                {
                    DataGridComboBoxColumn colonnaMagazzino = new DataGridComboBoxColumn();
                    colonnaMagazzino.Header = "Magazzino";
                    colonnaMagazzino.DisplayMemberPath = "Nome";
                    colonnaMagazzino.SelectedValuePath = "Id";
                    colonnaMagazzino.ItemsSource = magazzinoController.GetMagazzini();
                    colonne[i] = colonnaMagazzino;
                }
            }
        }

        private void dgvUtenti_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == "Negozio")
            {
                ComboBox cmb = e.EditingElement as ComboBox;
                Negozio negozioSelezionato = cmb.SelectedItem as Negozio;

                Utente utente = dgvUtenti.SelectedItem as Utente;

                utente.Negozio = negozioSelezionato;
            }
            if (e.Column.Header.ToString() == "Magazzino")
            {
                ComboBox cmb = e.EditingElement as ComboBox;
                Magazzino magazzinoSelezionato = cmb.SelectedItem as Magazzino;

                Utente utente = dgvUtenti.SelectedItem as Utente;

                utente.Magazzino = magazzinoSelezionato;
            }
            if (e.Column.Header.ToString() == "Nuova Password")
            {
                TextBox txt = e.EditingElement as TextBox;
                string nuovaPassword = txt.Text;

                Utente utente = dgvUtenti.SelectedItem as Utente;
                utente.Password = Libreria.Utilities.HashUtility.CalcoloSHA1(nuovaPassword);

            }
        }

        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            this.Content = dashboard;
        }

        private void btnSalva_Click(object sender, RoutedEventArgs e)
        {
            List<Libreria.Model.Utente> utenti = dgvUtenti.ItemsSource.Cast<Libreria.Model.Utente>().ToList();

            Libreria.Controller.UtenteController utenteController = new Libreria.Controller.UtenteController(db, "Utenti");
            utenteController.SalvaUtenti(utenti);
            MessageBox.Show("Impostazioni salvate correttamente");
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Utente utente = dgvUtenti.SelectedItem as Utente;
            if (utente != null)
            {
                Libreria.Controller.UtenteController utenteController = new Libreria.Controller.UtenteController(db, "Utenti");
                utenteController.EliminaUtente(utente);
                caricaDgvUtenti();
            }
        }
    }
}
