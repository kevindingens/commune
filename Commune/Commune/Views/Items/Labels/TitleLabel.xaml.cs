using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Commune.Views.Items.Labels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TitleLabel : Label
    {
        public TitleLabel()
        {
            InitializeComponent();
        }
    }
}