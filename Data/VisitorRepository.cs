using Microsoft.Extensions.Configuration;
using Hostel_Consume_2026.Models;
using System.Data;
using System.Data.SqlClient;


namespace Hostel_2026.Data
{
    public class VisitorRepository
    {
        private readonly string _connectionString;

        public VisitorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 🔹 SELECT ALL
        public IEnumerable<VisitorModel> SelectAll()
        {
            var visitors = new List<VisitorModel>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Visitors_SelectAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                visitors.Add(new VisitorModel
                {
                    Visitor_id = reader.GetInt32(reader.GetOrdinal("Visitor_id")),
                    VisitorName = reader["VisitorName"].ToString(),
                    Gender = reader["Gender"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    Email = reader["Email"] as string,
                    Address = reader["Address"].ToString(),
                    VisitDate = reader.GetDateTime(reader.GetOrdinal("VisitDate")),
                    InTime = (TimeSpan)reader["InTime"],
                    OutTime = reader["OutTime"] == DBNull.Value ? null : (TimeSpan?)reader["OutTime"],
                    Purpose = reader["Purpose"].ToString(),
                    Student_id = reader.GetInt32(reader.GetOrdinal("Student_id")),
                    Warden_id = reader["Warden_id"] == DBNull.Value ? null : (int?)reader["Warden_id"],
                    Status = reader.GetBoolean(reader.GetOrdinal("Status")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                });
            }

            return visitors;
        }

        // 🔹 SELECT BY PRIMARY KEY
        public VisitorModel SelectByPK(int Visitor_id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Visitors_SelectByID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Visitor_id", SqlDbType.Int).Value = Visitor_id;

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new VisitorModel
                {
                    Visitor_id = reader.GetInt32(reader.GetOrdinal("Visitor_id")),
                    VisitorName = reader["VisitorName"].ToString(),
                    Gender = reader["Gender"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    Email = reader["Email"] as string,
                    Address = reader["Address"].ToString(),
                    VisitDate = reader.GetDateTime(reader.GetOrdinal("VisitDate")),
                    InTime = (TimeSpan)reader["InTime"],
                    OutTime = reader["OutTime"] == DBNull.Value ? null : (TimeSpan?)reader["OutTime"],
                    Purpose = reader["Purpose"].ToString(),
                    Student_id = reader.GetInt32(reader.GetOrdinal("Student_id")),
                    Warden_id = reader["Warden_id"] == DBNull.Value ? null : (int?)reader["Warden_id"],
                    Status = reader.GetBoolean(reader.GetOrdinal("Status")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                };
            }

            return null;
        }

        // 🔹 INSERT
        public bool Insert(VisitorModel visitor)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Visitors_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@VisitorName", SqlDbType.VarChar, 100).Value = visitor.VisitorName;
            cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 10).Value = visitor.Gender;
            cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 15).Value = visitor.Phone;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = (object?)visitor.Email ?? DBNull.Value;
            cmd.Parameters.Add("@Address", SqlDbType.VarChar, 255).Value = visitor.Address;
            cmd.Parameters.Add("@VisitDate", SqlDbType.Date).Value = visitor.VisitDate;
            cmd.Parameters.Add("@InTime", SqlDbType.Time).Value = visitor.InTime;
            cmd.Parameters.Add("@OutTime", SqlDbType.Time).Value = (object?)visitor.OutTime ?? DBNull.Value;
            cmd.Parameters.Add("@Purpose", SqlDbType.VarChar, 200).Value = visitor.Purpose;
            cmd.Parameters.Add("@Student_id", SqlDbType.Int).Value = visitor.Student_id;
            cmd.Parameters.Add("@Warden_id", SqlDbType.Int).Value = (object?)visitor.Warden_id ?? DBNull.Value;
            cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = visitor.Status;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 UPDATE
        public bool Update(VisitorModel visitor)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Visitors_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Visitor_id", SqlDbType.Int).Value = visitor.Visitor_id;
            cmd.Parameters.Add("@VisitorName", SqlDbType.VarChar, 100).Value = visitor.VisitorName;
            cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 10).Value = visitor.Gender;
            cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 15).Value = visitor.Phone;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = (object?)visitor.Email ?? DBNull.Value;
            cmd.Parameters.Add("@Address", SqlDbType.VarChar, 255).Value = visitor.Address;
            cmd.Parameters.Add("@VisitDate", SqlDbType.Date).Value = visitor.VisitDate;
            cmd.Parameters.Add("@InTime", SqlDbType.Time).Value = visitor.InTime;
            cmd.Parameters.Add("@OutTime", SqlDbType.Time).Value = (object?)visitor.OutTime ?? DBNull.Value;
            cmd.Parameters.Add("@Purpose", SqlDbType.VarChar, 200).Value = visitor.Purpose;
            cmd.Parameters.Add("@Student_id", SqlDbType.Int).Value = visitor.Student_id;
            cmd.Parameters.Add("@Warden_id", SqlDbType.Int).Value = (object?)visitor.Warden_id ?? DBNull.Value;
            cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = visitor.Status;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 DELETE
        public bool Delete(int Visitor_id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Visitors_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Visitor_id", SqlDbType.Int).Value = Visitor_id;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}