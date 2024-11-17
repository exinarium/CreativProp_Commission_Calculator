using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CreativProp.Constants;
using CreativProp.Models;
using CreativProp.Repositories;
using CreativProp.Utilities.Classes;
using CreativProp.Utilities.Formatters;
using Xamarin.Forms;

namespace CreativProp.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private Settings _settings;

        bool vatEnabled = false;
        public bool VatEnabled
        {
            get { return !vatEnabled; }
            set { SetProperty(ref vatEnabled, value); }
        }

        public BooleanValue VatEnabledSelected { get; set; }
        public string VatPercentage { get; set; }
        public int CountryCode { get; set; }
        public string CurrencyCode { get; set; }
        public string CountryName { get; set; }
        public List<BooleanValue> BooleanPickerList { get; set; }

        public SettingsViewModel()
        {
            IsBusy = true;

            Task.Run(async () =>
            {

                if (DataStore == null)
                {
                    throw new NullReferenceException(ErrorConstants.REPOSITORY_NOT_RESOLVED);
                }

                _settings = DataStore.GetAsync(1).GetAwaiter().GetResult();

                if (_settings == null)
                {
                    throw new NullReferenceException(ErrorConstants.SETTINGS_NOT_FOUND);
                }

                BooleanPickerList = new List<BooleanValue> {
                    new BooleanValue { Key = "Yes", Value = true },
                    new BooleanValue { Key = "No", Value = false },
                };



                VatEnabled = _settings.VatEnabled;
                VatEnabledSelected = BooleanPickerList.Where(x => x.Value == _settings.VatEnabled).FirstOrDefault();
                VatPercentage = _settings.VatPercentage.ToString();
                CountryCode = _settings.CountryCode;
                CurrencyCode = _settings.CurrencyCode;
                CountryName = _settings.CountryName;

                IsBusy = false;
            });
        }

        public async Task SaveSettingsAsync() 
        {
            IsBusy = true;

            await Task.Run(async () =>
            {
                _settings.VatEnabled = !VatEnabled;
                _settings.VatPercentage = Formatters.FromatToDecimal(VatPercentage);
                _settings.CountryCode = CountryCode;
                _settings.CurrencyCode = CurrencyCode;
                _settings.CountryName = CountryName;

                await DataStore.UpdateAsync(_settings);

                IsBusy = false;
            });
        }
    }
}

