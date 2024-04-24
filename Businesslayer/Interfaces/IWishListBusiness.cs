using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesslayer.Interfaces
{
    public interface IWishListBusiness
    {
        public List<Book> AddToWishList(WishList model);
        public List<Book> GetWhishListBooks(int userid);
        public bool DeleteWhishlist(WishList model);
    }
}
