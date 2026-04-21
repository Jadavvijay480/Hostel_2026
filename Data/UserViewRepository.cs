using Hostel_2026.Models;
using Hostel_Consume_2026.Models;
using System.Data;
using System.Data.SqlClient;

namespace Hostel_2026.Data
{
    public class UserViewRepository
    {
        private readonly string _connectionString;

        public UserViewRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 🔹 SELECT ALL
        public IEnumerable<UserModel> SelectAll()
        {
            var userView = new List<UserModel>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Users_SelectAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                userView.Add(new UserModel
                {
                    UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                    FullName = reader["FullName"]?.ToString(),
                    Email = reader["Email"]?.ToString(),
                    Password = reader["Password"]?.ToString(),
                    MobileNo = reader["MobileNo"]?.ToString(),
                    Role = reader["Role"]?.ToString(),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
                });
            }

            return userView;
        }

        // 🔹 SELECT BY PRIMARY KEY
        public UserModel SelectByPK(int UserId)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Users_SelectById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new UserModel
                {
                    UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                    FullName = reader["FullName"]?.ToString(),
                    Email = reader["Email"]?.ToString(),
                    Password = reader["Password"]?.ToString(),
                    MobileNo = reader["MobileNo"]?.ToString(),
                    Role = reader["Role"]?.ToString(),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
                };
            }

            return null;
        }

        // 🔹 INSERT
        public bool Insert(UserModel userView)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Users_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@FullName", SqlDbType.VarChar, 100).Value = userView.FullName;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = userView.Email;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar, 255).Value = userView.Password;
            cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar, 15).Value = userView.MobileNo;
            cmd.Parameters.Add("@Role", SqlDbType.VarChar, 50).Value = userView.Role;
            cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = userView.IsActive;
            //cmd.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = userView.CreatedAt;
            //cmd.Parameters.Add("@UpdatedAt", SqlDbType.DateTime).Value =
            //(object?)userView.UpdatedAt ?? DBNull.Value;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 UPDATE
        public bool Update(UserModel userView)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Users_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userView.UserId;
            cmd.Parameters.Add("@FullName", SqlDbType.VarChar, 100).Value = userView.FullName;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = userView.Email;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar, 255).Value = userView.Password;
            cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar, 15).Value = userView.MobileNo;
            cmd.Parameters.Add("@Role", SqlDbType.VarChar, 50).Value = userView.Role;
            cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = userView.IsActive;
            //cmd.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = userView.CreatedAt;
            //cmd.Parameters.Add("@UpdatedAt", SqlDbType.DateTime).Value =
            //(object?)userView.UpdatedAt ?? DBNull.Value;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 DELETE
        public bool Delete(int UserId)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Users_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}