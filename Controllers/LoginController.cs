using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaCadastroEleitoral.Models;
using SistemaCadastroEleitoral.Infraestrutura.Data;

namespace SistemaCadastroEleitoral.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Route("/login/logar")]
    [HttpPost]
    public IActionResult Logar(string login, string senha)
    {
        if(string.IsNullOrEmpty(login) || string.IsNullOrEmpty(senha))
        {
            ViewBag.Error = "Digite Login e Senha";
        }
        else
        {
            var adm = new BancoContext().Admins.Where(a => a.Login == login && a.Senha == senha).ToList();
            if(adm.Count > 0)
            {
                //escrever o cookie
                this.HttpContext.Response.Cookies.Append("adm_sis", adm.First().Id.ToString(), new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(1),
                    HttpOnly = true
                });
                Response.Redirect("/Home/Index");

            }
            else
            {
                ViewBag.Error = "Dados inválidos, por favor tente novamente.";
            }
        }

        return View("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
