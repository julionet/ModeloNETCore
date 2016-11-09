using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Service.Security
{
    public class Token
    {
        private const string passwordPhrase = "_GxYhJ@159753_";

        public static string GerarToken(IMemoryCache cache)
        {
            string guid = Guid.NewGuid().ToString("N").ToUpper();
            MemoryCacheEntryOptions opcoesCache = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(10)
            };
            cache.Set(guid, guid, opcoesCache);
            return StringToBase64(guid);
        }

        public static bool ValidarToken(string token, IMemoryCache cache)
        {
            if (!IsBase64(token))
                return false;
            {
                string tokenNovo = Base64ToString(token);
                if (!tokenNovo.Contains("|"))
                    return false;
                else
                {
                    if (tokenNovo.Split('|')[0] != passwordPhrase)
                        return false;
                    else
                    {
                        string guid = "";
                        return cache.TryGetValue(tokenNovo.Split('|')[1], out guid);
                    }
                }
            }
        }

        public static string Base64ToString(string s)
        {
            byte[] b = Convert.FromBase64String(s);
            return Encoding.GetEncoding("ISO-8859-1").GetString(b);
        }

        public static string StringToBase64(string s)
        {

            return Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(s));
        }
        
        public static bool IsBase64(string base64String)
        {
            if ((base64String == null) || (base64String.Length == 0) || (base64String.Length % 4 != 0) || 
                base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n"))
                return false;

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}