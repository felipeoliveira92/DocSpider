using DocSpider.Application.Interfaces;
using DocSpider.Domain;
using DocSpider.Infra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocSpider.WebApp.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDocumentServices _documentServices;
        private readonly IDocumentRepository _repository;

        public DocumentsController(AppDbContext context, IDocumentServices documentServices, IDocumentRepository repository)
        {
            _context = context;
            _documentServices = documentServices;
            _repository = repository;
        }
        
        public ActionResult Index()
        {
            return View(_repository.ListDocuments());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Document = _repository.Details(id);

            if (Document == null)
            {
                return NotFound();
            }

            return View(Document);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file, Document document)
        {
            if (!_documentServices.TitleExists(document.Title))
            {
                await CheckExtension(file);

                IFormFile formFile = file;

                MemoryStream memoryStream = new MemoryStream();
                formFile.OpenReadStream().CopyTo(memoryStream);

                var newdocument = new Document();
                newdocument.Title = document.Title;
                newdocument.Description = document.Description;
                newdocument.File = memoryStream.ToArray();
                newdocument.ContentType = formFile.ContentType;
                newdocument.FileName = document.FileName;

                AddExtension(newdocument, file);

                _context.Documents.Add(newdocument);
                await _context.SaveChangesAsync();

                return View();
            }

            return View();
            
        }

        public async Task<IActionResult> Download(int id)
        {
            var document = _repository.GetById(id);

            if(document == null)
            {
                return NotFound();
            }                      

            return File(document.File, document.ContentType, document.FileName);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = _repository.GetById(id);
            if (document == null)
            {
                return NotFound();
            }
                        
            return View(document);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, IFormFile file, [Bind("Id, Title, Description, File, FileName, DateCreate")] Document document)
        {
            if (id != document.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await CheckExtension(file);
                try
                {
                    _repository.Edit(id, document);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!_documentServices.DocumentExists(document.Id))
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
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = _repository.GetById(id);
            if (document == null)
            {
                return NotFound();
            }
            return View(document);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var document = _repository.GetById(id);
            await _repository.Delete(id);

            return RedirectToAction(nameof(Index));
        }
               
        private async Task<IActionResult> CheckExtension(IFormFile formFile)
        {
            if(formFile == null)
            {
                return View();
            }
            if (formFile.FileName.Contains(".exe") || formFile.FileName.Contains(".zip") 
                || formFile.FileName.Contains(".bat"))
            {

                return View("Objeto invalido");
            }

            return View();
        }
        
        private Document AddExtension(Document document, IFormFile file)
        {
            if (file.FileName.Contains(".jpg"))
            {
                document.FileName += ".jpg";
            }
            else if (file.FileName.Contains(".jpeg"))
            {
                document.FileName += ".jpeg";
            }
            else if (file.FileName.Contains(".gif"))
            {
                document.FileName += ".gif";
            }
            else if (file.FileName.Contains(".png"))
            {
                document.FileName += ".png";
            }
            else if (file.FileName.Contains(".pdf"))
            {
                document.FileName += ".pdf";
            }
            else if(file.FileName.Contains(".docx"))
            {
                document.FileName += ".docx";
            }
            else if(file.FileName.Contains(".xls"))
            {
                document.FileName += ".xls";
            }

            return document;                
        }
    }
}
