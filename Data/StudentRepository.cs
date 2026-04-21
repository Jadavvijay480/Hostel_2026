using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Hostel_2026.Models;
using Microsoft.Extensions.Configuration;

namespace Hostel_2026.Data
{
    public class StudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }



        // 🔹 GetDropdown method
        public List<StudentDropdownModel> GetDropdown()
        {
            var list = new List<StudentDropdownModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string sql = "SELECT Student_id, First_name, Last_name, Phone, Email FROM Student ORDER BY First_name, Last_name";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new StudentDropdownModel
                            {
                                Student_id = reader["Student_id"] != DBNull.Value ? Convert.ToInt32(reader["Student_id"]) : 0,
                                First_name = reader["First_name"].ToString(),
                                Last_name = reader["Last_name"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Email = reader["Email"].ToString()
                            });
                        }
                    }
                }
            }

            return list;
        }




        // 🔹 SELECT ALL
        public IEnumerable<StudentModel> SelectAll()
        {
            var students = new List<StudentModel>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Student_SelectAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                students.Add(new StudentModel
                {
                    Student_id = reader.GetInt32(reader.GetOrdinal("Student_id")),
                    First_name = reader["First_name"]?.ToString(),
                    Last_name = reader["Last_name"]?.ToString(),
                    Gender = reader["Gender"]?.ToString(),
                    Dob = reader.GetDateTime(reader.GetOrdinal("Dob")),
                    Phone = reader["Phone"]?.ToString(),
                    Email = reader["Email"]?.ToString(),
                    Address = reader["Address"]?.ToString(),
                });
            }

            return students;
        }

        // 🔹 SELECT BY PRIMARY KEY
        public StudentModel SelectByPK(int Student_id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Student_Select_By_PK", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Student_id", SqlDbType.Int).Value = Student_id;

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new StudentModel
                {
                    Student_id = reader.GetInt32(reader.GetOrdinal("Student_id")),
                    First_name = reader["First_name"]?.ToString(),
                    Last_name = reader["Last_name"]?.ToString(),
                    Gender = reader["Gender"]?.ToString(),
                    Dob = reader.GetDateTime(reader.GetOrdinal("Dob")),
                    Phone = reader["Phone"]?.ToString(),
                    Email = reader["Email"]?.ToString(),
                    Address = reader["Address"]?.ToString(),
                };
            }

            return null;
        }

        // 🔹 INSERT
        public bool Insert(StudentModel student)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Student_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@First_name", SqlDbType.VarChar, 100).Value = student.First_name;
            cmd.Parameters.Add("@Last_name", SqlDbType.VarChar, 100).Value = student.Last_name;
            cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 10).Value = student.Gender;
            cmd.Parameters.Add("@Dob", SqlDbType.DateTime).Value = student.Dob;
            cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 15).Value = student.Phone;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = student.Email;
            cmd.Parameters.Add("@Address", SqlDbType.VarChar, 200).Value = student.Address;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 UPDATE
        public bool Update(StudentModel student)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Student_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Student_id", SqlDbType.Int).Value = student.Student_id;
            cmd.Parameters.Add("@First_name", SqlDbType.VarChar, 100).Value = student.First_name;
            cmd.Parameters.Add("@Last_name", SqlDbType.VarChar, 100).Value = student.Last_name;
            cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 10).Value = student.Gender;
            cmd.Parameters.Add("@Dob", SqlDbType.DateTime).Value = student.Dob;
            cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 15).Value = student.Phone;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = student.Email;
            cmd.Parameters.Add("@Address", SqlDbType.VarChar, 200).Value = student.Address;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 DELETE
        public bool Delete(int Student_id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Student_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Student_id", SqlDbType.Int).Value = Student_id;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }



       


    }
}

