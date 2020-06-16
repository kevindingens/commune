using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Commune.Services;
using Commune.Views;
using Commune.Services.TokenWrapper;

namespace Commune
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<ITokenWrapper, TokenWrapper>();
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
