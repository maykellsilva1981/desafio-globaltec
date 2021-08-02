using ProjetoApi.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoApi.Models
{
    public class InicializaBd
    {
        public static void Inicializar(ContextoBd contexto)
        {
            contexto.Database.EnsureCreated();

            if (contexto.Pessoas.Any())
            {
                return;
            }

            var senha = Util.GerarHashMD5("123");

            var maykell = new Pessoa { Nome = "Maykell", CPF = "11111111111", Email = "maykell.ribeiro@gmail.com", Senha = senha, UF = "GO", DataNascimento = DateTime.ParseExact("06/09/1981", "dd/MM/yyyy", CultureInfo.InvariantCulture) };
            
            contexto.Pessoas.Add(maykell);
            
            contexto.SaveChanges();
           
        }
    }
}
