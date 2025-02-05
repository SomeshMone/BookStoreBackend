﻿using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IAddressRepository
    {
        public AddressModel AddAddress(AddressModel model);
        public List<AddressModel> GetAddresses(int userid);
        public UpdateAddressModel UpdateAddress(UpdateAddressModel model);
    }
}
