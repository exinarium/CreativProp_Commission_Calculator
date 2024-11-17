using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CreativProp.Constants;
using CreativProp.Utilities.CommissionCalculator;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CreativProp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            OpenWebCommand = new Command(async () => await Browser.OpenAsync(WordingConstants.WebsiteAddress));
        }

        public ICommand OpenWebCommand { get; }
    }
}
