using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Galchenko.TestTask.DesktopClient.Converters
{
    public class EnumBooleanConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            string? parameterString = parameter as string;
            
            if ( parameterString == null ) {
              return DependencyProperty.UnsetValue;
            }

            if ( value != null && Enum.IsDefined( value.GetType(), value ) == false)
              return DependencyProperty.UnsetValue;

            if ( value != null ) {
                object parameterValue = Enum.Parse( value.GetType(), parameterString );
                return parameterValue.Equals(value);
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            string? parameterString = parameter as string;
            if ( parameterString == null )
                return DependencyProperty.UnsetValue;

            return Enum.Parse( targetType, parameterString );
        }
    }
}
