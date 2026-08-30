using MediCare.Interfaces;
using MediCare.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace MediCare.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(
            IAppointmentRepository repository)
        {
            this._appointmentRepository = repository;
        }

        public void BookAppointment(Appointment appointment)
        {
            _appointmentRepository.Add(appointment);
        }

        public List<Appointment> GetAllAppointments()
        {
            return _appointmentRepository.GetAll();
        }
    }
}