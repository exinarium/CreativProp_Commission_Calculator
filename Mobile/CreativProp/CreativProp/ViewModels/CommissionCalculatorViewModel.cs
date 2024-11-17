using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CreativProp.Constants;
using CreativProp.Models;
using CreativProp.Repositories;
using CreativProp.Utilities.Classes;
using CreativProp.Utilities.CommissionCalculator;
using CreativProp.Utilities.Enums;
using CreativProp.Utilities.Formatters;
using CreativProp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CreativProp.ViewModels
{
    public class CommissionCalculatorViewModel : BaseViewModel
    {
        public IRepository<History> HistoryDataStore => DependencyService.Get<IRepository<History>>();

        public string CalculationName { get; set; }

        private double sellPrice;
        public string SellPrice
        {
            get { return sellPrice.ToString(); }
            set { SetProperty(ref sellPrice, Formatters.FromatToDecimal(value)); }
        }

        private double proceedsToSeller;
        public string ProceedsToSeller
        {
            get { return proceedsToSeller.ToString(); }
            set { SetProperty(ref proceedsToSeller, Formatters.FromatToDecimal(value)); }
        }

        private double commissionPercentage;
        public string CommissionPercentage
        {
            get { return commissionPercentage.ToString(); }
            set { SetProperty(ref commissionPercentage, Formatters.FromatToDecimal(value)); }
        }

        private double commissionAmountPlusVat;
        public string CommissionAmountPlusVat
        {
            get { return commissionAmountPlusVat.ToString(); }
            set { SetProperty(ref commissionAmountPlusVat, Formatters.FromatToDecimal(value)); }
        }

        private double commissionAmount;
        public string CommissionAmount
        {
            get { return commissionAmount.ToString(); }
            set { SetProperty(ref commissionAmount, Formatters.FromatToDecimal(value)); }
        }

        private double vatAmount;
        public string VatAmount
        {
            get { return vatAmount.ToString(); }
            set { SetProperty(ref vatAmount, Formatters.FromatToDecimal(value)); }
        }

        private bool isButtonEnabled = false;
        public bool IsButtonEnabled
        {
            get { return isButtonEnabled; }
            set { base.SetProperty(ref isButtonEnabled, value); }
        }

        private CommissionCalculatorOptions? calculation = null;

        public CommissionCalculatorOptions? Calculation
        {
            get { return calculation; }
            set { SetProperty(ref calculation, value); }
        }

        public CalculatorOptionValue CalculationOptionSelected { get; set; }

        private bool isPropertyVatRegistered = false;
        public bool IsPropertyVatRegistered
        {
            get { return isPropertyVatRegistered; }
            set { SetProperty(ref isPropertyVatRegistered, value); }
        }

        public BooleanValue PropertyVatRegisteredSelected { get; set; }

        public List<BooleanValue> BooleanPickerList { get; internal set; }

        public List<CalculatorOptionValue> CalculatorOptionValues { get; internal set; }

        public CommissionCalculatorViewModel()
        {
            IsBusy = true;

            Task.Run(async () =>
            {
                CalculatorOptionValues = new List<CalculatorOptionValue>
                {
                    new CalculatorOptionValue
                    {
                        Key = WordingConstants.SellPrice,
                        Value = CommissionCalculatorOptions.CALCULATE_SELL_PRICE
                    },
                    new CalculatorOptionValue
                    {
                        Key = WordingConstants.ProceedsToSeller,
                        Value = CommissionCalculatorOptions.CALCULATE_PROCEEDS_TO_SELLER
                    },
                    new CalculatorOptionValue
                    {
                        Key = WordingConstants.CommissionPercentage,
                        Value = CommissionCalculatorOptions.CALCULATE_COMMISSION_PERCENTAGE
                    },
                    new CalculatorOptionValue
                    {
                        Key = WordingConstants.CommissionPlusVAT,
                        Value = CommissionCalculatorOptions.CALCULATE_COMMISSION_AMOUNT_WITH_VAT
                    },
                    new CalculatorOptionValue
                    {
                        Key = WordingConstants.CommissionAmount,
                        Value = CommissionCalculatorOptions.CALCULATE_COMMISSION_AMOUNT_WITHOUT_VAT
                    },
                    new CalculatorOptionValue
                    {
                        Key = WordingConstants.VATAmount,
                        Value = CommissionCalculatorOptions.CALCULATE_VAT_AMOUNT
                    }
                };

                BooleanPickerList = new List<BooleanValue>
                {
                    new BooleanValue { Key = "Yes", Value = true },
                    new BooleanValue { Key = "No", Value = false },
                };

                IsPropertyVatRegistered = false;
                PropertyVatRegisteredSelected = BooleanPickerList.Where(x => x.Value == false).FirstOrDefault();

                IsBusy = false;
            });
        }

        protected new void SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            base.SetProperty<T>(ref backingStore, value, propertyName, onChanged);

            var totalCount = 0;

            if (Calculation != null) totalCount++;
            if (sellPrice > 0) totalCount++;
            if (proceedsToSeller > 0) totalCount++;
            if (commissionAmount > 0) totalCount++;
            if (commissionAmountPlusVat > 0) totalCount++;
            if (commissionPercentage > 0) totalCount++;
            if (vatAmount > 0) totalCount++;

            if (totalCount >= 3)
            {
                IsButtonEnabled = true;
            }
            else
            {
                IsButtonEnabled = false;
            }
        }

        public async Task CalculateAsync()
        {
            IsBusy = true;

            await Task.Run(async () =>
            {
                var commissionCalculator = new CommissionCalculator(commissionPercentage, sellPrice, proceedsToSeller, vatAmount, commissionAmount, IsPropertyVatRegistered);
                var calculatorResult = new History();

                switch (Calculation)
                {
                    case CommissionCalculatorOptions.CALCULATE_SELL_PRICE:
                        {                            
                            calculatorResult.CalculationName = CalculationName;
                            calculatorResult.SellPrice = commissionCalculator.CalculateSellPrice();
                            calculatorResult.CommissionAmount = commissionCalculator.CommissionAmount;
                            calculatorResult.CommissionAmountPlusVat = commissionCalculator.CommissionAmount + commissionCalculator.VATAmount;
                            calculatorResult.CommissionPercentage = commissionCalculator.CommissionPercentage;
                            calculatorResult.ProceedsToSeller = commissionCalculator.ProceedsToSeller;
                            calculatorResult.VatAmount = commissionCalculator.VATAmount;
                            calculatorResult.Option = CommissionCalculatorOptions.CALCULATE_SELL_PRICE;
                            calculatorResult.VatPercentage = commissionCalculator.VATPercentage;

                            break;
                        }
                    case CommissionCalculatorOptions.CALCULATE_PROCEEDS_TO_SELLER:
                        {
                            calculatorResult.CalculationName = CalculationName;
                            calculatorResult.ProceedsToSeller = commissionCalculator.CalculateProceedsToSeller();
                            calculatorResult.SellPrice = commissionCalculator.SellPrice;
                            calculatorResult.CommissionAmount = commissionCalculator.CommissionAmount;
                            calculatorResult.CommissionAmountPlusVat = commissionCalculator.CommissionAmount + commissionCalculator.VATAmount;
                            calculatorResult.CommissionPercentage = commissionCalculator.CommissionPercentage;
                            calculatorResult.VatAmount = commissionCalculator.VATAmount;
                            calculatorResult.Option = CommissionCalculatorOptions.CALCULATE_PROCEEDS_TO_SELLER;
                            calculatorResult.VatPercentage = commissionCalculator.VATPercentage;

                            break;
                        }
                    case CommissionCalculatorOptions.CALCULATE_COMMISSION_AMOUNT_WITH_VAT:
                        {
                            calculatorResult.CalculationName = CalculationName;
                            calculatorResult.CommissionAmountPlusVat = commissionCalculator.CalculateCommissionAmountWithVAT();
                            calculatorResult.SellPrice = commissionCalculator.SellPrice;
                            calculatorResult.CommissionAmount = commissionCalculator.CommissionAmount;
                            calculatorResult.CommissionPercentage = commissionCalculator.CommissionPercentage;
                            calculatorResult.ProceedsToSeller = commissionCalculator.ProceedsToSeller;
                            calculatorResult.VatAmount = commissionCalculator.VATAmount;
                            calculatorResult.Option = CommissionCalculatorOptions.CALCULATE_COMMISSION_AMOUNT_WITH_VAT;
                            calculatorResult.VatPercentage = commissionCalculator.VATPercentage;

                            break;
                        }
                    case CommissionCalculatorOptions.CALCULATE_COMMISSION_AMOUNT_WITHOUT_VAT:
                        {
                            calculatorResult.CalculationName = CalculationName;
                            calculatorResult.CommissionAmount = commissionCalculator.CalculateCommissionAmountWithoutVAT();
                            calculatorResult.SellPrice = commissionCalculator.SellPrice;
                            calculatorResult.CommissionAmountPlusVat = commissionCalculator.CommissionAmount + commissionCalculator.VATAmount;
                            calculatorResult.CommissionPercentage = commissionCalculator.CommissionPercentage;
                            calculatorResult.ProceedsToSeller = commissionCalculator.ProceedsToSeller;
                            calculatorResult.VatAmount = commissionCalculator.VATAmount;
                            calculatorResult.Option = CommissionCalculatorOptions.CALCULATE_COMMISSION_AMOUNT_WITHOUT_VAT;
                            calculatorResult.VatPercentage = commissionCalculator.VATPercentage;

                            break;
                        }
                    case CommissionCalculatorOptions.CALCULATE_COMMISSION_PERCENTAGE:
                        {
                            calculatorResult.CalculationName = CalculationName;
                            calculatorResult.CommissionPercentage = commissionCalculator.CalculateCommissionPercentage();
                            calculatorResult.SellPrice = commissionCalculator.SellPrice;
                            calculatorResult.CommissionAmount = commissionCalculator.CommissionAmount;
                            calculatorResult.CommissionAmountPlusVat = commissionCalculator.CommissionAmount + commissionCalculator.VATAmount;
                            calculatorResult.ProceedsToSeller = commissionCalculator.ProceedsToSeller;
                            calculatorResult.VatAmount = commissionCalculator.VATAmount;
                            calculatorResult.Option = CommissionCalculatorOptions.CALCULATE_COMMISSION_PERCENTAGE;
                            calculatorResult.VatPercentage = commissionCalculator.VATPercentage;

                            break;
                        }
                    case CommissionCalculatorOptions.CALCULATE_VAT_AMOUNT:
                        {
                            calculatorResult.CalculationName = CalculationName;
                            calculatorResult.VatAmount = commissionCalculator.CalculateVATAmount();
                            calculatorResult.SellPrice = commissionCalculator.SellPrice;
                            calculatorResult.CommissionAmount = commissionCalculator.CommissionAmount;
                            calculatorResult.CommissionAmountPlusVat = commissionCalculator.CommissionAmount + commissionCalculator.VATAmount;
                            calculatorResult.CommissionPercentage = commissionCalculator.CommissionPercentage;
                            calculatorResult.ProceedsToSeller = commissionCalculator.ProceedsToSeller;
                            calculatorResult.Option = CommissionCalculatorOptions.CALCULATE_VAT_AMOUNT;
                            calculatorResult.VatPercentage = commissionCalculator.VATPercentage;

                            break;
                        }
                }

                calculatorResult.PropertyVatRegistered = IsPropertyVatRegistered;

                await HistoryDataStore.AddAsync(calculatorResult);

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.Navigation.PushAsync(new CalculatorResultsPage(calculatorResult));
                });

                IsBusy = false;
            });
        }
    }
}
