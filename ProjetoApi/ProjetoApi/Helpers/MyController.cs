using Microsoft.AspNetCore.Mvc;
using ProjetoApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjetoApi.Helpers
{
    public class MyController : ControllerBase
    {
        public Pessoa pessoaLogado
        {
            get
            {

                var user = HttpContext.User;

                var pessoaLogado = new Pessoa
                {
                    Nome = user.Claims.FirstOrDefault(n => n.Type == ClaimTypes.Name).Value,
                    Email = user.Claims.FirstOrDefault(n => n.Type == ClaimTypes.Email).Value,
                    pessoaId = Convert.ToInt32(user.Claims.FirstOrDefault(n => n.Type == ClaimTypes.SerialNumber).Value)
                };

                return pessoaLogado;
            }
        }
    }
}
