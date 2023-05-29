using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExemploTrabalho.Data;
using ExemploTrabalho.Models;

namespace ExemploTrabalho.Controllers
{
    public class LivrosController : Controller
    {
        private readonly ExemploContext _context;

        public LivrosController(ExemploContext context)
        {
            _context = context;
        }

        // GET: Livroes
        public async Task<IActionResult> Index()
        {
            return _context.Livros != null ?
                        View(await _context.Livros.ToListAsync()) :
                        Problem("Entity set 'ExemploContext.Livros'  is null.");
        }

        // GET: Livroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Livros == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            var autores = await (from autorLivro in _context.AutorLivros
                          join autor in _context.Autores on autorLivro.AutorId equals autor.Id
                          select autor.Nome).ToListAsync();

            ViewBag.Autores = autores;

            return View(livro);
        }

        // GET: Livroes/Create
        public IActionResult Create()
        {
            ViewData["LivroId"] = new SelectList(_context.Livros, "Id", "Id");
            ViewBag.Autores = _context.Autores?.Select(a => new SelectListItem(a.Nome,a.Id.ToString()));
            return View();
        }

        // POST: Livroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,AnoLancamento")] Livro livro, [Bind("Autores")] List<string> autores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(livro);
                await _context.SaveChangesAsync();
                _context.AddRange(autores.Select(autor => new AutorLivro() { LivroId = livro.Id ?? 0, AutorId = Convert.ToInt32(autor) }));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(livro);
        }

        // GET: Livroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Livros == null)
            {
                return NotFound();
            }
            
            ViewBag.Autores = _context.Autores?.Select(a => new SelectListItem(a.Nome, a.Id.ToString()));

            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        // POST: Livroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Titulo,AnoLancamento")] Livro livro, [Bind("Autores")] List<string> autores)
        {
            if (id != livro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livro);
                    _context.AutorLivros.RemoveRange(_context.AutorLivros.Where(autor => autor.LivroId == livro.Id));
                    _context.AddRange(autores.Select(autor => new AutorLivro() { LivroId = livro.Id ?? 0, AutorId = Convert.ToInt32(autor) }));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.Id))
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
            return View(livro);
        }

        // GET: Livroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Livros == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // POST: Livroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Livros == null)
            {
                return Problem("Entity set 'ExemploContext.Livros'  is null.");
            }
            var livro = await _context.Livros.FindAsync(id);
            if (livro != null)
            {
                _context.Livros.Remove(livro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivroExists(int? id)
        {
            return (_context.Livros?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
