using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DocSpider.Application.Interfaces
{
    public interface IDocumentServices
    {
        public bool TitleExists(string Title);

        public bool DocumentExists(int id);
    }
}
