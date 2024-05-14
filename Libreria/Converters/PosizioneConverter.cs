using Libreria.Controller;
using Libreria.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Libreria.Converters
{
    public class IdToPosizioneConverter : IValueConverter
    {
        private PosizioneController posizioneController;

        public IdToPosizioneConverter(PosizioneController posizioneController)
        {
            this.posizioneController = posizioneController;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int id)
            {
                return posizioneController.GetPosizione(id);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Posizione posizione)
            {
                return posizione.Id;
            }
            return null;
        }
    }

    public class PosizioneToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Posizione posizione)
            {
                return posizione.Nome;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Implementare la logica per convertire la stringa di nuovo in un oggetto Posizione
            throw new NotImplementedException();
        }
    }
}
