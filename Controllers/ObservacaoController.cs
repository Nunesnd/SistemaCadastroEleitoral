using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaCadastroEleitoral.Infraestrutura.Data;
using SistemaCadastroEleitoral.Infraestrutura.Autenticacao;
using SistemaCadastroEleitoral.Models.Observacao;

namespace SistemaCadastroEleitoral.Controllers
{
    public class ObservacaoController : Controller
    {
        private readonly BancoContext _context;

        public ObservacaoController(BancoContext context)
        {
            _context = context;
        }

        // GET: Observacao
        [Logado]
        public async Task<IActionResult> Index()
        {
            var bancoContext = _context.Observacoes.Include(o => o.Cadastro);
            return View(await bancoContext.ToListAsync());
        }

        // GET: Observacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Observacoes == null)
            {
                return NotFound();
            }

            var observacaoModel = await _context.Observacoes
                .Include(o => o.Cadastro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (observacaoModel == null)
            {
                return NotFound();
            }

            return View(observacaoModel);
        }

        // GET: Observacao/Create
        public IActionResult Create()
        {
            ViewData["cadastroId"] = new SelectList(_context.Cadastros, "Id", "Nome");
            return View();
        }

        // POST: Observacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Conteudo,Data,cadastroId")] ObservacaoModel observacaoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(observacaoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["cadastroId"] = new SelectList(_context.Cadastros, "Id", "Nome", observacaoModel.cadastroId);
            return View(observacaoModel);
        }

        // GET: Observacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Observacoes == null)
            {
                return NotFound();
            }

            var observacaoModel = await _context.Observacoes.FindAsync(id);
            if (observacaoModel == null)
            {
                return NotFound();
            }
            ViewData["cadastroId"] = new SelectList(_context.Cadastros, "Id", "Nome", observacaoModel.cadastroId);
            return View(observacaoModel);
        }

        // POST: Observacao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Conteudo,Data,cadastroId")] ObservacaoModel observacaoModel)
        {
            if (id != observacaoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(observacaoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObservacaoModelExists(observacaoModel.Id))
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
            ViewData["cadastroId"] = new SelectList(_context.Cadastros, "Id", "Nome", observacaoModel.cadastroId);
            return View(observacaoModel);
        }

        // GET: Observacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Observacoes == null)
            {
                return NotFound();
            }

            var observacaoModel = await _context.Observacoes
                .Include(o => o.Cadastro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (observacaoModel == null)
            {
                return NotFound();
            }

            return View(observacaoModel);
        }

        // POST: Observacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Observacoes == null)
            {
                return Problem("Entity set 'BancoContext.Observacoes'  is null.");
            }
            var observacaoModel = await _context.Observacoes.FindAsync(id);
            if (observacaoModel != null)
            {
                _context.Observacoes.Remove(observacaoModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ObservacaoModelExists(int id)
        {
          return _context.Observacoes.Any(e => e.Id == id);
        }
    }
}
