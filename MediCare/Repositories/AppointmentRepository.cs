using System;
using System.Collections.Generic;
using System.Text;
using MediCare.Interfaces;
using MediCare.Models;

namespace MediCare.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly string filePath =
         Path.Combine(
             AppContext.BaseDirectory,
             "Data",
             "appointments.txt");

        public AppointmentRepository()
        {
            EnsureFileExists();
        }

        private void EnsureFileExists()
        {
            string? directory = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory!);
            }

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
        }

        public void Add(Appointment appointment)
        {
            string line =
                $"{appointment.Id}|{appointment.UserId}|{appointment.ProviderId}|{appointment.Date:yyyy-MM-dd}";

            File.AppendAllText(
                filePath,
                line + Environment.NewLine);
        }

        public void Remove(Appointment appointment)
        {
            List<Appointment> appointments = GetAll();

            Appointment? existingAppointment =
                appointments.FirstOrDefault(
                    a => a.Id == appointment.Id);

            if (existingAppointment == null)
            {
                return;
            }

            appointments.Remove(existingAppointment);

            SaveAll(appointments);
        }

        public List<Appointment> GetAll()
        {
            List<Appointment> appointments = new();

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] data = line.Split('|');

                Appointment appointment = new Appointment
                {
                    Id = int.Parse(data[0]),
                    UserId = int.Parse(data[1]),
                    ProviderId = int.Parse(data[2]),
                    Date = DateTime.Parse(data[3])
                };

                appointments.Add(appointment);
            }

            return appointments;
        }

        private void SaveAll(List<Appointment> appointments)
        {
            List<string> lines = new();

            foreach (Appointment appointment in appointments)
            {
                lines.Add(
                    $"{appointment.Id}|{appointment.UserId}|{appointment.ProviderId}|{appointment.Date:yyyy-MM-dd}");
            }

            File.WriteAllLines(filePath, lines);
        }
    }
}