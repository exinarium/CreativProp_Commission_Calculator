using System;
using System.Linq;
using System.Threading;
using CreativProp.Constants;
using CreativProp.Models;
using CreativProp.Repositories;
using Xamarin.Forms;

namespace CreativProp.Utilities.CommissionCalculator
{
    public class CommissionCalculator : ICommissionCalculator
    {
        private readonly Settings _settings;
        private readonly IRepository<Settings> _repository;

        public double VATPercentage { get; private set; }
        public bool VATApplicable { get; private set; }

        public double CommissionPercentage { get; set; }
        public double SellPrice { get; set; }
        public double ProceedsToSeller { get; set; }
        public double VATAmount { get; set; }
        public double CommissionAmount { get; set; }
        public bool IsPropertyVATRegistered { get; set; }

        public CommissionCalculator()
        {
            _repository = DependencyService.Resolve<IRepository<Settings>>();

            if (_repository == null)
            {
                throw new NullReferenceException(ErrorConstants.REPOSITORY_NOT_RESOLVED);
            }

            _settings = _repository.GetAsync(1).GetAwaiter().GetResult();

            if (_settings == null)
            {
                throw new NullReferenceException(ErrorConstants.SETTINGS_NOT_FOUND);
            }

            VATApplicable = _settings.VatEnabled;
            VATPercentage = VATApplicable ? _settings.VatPercentage : 0.0;
        }

        public CommissionCalculator(
            double commissionPercentage,
            double sellPrice,
            double proceedsToSeller,
            double vatAmount,
            double commissionAmount,
            bool isPropertyVATRegistered
            ) : this()
        {
            CommissionPercentage = commissionPercentage;
            SellPrice = sellPrice;
            ProceedsToSeller = proceedsToSeller;
            VATAmount = vatAmount;
            CommissionAmount = commissionAmount;
            IsPropertyVATRegistered = isPropertyVATRegistered;
        }

        public double CalculateSellPrice()
        {
            if (CommissionPercentage != 0.0)
            {
                if (ProceedsToSeller == 0.0 && CommissionAmount != 0.0)
                {
                    SellPrice = CommissionAmount * 100.0 / CommissionPercentage;
                    CommissionAmount = SellPrice * CommissionPercentage / 100.0;
                    ProceedsToSeller = (SellPrice / CalculateCommissionFactor());

                    CalculateVAT();
                    SellPrice = ProceedsToSeller + CommissionAmount + VATAmount;
                }
                else if (CommissionAmount == 0.0 && ProceedsToSeller != 0.0)
                {
                    SellPrice = (ProceedsToSeller * CalculateCommissionFactor());
                    CommissionAmount = SellPrice * CommissionPercentage / 100.0;

                    CalculateVAT();
                    SellPrice = ProceedsToSeller + CommissionAmount + VATAmount;
                }
            }
            else if (CommissionAmount != 0.0 && ProceedsToSeller != 0.0)
            {
                SellPrice = ProceedsToSeller + CommissionAmount;
                CommissionPercentage = CommissionAmount / SellPrice * 100.0;

                CalculateVAT();
                SellPrice = ProceedsToSeller + CommissionAmount + VATAmount;
            }

            return SellPrice;
        }

