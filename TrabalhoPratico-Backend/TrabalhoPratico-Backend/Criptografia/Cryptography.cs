using System;
using System.Security.Cryptography;
using System.Text;

namespace TrabalhoPratico_Backend.Criptografia
{
    public class Cryptography
    {
        public string Valor { get; set; }

        public string Encrypt(string valor)
        {
            try
            {
                if (!string.IsNullOrEmpty(valor))
                {
                    var sb = new StringBuilder();
                    foreach (var c in MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(valor)))
                    {
                        sb.Append(c.ToString("X2"));
                    }
                    return sb.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
