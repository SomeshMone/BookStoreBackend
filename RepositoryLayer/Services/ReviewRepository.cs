using CommonLayer.Models;
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
    public class ReviewRepository:IReviewRepository
    {
        string connectionString = @"Data Source=LAPTOP-HFJ7MFRU\SQLEXPRESS;Initial Catalog=BookStore;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public ReviewModel AddReview(ReviewModel model)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spAddReview", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@Review", model.Review);
                    cmd.Parameters.AddWithValue("@Stars", model.Star);
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return model;
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

        public List<ReviewsList> GetReviews(int bookid)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("GetReviewsForBook", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", bookid);

                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    List<ReviewsList> reviews = new List<ReviewsList>();

                    while (dataReader.Read())
                    {
                        ReviewsList review = new ReviewsList();
                        review.FullName = dataReader["FullName"].ToString();
                        review.Review = dataReader["Review"].ToString();
                        review.Stars = Convert.ToInt32(dataReader["Stars"]);
                        //review.BookId = Convert.ToInt32(dataReader["BookId"]);

                        reviews.Add(review);

                    }
                    return reviews;
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
