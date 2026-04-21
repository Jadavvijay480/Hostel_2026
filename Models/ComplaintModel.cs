using System;

namespace Hostel_2026.Models
{
    public class ComplaintModel
    {
        public int Complaint_id { get; set; }

        public int Student_id { get; set; }

        public string ComplaintType { get; set; }

        public string ComplaintDetails { get; set; }

        public DateTime ComplaintDate { get; set; }

        public string Status { get; set; }

        public string ActionTaken { get; set; }

        public DateTime? ResolvedDate { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}