using DocSpider.Application.Interfaces;
using DocSpider.Domain;
using DocSpider.Infra;
using System;
using System.Collections.Generic;
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
        public Document CreateNew()
        {
            throw new NotImplementedException();
        }

        public Task<Document> Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public Document Details(int? id)
        {
            throw new NotImplementedException();
        }

        public Document Edit(int? id)
        {
            throw new NotImplementedException();
        }

        public List<Document> ListDocuments()
        {
            throw new NotImplementedException();
        }

        public Document GetById(int? id)
        {
            if(id == null)
            {
                return null;
            }
            return _context.Documents.Find(id);
        }

        public Document GetByName(string name)
        {
            if (name == null)
            {
                return null;
            }
            
            return _context.Documents.Find(name);
        }
    }
}
