using Businesslayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Interfaces;
using CommonLayer.Models;

namespace Businesslayer.Services
{
    public  class UserBusiness:IUserBusiness
    {
        private readonly IUserRepository _repository;
        public UserBusiness(IUserRepository repository)
        {
            _repository = repository;
        }
        public UserModel RegisterUser(UserModel model)
        {
            return _repository.RegisterUser(model);
        }
        public object GetData()
        {
            return _repository.GetData();
        }
        public LoginTokenModel LoginUser(LoginModel model)
        {
            return _repository.LoginUser(model);
        }
        public bool ResetPassword(string email, string password)
        {
            return _repository.ResetPassword(email, password);
        }
        public ForgotPasswordModel ForgotPassword(string email)
        {
            return _repository.ForgotPassword(email);
        }
        public object UpdateUser(int id, UpdateUser model)
        {
            return _repository.UpdateUser(id, model);
        }
    }
}
