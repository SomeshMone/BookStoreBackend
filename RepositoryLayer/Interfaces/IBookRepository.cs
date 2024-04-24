using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IBookRepository
    {
        public object GetAllBooks();
        public Book GetByTitle(string Title);
        public Book GetById(int id);

        public Book GetByAuthor(string Author);
        public Book GetBookByTitleAndAuthor(string title, string author);

    }
}
