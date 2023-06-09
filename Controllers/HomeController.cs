﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaCadastroEleitoral.Models;
using SistemaCadastroEleitoral.Infraestrutura.Data;
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
    
    private readonly BancoContext _context;

    public HomeController(BancoContext context)
    {
        _context = context;
    }

    [Logado]
    public IActionResult Index()
    {
        string adminName = GetAdminName();
        ViewBag.AdminName = adminName;
        return View();
    }

    private string GetAdminName()
    {
        int adminId;
        bool isAdminIdPresent = int.TryParse(HttpContext.Request.Cookies["adm_sis"], out adminId);
        if (isAdminIdPresent)
        {
            //var admin = _context.Admins.FirstOrDefault(a => a.Id == adminId);
            var admin = _context.Admins.FirstOrDefault(a => a.Id == adminId);
            if (admin != null)
            {
                return admin.NomeAdmin;
            }
        }
        return null;
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
