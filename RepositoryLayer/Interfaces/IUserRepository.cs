using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRepository
    {
        public UserModel RegisterUser(UserModel model);
        public object GetData();
        public LoginTokenModel LoginUser(LoginModel model);
        public bool ResetPassword(string email, string password);
        public ForgotPasswordModel ForgotPassword(string email);

        public object UpdateUser(int id, UpdateUser model);
    }
}
