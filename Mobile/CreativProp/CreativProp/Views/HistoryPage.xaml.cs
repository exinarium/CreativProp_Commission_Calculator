using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CreativProp.Models;
using CreativProp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace CreativProp.Views
{
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var context = this.BindingContext as HistoryViewModel;
            Task.Run(async () =>
            {
                context.Page = 1;
                await context.FetchHistory(0, 20);
            });
        }

        void HistoryList_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new CalculatorResultsPage(e.SelectedItem as History));
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var answer = await DisplayAlert("Delete Item", "Are you sure you want to delete this item?", "Yes", "No");

            if (answer)
            {
                Task.Run(async () =>
                {
                    var context = this.BindingContext as HistoryViewModel;
                    var itemId = (mi.CommandParameter as History).ID;
                    context.DeleteItem(itemId);
                });

            }
        }
    }
}

