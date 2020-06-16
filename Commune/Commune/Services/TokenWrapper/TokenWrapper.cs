using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Commune.Services.TokenWrapper
{
    public class TokenWrapper: ITokenWrapper
    {
        private const string tokenKey = "userToken";

        public async Task<string> GetCredentials()
        {
            string token = null;
            try
            {
                token = await SecureStorage.GetAsync(tokenKey);
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Could Not Get User Token", e.Message + " : " + (e.InnerException == null ? string.Empty : e.InnerException.Message), "OK");
            }
            return token;
        }

        public async void SaveCredentials(string token)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                try
                {
                    await SecureStorage.SetAsync(tokenKey, token);
                }
                catch (Exception e)
                {
                    await App.Current.MainPage.DisplayAlert("Could Not Save User Token", e.Message + " : " + (e.InnerException == null ? string.Empty : e.InnerException.Message), "OK");
                }
            }
        }

        public async void RemoveCredentials()
        {
            try
            {
                SecureStorage.Remove(tokenKey);
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Could Not Remove User Token", e.Message + " : " + (e.InnerException == null ? string.Empty : e.InnerException.Message), "OK");
            }
        }
    }
}
