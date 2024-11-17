using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CreativProp.Constants;
using CreativProp.Models;
using CreativProp.Utilities.CommissionCalculator;
using CreativProp.Utilities.Enums;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CreativProp.ViewModels
{
    public class CalculatorResultsViewModel : BaseViewModel
    {
        public double SellPrice { get; set; }
        public double ProceedsToSeller { get; set; }
        public double CommissionAmountPlusVat { get; set; }
        public double CommissionAmount { get; set; }
        public double CommissionPercentage { get; set; }
        public double VatAmount { get; set; }
        public double VATPercentage { get; set; }
        public CommissionCalculatorOptions Calculation { get; set; }
        public string CalculationName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string PropertyVatRegistered { get; set; }

        public CalculatorResultsViewModel()
        {
            
        }

        public CalculatorResultsViewModel(History result) : this()
        {
            SellPrice = result.SellPrice;
            ProceedsToSeller = result.ProceedsToSeller;
            CommissionAmountPlusVat = result.CommissionAmountPlusVat;
            CommissionAmount = result.CommissionAmount;
            CommissionPercentage = result.CommissionPercentage;
            VatAmount = result.VatAmount;
            VATPercentage = result.VatPercentage;
            CalculationName = result.CalculationName;
            Calculation = result.Option;
            CreatedDate = result.CreatedDate;
            PropertyVatRegistered = result.PropertyVatRegistered ? "Yes" : "No";
        }
    }
}
