using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CreativProp.Views;
using CreativProp.Repositories;
using System.IO;
using CreativProp.Constants;
using CreativProp.Models;
using System.Threading.Tasks;
using System.Globalization;

[assembly: ExportFont("MaterialIcons-Regular.ttf", Alias = "Material")]
namespace CreativProp
{
    public partial class App : Application
    {
        public App()
        {
            CreateDatabase();
            RegisterServices();

            Task.Run(async () =>
            {
                await Seed.InitializeDatabase();
            });

            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        void CreateDatabase()
        {
            new Database(Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
                ConfigurationConstants.DB_PATH));
        }

        void RegisterServices()
        {
            DependencyService.Register<IRepository<Settings>, Repository<Settings>>();
            DependencyService.Register<IRepository<History>, Repository<History>>();
        }
    }
}

