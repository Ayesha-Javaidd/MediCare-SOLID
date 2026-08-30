using System;
using System.Collections.Generic;
using System.Text;
using MediCare.Models;

namespace MediCare.Interfaces
{
    public interface IAppointmentService
    {
        void BookAppointment(Appointment appointment);
    }
}