using Commune.Services.TokenWrapper;
using Commune.Shared.Auth;
using Commune.Shared.Enums;
using Commune.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Commune.ViewModels
{
    class LoginViewModel: BaseViewModel, INotifyPropertyChanged
    {
        ITokenWrapper tokenWrapper;
        IAuth auth;

        private string email;

        private bool isInvalidEmail;

        private string loginText;

        private LoginStates loginState;

        public bool ShowPassword { get { return LoginState != LoginStates.EmailEntry; } }
        public bool ShowConfirmPassword { get { return LoginState == LoginStates.SignUp; } }

        private LoginStates LoginState
        {
            get { return this.loginState; }
            set
            {
                if (this.loginState == value)
                    return;

                this.loginState = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged("ShowPassword");
                this.OnPropertyChanged("ShowConfirmPassword");
            }
        }

        public string LoginText
        {
            get { return this.loginText; }
            set
            {
                if (this.loginText == value)
                    return;

                this.loginText = value;
                this.OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return this.email; }
            set
            {
                if (this.email == value)
                    return;

                this.email = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsInvalidEmail
        {
            get { return this.isInvalidEmail; }
            set
            {
                if (this.isInvalidEmail == value)
                    return;

                this.isInvalidEmail = value;
                this.OnPropertyChanged();
            }
        }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public LoginViewModel()
        {
            LoginText = "Login/Sign up";
            LoginState = LoginStates.EmailEntry;
            auth = DependencyService.Get<IAuth>();
        }

        async Task Login()
        {
            var result = await auth.LoginWithEmailPassword(Email, Password);
            if (result.Item1 == LoginResult.Success)
            {
                tokenWrapper.SaveCredentials(result.Item2);
                await App.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            else
            {
                ShowError(result.Item1);
            }
        }

        async Task SignUp()
        {
            var result = await auth.SignUpWithEmailPassword(Email, Password);
            if (result.Item1 == LoginResult.Success)
            {
                tokenWrapper.SaveCredentials(result.Item2);
                await App.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            else
            {
                ShowError(result.Item1);
            }
        }

        async Task<bool> EmailExists(string email)
        {
            var result = await auth.SignUpWithEmailPassword(Email, "x_test_pwd");
            var exists = result.Item1 == LoginResult.SignUpUserExists;

            LoginText = exists ? "Login" : "Sign Up";

            return exists;
        }

        async private void ShowError(LoginResult result)
        {
            switch (result)
            {
                case LoginResult.LoginInvalidPassword:
                    await App.Current.MainPage.DisplayAlert("Login Failed", "Password is incorrect.", "OK");
                    break;
                case LoginResult.LoginInvalidUser:
                    await App.Current.MainPage.DisplayAlert("Login Failed", "Email is incorrect.", "OK");
                    break;
                case LoginResult.SignUpBadEmail:
                    await App.Current.MainPage.DisplayAlert("Sign Up Failed", "Email is incorrect.", "OK");
                    break;
                case LoginResult.SignUpWeakPassword:
                    await App.Current.MainPage.DisplayAlert("Login Failed", "Password is not strong enough.", "OK");
                    break;
                case LoginResult.SignUpUserExists:
                    await App.Current.MainPage.DisplayAlert("Login Failed", "Password is incorrect.", "OK");
                    break;
            }
        }

        public async Task EmailFormExited()
        {
            if (this.isInvalidEmail)
            {
                await App.Current.MainPage.DisplayAlert("Invalid Email", "Please enter a valid email.", "OK");
                LoginState = LoginStates.EmailEntry;
            } else
            {
                var emailExists = await EmailExists(this.Email);
                LoginState = emailExists ? LoginStates.Login : LoginStates.SignUp;
            }
        }

        public Command LoginCommand { get { return new Command(LoginButtonPressed); } }
        public async void LoginButtonPressed()
        {
            if (LoginState == LoginStates.Login)
            {
                await Login();
            } else if (LoginState == LoginStates.SignUp)
            {
                await SignUp();
            } else
            {
                await EmailFormExited();
            }
        }
    }

    public enum LoginStates
    {
        EmailEntry,
        Login,
        SignUp
    }
}
