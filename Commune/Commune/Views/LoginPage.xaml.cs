using Commune.Services.TokenWrapper;
using Commune.Shared.Auth;
using Commune.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Commune.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        ITokenWrapper tokenWrapper;

        public LoginPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            tokenWrapper = DependencyService.Get<ITokenWrapper>();
            var tokenResult = tokenWrapper.GetGredentials();
            if (!string.IsNullOrWhiteSpace(tokenResult.Result))
            {
                await App.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
        }
    }
}