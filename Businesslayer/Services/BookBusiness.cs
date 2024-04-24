using Businesslayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesslayer.Services
{
    public class BookBusiness:IBookBusiness
    {
        private readonly IBookRepository _repository;
        public BookBusiness(IBookRepository repository)
        {
            this._repository = repository;
        }
        public object GetAllBooks()
        {
            return _repository.GetAllBooks();   
        }
        public Book GetByTitle(string Title)
        {
            return _repository.GetByTitle(Title);
        }
        public Book GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Book GetByAuthor(string Author)
        {
            return _repository.GetByAuthor(Author);
        }
        public Book GetBookByTitleAndAuthor(string title, string author)
        {
            return _repository.GetBookByTitleAndAuthor(title, author);
        }
    }
}
