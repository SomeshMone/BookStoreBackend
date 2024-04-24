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
    public class OrderBusiness:IOrderBusiness
    {
        private readonly IOrderRepository _repoistory;
        public OrderBusiness(IOrderRepository repoistory)
        {
            _repoistory = repoistory;
        }
        public List<Book> GetOrders(int userid)
        {
            return _repoistory.GetOrders(userid);   
        }
        public List<Book> AddToOrder(OrderModel model, int userid)
        {
            return _repoistory.AddToOrder(model, userid);
        }
        public double GetPriceInOrder(int userid)
        {
            return _repoistory.GetPriceInOrder(userid);
        }
    }
}
