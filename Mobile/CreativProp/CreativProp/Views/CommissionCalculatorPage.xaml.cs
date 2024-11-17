using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CreativProp.ViewModels;
using Xamarin.Forms;

namespace CreativProp.Views
{
    public partial class CommissionCalculatorPage : ContentPage
    {
        public CommissionCalculatorPage()
        {
            InitializeComponent();
        }

        void PropertyVatRegistered_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            var commissionCalculator = this.BindingContext as CommissionCalculatorViewModel;
            commissionCalculator.IsPropertyVatRegistered = commissionCalculator.PropertyVatRegisteredSelected.Value;
        }

        void CalculationOption_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            var commissionCalculator = this.BindingContext as CommissionCalculatorViewModel;
            commissionCalculator.Calculation = commissionCalculator.CalculationOptionSelected.Value;
        }

        void Calculate(System.Object sender, System.EventArgs e)
        {
            var commissionCalculator = this.BindingContext as CommissionCalculatorViewModel;

            Task.Run(async () =>
            {
                await commissionCalculator.CalculateAsync();                
            });     
        }
    }
}

