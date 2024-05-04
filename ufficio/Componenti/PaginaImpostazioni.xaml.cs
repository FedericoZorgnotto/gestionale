using Libreria;
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

namespace ufficio.Componenti
{

    public partial class PaginaImpostazioni : UserControl
    {
        private DatabaseLibrary db;
        private object dashboard;
        Libreria.Controller.ImpostazioniController impostazioni;

        public PaginaImpostazioni(DatabaseLibrary db, object dashboard)
        {
            InitializeComponent();
            this.db = db;
            this.dashboard = dashboard;
            impostazioni = new Libreria.Controller.ImpostazioniController(db, "impostazioni");
            impostazioni.CaricaImpostazioni();
            txtNome.Text = Libreria.Model.Impostazioni.NomeAzienda;

            byte[] binaryData = Convert.FromBase64String(Libreria.Model.Impostazioni.logoBase64);
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new System.IO.MemoryStream(binaryData);
            bitmapImage.EndInit();
            ImgLogo.Source = bitmapImage;

        }

        private void btnSalva_Click(object sender, RoutedEventArgs e)
        {
            Libreria.Model.Impostazioni.NomeAzienda = txtNome.Text;
            
            byte[] binaryData;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImgLogo.Source));
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                encoder.Save(ms);
                binaryData = ms.ToArray();
            }
            Libreria.Model.Impostazioni.logoBase64 = Convert.ToBase64String(binaryData);

            impostazioni.SalvaImpostazioni();
            MessageBox.Show("Impostazioni salvate correttamente");
        }

        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            this.Content = dashboard;
        }

        private void btnSelezionaLogo_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.gif)|*.jpg; *.jpeg; *.png; *.gif";
            dlg.Multiselect = false;
            dlg.FilterIndex = 0;


            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                BitmapImage bitmap = new BitmapImage(new Uri(filename));
                ImgLogo.Source = bitmap;
            }
        }
    }
}