        public double CalculateProceedsToSeller()
        {
            if (CommissionPercentage != 0.0)
            {
                if (SellPrice == 0.0 && CommissionAmount != 0.0)
                {
                    SellPrice = CommissionAmount * 100.0 / CommissionPercentage;
                    ProceedsToSeller = (SellPrice / CalculateCommissionFactor());

                    CalculateVAT();
                    SellPrice = ProceedsToSeller + CommissionAmount + VATAmount;
                }
                else if (CommissionAmount == 0.0 && SellPrice != 0.0)
                {
                    VATAmount = IsPropertyVATRegistered ? SellPrice * 15 / 115 : 0;
                    SellPrice = SellPrice - VATAmount;
                    ProceedsToSeller = (SellPrice / CalculateCommissionFactor());
                    CommissionAmount = SellPrice - ProceedsToSeller;

                    CalculateVAT();
                    SellPrice = ProceedsToSeller + CommissionAmount + VATAmount;
                }
            }
            else if (CommissionAmount != 0.0 && SellPrice != 0.0)
            {
                VATAmount = IsPropertyVATRegistered ? SellPrice * 15 / 115 : 0;
                SellPrice = SellPrice - VATAmount;
                ProceedsToSeller = SellPrice - CommissionAmount;
                CommissionPercentage = CommissionAmount / SellPrice * 100;

                if(!IsPropertyVATRegistered)
                {
                    ProceedsToSeller = ProceedsToSeller - (CommissionAmount * VATPercentage / 100);
                }

                CalculateVAT();
                SellPrice = ProceedsToSeller + CommissionAmount + VATAmount;
            }

            return ProceedsToSeller;
        }

        public double CalculateCommissionAmountWithoutVAT()
        {
            if (CommissionPercentage != 0.0)
            {
                if (SellPrice == 0.0 && ProceedsToSeller != 0.0)
                {
                    CalculateSellPrice();
                }
                else if (ProceedsToSeller == 0.0 && SellPrice != 0.0)
                {
                    CalculateProceedsToSeller();
                }
            }
            else if (SellPrice != 0.0 && ProceedsToSeller != 0.0)
            {
                double commissionPlusVat = SellPrice - ProceedsToSeller;
                CalculateCommissionFromVAT(commissionPlusVat);
                CommissionPercentage = CommissionAmount / SellPrice * 100.0;
            }

            return CommissionAmount;
        }

        public double CalculateCommissionAmountWithVAT()
        {
            CalculateCommissionAmountWithoutVAT();
            return CommissionAmount + VATAmount;
        }

        public double CalculateCommissionPercentage()
        {
            if (CommissionAmount != 0.0)
            {
                if (SellPrice == 0.0 && ProceedsToSeller != 0.0)
                {
                    CalculateSellPrice();
                }
                else if (ProceedsToSeller == 0.0 && SellPrice != 0.0)
                {
                    CalculateProceedsToSeller();
                }
            }
            else if (SellPrice != 0.0 && ProceedsToSeller != 0.0)
            {
                double commissionPlusVat = SellPrice - ProceedsToSeller;
                CalculateCommissionFromVAT(commissionPlusVat);
                CommissionPercentage = CommissionAmount / SellPrice * 100.0;
            }

            return CommissionPercentage;
        }

        public double CalculateVATAmount()
        {
            CalculateCommissionAmountWithoutVAT();
            return VATAmount;
        }

        private void CalculateVAT()
        {
            if (VATApplicable)
            {
                if (IsPropertyVATRegistered)
                {
                    double total = (ProceedsToSeller + CommissionAmount) * 115 / 100;
                    VATAmount = total * (VATPercentage / 115.0);
                }
                else
                {
                    VATAmount = CommissionAmount * VATPercentage / 100.0;
                }
            }
        }

        private void CalculateCommissionFromVAT(double commissionPlusVat)
        {
            if (VATApplicable)
            {
                if (IsPropertyVATRegistered)
                {
                    VATAmount = SellPrice * VATPercentage / 100.0;
                    CommissionAmount = commissionPlusVat - VATAmount;
                }
                else
                {
                    CommissionAmount = commissionPlusVat * 100.0 / (100.0 + VATPercentage);
                    VATAmount = CommissionAmount * VATPercentage / 100.0;
                }
            }
        }

        private double CalculateCommissionFactor()
        {
            double tempSellPrice = 2000000.0;
            double tempCommissionAmount = tempSellPrice * CommissionPercentage / 100.0;
            double tempVATAmount = IsPropertyVATRegistered ? 0 : tempCommissionAmount * VATPercentage / 100.0;
            double tempProceeds = tempSellPrice - tempCommissionAmount - tempVATAmount;
            double factor = tempSellPrice / tempProceeds;

            return factor;
        }
    }
}

