using Libreria;
using Libreria.Controller;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ufficio.Componenti.utenti
{
    /// <summary>
    /// Logica di interazione per PaginaModificaUtente.xaml
    /// </summary>
    public partial class PaginaModificaUtente : UserControl
    {
        Libreria.DatabaseLibrary db;
        Libreria.Model.Utente utente;
        object dashboard;

        public PaginaModificaUtente(Libreria.DatabaseLibrary db, Libreria.Model.Utente utente, object dashboard)
        {
            InitializeComponent();
            this.db = db;
            this.utente = utente;
            this.dashboard = dashboard;
            PosizioneController posizioneController = new PosizioneController(db, "Posizioni");

            cmbRuolo.ItemsSource = Enum.GetValues(typeof(Libreria.Model.Ruoli));
            IEnumerable posizioni = posizioneController.GetAll();
            cmbPosizione.ItemsSource = posizioni;
            cmbPosizione.DisplayMemberPath = "Nome";    
            cmbPosizione.SelectedValuePath = "Id";

            txtUsername.Text = utente.Username;
            txtNome.Text = utente.Nome;
            txtCognome.Text = utente.Cognome;
            txtEmail.Text = utente.Email;
            txtTelefono.Text = utente.Telefono;
            txtIndirizzo.Text = utente.Indirizzo;
            txtCitta.Text = utente.Citta;
            cmbRuolo.SelectedItem = utente.Ruolo;
            cmbPosizione.SelectedValue = utente.Posizione != null ? utente.Posizione.Id : -1;
            //cmbPosizione.SelectedItem = utente.Posizione;
            txtNote.Text = utente.Note;

        }

        private void btnSalva_Click(object sender, RoutedEventArgs e)
        {
            utente.Username = txtUsername.Text;
            utente.Nome = txtNome.Text;
            utente.Cognome = txtCognome.Text;
            utente.Email = txtEmail.Text;
            utente.Telefono = txtTelefono.Text;
            utente.Indirizzo = txtIndirizzo.Text;
            utente.Citta = txtCitta.Text;
            utente.Ruolo = (Libreria.Model.Ruoli)Enum.Parse(typeof(Libreria.Model.Ruoli), cmbRuolo.Text);
            utente.Posizione = cmbPosizione.SelectedItem as Libreria.Model.Posizione;
            utente.Note = txtNote.Text;
            if(txtPassword.Text != "")
                utente.Password = Libreria.Utilities.HashUtility.CalcoloSHA1(txtPassword.Text);


            Libreria.Controller.UtenteController utenteController = new Libreria.Controller.UtenteController(db, "Utenti");

            utenteController.Modifica(utente);

            PaginaUtenti paginaUtenti = new PaginaUtenti(db, dashboard);
            this.Content = paginaUtenti;
        }

        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            PaginaUtenti paginaUtenti= new PaginaUtenti(db, dashboard);
            this.Content = paginaUtenti;
        }


    }
}
