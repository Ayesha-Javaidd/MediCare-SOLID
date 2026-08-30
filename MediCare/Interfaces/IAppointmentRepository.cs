using MediCare.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediCare.Interfaces
{
    public interface IAppointmentRepository
    {
        void Add(Appointment appointment);

        void Remove(Appointment appointment);

        List<Appointment> GetAll();
    }
}
