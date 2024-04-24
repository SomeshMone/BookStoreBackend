using Businesslayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesslayer.Services
{
    public class AddressBusiness:IAddressBusiness
    {
        private readonly IAddressRepository _repository;
        public AddressBusiness(IAddressRepository repository)
        {
            _repository = repository;
        }

        public AddressModel AddAddress(AddressModel model)
        {
            return _repository.AddAddress(model);
        }
        public List<AddressModel> GetAddresses(int userid)
        {
            return _repository.GetAddresses(userid);    
        }
        public UpdateAddressModel UpdateAddress(UpdateAddressModel model)
        {
            return _repository.UpdateAddress(model);
        }
    }
}
