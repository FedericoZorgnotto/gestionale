using Libreria.Controller;
using System;
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

namespace ufficio.Componenti.prodotti
{
    /// <summary>
    /// Logica di interazione per PaginaModificaProdotto.xaml
    /// </summary>
    public partial class PaginaModificaProdotto : UserControl
    {
        Libreria.DatabaseLibrary db;
        object dashboard;
        Libreria.Model.Prodotto prodotto;

        public PaginaModificaProdotto(Libreria.DatabaseLibrary db, object dashboard, Libreria.Model.Prodotto prodotto)
        {
            this.db= db;
            this.dashboard = dashboard;
            this.prodotto = prodotto;
            InitializeComponent();
            txtNome.Text = prodotto.Nome;
            txtPrezzo.Text = Convert.ToString(prodotto.Prezzo);
            txtNote.Text = prodotto.Note;
        }

        private void btnSalva_Click(object sender, RoutedEventArgs e)
        {
            prodotto.Nome = txtNome.Text;
            prodotto.Prezzo = Convert.ToDecimal(txtPrezzo.Text);
            prodotto.Note = txtNote.Text;

            ProdottiController prodottiController = new ProdottiController(db, "Prodotti");
            prodottiController.Modifica(prodotto);

            PaginaProdotti paginaProdotti = new PaginaProdotti(db, dashboard);
            this.Content = paginaProdotti;
        }

        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            PaginaProdotti paginaProdotti = new PaginaProdotti(db, dashboard);
            this.Content = paginaProdotti;
        }
    }
}
