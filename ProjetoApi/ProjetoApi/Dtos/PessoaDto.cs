using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProjetoApi.Models;

namespace ProjetoApi.Dtos
{
    public class PessoaDto
    {
        public PessoaDto()
        {

        }

        public PessoaDto(Pessoa pessoa)
        {
            PessoaId = pessoa.pessoaId;
            Nome = pessoa.Nome;
            CPF = pessoa.CPF;
            Email = pessoa.Email;
            UF = pessoa.UF;
            DataNascimento = pessoa.DataNascimento;
        }


        public int PessoaId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(2, ErrorMessage = "O nome deve conter no mínimo 2 caracteres")]
        [MaxLength(500, ErrorMessage = "O nome deve ter no máximo 500 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [MinLength(11, ErrorMessage = "CPF deve conter 11 caracteres. Retire outros caracteres como . - ou /")]
        [MaxLength(11, ErrorMessage = "CPF deve conter 11 caracteres. Retire outros caracteres como . - ou /")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A UF é obrigatória")]
        [MinLength(2, ErrorMessage = "A UF deve ser informada no padrão de sigla com duas letras. Ex.: GO")]
        [MaxLength(2, ErrorMessage = "A UF deve ser informada no padrão de sigla com duas letras. Ex.: GO")]
        public string UF { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime DataNascimento { get; set; }

        public Pessoa Topessoa(Pessoa pessoa = null)
        {
            if (pessoa == null)
                pessoa = new Pessoa();

            pessoa.pessoaId = PessoaId;
            pessoa.Nome = Nome;
            pessoa.CPF = CPF;
            pessoa.Email = Email;
            pessoa.UF = UF;
            pessoa.DataNascimento = Convert.ToDateTime(DataNascimento.ToString("dd/MM/yyyy"));

            return pessoa;
        }
    }
}
