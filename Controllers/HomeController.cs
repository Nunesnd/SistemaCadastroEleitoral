using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaCadastroEleitoral.Models;
using SistemaCadastroEleitoral.Infraestrutura.Autenticacao;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;



namespace SistemaCadastroEleitoral.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [Logado]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Sair()
    {
        this.HttpContext.Response.Cookies.Delete("adm_sis");
        return Redirect("/login");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
