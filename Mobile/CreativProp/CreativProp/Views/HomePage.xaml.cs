using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CreativProp.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new CommissionCalculatorPage());
            });
        }
    }
}
