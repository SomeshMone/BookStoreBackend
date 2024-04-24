using Businesslayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesslayer.Services
{
    public class WishListBusiness:IWishListBusiness
    {
        private readonly IWishListRepository _repository;
        public WishListBusiness (IWishListRepository repository)
        {
            _repository = repository;   
        }
        public List<Book> AddToWishList(WishList model)
        {
            return _repository.AddToWishList(model);
        }
        public List<Book> GetWhishListBooks(int userid)
        {
            return _repository.GetWhishListBooks(userid);
        }
        public bool DeleteWhishlist(WishList model)
        {
            return _repository.DeleteWhishlist(model);
        }
    }
}
