using System;

namespace Hostel_2026.Models
{
    public class StudentAttendanceModel
    {
        public int Attendance_id { get; set; }

        public int Student_id { get; set; }

        public DateTime AttendanceDate { get; set; }

        public string Status { get; set; }

        public string Place { get; set; }

        public string Remarks { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}