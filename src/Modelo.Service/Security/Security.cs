using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modelo.Service.Security
{
    public class Security
    {
        private Security()
        {
        }

        public static string Encrypt(string chave, string s)
        {
            string sNovaSenha = "";

            for (int i = 0; i < chave.Length; i++)
            {
                sNovaSenha = "";
                for (int j = 0; j < s.Length; j++)
                    sNovaSenha += Convert.ToChar((Convert.ToInt32(chave[i])) ^ (Convert.ToInt32(s[j])));
                s = sNovaSenha;
            }

            return sNovaSenha;
        }

        public static string Decrypt(string chave, string s)
        {
            string sNovaSenha = "";

            for (int i = 0; i < chave.Length; i++)
            {
                sNovaSenha = "";
                for (int j = 0; j < s.Length; j++)
                    sNovaSenha += Convert.ToChar((Convert.ToInt32(chave[i])) ^ (Convert.ToInt32(s[j])));
                s = sNovaSenha;
            }

            return sNovaSenha;
        }

        public static string GetHashAutenticacaoWebapi()
        {
            string _passwordKey = "_GxYhJ@159753$";
            return GetSHA1(_passwordKey + DateTime.Now.ToString("_dd_MM_yyyy_hh_"));
        }

        public static string Criptografe(string s, string k)
        {
            return Security.Uncriptografe(s, k);
        }

        public static string Uncriptografe(string s, string k)
        {
            string result = "";
            for (int i = 1; i <= s.Length; i++)
            {
                int jk = Convert.ToInt32(k[(i % k.Length)].ToString(), 16);
                int js = Convert.ToInt32(s[i - 1].ToString(), 16);
                result += (jk ^ js).ToString("X").Substring(0, 1);
            }
            return result;
        }

        public static string GetKey(string s)
        {
            int total = 0;
            int i = 1;
            foreach (char c in s.ToCharArray())
            {
                total += Convert.ToInt32(c.ToString(), 16) * i;
                i++;
            }
            return (total % 256).ToString("X").PadLeft(2, '0').Substring(0, 2);
        }

        public static string GetMD5(string s)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(s);
            byte[] hash = md5.ComputeHash(inputBytes);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (byte b in hash)
            {
                sb.Append(b.ToString("X2"));
            }
            return (sb.ToString().ToUpper());
        }

        public static string GetSHA1(string s)
        {
            System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();

            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(s);
            byte[] hash = sha1.ComputeHash(inputBytes);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (byte b in hash)
            {
                sb.Append(b.ToString("x2"));
            }

            return (sb.ToString().ToUpper());
        }

        public static string EncryptConnectionString(string connectionString)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(connectionString);
            return Convert.ToBase64String(b);
        }

        public static string DecryptConnectionString(string connectionString)
        {
            byte[] b = Convert.FromBase64String(connectionString);
            return System.Text.ASCIIEncoding.ASCII.GetString(b);
        }

        public static string CreatePasswordWeb()
        {
            string senha = (char)(new Random().Next(65, 90)) + new Random().Next(9999).ToString("0000");
            Thread.Sleep(300);
            return  senha + (char)(new Random().Next(65, 90));
        }
    }
}
