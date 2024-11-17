using System;
using System.Threading.Tasks;
using CreativProp.Constants;
using CreativProp.Models;
using Xamarin.Forms;

namespace CreativProp.Repositories
{
    public static class Seed
    {
        public async static Task InitializeDatabase()
        {
            var repository = DependencyService.Resolve<IRepository<Settings>>();

            if (repository == null)
            {
                throw new NullReferenceException(ErrorConstants.REPOSITORY_NOT_RESOLVED);
            }

            var settings = await repository.GetAsync(1);

            if (settings == null)
            {
                settings = new Settings
                {
                    CountryCode = 710,
                    CountryName = "South Africa",
                    CurrencyCode = "ZAR",
                    VatEnabled = true,
                    VatPercentage = 15.0f
                };

                await repository.AddAsync(settings);
            }
        }
    }
}

