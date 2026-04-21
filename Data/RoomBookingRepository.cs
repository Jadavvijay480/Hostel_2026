using Hostel_2026.Models;
using Hostel_Consume_2026.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Hostel_2026.Data
{
    public class RoomBookingRepository
    {
        private readonly string _connectionString;

        public RoomBookingRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 🔹 SELECT ALL
        public IEnumerable<RoomBookingModel> SelectAll()
        {
            var books = new List<RoomBookingModel>();
            
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_GetAllRoomBookings", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                books.Add(new RoomBookingModel
                {
                    Booking_Id = reader.GetInt32(reader.GetOrdinal("Booking_Id")),
                    Guest_Name = reader["Guest_Name"]?.ToString(),
                    Mobile_No = reader["Mobile_No"]?.ToString(),
                    Room_Number = reader["Room_Number"]?.ToString(),
                    Address = reader["Address"]?.ToString(),
                    Booking_Date = reader.GetDateTime(reader.GetOrdinal("Booking_Date")),
                    Booking_Status = reader["Booking_Status"]?.ToString(),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                });
            }

            return books;
        }

        // 🔹 SELECT BY PRIMARY KEY
        public RoomBookingModel SelectByPK(int Booking_Id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_GetRoomBookingById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Booking_Id", SqlDbType.Int).Value = Booking_Id;

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new RoomBookingModel
                {
                    Booking_Id = reader.GetInt32(reader.GetOrdinal("Booking_Id")),
                    Guest_Name = reader["Guest_Name"]?.ToString(),
                    Mobile_No = reader["Mobile_No"]?.ToString(),
                    Room_Number = reader["Room_Number"]?.ToString(),
                    Address = reader["Address"]?.ToString(),
                    Booking_Date = reader.GetDateTime(reader.GetOrdinal("Booking_Date")),
                    Booking_Status = reader["Booking_Status"]?.ToString(),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                };
            }

            return null;
        }

        // 🔹 INSERT
        public bool Insert(RoomBookingModel books)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_InsertRoomBooking", conn)
            {
                CommandType = CommandType.StoredProcedure
            };


            cmd.Parameters.Add("@Guest_Name", SqlDbType.NVarChar, 100).Value = books.Guest_Name;
            cmd.Parameters.Add("@Mobile_No", SqlDbType.NVarChar, 15).Value = books.Mobile_No;
            cmd.Parameters.Add("@Room_Number", SqlDbType.NVarChar, 20).Value = books.Room_Number;
            cmd.Parameters.Add("@Booking_Date", SqlDbType.Date).Value = books.Booking_Date;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 20).Value = books.Address;
            cmd.Parameters.Add("@Booking_Status", SqlDbType.NVarChar, 50).Value = books.Booking_Status;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 UPDATE
        public bool Update(RoomBookingModel books)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_UpdateRoomBooking", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add("@Booking_Id", SqlDbType.Int).Value = books.Booking_Id;
            cmd.Parameters.Add("@Guest_Name", SqlDbType.NVarChar, 100).Value = books.Guest_Name;
            cmd.Parameters.Add("@Mobile_No", SqlDbType.NVarChar, 15).Value = books.Mobile_No;
            cmd.Parameters.Add("@Room_Number", SqlDbType.NVarChar, 20).Value = books.Room_Number;
            cmd.Parameters.Add("@Booking_Date", SqlDbType.Date).Value = books.Booking_Date;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 20).Value = books.Address;
            cmd.Parameters.Add("@Booking_Status", SqlDbType.NVarChar, 50).Value = books.Booking_Status;


            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 DELETE
        public bool Delete(int Booking_Id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_DeleteRoomBooking", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Booking_Id", SqlDbType.Int).Value = Booking_Id;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }



    }
}