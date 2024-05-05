using Libreria.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Libreria.Converters
{
    public class NegozioToIdConverter : IValueConverter
    {
        private DatabaseLibrary db;

        public NegozioToIdConverter(DatabaseLibrary db)
        {
            this.db = db;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Negozio negozio)
            {
                return negozio.Id;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Negozio negozio)
            {
                return negozio;
            }
            return null;
        }
    }
}
