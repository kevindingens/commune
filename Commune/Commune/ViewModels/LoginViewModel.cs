using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Commune.ViewModels
{
    class LoginViewModel: BaseViewModel
    {
        private string email;

        private bool isInvalidEmail;

        private string loginText;

        public string LoginText
        {
            get
            {
                return this.loginText;
            }

            set
            {
                if (this.loginText == value)
                {
                    return;
                }

                this.loginText = value;
                this.OnPropertyChanged();
            }
        }

        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                if (this.email == value)
                {
                    return;
                }

                this.email = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsInvalidEmail
        {
            get
            {
                return this.isInvalidEmail;
            }

            set
            {
                if (this.isInvalidEmail == value)
                {
                    return;
                }

                this.isInvalidEmail = value;
                this.OnPropertyChanged();
            }
        }

        public LoginViewModel()
        {
            LoginText = "Login/Sign up";
        }

        public void EmailFormExited()
        {
            LoginText = "Login";
        }
    }
}
