using DocSpider.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocSpider.Application.Interfaces
{
    public interface IDocumentRepository
    {
        public List<Document> ListDocuments();

        public Document Details(int? id);

        public void Save(Document document);

        public Document Edit(int? id);

        public Task<Document> Delete(int? id);

        public Document GetById(int? id);

        public Document GetByName(string name);
    }
}
