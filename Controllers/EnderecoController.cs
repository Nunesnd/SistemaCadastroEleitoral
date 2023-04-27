using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaCadastroEleitoral.Infraestrutura.Data;
using SistemaCadastroEleitoral.Models.Endereco;

namespace SistemaCadastroEleitoral.Controllers
{
    public class EnderecoController : Controller
    {
        private readonly BancoContext _context;

        public EnderecoController(BancoContext context)
        {
            _context = context;
        }

        // GET: Endereco
        public async Task<IActionResult> Index()
        {
            var bancoContext = _context.Enderecos.Include(e => e.Cadastro);
            return View(await bancoContext.ToListAsync());
        }

        // GET: Endereco/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Enderecos == null)
            {
                return NotFound();
            }

            var enderecoModel = await _context.Enderecos
                .Include(e => e.Cadastro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enderecoModel == null)
            {
                return NotFound();
            }

            return View(enderecoModel);
        }

        // GET: Endereco/Create
        public IActionResult Create()
        {
            ViewData["cadastroId"] = new SelectList(_context.Cadastros, "Id", "Nome");
            return View();
        }

        // POST: Endereco/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Logradouro,Número,Complemento,Bairro,Cidade,UF,CEP,cadastroId")] EnderecoModel enderecoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enderecoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["cadastroId"] = new SelectList(_context.Cadastros, "Id", "Nome", enderecoModel.cadastroId);
            return View(enderecoModel);
        }

        // GET: Endereco/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Enderecos == null)
            {
                return NotFound();
            }

            var enderecoModel = await _context.Enderecos.FindAsync(id);
            if (enderecoModel == null)
            {
                return NotFound();
            }
            ViewData["cadastroId"] = new SelectList(_context.Cadastros, "Id", "Nome", enderecoModel.cadastroId);
            return View(enderecoModel);
        }

        // POST: Endereco/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logradouro,Número,Complemento,Bairro,Cidade,UF,CEP,cadastroId")] EnderecoModel enderecoModel)
        {
            if (id != enderecoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enderecoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnderecoModelExists(enderecoModel.Id))
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
            ViewData["cadastroId"] = new SelectList(_context.Cadastros, "Id", "Nome", enderecoModel.cadastroId);
            return View(enderecoModel);
        }

        // GET: Endereco/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Enderecos == null)
            {
                return NotFound();
            }

            var enderecoModel = await _context.Enderecos
                .Include(e => e.Cadastro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enderecoModel == null)
            {
                return NotFound();
            }

            return View(enderecoModel);
        }

        // POST: Endereco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Enderecos == null)
            {
                return Problem("Entity set 'BancoContext.Enderecos'  is null.");
            }
            var enderecoModel = await _context.Enderecos.FindAsync(id);
            if (enderecoModel != null)
            {
                _context.Enderecos.Remove(enderecoModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnderecoModelExists(int id)
        {
          return _context.Enderecos.Any(e => e.Id == id);
        }
    }
}
