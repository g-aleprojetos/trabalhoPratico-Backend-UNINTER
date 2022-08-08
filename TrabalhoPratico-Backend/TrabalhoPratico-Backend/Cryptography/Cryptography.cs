using System;
using System.Security.Cryptography;
using System.Text;

namespace TrabalhoPratico_Backend.Criptografia
{
    public class Cryptography
    {
        //Método para encripitar a senha
        public string Encrypt(string valor)
        {
            try
            {   //verifica se o valor é nulo ou vazio
                if (!string.IsNullOrEmpty(valor))
                {
                    //variável local para manipular a cripitografia
                    var sb = new StringBuilder();
                    foreach (var c in MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(valor)))
                    {
                        sb.Append(c.ToString("X2"));
                    }
                    //retorno da senha cripografada
                    return sb.ToString();
                }
                else
                {
                    //Se caso o valor seja vazio ou nulo
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
