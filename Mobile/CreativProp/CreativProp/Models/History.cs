using System;
using CreativProp.Utilities.Enums;
using SQLite;

namespace CreativProp.Models
{
    public class History: ModelBase
    {
        public string CalculationName { get; set; }
        public CommissionCalculatorOptions Option { get; set; }
        public double SellPrice { get; set; }
        public double ProceedsToSeller { get; set; }
        public double CommissionAmountPlusVat { get; set; }
        public double CommissionAmount { get; set; }
        public double VatAmount { get; set; }
        public double VatPercentage { get; set; }
        public double CommissionPercentage { get; set; }
        public bool PropertyVatRegistered { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}

