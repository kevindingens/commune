using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commune.Shared.Auth;
using Commune.Shared.Enums;
using Firebase.Auth;
using Foundation;
using Plugin.CloudFirestore;
using UIKit;

namespace Commune.iOS.Auth
{
    public class AuthIOS: IAuth
    {
        public Task<(LoginResult, string)> LoginWithEmailPassword(string email, string password)
        {
            var result = new TaskCompletionSource<(LoginResult, string)>();
            try
            {
                Firebase.Auth.Auth.DefaultInstance.SignInWithPassword(email, password, (AuthDataResult authResult, Foundation.NSError error) => {
                    if (error == null)
                    {
                        authResult.User.GetIdToken((string token, Foundation.NSError error1) => {
                            result.SetResult((LoginResult.Success, token));
                        });
                    }
                    else
                    {
                        AuthErrorCode errorCode;
                        if (IntPtr.Size == 8)
                            errorCode = (AuthErrorCode)((long)error.Code);
                        else
                            errorCode = (AuthErrorCode)((int)error.Code);
                        switch (errorCode)
                        {
                            case AuthErrorCode.InvalidEmail:
                                result.SetResult((LoginResult.LoginInvalidUser, string.Empty));
                                break;
                            case AuthErrorCode.WrongPassword:
                                result.SetResult((LoginResult.LoginInvalidPassword, string.Empty));
                                break;
                        }
                    }
                });
            }
            catch (Exception e)
            {
                result.SetResult((LoginResult.Fail, string.Empty));
            }
            return result.Task;
        }

        public Task<(LoginResult, string)> SignUpWithEmailPassword(string email, string password)
        {
            var result = new TaskCompletionSource<(LoginResult,string)>();
            try
            {
                Firebase.Auth.Auth.DefaultInstance.CreateUser(email, password, (AuthDataResult authResult, Foundation.NSError error) => {
                    if (error == null)
                    {
                        authResult.User.GetIdToken((string token, Foundation.NSError error1) => {
                            result.SetResult((LoginResult.Success, token));
                        });
                    } else
                    {
                        AuthErrorCode errorCode;
                        if (IntPtr.Size == 8)
                            errorCode = (AuthErrorCode)((long)error.Code);
                        else
                            errorCode = (AuthErrorCode)((int)error.Code);
                        switch (errorCode)
                        {
                            case AuthErrorCode.InvalidEmail:
                                result.SetResult((LoginResult.SignUpBadEmail, string.Empty));
                                break;
                            case AuthErrorCode.WeakPassword:
                                result.SetResult((LoginResult.SignUpWeakPassword, string.Empty));
                                break;
                            case AuthErrorCode.EmailAlreadyInUse:
                                result.SetResult((LoginResult.SignUpUserExists, string.Empty));
                                break;
                        }
                    }
                });
            }
            catch (Exception e)
            {
                result.SetResult((LoginResult.Fail, string.Empty));
            }
            return result.Task;
        }
    }
}