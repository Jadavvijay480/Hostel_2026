using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Hostel_2026.Models;
using Microsoft.Extensions.Configuration;

namespace Hostel_2026.Data
{
    public class FeesRepository
    {
        private readonly string _connectionString;

        public FeesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }



        // 🔹 SELECT ALL (Fees + Student + Room)
        public List<FeesViewModel> SelectAllWithStudentRoom()
        {
            var list = new List<FeesViewModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string sql = @"
                SELECT 
                    f.Fee_id,
                    f.Student_id,
                    s.First_name + ' ' + s.Last_name AS Student_Name,
                    s.Phone,
                    s.Email,
                    f.Amount,
                    f.Fee_month,
                    f.Due_date,
                    f.Status,
                    r.Room_id,
                    r.Room_number,
                    r.Room_type
                FROM Fees f
                INNER JOIN Student s ON f.Student_id = s.Student_id
                LEFT JOIN Rooms r ON r.Room_id = f.Room_id
                ORDER BY f.Due_date DESC";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new FeesViewModel
                            {
                                Fee_id = Convert.ToInt32(reader["Fee_id"]),
                                Student_id = Convert.ToInt32(reader["Student_id"]),
                                Student_Name = reader["Student_Name"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Email = reader["Email"].ToString(),
                                Amount = Convert.ToDecimal(reader["Amount"]),
                                Fee_month = reader["Fee_month"].ToString(),
                                Due_date = Convert.ToDateTime(reader["Due_date"]),
                                Status = reader["Status"].ToString(),
                                Room_id = reader["Room_id"] != DBNull.Value
                                          ? Convert.ToInt32(reader["Room_id"])
                                          : (int?)null,
                                Room_number = reader["Room_number"]?.ToString(),
                                Room_type = reader["Room_type"]?.ToString()
                            });
                        }
                    }
                }
            }

            return list;
        }




        // 🔹 SELECT ALL
        public IEnumerable<FeesModel> SelectAll()
        {
            var fees = new List<FeesModel>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Fees_SelectAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                fees.Add(new FeesModel
                {
                    Fee_id = reader.GetInt32(reader.GetOrdinal("Fee_id")),
                    Student_id = reader.GetInt32(reader.GetOrdinal("Student_id")),
                    Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                    Fee_month = reader["Fee_month"]?.ToString(),
                    Due_date = reader.GetDateTime(reader.GetOrdinal("Due_date")),
                    Status = reader["Status"]?.ToString()
                });
            }

            return fees;
        }

        // 🔹 SELECT BY PK
        public FeesModel SelectByPK(int Fee_id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Fees_SelectByPK", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Fee_id", SqlDbType.Int).Value = Fee_id;

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new FeesModel
                {
                    Fee_id = reader.GetInt32(reader.GetOrdinal("Fee_id")),
                    Student_id = reader.GetInt32(reader.GetOrdinal("Student_id")),
                    Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                    Fee_month = reader["Fee_month"]?.ToString(),
                    Due_date = reader.GetDateTime(reader.GetOrdinal("Due_date")),
                    Status = reader["Status"]?.ToString()
                };
            }

            return null;
        }

        // 🔹 INSERT
        public bool Insert(FeesModel fees)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Fees_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Student_id", SqlDbType.Int).Value = fees.Student_id;
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = fees.Amount;
            cmd.Parameters.Add("@Fee_month", SqlDbType.VarChar, 20).Value = fees.Fee_month;
            cmd.Parameters.Add("@Due_date", SqlDbType.DateTime).Value = fees.Due_date;
            cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = fees.Status;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 UPDATE
        public bool Update(FeesModel fees)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Fees_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Fee_id", SqlDbType.Int).Value = fees.Fee_id;
            cmd.Parameters.Add("@Student_id", SqlDbType.Int).Value = fees.Student_id;
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = fees.Amount;
            cmd.Parameters.Add("@Fee_month", SqlDbType.VarChar, 20).Value = fees.Fee_month;
            cmd.Parameters.Add("@Due_date", SqlDbType.DateTime).Value = fees.Due_date;
            cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = fees.Status;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 DELETE
        public bool Delete(int Fee_id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Fees_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Fee_id", SqlDbType.Int).Value = Fee_id;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}