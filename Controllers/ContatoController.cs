using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaCadastroEleitoral.Infraestrutura.Data;
using SistemaCadastroEleitoral.Infraestrutura.Autenticacao;
using SistemaCadastroEleitoral.Models.Contato;
using SistemaCadastroEleitoral.Enum.TipoLink;
using SistemaCadastroEleitoral.Enum;

namespace SistemaCadastroEleitoral.Controllers
{
    public class ContatoController : Controller
    {
        private readonly BancoContext _context;

        public ContatoController(BancoContext context)
        {
            _context = context;
        }

        // GET: Contato
        [Logado]
        public async Task<IActionResult> Index()
        {
            var bancoContext = _context.Contatos.Include(c => c.Cadastro);
            return View(await bancoContext.ToListAsync());
        }

        // GET: Contato/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contatos == null)
            {
                return NotFound();
            }

            var contatoModel = await _context.Contatos
                .Include(c => c.Cadastro)
                .FirstOrDefaultAsync(m => m.IdContato == id);
            if (contatoModel == null)
            {
                return NotFound();
            }

            return View(contatoModel);
        }

        // GET: Contato/Create
        public IActionResult Create(int cadastroId)
        {
            ViewData["CadastroId"] = cadastroId;
            ViewData["cadastroId"] = new SelectList(_context.Cadastros, "Id", "Nome", cadastroId);
            
            return View();
        }

        

        // POST: Contato/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdContato,NomeContato,LinkContato,cadastroId")] ContatoModel contatoModel)
        {
            ModelState.Clear(); // Limpar valores preenchidos anteriormente

            if (ModelState.IsValid)
            {
                _context.Add(contatoModel);
                await _context.SaveChangesAsync();
                
                var contatos = _context.Contatos.Where(c => c.cadastroId == contatoModel.cadastroId).ToList();
                ModelState.Clear();
                ViewData["contatos"] = contatos;
                return View(new ContatoModel { cadastroId = contatoModel.cadastroId });
    
                //return RedirectToAction("Create", new { cadastroId = contatoModel.cadastroId });
    
                //return RedirectToAction(nameof(Index));
                //return RedirectToAction("Create", "Endereco", new { cadastroId = contatoModel.cadastroId, contatoId = contatoModel.IdContato });
            }
            ViewData["cadastroId"] = new SelectList(_context.Cadastros, "Id", "Nome", contatoModel.cadastroId);
            return View(contatoModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Proximo([Bind("IdContato,NomeContato,LinkContato,cadastroId")] ContatoModel contatoModel)
        {
            return RedirectToAction("Create", "Endereco", new { cadastroId = contatoModel.cadastroId, contatoId = contatoModel.IdContato });
        }


        // GET: Contato/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contatos == null)
            {
                return NotFound();
            }

            var contatoModel = await _context.Contatos.FindAsync(id);
            if (contatoModel == null)
            {
                return NotFound();
            }
            ViewData["cadastroId"] = new SelectList(_context.Cadastros, "Id", "Nome", contatoModel.cadastroId);
            return View(contatoModel);
        }

        // POST: Contato/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdContato,NomeContato,LinkContato,cadastroId")] ContatoModel contatoModel)
        {
            if (id != contatoModel.IdContato)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contatoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContatoModelExists(contatoModel.IdContato))
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
            ViewData["cadastroId"] = new SelectList(_context.Cadastros, "Id", "Nome", contatoModel.cadastroId);
            return View(contatoModel);
        }

        // GET: Contato/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contatos == null)
            {
                return NotFound();
            }

            var contatoModel = await _context.Contatos
                .Include(c => c.Cadastro)
                .FirstOrDefaultAsync(m => m.IdContato == id);
            if (contatoModel == null)
            {
                return NotFound();
            }

            return View(contatoModel);
        }

        // POST: Contato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contatos == null)
            {
                return Problem("Entity set 'BancoContext.Contatos'  is null.");
            }
            var contatoModel = await _context.Contatos.FindAsync(id);
            if (contatoModel != null)
            {
                _context.Contatos.Remove(contatoModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContatoModelExists(int id)
        {
          return _context.Contatos.Any(e => e.IdContato == id);
        }
    }
}
