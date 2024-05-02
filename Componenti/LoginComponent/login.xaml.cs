using Libreria;
using Libreria.Controller;
using Libreria.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ComponentiGrafiche
{

    public partial class Login : UserControl
    {
        private Libreria.DatabaseLibrary _database;
        private UtenteController controller;

        public Libreria.DatabaseLibrary database
        {
            get { return _database; }
            set { _database = value; }
        }

        public Action<Utente> login { get; set; }

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            UtenteController controller = new UtenteController(_database, "Utenti");
            Utente utente = controller.Login(username, password);
            if (utente != null)
            {
                login?.Invoke(utente);
            }
            else
            {
                MessageBox.Show("Login fallito");
            }

        }

    }
}
