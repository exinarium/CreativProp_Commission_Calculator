using System;
namespace CreativProp.Utilities.Formatters
{
    public class Formatters
    {
        public Formatters()
        {
        }

        public static double FromatToDecimal(string value)
        {
            var tempString = value.Replace('.', System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToCharArray()[0]);
            tempString = tempString.Replace(',', System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToCharArray()[0]);

            return string.IsNullOrEmpty(tempString) ? 0 : Convert.ToDouble(tempString);
        }
    }
}

