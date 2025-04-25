using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CrudUser.Models;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorio_projeto.Models
{
    [Table("Tabela Laboratorio")]
    public class Pessoas
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome pode ter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Key]
        [Required(ErrorMessage = "O CPF é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter exatamente 11 dígitos")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O Telefone deve ter exatamente 11 caracteres")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O Plano é obrigatório")]
        [StringLength(45, ErrorMessage = "O Plano só pode ter no máximo 45 caracteres")]
        public string Plano { get; set; }

        [Required(ErrorMessage = "O Convênio é obrigatório")]
        [StringLength(45, ErrorMessage = "O Convênio pode ter no máximo 45 caracteres")]
        public string Convenio { get; set; }

        public List<PessoasExames> ExamesMarcados { get; set; } = new List<PessoasExames>();

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nome) || Nome.Length > 100)
                throw new ApplicationException("O campo Nome é obrigatório");

            if (string.IsNullOrEmpty(CPF)|| CPF.Length != 11)
                throw new ApplicationException("O campo Nome é obrigatório");

            if (string.IsNullOrEmpty(Telefone)|| Telefone.Length != 11)
                throw new ApplicationException("O campo Telefone é obrigatório");

            if (string.IsNullOrEmpty(Plano) || Plano.Length > 45)
                throw new ApplicationException("O campo Plano é obrigatório");

            if (string.IsNullOrEmpty(Convenio)|| Convenio.Length > 45)
                throw new ApplicationException("O campo Convenio é obrigatório");

        }
    }
}