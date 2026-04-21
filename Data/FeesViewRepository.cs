using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Hostel_2026.Models;
using Microsoft.Extensions.Configuration;

namespace Hostel_2026.Data
{
    public class FeesViewRepository
    {
        private readonly string _connectionString;

        public FeesViewRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<FeesViewModel> SelectAll()
        {
            var list = new List<FeesViewModel>();

            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Fees_List", con);

            // 🔹 VERY IMPORTANT
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

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

                    Room_number = reader["Room_number"] != DBNull.Value
                        ? reader["Room_number"].ToString()
                        : null,

                    Room_type = reader["Room_type"] != DBNull.Value
                        ? reader["Room_type"].ToString()
                        : null
                });
            }

            return list;
        }
    }
}