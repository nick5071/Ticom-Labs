namespace Laboratorio_projeto.Models
{
    public class PessoasViewModel
    {
            public Pessoas Pessoa { get; set; }
            public PessoasExames PessoaExame { get; set; }
            public string CPFBusca { get; set; }
            public List<string> ExamesSelecionados { get; set; } = new List<string>();
    }
}