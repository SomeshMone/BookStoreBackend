using CommonLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class OrderRepository : IOrderRepository
    {
        string connectionString = @"Data Source=LAPTOP-HFJ7MFRU\SQLEXPRESS;Initial Catalog=BookStore;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public List<Book> AddToOrder(OrderModel model, int userid)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spAddOrder", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userid);
                    cmd.Parameters.AddWithValue("@Quantity", model.Quantity);
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return GetOrders(userid);
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

        public List<Book> GetOrders(int userid)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spGetOrders", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userid);

                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    List<Book> orders = new List<Book>();

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
                        orders.Add(book);

                    }
                    return orders;
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

        public double GetPriceInOrder(int userid)
        {
            List<Book> bookList = GetOrders(userid);
            double totalPrice = 0;
            foreach (var book in bookList)
            {
                totalPrice += (book.Quantity * book.Price);
            }
            return totalPrice;
        }
    }
}
