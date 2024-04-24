using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class AddressRepository:IAddressRepository
    {
        string connectionString = @"Data Source=LAPTOP-HFJ7MFRU\SQLEXPRESS;Initial Catalog=BookStore;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        public AddressModel AddAddress(AddressModel model)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spAddAddress", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@FullAddress", model.FullAddress);
                    cmd.Parameters.AddWithValue("@City", model.City);
                    cmd.Parameters.AddWithValue("@State", model.State);
                    cmd.Parameters.AddWithValue("@Type", model.Type);
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

        public List<AddressModel> GetAddresses(int userid)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    List<AddressModel> addresses = new List<AddressModel>();
                    SqlCommand cmd = new SqlCommand("spGetAddress", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userid);
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        AddressModel model = new AddressModel();
                        model.UserId = Convert.ToInt32(dataReader["UserId"]);
                        model.FullAddress = dataReader["FullAddress"].ToString();
                        model.City = dataReader["City"].ToString();
                        model.State = dataReader["State"].ToString();
                        model.Type = dataReader["Type"].ToString();
                        addresses.Add(model);

                    }
                    return addresses;
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

        public UpdateAddressModel UpdateAddress(UpdateAddressModel model)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spUpdateAddress", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@AdId", model.AdID);
                    cmd.Parameters.AddWithValue("@FullAddress", model.FullAddress);
                    cmd.Parameters.AddWithValue("@City", model.City);
                    cmd.Parameters.AddWithValue("@State", model.State);
                    cmd.Parameters.AddWithValue("@Type", model.Type);
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

    }
}
