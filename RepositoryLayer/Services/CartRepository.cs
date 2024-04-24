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
    public class CartRepository:ICartRepository
    {
        string connectionString = @"Data Source=LAPTOP-HFJ7MFRU\SQLEXPRESS;Initial Catalog=BookStore;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public List<Book> AddToCart(CartModel model, int userid)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spAddToCart", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userid);
                    cmd.Parameters.AddWithValue("@Quantity", model.Quantity);
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return GetCartBooks(userid);
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

        public List<Book> GetCartBooks(int userid)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spGet_cart", conn);
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
                        book.Quantity = Convert.ToInt32(dataReader["Quantity"]);
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

        public double GetPrice(int userid)
        {
            List<Book> list = GetCartBooks(userid);
            double totalPrice = 0;
            foreach (var book in list)
            {
                totalPrice += (book.Quantity * book.Price);
            }
            return totalPrice;
        }

        public CartModel UpdateQuantity(int userid, CartModel model)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spUpdateQuantity", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userid);
                    cmd.Parameters.AddWithValue("@Quantity", model.Quantity);
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);
                    conn.Open();
                    int rowseefected = cmd.ExecuteNonQuery();
                    if (rowseefected > 0)
                    {
                        return model;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally { conn.Close(); }
                return null;

            }
        }

        public bool DeleteCart(DeleteCart model)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spDelete_cart", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);


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
