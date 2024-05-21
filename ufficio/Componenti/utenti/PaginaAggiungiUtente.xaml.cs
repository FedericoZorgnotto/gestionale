using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
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

namespace ufficio.Componenti.utenti
{
    /// <summary>
    /// Logica di interazione per PaginaAggiungiUtente.xaml
    /// </summary>
    public partial class PaginaAggiungiUtente : UserControl
    {
        Libreria.DatabaseLibrary db;
        object dashboard;
        public PaginaAggiungiUtente(Libreria.DatabaseLibrary db, object dashboard)
        {
            InitializeComponent();
            this.db = db;
            this.dashboard = dashboard;
            cmbRuolo.ItemsSource = Enum.GetValues(typeof(Libreria.Model.Ruoli));
            Libreria.Controller.PosizioneController posizioneController = new Libreria.Controller.PosizioneController(db, "Posizioni");
            IEnumerable posizioni = posizioneController.GetAll();
            cmbPosizione.ItemsSource = posizioni;
            cmbPosizione.DisplayMemberPath = "Nome";

        }

        public void btnSalva_Click(object sender, RoutedEventArgs e)
        {
            if(txtNome.Text == "")
            {
                MessageBox.Show("Inserire il nome");
                return;
            }
            if(txtCognome.Text == "")
            {
                MessageBox.Show("Inserire il cognome");
                return;
            }
            if(txtUsername.Text == "")
            {
                MessageBox.Show("Inserire lo username");
                return;
            }
            if(txtPassword.Text == "")
            {
                MessageBox.Show("Inserire la password");
                return;
            }
            if(txtEmail.Text == "")
            {
                MessageBox.Show("Inserire l'email");
                return;
            }
            if(txtTelefono.Text == "")
            {
                MessageBox.Show("Inserire il telefono");
                return;
            }
            if(txtIndirizzo.Text == "")
            {
                MessageBox.Show("Inserire l'indirizzo");
                return;
            }
            if(txtCitta.Text == "")
            {
                MessageBox.Show("Inserire la città");
                return;
            }
            if(cmbRuolo.SelectedItem == null)
            {
                MessageBox.Show("Selezionare il ruolo");
                return;
            }



            Libreria.Controller.UtenteController utenteController = new Libreria.Controller.UtenteController(db, "Utenti");
            Libreria.Model.Utente utente = new Libreria.Model.Utente();
            utente.Nome = txtNome.Text;
            utente.Cognome = txtCognome.Text;
            utente.Username = txtUsername.Text;
            utente.Password = Libreria.Utilities.HashUtility.CalcoloSHA1(txtPassword.Text);
            utente.Email = txtEmail.Text;
            utente.Telefono = txtTelefono.Text;
            utente.Ruolo = cmbRuolo.SelectedItem != null ? (Libreria.Model.Ruoli)cmbRuolo.SelectedItem : Libreria.Model.Ruoli.Commesso;
            utente.Indirizzo= txtIndirizzo.Text;
            utente.Citta = txtCitta.Text;
            utente.Posizione = cmbPosizione.SelectedItem != null ? (Libreria.Model.Posizione)cmbPosizione.SelectedItem : null;
            utente.Note = txtNote.Text;

            utenteController.Aggiungi(utente);
            MessageBox.Show("Utente aggiunto correttamente");
            PaginaUtenti paginaUtenti = new PaginaUtenti(db, dashboard);
            this.Content = paginaUtenti;
        }
        public void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            PaginaUtenti paginaUtenti= new PaginaUtenti(db, dashboard);
            this.Content = paginaUtenti;
        }
    }
}
