using Hostel_2026.Models;
using Hostel_Consume_2026.Models;
using System.Data;
using System.Data.SqlClient;

namespace Hostel_2026.Data
{
    public class AdminRepository
    {
        private readonly string _connectionString;

        public AdminRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 🔹 SELECT ALL
        public IEnumerable<AdminModel> SelectAll()
        {
            var admin = new List<AdminModel>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Admin_SelectAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                admin.Add(new AdminModel
                {
                    Admin_id = reader.GetInt32(reader.GetOrdinal("Admin_id")),
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    Gender = reader["Gender"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    Email = reader["Email"].ToString(),
                    Address = reader["Address"].ToString(),
                    JoiningDate = reader.GetDateTime(reader.GetOrdinal("JoiningDate")),
                    ExperienceYears = reader.GetInt32(reader.GetOrdinal("ExperienceYears")),
                    Status = reader.GetBoolean(reader.GetOrdinal("Status")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                });
            }

            return admin;
        }

        // 🔹 SELECT BY PRIMARY KEY
        public AdminModel SelectByPK(int Admin_id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Admin_SelectById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Admin_id", SqlDbType.Int).Value = Admin_id;

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new AdminModel
                {
                    Admin_id = reader.GetInt32(reader.GetOrdinal("Admin_id")),
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    Gender = reader["Gender"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    Email = reader["Email"].ToString(),
                    Address = reader["Address"].ToString(),
                    JoiningDate = reader.GetDateTime(reader.GetOrdinal("JoiningDate")),
                    ExperienceYears = reader.GetInt32(reader.GetOrdinal("ExperienceYears")),
                    Status = reader.GetBoolean(reader.GetOrdinal("Status")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                };
            }

            return null;
        }

        // 🔹 INSERT
        public bool Insert(AdminModel admin)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Admin_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = admin.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = admin.LastName;
            cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 10).Value = admin.Gender;
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 15).Value = admin.Phone;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = admin.Email;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value = admin.Address;
            cmd.Parameters.Add("@JoiningDate", SqlDbType.Date).Value = admin.JoiningDate;
            cmd.Parameters.Add("@ExperienceYears", SqlDbType.Int).Value = admin.ExperienceYears;
            cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = admin.Status;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 UPDATE
        public bool Update(AdminModel admin)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Admin_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Admin_id", SqlDbType.Int).Value = admin.Admin_id;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = admin.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = admin.LastName;
            cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 10).Value = admin.Gender;
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 15).Value = admin.Phone;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = admin.Email;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value = admin.Address;
            cmd.Parameters.Add("@JoiningDate", SqlDbType.Date).Value = admin.JoiningDate;
            cmd.Parameters.Add("@ExperienceYears", SqlDbType.Int).Value = admin.ExperienceYears;
            cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = admin.Status;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 DELETE
        public bool Delete(int Admin_id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Admin_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Admin_id", SqlDbType.Int).Value = Admin_id;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}