using System.Diagnostics;
using CrudUser.Models;
using Laboratorio_projeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;



namespace Laboratorio_projeto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Conexao _conexao;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, Conexao conexao, IConfiguration configuration)
        {
            _logger = logger;
            _conexao = conexao;
            _configuration = configuration;
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

            var accountSid = _configuration["Twilio:AccountSid"];
            var authToken = _configuration["Twilio:AuthToken"];
            var fromNumber = _configuration["Twilio:FromNumber"];

            TwilioClient.Init(accountSid, authToken);

            var toNumber = pessoas.Telefone; 

            var messageBody = $"Olá {pessoas.Nome}. Esta é uma confirmação via SMS da TICOM LABS.";

            if (!toNumber.StartsWith("+"))
            {
                toNumber = "+55" + toNumber; 
            }

            var message = MessageResource.Create(
                to: new PhoneNumber(toNumber),
                from: new PhoneNumber(fromNumber),
                body: messageBody
            );

            TempData["Mensagem4"] = "SMS enviado com sucesso!";

            return RedirectToAction("PaginaExames");
        }

        public IActionResult Editar(string cpf)
        {

            var Pessoas = _conexao.Pessoas.FirstOrDefault(p => p.CPF == cpf);

            if (Pessoas == null)
            {
                TempData["MensagemErro"] = "Usuario não encontrado";
            }

            return View(Pessoas);
        }

        [HttpPost]
        public IActionResult EditarUsuario(string cpf, Pessoas pessoa)
        {
            var Pessoas = _conexao.Pessoas.FirstOrDefault(p => p.CPF == cpf);

            if (Pessoas == null)
            {
                TempData["Mensagem"] = "Usuario não encontrado";
            }

            if (pessoa.CPF != Pessoas.CPF)
            {
                TempData["Mensagem"] = "Não é permitido editar o CPF";
            }

            Pessoas.Nome = pessoa.Nome;
            Pessoas.Telefone = pessoa.Telefone;
            Pessoas.Convenio = pessoa.Convenio;
            Pessoas.Plano = pessoa.Plano;

            _conexao.Update(Pessoas);
            _conexao.SaveChanges();

            TempData["MensagemSalvo"] = "Usuário editado com sucesso!";

            return View("PaginaUsuarios");
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
