using DocSpider.Domain;
using DocSpider.Infra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocSpider.WebApp.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly AppDbContext _context;

        public DocumentsController(AppDbContext context)
        {
            _context = context;            
        }
        // GET: DocumentsController
        public ActionResult Index()
        {
            return View(_context.Documents.ToList());
        }

        // GET: DocumentsController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Document = _context.Documents
                .FirstOrDefault(d => d.Id == id);

            if (Document == null)
            {
                return NotFound();
            }

            return View(Document);
        }

        // GET: DocumentsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DocumentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, Document document)
        {
            IFormFile formFile = file;

            MemoryStream memoryStream = new MemoryStream();
            formFile.OpenReadStream().CopyTo(memoryStream);

            var newdocument = new Document();
            newdocument.Title = document.Title;
            newdocument.Description = document.Description;
            newdocument.File = memoryStream.ToArray();
            newdocument.ContentType = formFile.ContentType;
            newdocument.FileName = document.FileName;

            _context.Documents.Add(newdocument);
            await _context.SaveChangesAsync();

            return View();
        }

        public async Task<IActionResult> Download(int id)
        {
            var document = _context.Documents.FirstOrDefault(a => a.Id == id);

            return File(document.File, document.ContentType, document.Title);
        }

        // GET: DocumentsController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documento = await _context.Documents.FindAsync(id);
            if (documento == null)
            {
                return NotFound();
            }
            return View(documento);
        }

        // POST: DocumentsController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Title, Description, File, FileName, DateCreate")] Document document)
        {
            if (id != document.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(document);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentoExists(document.Id))
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
            return View(document);
        }

        private bool DocumentoExists(int id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }

        // GET: DocumentsController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            return View(document);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
