using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Galchenko.TestTask.DesktopClient.Converters
{
    public class MonthToIntConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value is int ival ) {

                return ival - 1;
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value is int ival && ival >= 0 ) {

                return ival + 1;
            }

            return DependencyProperty.UnsetValue;
        }
    }
}
