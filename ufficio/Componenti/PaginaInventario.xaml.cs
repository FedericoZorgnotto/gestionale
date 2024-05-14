using Libreria;
using Libreria.Controller;
using Libreria.Model;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ufficio.Componenti
{
    /// <summary>
    /// Logica di interazione per PaginaInventario.xaml
    /// </summary>
    public partial class PaginaInventario : UserControl
    {
        object dashboard;
        DatabaseLibrary db;
        private Posizione negozio;

        //Tipo Tipo;
        public PaginaInventario(DatabaseLibrary db, object dashboard, Posizione negozio)  //, Tipo tipo
        {
            this.dashboard = dashboard;
            this.db = db;
            this.negozio = negozio;
            //this.Tipo = tipo;
            InitializeComponent();
            MessageBox.Show(negozio.Nome);
        }


        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            this.Content = dashboard;
        }

        private void btnSalva_Click(object sender, RoutedEventArgs e)
        {
            List<Posizione> negozi = dgvInventario.ItemsSource.Cast<Posizione>().ToList();

            PosizioneController posizioneController = new PosizioneController(db, "Negozi");
            posizioneController.SalvaPosizioni(negozi);
            MessageBox.Show("Impostazioni salvate correttamente");

        }
    }
}
