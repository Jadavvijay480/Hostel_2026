using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Hostel_2026.Models;
using Microsoft.Extensions.Configuration;

namespace Hostel_2026.Data
{
    public class WardenRepository
    {
        private readonly string _connectionString;

        public WardenRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 🔹 SELECT ALL
        public IEnumerable<Warden> SelectAll()
        {
            var wardens = new List<Warden>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Wardens_SelectAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                wardens.Add(new Warden
                {
                    Warden_id = reader.GetInt32(reader.GetOrdinal("Warden_id")),
                    FirstName = reader["FirstName"]?.ToString(),
                    LastName = reader["LastName"]?.ToString(),
                    Gender = reader["Gender"]?.ToString(),
                    Phone = reader["Phone"]?.ToString(),
                    Email = reader["Email"]?.ToString(),
                    Address = reader["Address"]?.ToString(),
                    JoiningDate = reader.GetDateTime(reader.GetOrdinal("JoiningDate")),
                    ExperienceYears = reader.GetInt32(reader.GetOrdinal("ExperienceYears")),
                    Status = reader.GetBoolean(reader.GetOrdinal("Status")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                });
            }

            return wardens;
        }

        // 🔹 SELECT BY PRIMARY KEY
        public Warden SelectByPK(int Warden_id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Wardens_SelectById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Warden_id", SqlDbType.Int).Value = Warden_id;

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Warden
                {
                    Warden_id = reader.GetInt32(reader.GetOrdinal("Warden_id")),
                    FirstName = reader["FirstName"]?.ToString(),
                    LastName = reader["LastName"]?.ToString(),
                    Gender = reader["Gender"]?.ToString(),
                    Phone = reader["Phone"]?.ToString(),
                    Email = reader["Email"]?.ToString(),
                    Address = reader["Address"]?.ToString(),
                    JoiningDate = reader.GetDateTime(reader.GetOrdinal("JoiningDate")),
                    ExperienceYears = reader.GetInt32(reader.GetOrdinal("ExperienceYears")),
                    Status = reader.GetBoolean(reader.GetOrdinal("Status")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                };
            }

            return null;
        }

        // 🔹 INSERT
        public bool Insert(Warden warden)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Wardens_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = warden.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = warden.LastName;
            cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 10).Value = warden.Gender;
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 15).Value = warden.Phone;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = warden.Email;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value = warden.Address;
            cmd.Parameters.Add("@JoiningDate", SqlDbType.Date).Value = warden.JoiningDate;
            cmd.Parameters.Add("@ExperienceYears", SqlDbType.Int).Value = warden.ExperienceYears;
            cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = warden.Status;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 UPDATE
        public bool Update(Warden warden)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Wardens_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Warden_id", SqlDbType.Int).Value = warden.Warden_id;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = warden.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = warden.LastName;
            cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 10).Value = warden.Gender;
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 15).Value = warden.Phone;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = warden.Email;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value = warden.Address;
            cmd.Parameters.Add("@JoiningDate", SqlDbType.Date).Value = warden.JoiningDate;
            cmd.Parameters.Add("@ExperienceYears", SqlDbType.Int).Value = warden.ExperienceYears;
            cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = warden.Status;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 DELETE
        public bool Delete(int Warden_id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Wardens_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Warden_id", SqlDbType.Int).Value = Warden_id;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
