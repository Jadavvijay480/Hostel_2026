using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Hostel_2026.Models;
using Microsoft.Extensions.Configuration;

namespace Hostel_2026.Data
{
    public class RoomRepository
    {
        private readonly string _connectionString;

        public RoomRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }



        // 🔹 GetDropdown method
        public List<RoomDropdownModel> GetDropdown()
        {
            var list = new List<RoomDropdownModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                // Only available rooms
                string sql = @"
            SELECT Room_id, Room_number, Room_type, Capacity
            FROM Rooms
            WHERE Status IN ('Occupied', 'Maintenance', 'Available')
            ORDER BY Room_number";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new RoomDropdownModel
                            {
                                Room_id = reader["Room_id"] != DBNull.Value ? Convert.ToInt32(reader["Room_id"]) : 0,
                                Room_number = reader["Room_number"].ToString(),
                                Room_type = reader["Room_type"].ToString(),
                                Capacity = reader["Capacity"] != DBNull.Value ? Convert.ToInt32(reader["Capacity"]) : 0
                            });
                        }
                    }
                }
            }

            return list;
        }








        // 🔹 SELECT ALL
        public IEnumerable<RoomModel> SelectAll()
        {
            var rooms = new List<RoomModel>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Rooms_SelectAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                rooms.Add(new RoomModel
                {
                    Room_id = reader.GetInt32(reader.GetOrdinal("Room_id")),
                    Student_id = reader.GetInt32(reader.GetOrdinal("Student_id")),
                    Hostel_id = reader.GetInt32(reader.GetOrdinal("Hostel_id")),
                    Room_number = reader["Room_number"]?.ToString(),
                    Room_type = reader["Room_type"]?.ToString(),
                    Capacity = reader.GetInt32(reader.GetOrdinal("Capacity")),
                    Status = reader["Status"]?.ToString()
                });
            }

            return rooms;
        }

        // 🔹 SELECT BY PRIMARY KEY
        public RoomModel SelectByPK(int Room_id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Rooms_SelectByPK", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Room_id", SqlDbType.Int).Value = Room_id;

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new RoomModel
                {
                    Room_id = reader.GetInt32(reader.GetOrdinal("Room_id")),
                    Student_id = reader.GetInt32(reader.GetOrdinal("Student_id")),
                    Hostel_id = reader.GetInt32(reader.GetOrdinal("Hostel_id")),
                    Room_number = reader["Room_number"]?.ToString(),
                    Room_type = reader["Room_type"]?.ToString(),
                    Capacity = reader.GetInt32(reader.GetOrdinal("Capacity")),
                    Status = reader["Status"]?.ToString()
                };
            }

            return null;
        }

        // 🔹 INSERT
        public bool Insert(RoomModel room)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Rooms_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Student_id", SqlDbType.Int).Value = room.Student_id;
            cmd.Parameters.Add("@Hostel_id", SqlDbType.Int).Value = room.Hostel_id;
            cmd.Parameters.Add("@Room_number", SqlDbType.VarChar, 10).Value = room.Room_number;
            cmd.Parameters.Add("@Room_type", SqlDbType.VarChar, 50).Value = room.Room_type;
            cmd.Parameters.Add("@Capacity", SqlDbType.Int).Value = room.Capacity;
            cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = room.Status;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 UPDATE
        public bool Update(RoomModel room)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Rooms_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Room_id", SqlDbType.Int).Value = room.Room_id;
            cmd.Parameters.Add("@Student_id", SqlDbType.Int).Value = room.Student_id;
            cmd.Parameters.Add("@Hostel_id", SqlDbType.Int).Value = room.Hostel_id;
            cmd.Parameters.Add("@Room_number", SqlDbType.VarChar, 10).Value = room.Room_number;
            cmd.Parameters.Add("@Room_type", SqlDbType.VarChar, 50).Value = room.Room_type;
            cmd.Parameters.Add("@Capacity", SqlDbType.Int).Value = room.Capacity;
            cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = room.Status;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 DELETE
        public bool Delete(int Room_id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Rooms_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Room_id", SqlDbType.Int).Value = Room_id;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }


       
    }
}