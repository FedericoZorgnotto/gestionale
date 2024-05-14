using Libreria.Controller;
using Libreria.Converters;
using Libreria.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

//TODO: comprendere perché non vengono visualizzati i negozi e i magazzini all'avvio nelle combobox
namespace ufficio.Componenti
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

            dgvUtenti.ItemsSource = utenteController.GetUtenti();

            dgvUtenti.Columns.Clear();
            //DataGridComboBoxColumn colonnaPosizione;
            //int lunghezza = dgvUtenti.Columns.Count;
            //for (int i = 0; i < lunghezza; i++)
            //{
            //    if (dgvUtenti.Columns[i].Header.ToString() == "Posizione")
            //    {
            //        colonnaPosizione = new DataGridComboBoxColumn();
            //        colonnaPosizione.Header = "Posizione";
            //        colonnaPosizione.ItemsSource = posizioneController.GetPosizioni();
            //        colonnaPosizione.DisplayMemberPath = "Nome";
            //        colonnaPosizione.SelectedValuePath = "Id";
            //        colonnaPosizione.SelectedValueBinding = new Binding("Posizione.Id")
            //        {
            //            Converter = new IdToPosizioneConverter(posizioneController),
            //        };
            //        dgvUtenti.Columns[i] = colonnaPosizione;
            //        break;
            //    }
            //}

            DataGridTemplateColumn colonnaPosizione = new DataGridTemplateColumn();
            colonnaPosizione.Header = "Posizione";
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(ComboBox));
            factory.SetValue(ComboBox.ItemsSourceProperty, posizioneController.GetPosizioni());
            factory.SetValue(ComboBox.DisplayMemberPathProperty, "Nome");
            factory.SetValue(ComboBox.SelectedValuePathProperty, "Id");
            factory.SetBinding(ComboBox.SelectedValueProperty, new Binding("Posizione.Id"));
            DataTemplate cellTemplate = new DataTemplate { VisualTree = factory };
            colonnaPosizione.CellTemplate = cellTemplate;
            dgvUtenti.Columns.Add(colonnaPosizione);
        }


        private void dgvUtenti_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == "Posizione")
            {
                ComboBox cmb = e.EditingElement as ComboBox;
                if (cmb != null)
                {
                    Posizione posizioneSelezionata = cmb.SelectedItem as Posizione;

                    Utente utente = dgvUtenti.SelectedItem as Utente;

                    utente.Posizione = posizioneSelezionata;
                }
            }
            if (e.Column.Header.ToString() == "Nuova Password")
            {
                TextBox txt = e.EditingElement as TextBox;
                if (txt != null)
                {
                    string nuovaPassword = txt.Text;

                    Utente utente = dgvUtenti.SelectedItem as Utente;
                    utente.Password = Libreria.Utilities.HashUtility.CalcoloSHA1(nuovaPassword);
                }

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
