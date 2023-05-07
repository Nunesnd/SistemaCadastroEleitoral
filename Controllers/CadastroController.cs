using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaCadastroEleitoral.Infraestrutura.Autenticacao;
using SistemaCadastroEleitoral.Infraestrutura.Data;
using SistemaCadastroEleitoral.Models.Cadastro;

namespace SistemaCadastroEleitoral.Controllers
{
    public class CadastroController : Controller
    {
        private readonly BancoContext _context;

        public CadastroController(BancoContext context)
        {
            _context = context;
        }

        // GET: Cadastro        
        [Logado]
        public async Task<IActionResult> Index()
        {
            var bancoContext = _context.Cadastros.Include(c => c.Admin);
            return View(await bancoContext.ToListAsync());
        }

        // GET: Cadastro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cadastros == null)
            {
                return NotFound();
            }

            var cadastroModel = await _context.Cadastros
                .Include(c => c.Admin)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cadastroModel == null)
            {
                return NotFound();
            }

            return View(cadastroModel);
        }

        // GET: Cadastro/Create
        public IActionResult Create()
        {
            int adminId = Convert.ToInt32(HttpContext.Request.Cookies["adm_sis"]);
            ViewBag.AdminId = adminId;

            //ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "CPF");
            return View();
        }

        // POST: Cadastro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Apelido,CPF,Filhos,Nascimento,Fone,AdminId")] CadastroModel cadastroModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cadastroModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "CPF", cadastroModel.AdminId);
            return View(cadastroModel);
        }

        // GET: Cadastro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cadastros == null)
            {
                return NotFound();
            }

            var cadastroModel = await _context.Cadastros.FindAsync(id);
            if (cadastroModel == null)
            {
                return NotFound();
            }
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "CPF", cadastroModel.AdminId);
            return View(cadastroModel);
        }

        // POST: Cadastro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Apelido,CPF,Filhos,Nascimento,Fone,AdminId")] CadastroModel cadastroModel)
        {
            if (id != cadastroModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cadastroModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CadastroModelExists(cadastroModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "CPF", cadastroModel.AdminId);
            return View(cadastroModel);
        }

        // GET: Cadastro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cadastros == null)
            {
                return NotFound();
            }

            var cadastroModel = await _context.Cadastros
                .Include(c => c.Admin)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cadastroModel == null)
            {
                return NotFound();
            }

            return View(cadastroModel);
        }

        // POST: Cadastro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cadastros == null)
            {
                return Problem("Entity set 'BancoContext.Cadastros'  is null.");
            }
            var cadastroModel = await _context.Cadastros.FindAsync(id);
            if (cadastroModel != null)
            {
                _context.Cadastros.Remove(cadastroModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CadastroModelExists(int id)
        {
          return _context.Cadastros.Any(e => e.Id == id);
        }
    }
}
