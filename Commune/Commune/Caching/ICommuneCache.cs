using System;
using System.Collections.Generic;
using System.Text;

namespace Commune.Caching
{
    public interface ICommuneCache
    {
        void SetToken(string value);
        string GetToken();
        void Set<T>(string key, T value);
        T Get<T>(string key);
        string GetEmail();
    }
}
