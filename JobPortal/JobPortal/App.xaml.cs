using JobPortal.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobPortal
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Tabbed_Page());
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
    }
}
