using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Commune.Services;
using Commune.Views;

namespace Commune
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new LoginPage());
            NavigationPage.SetHasNavigationBar(MainPage, false);
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
