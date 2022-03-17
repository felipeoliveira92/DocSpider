using System;
using System.Collections.Generic;
using System.Text;

namespace DocSpider.Application.Interfaces
{
    public interface IDocumentServices
    {
        public bool TitleExists(string Title);

        public bool DocumentExists(int id);
    }
}
