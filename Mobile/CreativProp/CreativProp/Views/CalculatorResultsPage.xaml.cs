using System;
using System.ComponentModel;
using CreativProp.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CreativProp.Models;

namespace CreativProp.Views
{
    public partial class CalculatorResultsPage : ContentPage
    {
        public CalculatorResultsPage()
        {
            InitializeComponent();
        }

        public CalculatorResultsPage(History result) : this()
        {
            this.BindingContext = new CalculatorResultsViewModel(result);
        }

        void Finish(System.Object sender, System.EventArgs e)
        {
            Task.Run(async () =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    App.Current.MainPage = new AppShell();
                });
            });
        }
    }
}
