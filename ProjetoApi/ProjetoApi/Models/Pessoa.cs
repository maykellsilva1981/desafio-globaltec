using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoApi.Models
{
    public class Pessoa
    {
        public int pessoaId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string UF { get; set; }
        public DateTime DataNascimento { get; set; }

    }
}
