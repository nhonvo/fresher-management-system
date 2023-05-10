using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ReportAttendence : BaseEntity
    {
        public string Reason { get; set; }
        public StatusAttendance statusAttendance { get; set; } = StatusAttendance.Waiting;
        public DateTime expectedDates { get; set; }
        public string StudentId { get; set; }
        public User Student { get; set; }

    }
}
