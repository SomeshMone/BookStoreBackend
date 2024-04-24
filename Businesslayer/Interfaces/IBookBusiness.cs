using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesslayer.Interfaces
{
    public interface IBookBusiness
    {
        public object GetAllBooks();
        public Book GetByTitle(string Title);
        public Book GetById(int id);

        public Book GetByAuthor(string Author);
        public Book GetBookByTitleAndAuthor(string title, string author);
    }
}
