using DocSpider.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocSpider.Application.Interfaces
{
    public interface IDocumentRepository
    {
        List<Document> ListDocuments();

        Document Details(int? id);

        Document CreateNew();

        Document Edit(int? id);

        Task<Document> Delete(int? id);

        Document GetById(int? id);

        Document GetByName(string name);
    }
}
