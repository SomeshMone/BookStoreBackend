using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IOrderRepository
    {
        public List<Book> GetOrders(int userid);
        public List<Book> AddToOrder(OrderModel model, int userid);

        public double GetPriceInOrder(int userid);
    }
}
