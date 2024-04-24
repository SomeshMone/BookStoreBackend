using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ICartRepository
    {
        public List<Book> GetCartBooks(int userid);
        public List<Book> AddToCart(CartModel model, int userid);
        public double GetPrice(int userid);
        public CartModel UpdateQuantity(int userid, CartModel model);
        public bool DeleteCart(DeleteCart model);
    }
}
