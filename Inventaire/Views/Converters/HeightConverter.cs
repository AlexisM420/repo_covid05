using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace BillingManagement.UI.Views.Converters
{
    /// <summary>
    /// Source : https://stackoverflow.com/a/23906536/503842
    /// </summary>
    public class HeightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double allHeight = (double)values[0];
            double headerHeight = (double)values[1];

            var diff = allHeight - headerHeight;

            return  diff / 15;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
