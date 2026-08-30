using System;
using System.Collections.Generic;
using System.Text;

namespace MediCare.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProviderId { get; set; }
        public DateTime Date { get; set; }
    }
}
