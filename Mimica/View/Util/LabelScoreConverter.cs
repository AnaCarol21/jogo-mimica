using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Mimica.View.Util
{
    public class LabelScoreConverter : IValueConverter
    {
        //View > ViewModel (Dado)
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((byte)value == 0 ? "Palavra": ("Pontuação: " + value));
        }
        //ViewModel > View
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
