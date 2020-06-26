using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Galchenko.TestTask.DesktopClient
{
    public static class DateElement
    {
        public const int FEBRUARY = 2;

        #region Month dependency property
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetMonth( DependencyObject element, int value )
        {
            if (element == null) {
                throw new ArgumentNullException( "element" );
            }

            element.SetValue( MonthProperty, value );
        }

        public static int GetMonth( DependencyObject element )
        {
            if (element == null) {
                throw new ArgumentNullException( "element" );
            }

            return ( int )element.GetValue( MonthProperty );
        }

        public static readonly DependencyProperty MonthProperty =
            DependencyProperty.RegisterAttached( "Month", typeof( int ), typeof( DateElement ),
                new PropertyMetadata( 0,
                    new PropertyChangedCallback( OnMonthPropertyChanged )
                    ),
                new ValidateValueCallback( IsIntValueNotNegative ) );

        public static void OnMonthPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            if (d is ComboBox cb) {
                var year = ( int )cb.GetValue( DateElement.YearProperty );
                var month = ( int )e.NewValue;

                FillDays( cb, year, month );
            }
        }

        #endregion


        #region Year dependency property
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYear( DependencyObject element, int value )
        {
            if (element == null) {
                throw new ArgumentNullException( "element" );
            }

            element.SetValue( YearProperty, value );
        }

        public static int GetYear( DependencyObject element )
        {
            if (element == null) {
                throw new ArgumentNullException( "element" );
            }

            return ( int )element.GetValue( YearProperty );
        }

        public static readonly DependencyProperty YearProperty =
            DependencyProperty.RegisterAttached( "Year", typeof( int ), typeof( DateElement ),
                new PropertyMetadata( 0,
                    new PropertyChangedCallback( OnYearPropertyChanged )
                    ),
                new ValidateValueCallback( IsIntValueNotNegative )
                );

        public static void OnYearPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            if (d is ComboBox cb) {
                var year = ( int )e.NewValue;
                var month = ( int )cb.GetValue( DateElement.MonthProperty );
                
                FillDays( cb, year, month );
            }
        }

        #endregion


        private static void FillDays( ComboBox cb, int year, int month )
        {
            if ( year == 0 ) year = 1980;

            var maxDay = (new DateTime( year, month == 12 ? 1 : month + 1, 1 ) - TimeSpan.FromHours( 1 )).Day;
            cb.ItemsSource = Enumerable.Range( 1, maxDay );
        }


        #region MinYear dependency property
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetMinYear( DependencyObject element, int value )
        {
            if (element == null) {
                throw new ArgumentNullException( "element" );
            }

            element.SetValue( MinYearProperty, value );
        }

        public static int GetMinYear( DependencyObject element )
        {
            if (element == null) {
                throw new ArgumentNullException( "element" );
            }

            return ( int )element.GetValue( MinYearProperty );
        }

        public static readonly DependencyProperty MinYearProperty =
            DependencyProperty.RegisterAttached( "MinYear", typeof( int ), typeof( DateElement ),
                new PropertyMetadata( 0,
                    new PropertyChangedCallback( OnMinYearPropertyChanged )
                    ),
                new ValidateValueCallback( IsIntValueNotNegative ) );

        public static void OnMinYearPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            if (d is ComboBox cb) {
                var newMinYear = ( int )e.NewValue;

                var maxYear = ( int )cb.GetValue( DateElement.MaxYearProperty );

                if ( newMinYear < maxYear ) {
                    cb.ItemsSource = Enumerable.Range( newMinYear, maxYear - newMinYear + 1 ).Reverse().ToList();
                    cb.SelectedIndex = -1;
                }
            }
        }

        #endregion


        #region MaxYear dependency property
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetMaxYear( DependencyObject element, int value )
        {
            if (element == null) {
                throw new ArgumentNullException( "element" );
            }

            element.SetValue( MaxYearProperty, value );
        }

        public static int GetMaxYear( DependencyObject element )
        {
            if (element == null) {
                throw new ArgumentNullException( "element" );
            }

            return ( int )element.GetValue( MaxYearProperty );
        }

        public static readonly DependencyProperty MaxYearProperty =
            DependencyProperty.RegisterAttached( "MaxYear", typeof( int ), typeof( DateElement ),
                new PropertyMetadata( 0,
                    new PropertyChangedCallback( OnMaxYearPropertyChanged )
                    ),
                new ValidateValueCallback( IsIntValueNotNegative ) );

        public static void OnMaxYearPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            if (d is ComboBox cb) {

                var newMaxYear = ( int )e.NewValue;
                var minYear = ( int )cb.GetValue( DateElement.MinYearProperty );

                if ( newMaxYear > minYear ) {
                    cb.ItemsSource = Enumerable.Range( minYear, newMaxYear - minYear + 1 ).Reverse().ToList();
                    cb.SelectedIndex = -1;
                }
            }
        }

        #endregion


        #region validators
        /// <summary>
        /// <see cref="DependencyProperty.ValidateValueCallback"/>
        /// </summary>
        private static bool IsYearValueNotNegative( object value )
        {
            return (( int )value >= 1900);
        }


        /// <summary>
        /// <see cref="DependencyProperty.ValidateValueCallback"/>
        /// </summary>
        private static bool IsIntValueNotNegative( object value )
        {
            return (( int )value >= 0);
        }

        #endregion
    }
}
