using System.Diagnostics;
using CrudUser.Models;
using Laboratorio_projeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Laboratorio_projeto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Conexao _conexao;

        public HomeController(ILogger<HomeController> logger, Conexao conexao)
        {
            _logger = logger;
            _conexao = conexao;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult PaginaExames()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Pessoas pessoas, [FromServices] Conexao db)
        {
            pessoas.Validar();
            if (db.Pessoas.Any(c => c.CPF == pessoas.CPF))
            {
                ModelState.AddModelError("CPF", "CPF já cadastrado.");
            }
            if (!ModelState.IsValid)
            {
                return View("Index", pessoas);
            }
            _conexao.Pessoas.Add(pessoas);
            _conexao.SaveChanges();
            TempData["Mensagem"] = "Cadastrado com sucesso!";

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult ExamesMarcar(PessoasExames model, string cpf) 
        {
            if (model.Exames == null)
            {
                TempData["Mensagem"] = "Selecione os exames para marcar";
                return RedirectToAction("PaginaExames");
            }
            var pessoas = _conexao.Pessoas.FirstOrDefault(c => c.CPF == cpf);
            if (pessoas == null)
            {
                TempData["Mensagem"] = "Usuário não encontrado.";
                return RedirectToAction("PaginaExames", pessoas);
            }

            _conexao.PessoasExames.Add(model);
            _conexao.SaveChanges();

            TempData["Mensagem3"] = $"Exames marcados com sucesso!, Para: {pessoas.Nome}, CPF: {pessoas.CPF}, Convênio: {pessoas.Convenio}";

            return RedirectToAction("PaginaExames");
        }

        [HttpGet]
        public IActionResult Buscar(string cpf)
        {
            var pessoa = _conexao.Pessoas.FirstOrDefault(c => c.CPF == cpf);

            if (pessoa == null)
            {
                TempData["Mensagem"] = "Usuário não encontrado.";
                return RedirectToAction("PaginaUsuarios");
            }

            return RedirectToAction("PaginaUsuarios", new { cpf = pessoa.CPF });
        }

        public IActionResult PaginaUsuarios(string cpf)
        {
            var pessoas = _conexao.Pessoas
                .Where(p => p.CPF == cpf)
                .Include(p => p.ExamesMarcados) 
                .ToList();

            return View(pessoas);
        }

        [HttpPost]
        public IActionResult Excluir(string cpf)
        {
            var pessoa = _conexao.Pessoas
                .Include(p => p.ExamesMarcados) 
                .FirstOrDefault(p => p.CPF == cpf);

            if (pessoa == null)
            {
                TempData["Mensagem"] = "Usuário não encontrado";
                return RedirectToAction("PaginaUsuarios");
            }

            if (pessoa.ExamesMarcados != null)
            {
                _conexao.PessoasExames.RemoveRange(pessoa.ExamesMarcados);
            }

            _conexao.Pessoas.Remove(pessoa);
            _conexao.SaveChanges();

            TempData["Mensagem"] = "Usuário excluído com sucesso!";
            return RedirectToAction("PaginaUsuarios");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
