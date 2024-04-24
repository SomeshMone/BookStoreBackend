using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLayer.Models;

namespace RepositoryLayer.Services
{
    public class WishListRepository:IWishListRepository
    {
        string connectionString = @"Data Source=LAPTOP-HFJ7MFRU\SQLEXPRESS;Initial Catalog=BookStore;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        public List<Book> AddToWishList(WishList model)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spAddtowhishlist", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.Id);
                    cmd.Parameters.AddWithValue("@BookId", model.Bookid);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return GetWhishListBooks(model.Id);
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
        public List<Book> GetWhishListBooks(int userid)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spGetWhishlist", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userid);

                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    List<Book> list = new List<Book>();

                    while (dataReader.Read())
                    {
                        Book book = new Book();
                        book.Id = Convert.ToInt32(dataReader["BookId"]);
                        book.Title = dataReader["Title"].ToString();
                        book.Price = Convert.ToInt64(dataReader["Price"]);
                        book.Author = dataReader["Author"].ToString();
                        book.Description = dataReader["Description"].ToString();
                        book.Image = dataReader["Image"].ToString();
                        list.Add(book);

                    }
                    return list;
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

        public bool DeleteWhishlist(WishList model)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spDeleteWhishlist", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.Id);
                    cmd.Parameters.AddWithValue("@BookId", model.Bookid);


                    conn.Open();
                    int rowseefected = cmd.ExecuteNonQuery();
                    if (rowseefected > 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally { conn.Close(); }
                return false;

            }
        }
    }
}
