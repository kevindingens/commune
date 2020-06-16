using System;
using System.Collections.Generic;
using System.Text;

namespace Commune.ViewModels
{
    public class MasterViewModel: BaseViewModel
    {
        private string email;
        public string Email
        {
            get { return this.email;  }
            set {
                if (this.email == value)
                    return;

                this.email = value;
                this.OnPropertyChanged();
            }
        }
    }
}
