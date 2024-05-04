using Libreria;
using Libreria.Controller;
using Libreria.Model;
using System;
using System.Drawing;
using System.IO;
using System.Net.Http.Headers;
using System.Windows.Forms;
using ufficio.Componenti;
namespace ufficio
{
    public partial class LoginForm : Form
    {
        private Libreria.DatabaseLibrary db;

        private System.Windows.Forms.Integration.ElementHost loginHost;
        private System.Windows.Forms.Integration.ElementHost dashboardHost;

        private ComponentiGrafiche.Login login;
        private Dashboard dashboard;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            db = new Libreria.DatabaseLibrary("127.0.0.1", "3306", "pesca", "root", "1234");
            creaLogin();
        }

        private void creaLogin()
        {


            login = new ComponentiGrafiche.Login(db);
            login.login += loggato;
            loginHost = new System.Windows.Forms.Integration.ElementHost();

            loginHost.Name = "LoginHost";
            loginHost.TabIndex = 0;
            loginHost.Text = "LoginHost";
            loginHost.Child = login;
            loginHost.Dock = DockStyle.Fill;
            this.Controls.Clear();
            this.Controls.Add(loginHost);
        }

        private void loggato(Utente utente)
        {
            loginHost.Dispose();
            creaDashboard(utente);
        }

        private void creaDashboard(Utente utente)
        {
            dashboard = new Dashboard(db, utente);
            dashboard.logout += creaLogin;
            dashboardHost = new System.Windows.Forms.Integration.ElementHost();

            dashboardHost.Name = "DashboardHost";
            dashboardHost.TabIndex = 0;
            dashboardHost.Text = "DashboardHost";
            dashboardHost.Child = dashboard;
            dashboardHost.Dock = DockStyle.Fill;
            this.Controls.Clear();
            this.Controls.Add(dashboardHost);
        }
    }
}
