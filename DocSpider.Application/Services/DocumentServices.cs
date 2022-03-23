using DocSpider.Application.Interfaces;
using System;

namespace DocSpider.Application.Services
{
    public class DocumentServices : IDocumentServices
    {
        private readonly IDocumentRepository _repository;

        public DocumentServices(IDocumentRepository repository)
        {
            _repository = repository;
        }

        public bool DocumentExists(int id)
        {
            if(_repository.GetById(id) == null)
            {
                return false;
            }
            return true;            
        }

        public bool TitleExists(string title)
        {
            var document = _repository.GetByName(title);

            if (document == null)
            {
                return false;
            }
            else
            {
                return true;
            }            
        }
    }
}
