using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Hostel_2026.Models;
using Microsoft.Extensions.Configuration;

namespace Hostel_2026.Data
{
    public class UserRepository
    {
        private readonly string connectionString;

        public UserRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        // LOGIN
        public UserModel Login(string email, string password)
        {
            UserModel user = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT UserId, FullName, Email, MobileNo, Role, IsActive
                         FROM Users
                         WHERE Email=@Email
                         AND Password=@Password
                         AND IsActive = 1";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Email", email.Trim());
                cmd.Parameters.AddWithValue("@Password", password.Trim());

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new UserModel
                    {
                        UserId = Convert.ToInt32(reader["UserId"]),
                        FullName = reader["FullName"].ToString(),
                        Email = reader["Email"].ToString(),
                        MobileNo = reader["MobileNo"].ToString(),
                        Role = reader["Role"].ToString(),
                        IsActive = Convert.ToBoolean(reader["IsActive"])
                    };
                }
            }

            return user;
        }
        // SIGNUP
        public bool Register(UserModel user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Users
            (FullName, Email, Password, MobileNo, Role, IsActive, CreatedAt,UpdatedAt)
            VALUES
            (@FullName, @Email, @Password, @MobileNo, @Role, 1, GETDATE(),GETDATE())";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@MobileNo", user.MobileNo);
                cmd.Parameters.AddWithValue("@Role", user.Role);

                con.Open();

                int result = cmd.ExecuteNonQuery();

                return result > 0;
            }
        }

    }
}