using System;
using Commune.ViewModels;
using Xamarin.Forms;

namespace Commune.Triggers
{
    public class EmailTrigger: TriggerAction<Entry>
    {
        public EmailTrigger()
        {
        }

        protected override void Invoke(Entry sender)
        {
            var viewModel = sender?.BindingContext as LoginViewModel;
            viewModel.EmailFormExited();
        }
    }
}