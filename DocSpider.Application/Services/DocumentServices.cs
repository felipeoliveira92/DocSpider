using DocSpider.Application.Interfaces;
using DocSpider.Domain;
using System;
using System.Collections.Generic;
using System.Text;

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
            if (_repository.GetByName(title) != null)
            {
                return false;
            }
            return true;
        }
    }
}
