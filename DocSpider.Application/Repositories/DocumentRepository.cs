using DocSpider.Application.Interfaces;
using DocSpider.Domain;
using DocSpider.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocSpider.Application.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AppDbContext _context;

        public DocumentRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Save(Document document)
        {
            if(document != null)
            {
                _context.Documents.Add(document);
                _context.SaveChanges();
            }
        }

        public async Task<Document> Delete(int? id)
        {
            if(id != null)
            {
                var document = _context.Documents.First(d => d.Id == id);
                _context.Documents.Remove(document);

                return document;
            }
            else
            {
                return null;
            }
        }

        public Document Details(int? id)
        {
            if (id != null)
            {
                var document = _context.Documents.First(d => d.Id == id);

                return document;
            }
            else
            {
                return null;
            }
        }

        public Document Edit(int? id)
        {
            if (id != null)
            {
                var document = _context.Documents.First(d => d.Id == id);
                _context.Documents.Update(document);

                return document;
            }
            else
            {
                return null;
            }
        }

        public List<Document> ListDocuments()
        {
            var documents = _context.Documents.ToList();
            return documents;
        }

        public Document GetById(int? id)
        {
            if(id == null)
            {
                return null;
            }
            else
            {
                return _context.Documents.First(d => d.Id == id);
            }
            
        }

        public Document GetByName(string name)
        {
            var document = _context.Documents.First(d => d.Title == name);

            return document;
        }
    }
}
