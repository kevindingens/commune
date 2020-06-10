﻿using Commune.Shared.Auth;
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
        public event PropertyChangedEventHandler PropertyChanged;

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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ShowPassword"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ShowConfirmPassword"));
            }
        }

        public string LoginText
        {
            get { return this.LoginText; }
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

        async void Login(object sender, EventArgs e)
        {
            var result = await auth.LoginWithEmailPassword(Email, Password);
            if (result.Item1 == LoginResult.Success)
            {
                await App.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            else
            {
                ShowError(result.Item1);
            }
        }

        async void SignUp(object sender, EventArgs e)
        {
            var result = await auth.SignUpWithEmailPassword(Email, Password);
            if (result.Item1 == LoginResult.Success)
            {
                await App.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            else
            {
                ShowError(result.Item1);
            }
        }

        async Task<bool> EmailExists(string email)
        {
            var result = await auth.SignUpWithEmailPassword(Email, string.Empty);
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

        public async void EmailFormExited()
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
                var loginResult = await auth.LoginWithEmailPassword(Email, Password);
                if (loginResult.Item1 == LoginResult.Success)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                } else
                {
                    ShowError(loginResult.Item1);
                }
            } else if (LoginState == LoginStates.SignUp)
            {
                var signupResult = await auth.SignUpWithEmailPassword(Email, Password);
                if (signupResult.Item1 == LoginResult.Success)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                }
                else
                {
                    ShowError(signupResult.Item1);
                }
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
