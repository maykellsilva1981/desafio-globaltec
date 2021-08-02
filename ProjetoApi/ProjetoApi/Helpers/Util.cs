using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoApi.Helpers
{
    public class Util
    {
        /// <summary>
        /// Método responsável por receber um texto e efetuar o Hash MD5 do mesmo
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string GerarHashMD5(string texto)
        {
            // Cria o Hash MD5 hash
            var oMD5Provider = new MD5CryptoServiceProvider();

            // Gera o Hash Code
            var bytHashCode = oMD5Provider.ComputeHash(Encoding.Default.GetBytes(texto));

            var resultadoHash = new StringBuilder();

            for (int i = 0; i < bytHashCode.Length; i++)
                resultadoHash.Append(bytHashCode[i].ToString("x2"));

            return resultadoHash.ToString();
        }

    }
}
