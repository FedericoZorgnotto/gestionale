using Libreria.Controller;
using Libreria.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
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

namespace ufficio.Componenti.prodotti
{
    /// <summary>
    /// Logica di interazione per PaginaAggiungiProdotto.xaml
    /// </summary>
    public partial class PaginaAggiungiProdotto : UserControl
    {
        private Libreria.DatabaseLibrary db;
        private object dashboard;

        public PaginaAggiungiProdotto(Libreria.DatabaseLibrary db, object dashboard)
        {
            this.db = db;
            this.dashboard = dashboard;
            InitializeComponent();
        }

        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            PaginaProdotti paginaProdotti = new PaginaProdotti(db, dashboard);
            this.Content = paginaProdotti;

        }

        private void btnSalva_Click(object sender, RoutedEventArgs e)
        {
            if (txtNome.Text == "")
            {
                MessageBox.Show("Inserire il nome");
                return;
            }
            if (txtPrezzo.Text == "")
            {
                MessageBox.Show("Inserire il prezzo");
                return;
            }

            Prodotto prodotto = new Prodotto();
            prodotto.Nome = txtNome.Text;
            prodotto.Prezzo = Convert.ToDecimal(txtPrezzo.Text);
            prodotto.Note = txtNote.Text;

            ProdottiController prodottiController= new ProdottiController(db, "Prodotti");
            prodottiController.Aggiungi(prodotto);

            MessageBox.Show("Prodotto aggiunto correttamente");

            PaginaProdotti paginaProdotti = new PaginaProdotti(db, dashboard);
            this.Content = paginaProdotti;
        }
    }
}
