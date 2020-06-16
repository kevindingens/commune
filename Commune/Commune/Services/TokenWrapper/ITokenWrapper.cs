using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commune.Services.TokenWrapper
{
    public interface ITokenWrapper
    {
        Task<string> GetCredentials();
        void SaveCredentials(string token);
        void RemoveCredentials();
    }
}
