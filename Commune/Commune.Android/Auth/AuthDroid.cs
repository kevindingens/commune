using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Commune.Droid.Auth;
using Commune.Shared.Auth;
using Commune.Shared.Enums;
using Firebase.Auth;

[assembly: Xamarin.Forms.Dependency(typeof(AuthDroid))]
namespace Commune.Droid.Auth
{
    public class AuthDroid: IAuth
    {
        public async Task<(LoginResult, string)> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdTokenAsync(false);
                return (LoginResult.Success, token.Token);
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return (LoginResult.LoginInvalidUser, string.Empty);
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                return (LoginResult.LoginInvalidPassword, string.Empty);
            }
            catch (Exception e)
            {
                return (LoginResult.Fail, string.Empty);
            }
        }

        public async Task<(LoginResult, string)> SignUpWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdTokenAsync(false);
                return (LoginResult.Success, token.Token);
            }
            catch (FirebaseAuthWeakPasswordException e)
            {
                e.PrintStackTrace();
                return (LoginResult.SignUpWeakPassword, string.Empty);
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                return (LoginResult.SignUpBadEmail, string.Empty);
            }
            catch (FirebaseAuthUserCollisionException e)
            {
                e.PrintStackTrace();
                return (LoginResult.SignUpUserExists, string.Empty);
            }
            catch (Exception e)
            {
                return (LoginResult.Fail, string.Empty);
            }
        }
    }
}