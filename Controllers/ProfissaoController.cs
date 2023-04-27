using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaCadastroEleitoral.Infraestrutura.Data;
using SistemaCadastroEleitoral.Models.Profissao;

namespace SistemaCadastroEleitoral.Controllers
{
    public class ProfissaoController : Controller
    {
        private readonly BancoContext _context;

        public ProfissaoController(BancoContext context)
        {
            _context = context;
        }

        // GET: Profissao
        public async Task<IActionResult> Index()
        {
            var bancoContext = _context.Profissoes.Include(p => p.Cadastro);
            return View(await bancoContext.ToListAsync());
        }

        // GET: Profissao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Profissoes == null)
            {
                return NotFound();
            }

            var profissaoModel = await _context.Profissoes
                .Include(p => p.Cadastro)
                .FirstOrDefaultAsync(m => m.IdProfissao == id);
            if (profissaoModel == null)
            {
                return NotFound();
            }

            return View(profissaoModel);
        }

        // GET: Profissao/Create
        public IActionResult Create()
        {
            ViewData["cadastroId"] = new SelectList(_context.Cadastros, "Id", "Nome");
            return View();
        }

        // POST: Profissao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProfissao,NomeProfissao,cadastroId")] ProfissaoModel profissaoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profissaoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["cadastroId"] = new SelectList(_context.Cadastros, "Id", "Nome", profissaoModel.cadastroId);
            return View(profissaoModel);
        }

        // GET: Profissao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Profissoes == null)
            {
                return NotFound();
            }

            var profissaoModel = await _context.Profissoes.FindAsync(id);
            if (profissaoModel == null)
            {
                return NotFound();
            }
            ViewData["cadastroId"] = new SelectList(_context.Cadastros, "Id", "Nome", profissaoModel.cadastroId);
            return View(profissaoModel);
        }

        // POST: Profissao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProfissao,NomeProfissao,cadastroId")] ProfissaoModel profissaoModel)
        {
            if (id != profissaoModel.IdProfissao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profissaoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfissaoModelExists(profissaoModel.IdProfissao))
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
            ViewData["cadastroId"] = new SelectList(_context.Cadastros, "Id", "Nome", profissaoModel.cadastroId);
            return View(profissaoModel);
        }

        // GET: Profissao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Profissoes == null)
            {
                return NotFound();
            }

            var profissaoModel = await _context.Profissoes
                .Include(p => p.Cadastro)
                .FirstOrDefaultAsync(m => m.IdProfissao == id);
            if (profissaoModel == null)
            {
                return NotFound();
            }

            return View(profissaoModel);
        }

        // POST: Profissao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Profissoes == null)
            {
                return Problem("Entity set 'BancoContext.Profissoes'  is null.");
            }
            var profissaoModel = await _context.Profissoes.FindAsync(id);
            if (profissaoModel != null)
            {
                _context.Profissoes.Remove(profissaoModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfissaoModelExists(int id)
        {
          return _context.Profissoes.Any(e => e.IdProfissao == id);
        }
    }
}
