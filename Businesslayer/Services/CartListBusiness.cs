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
    public class CartListBusiness:ICartListBusiness
    {
        private readonly ICartRepository _repository;
        public CartListBusiness(ICartRepository repository)
        {
            _repository = repository;
        }
        public List<Book> GetCartBooks(int userid)
        {
            return _repository.GetCartBooks(userid);
        }
        public List<Book> AddToCart(CartModel model, int userid)
        {
            return _repository.AddToCart(model, userid);
        }
        public double GetPrice(int userid)
        {
            return _repository.GetPrice(userid);
        }
        public CartModel UpdateQuantity(int userid, CartModel model)
        {
            return _repository.UpdateQuantity(userid, model);
        }
        public bool DeleteCart(DeleteCart model)
        {
            return _repository.DeleteCart(model);
        }
    }
}
