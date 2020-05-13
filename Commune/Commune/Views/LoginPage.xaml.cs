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
        public LoginPage()
        {
            InitializeComponent();
        }

        public void LoginButtonPressed(object sender, System.EventArgs e)
        {
            var passwordEntry = new Entry { VerticalOptions = LayoutOptions.Fill, Placeholder = "Password" };
            ContentGrid.Children.Add(passwordEntry, 4, 0);
            ContentGrid.RowDefinitions.ElementAt(4).Height = GridLength.Auto;
        }
    }
}