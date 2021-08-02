using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoApi.Dtos;
using ProjetoApi.Helpers;
using ProjetoApi.Models;

namespace ProjetoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly ContextoBd _context;

        public PessoasController(ContextoBd context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaDto>>> GetPessoas()
        {
            var lista = await _context.Pessoas.Select(n => new PessoaDto(n)).ToListAsync();
            return lista;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaDto>> Getpessoa(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);

            if (pessoa == null)
            {
                return BadRequest("Pessoa não encontrada");

            }

            return new PessoaDto(pessoa);
        }
        [HttpGet("BuscaPessoaPorUf")]
        public async Task<ActionResult<IEnumerable<PessoaDto>>> BuscaPessoaPorUf(string uf)
        {
            if (string.IsNullOrEmpty(uf))
                return BadRequest("Necessário informar a UF!");

            var lista = await _context.Pessoas
                                .Where(n => n.UF == uf)
                                .Select(n => new PessoaDto(n))
                                .ToListAsync();
            if (lista.Count == 0)
                return BadRequest("Não foram encontrados registros para esse estado");

            return lista;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Putpessoa(int id, PessoaDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var pessoa = await _context.Pessoas.FirstOrDefaultAsync(n => n.pessoaId == id);

            if (pessoa == null)
            {
                return BadRequest("Pessoa não encontrada!");
            }

            pessoa.Nome = dto.Nome;
            pessoa.CPF = dto.CPF;
            pessoa.UF = dto.UF;
            pessoa.DataNascimento = dto.DataNascimento;
            pessoa.Email = dto.Email;
            await _context.SaveChangesAsync();

            //return NoContent();
            return CreatedAtAction("Getpessoa", new { id = pessoa.pessoaId }, pessoa);
        }

        [HttpPost]
        public async Task<ActionResult<PessoaDto>> Postpessoa(PessoaDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var emailCadastro = await _context.Pessoas.FirstOrDefaultAsync(n => n.Email == dto.Email);

            if (emailCadastro != null)
                return BadRequest("E-Mail Já Cadastrado!");

            var pessoa = dto.Topessoa();
            pessoa.Senha = Util.GerarHashMD5("123");

            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();

            //return NoContent();
            return CreatedAtAction("Getpessoa", new { id = pessoa.pessoaId }, pessoa);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Deletepessoa(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();

            return Content("Exclusão realizada com sucesso!"); ;
        }
    }
}
