using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class BookRepository:IBookRepository
    {

        string connectionString = @"Data Source=LAPTOP-HFJ7MFRU\SQLEXPRESS;Initial Catalog=BookStore;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public object GetAllBooks()
        {
            List<Book> books = new List<Book>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetAllBooks", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read()) 
                    {
                        Book book = new Book();
                        book.Id = Convert.ToInt32(dr["BookId"]);
                        book.Title = dr["Title"].ToString();
                        book.Price = Convert.ToInt64(dr["Price"]);
                        book.Author = dr["Author"].ToString();
                        book.Description = dr["Description"].ToString();
                        book.Quantity = Convert.ToInt32(dr["Quantity"]);
                        book.Image = dr["Image"].ToString();
                        books.Add(book);

                    }
                    return books;
                }
                catch(Exception ex)
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

        public Book GetByTitle(string Title)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spGetBookByTitle", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Title", Title);
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
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
                        return book;

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

        public Book GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spGetBookById", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
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
                        return book;

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

        public Book GetByAuthor(string Author)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spGetBookByAuthor", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Author", Author);
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
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
                        return book;

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

        public Book GetBookByTitleAndAuthor(string title, string author)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spGetBookByTitleAndAuthor", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Author", author);
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
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
                        return book;

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

    }
}
