using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserRepository:IUserRepository
    {
        private readonly IConfiguration _config;

        public UserRepository(IConfiguration config)
        {
            _config = config;
        }
        string connectionString = @"Data Source=LAPTOP-HFJ7MFRU\SQLEXPRESS;Initial Catalog=BookStore;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public UserModel RegisterUser(UserModel model)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {


                    SqlCommand cmd = new SqlCommand("spRegisterUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FullName", model.FullName);
                    cmd.Parameters.AddWithValue("@EmailId", model.EmailId);
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return model;


        }

        public object GetData()
        {
            List<User> users = new List<User>();
            using(SqlConnection  conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("getAllUsersSp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        User user = new User();
                        user.UserId = Convert.ToInt32(dr["UserId"]);
                        user.FullName = dr["FullName"].ToString();
                        user.EmailId = dr["EmailId"].ToString();
                        user.Password = dr["Password"].ToString();
                        user.Mobile = Convert.ToInt64(dr["Mobile"]);
                        users.Add(user);

                    }
                    return users;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally { conn.Close(); }
                return null;
            }
        }
        private string GenerateToken(long UserId, string userEmail)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",userEmail),
                new Claim("UserId", UserId.ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(1440),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public LoginTokenModel LoginUser(LoginModel model)
        {
            User user = new User();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {


                    SqlCommand cmd = new SqlCommand("spLoginUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmailId", model.EmailId);
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    conn.Open();

                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        user.UserId = Convert.ToInt32(dataReader["UserId"]);
                        user.FullName = dataReader["FullName"].ToString();
                        user.EmailId = dataReader["EmailId"].ToString();
                        user.Password = dataReader["Password"].ToString();
                        user.Mobile = Convert.ToInt64(dataReader["Mobile"]);

                    }
                    if (user.EmailId == model.EmailId && user.Password == model.Password)
                    {
                        LoginTokenModel login = new LoginTokenModel();
                        var token = GenerateToken(user.UserId, user.EmailId);
                        login.Id = user.UserId;
                        login.FullName = user.FullName;
                        login.EmailId = user.EmailId;
                        login.Token = token;
                        login.Password = user.Password;
                        login.Mobile = user.Mobile;


                        return login;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }

        }

        public bool ResetPassword(string email, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open(); // Open the connection

                    SqlCommand cmd = new SqlCommand("ResetPassword", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    
                    int rowsAffected = cmd.ExecuteNonQuery();

                    
                    if (rowsAffected > 0)
                    {
                        
                        return true;
                    }
                    else
                    {
                        
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    
                    return false;
                }
                finally
                {
                   
                    conn.Close();
                }
            }

        }

        public ForgotPasswordModel ForgotPassword(string email)
        {
            User user = new User();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {


                    SqlCommand cmd = new SqlCommand("spForgotPassword", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmailId", email);
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        user.UserId = Convert.ToInt32(dataReader["UserId"]);
                        user.FullName = dataReader["FullName"].ToString();
                        user.EmailId = dataReader["EmailId"].ToString();
                        user.Password = dataReader["Password"].ToString();
                        user.Mobile = Convert.ToInt64(dataReader["Mobile"]);

                    }
                    if (email == user.EmailId)
                    {
                        ForgotPasswordModel model = new ForgotPasswordModel();

                        model.EmailId = user.EmailId;
                        model.UserId = user.UserId;
                        model.token = GenerateToken(user.UserId, user.EmailId);
                        return model;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }
        }

        public object UpdateUser(int id, UpdateUser model)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spUpdateUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@FullName", model.FullName);
                    cmd.Parameters.AddWithValue("@EmailId", model.EmailId);
                    cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        return null;
                    }
                    return model;

                    //SqlDataReader dataReader = cmd.ExecuteReader();
                    //while (dataReader.Read())
                    //{
                    //    User user = new User();

                    //    user.UserId = Convert.ToInt32(dataReader["Id"]);
                    //    user.FullName = dataReader["FullName"].ToString();
                    //    user.EmailId = dataReader["EmailId"].ToString();
                    //    user.Password = dataReader["Password"].ToString();
                    //    user.Mobile = Convert.ToInt64(dataReader["Mobile"]);
                    //    return user;

                    //}
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }
        }



    }

}

