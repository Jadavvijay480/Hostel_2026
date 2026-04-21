using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Hostel_2026.Models;
using Microsoft.Extensions.Configuration;

namespace Hostel_2026.Data
{
    public class ComplaintRepository
    {
        private readonly string _connectionString;

        public ComplaintRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 🔹 SELECT ALL
        public IEnumerable<ComplaintModel> SelectAll()
        {
            var complaint = new List<ComplaintModel>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Complaint_SelectAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                complaint.Add(new ComplaintModel
                {
                    Complaint_id = (int)reader["Complaint_id"],
                    Student_id = (int)reader["Student_id"],
                    ComplaintType = reader["ComplaintType"].ToString(),
                    ComplaintDetails = reader["ComplaintDetails"].ToString(),
                    ComplaintDate = (DateTime)reader["ComplaintDate"],
                    Status = reader["Status"].ToString(),
                    ActionTaken = reader["ActionTaken"]?.ToString(),
                    ResolvedDate = reader["ResolvedDate"] as DateTime?,
                    CreatedAt = (DateTime)reader["CreatedAt"]
                });
            }

            return complaint;
        }

        // 🔹 SELECT BY ID
        public ComplaintModel SelectByPK(int Complaint_id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Complaint_SelectById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Complaint_id", Complaint_id);

            conn.Open();

            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new ComplaintModel
                {
                    Complaint_id = (int)reader["Complaint_id"],
                    Student_id = (int)reader["Student_id"],
                    ComplaintType = reader["ComplaintType"].ToString(),
                    ComplaintDetails = reader["ComplaintDetails"].ToString(),
                    ComplaintDate = (DateTime)reader["ComplaintDate"],
                    Status = reader["Status"].ToString(),
                    ActionTaken = reader["ActionTaken"]?.ToString(),
                    ResolvedDate = reader["ResolvedDate"] as DateTime?,
                    CreatedAt = (DateTime)reader["CreatedAt"]
                };
            }

            return null;
        }

        // 🔹 INSERT
        public bool Insert(ComplaintModel model)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            using SqlCommand cmd = new SqlCommand("PR_Complaint_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Student_id", model.Student_id);
            cmd.Parameters.AddWithValue("@ComplaintType", model.ComplaintType);
            cmd.Parameters.AddWithValue("@ComplaintDetails", model.ComplaintDetails);
            cmd.Parameters.AddWithValue("@ComplaintDate", model.ComplaintDate);
            cmd.Parameters.AddWithValue("@Status", model.Status);
            cmd.Parameters.AddWithValue("@ActionTaken", model.ActionTaken);
            cmd.Parameters.AddWithValue("@ResolvedDate", model.ResolvedDate);
            

            conn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 UPDATE
        public bool Update(ComplaintModel model)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            using SqlCommand cmd = new SqlCommand("PR_Complaint_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Complaint_id", model.Complaint_id);
            cmd.Parameters.AddWithValue("@Student_id", model.Student_id);
            cmd.Parameters.AddWithValue("@ComplaintType", model.ComplaintType);
            cmd.Parameters.AddWithValue("@ComplaintDetails", model.ComplaintDetails);
            cmd.Parameters.AddWithValue("@ComplaintDate", model.ComplaintDate);
            cmd.Parameters.AddWithValue("@Status", model.Status);
            cmd.Parameters.AddWithValue("@ActionTaken", (object?)model.ActionTaken ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ResolvedDate", (object?)model.ResolvedDate ?? DBNull.Value);

            conn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 DELETE
        public bool Delete(int Complaint_id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            using SqlCommand cmd = new SqlCommand("PR_Complaint_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Complaint_id", Complaint_id);

            conn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}