using Commune.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commune.Shared.Auth
{
    public interface IAuth
    {
        Task<(LoginResult,string)> LoginWithEmailPassword(string email, string password);
        Task<(LoginResult, string)> SignUpWithEmailPassword(string email, string password);
    }
}
