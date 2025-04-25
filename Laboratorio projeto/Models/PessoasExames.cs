using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratorio_projeto.Models
{
    [Table("ExamesTabela2")]
    public class PessoasExames
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Os exames são obrigatórios")]
        public List<string> Exames { get; set; }

        [Required]
        public string CPF { get; set; } 

        [ForeignKey("CPF")]
        public Pessoas Pessoa { get; set; }
    }
}
