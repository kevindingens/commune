using System;
using System.Collections.Generic;
using System.Text;

namespace Commune.Shared.Enums
{
    public enum LoginResult
    {
        Success,
        Fail,
        SignUpUserExists,
        SignUpWeakPassword,
        SignUpBadEmail,
        LoginInvalidUser,
        LoginInvalidPassword
    }
}
