using System;
using SQLite;

namespace CreativProp.Models
{
    public class Settings: ModelBase
    {
        public bool VatEnabled { get; set; } = true;
        public double VatPercentage { get; set; } = 15.0;
        public int CountryCode { get; set; } = 710;
        public string CurrencyCode { get; set; } = "ZAR";
        public string CountryName { get; set; } = "South Africa";
    }
}

