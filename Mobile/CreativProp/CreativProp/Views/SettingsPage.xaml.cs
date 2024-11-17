using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CreativProp.ViewModels;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;

namespace CreativProp.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        void Picker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            var settings = this.BindingContext as SettingsViewModel;
            settings.VatEnabled = settings.VatEnabledSelected.Value;
        }

        void SaveSettings(System.Object sender, System.EventArgs e)
        {
            var settings = this.BindingContext as SettingsViewModel;            

            Task.Run(async () =>
            {
                await settings.SaveSettingsAsync();
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await this.DisplayToastAsync(new ToastOptions
                    {
                        BackgroundColor = Color.FromHex("#86a5ef"),
                        CornerRadius = 5,
                        Duration = TimeSpan.FromMilliseconds(2000),
                        MessageOptions = new MessageOptions
                        {
                            Foreground = Color.FromHex("#FFFFFF"),
                            Message = "Settings saved succesfully!"
                        }
                    });
                });
            });
        }
    }
}

