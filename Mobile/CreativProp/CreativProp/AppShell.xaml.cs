using System;
using CreativProp.Views;
using Xamarin.Forms;

namespace CreativProp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CalculatorResultsPage), typeof(CalculatorResultsPage));
        }
    }
}

