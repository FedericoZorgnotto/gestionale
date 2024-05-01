using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using Libreria;

namespace LoginComponent
{
    /// <summary>
    /// Logica di interazione per UserControl1.xaml
    /// </summary>
    public partial class login : UserControl
    {
        private Libreria.DatabaseLibrary database;
        public Libreria.DatabaseLibrary Database
        {
            get { return database; }
            set { database = value; }
        }
        
        public login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@username", username);
            sqlParameters[1] = new SqlParameter("@password", password);
            DataTable dt = database.EseguiQuery("SELECT * FROM utenti WHERE username = @username AND password = @password", sqlParameters);
            if(dt.Rows.Count == 1)
            {
                MessageBox.Show("Login effettuato con successo");
            }
            else
            {
                MessageBox.Show("Login fallito");
            }
        }
    }
}
