using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjetoApi.Dtos;
using ProjetoApi.Helpers;
using ProjetoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjetoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ContextoBd _contexto;

        public LoginController(IConfiguration configuration, ContextoBd contexto)
        {
            _configuration = configuration;
            _contexto = contexto;
        }

        /// <summary>
        /// Executa o login retornando um objeto contendo o token.
        /// </summary>
        /// <param name="login">Informe o e-mail e senha</param>
        /// <returns>Retorna um objeto contendo o token.</returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult ObterToken(LoginDto login)
        {
            var pessoa = _contexto.Pessoas.FirstOrDefault(n => n.Email == login.Email);

            //Valida se a matrícula e senha estão corretas.
            if (pessoa == null || pessoa.Senha != Util.GerarHashMD5(login.Senha))
                return BadRequest("Credenciais Inválidas...");



            //Monta o token com alguns dados do usuário.
            byte[] key = Encoding.ASCII.GetBytes(_configuration["Securitykey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, pessoa.Nome),
                    new Claim(ClaimTypes.SerialNumber, pessoa.pessoaId.ToString()),
                    new Claim(ClaimTypes.Email, pessoa.Email),
                    //new Claim("IdDispositivo", login.IdDispositivo )
                }),

                // Expiração do token de 1 dia
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);

            //instancia um objeto para retornar os dados do usuário e o token.
            var retorno = new
            {
                pessoa.Nome,
                pessoa.Email,
                Expira = tokenDescriptor.Expires.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return Ok(retorno);
        }

    }
}