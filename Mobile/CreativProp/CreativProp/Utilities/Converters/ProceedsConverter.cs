using System;
using CreativProp.Utilities.Enums;
using Xamarin.Forms;

namespace CreativProp.Utilities.Converters
{
    public class ProceedsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (CommissionCalculatorOptions?)value == CommissionCalculatorOptions.CALCULATE_PROCEEDS_TO_SELLER;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (CommissionCalculatorOptions?)value != CommissionCalculatorOptions.CALCULATE_PROCEEDS_TO_SELLER;
        }
    }
}

