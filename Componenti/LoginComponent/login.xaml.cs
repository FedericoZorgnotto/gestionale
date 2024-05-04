using Libreria;
using Libreria.Controller;
using Libreria.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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

        public Login(DatabaseLibrary database)
        {
            _database = database;
            InitializeComponent();
            ImpostazioniController impostazioni = new ImpostazioniController(_database, "impostazioni");
            impostazioni.CaricaImpostazioni();

            byte[] binaryData = Convert.FromBase64String(Impostazioni.logoBase64);
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new System.IO.MemoryStream(binaryData);
            bitmapImage.EndInit();
            imgLogo.Source = bitmapImage;
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

        private void imgLogo_Initialized(object sender, EventArgs e)
        {

        }
    }
}
