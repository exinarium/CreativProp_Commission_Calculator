using System;
namespace CreativProp.Utilities.CommissionCalculator
{
    public interface ICommissionCalculator
    {
        double VATPercentage { get; }

        bool VATApplicable { get; }

        double CommissionPercentage { get; set; }

        double SellPrice { get; set; }

        double ProceedsToSeller { get; set; }

        double VATAmount { get; set; }

        double CommissionAmount { get; set; }

        double CalculateVATAmount();

        double CalculateCommissionPercentage();

        double CalculateCommissionAmountWithoutVAT();

        double CalculateCommissionAmountWithVAT();

        double CalculateSellPrice();

        double CalculateProceedsToSeller();
    }
}

