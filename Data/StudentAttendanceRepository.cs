using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Hostel_2026.Models;
using Microsoft.Extensions.Configuration;

namespace Hostel_2026.Data
{
    public class StudentAttendanceRepository
    {
        private readonly string _connectionString;

        public StudentAttendanceRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 🔹 SELECT ALL
        public IEnumerable<StudentAttendanceModel> SelectAll()
        {
            var studentAttendance = new List<StudentAttendanceModel>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentAttendance_SelectAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                studentAttendance.Add(new StudentAttendanceModel
                {
                    Attendance_id = (int)reader["Attendance_id"],
                    Student_id = (int)reader["Student_id"],
                    AttendanceDate = (DateTime)reader["AttendanceDate"],
                    Status = reader["Status"].ToString(),
                    Place = reader["Place"].ToString(),
                    Remarks = reader["Remarks"]?.ToString(),
                    CreatedAt = (DateTime)reader["CreatedAt"]
                });
            }

            return studentAttendance;
        }

        // 🔹 SELECT BY ID
        public StudentAttendanceModel SelectByPK(int Attendance_id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentAttendance_SelectById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Attendance_id", Attendance_id);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new StudentAttendanceModel
                {
                    Attendance_id = (int)reader["Attendance_id"],
                    Student_id = (int)reader["Student_id"],
                    AttendanceDate = (DateTime)reader["AttendanceDate"],
                    Status = reader["Status"].ToString(),
                    Place = reader["Place"].ToString(),
                    Remarks = reader["Remarks"]?.ToString(),
                    CreatedAt = (DateTime)reader["CreatedAt"]
                };
            }

            return null;
        }

        // 🔹 INSERT
        public bool Insert(StudentAttendanceModel model)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_InsertStudentAttendance", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Student_id", model.Student_id);
            cmd.Parameters.AddWithValue("@AttendanceDate", model.AttendanceDate);
            cmd.Parameters.AddWithValue("@Status", model.Status);
            cmd.Parameters.AddWithValue("@Place", model.Place);
            cmd.Parameters.AddWithValue("@Remarks", (object?)model.Remarks ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 UPDATE
        public bool Update(StudentAttendanceModel model)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_UpdateStudentAttendance", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Attendance_id", model.Attendance_id);
            cmd.Parameters.AddWithValue("@Student_id", model.Student_id);
            cmd.Parameters.AddWithValue("@AttendanceDate", model.AttendanceDate);
            cmd.Parameters.AddWithValue("@Status", model.Status);
            cmd.Parameters.AddWithValue("@Place", model.Place);
            cmd.Parameters.AddWithValue("@Remarks", (object?)model.Remarks ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 DELETE
        public bool Delete(int Attendance_id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_DeleteStudentAttendance", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Attendance_id", Attendance_id);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}