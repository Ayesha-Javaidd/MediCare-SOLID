using MediCare.Interfaces;
using MediCare.Interfaces;
using MediCare.Models;
using MediCare.Models;
using MediCare.Repositories;
using MediCare.Services;
using MediCare.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediCare.UI
{
    public class UserUI
    {
        private readonly IAppointmentService appointmentService;
        private readonly IProviderRepository providerRepository;
        private readonly IMedicationService medicationService;

        public UserUI(
            IAppointmentService appointmentService,
            IProviderRepository providerRepository,
            IMedicationService medicationService)
        {
            this.appointmentService = appointmentService;

            this.providerRepository = providerRepository;

            this.medicationService = medicationService;
        }

        public void Show(User user)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("USER MENU");
                Console.WriteLine();
                Console.WriteLine($"Welcome, {user.Name}!");
                Console.WriteLine();
                Console.WriteLine("1. Book Appointment");
                Console.WriteLine("2. Order Medicines");
                Console.WriteLine("3. Logout");
                Console.WriteLine();

                int choice =
                    Helper.ReadInt("Enter choice: ");

                switch (choice)
                {
                    case 1:
                        BookAppointment(user);
                        break;

                    case 2:
                        OrderMedicines(user);
                        break;

                    case 3:
                        return;

                    default:
                        Console.WriteLine(
                            "Invalid choice.");

                        Helper.Pause();
                        break;
                }
            }
        }

        private void BookAppointment(User user)
        {
            Console.Clear();

            Console.WriteLine(
                "BOOK APPOINTMET");
            Console.WriteLine();

            List<Provider> providers =
                providerRepository.GetAll();

            if (providers.Count == 0)
            {
                Console.WriteLine(
                    "No providers available.");

                Helper.Pause();
                return;
            }

            Console.WriteLine("Available Providers:");
            Console.WriteLine();

            foreach (Provider provider in providers)
            {
                Console.WriteLine(
                    $"ID : {provider.Id} | " +
                    $"Doctor : {provider.Name} | " +
                    $"Specialty : {provider.Specialty}");
            }

            Console.WriteLine();

            int providerId =
                Helper.ReadInt(
                    "Enter provider ID : ");

            Provider? selectedProvider =
                providerRepository.GetById(providerId);

            if (selectedProvider == null)
            {
                Console.WriteLine(
                    "Provider not found.");

                Helper.Pause();
                return;
            }

            Console.WriteLine();

            DateTime date =
                Helper.ReadDate(
                    "Enter appointment date (yyyy-MM-dd): ");

            Appointment appointment =
                new Appointment
                {
                    Id = GetNextAppointmentId(),
                    UserId = user.Id,
                    ProviderId = selectedProvider.Id,
                    Date = date
                };

            try
            {
                appointmentService.BookAppointment(
                    appointment);

                Console.WriteLine();
                Console.WriteLine(
                    "Appointment booked successfully.");

                Console.WriteLine(
                    $"Doctor: {selectedProvider.Name}");

                Console.WriteLine(
                    $"Date: {appointment.Date:yyyy-MM-dd}");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }

            Helper.Pause();
        }

        private void OrderMedicines(User user)
        {
            Console.Clear();

            Console.WriteLine(
                "ORDER MEDICINES");
            Console.WriteLine();

            List<Medication> medications =
                medicationService.GetMedications();

            if (medications.Count == 0)
            {
                Console.WriteLine(
                    "No medications available.");

                Helper.Pause();
                return;
            }

            Console.WriteLine("Available Medications:");
            Console.WriteLine();

            foreach (Medication medication in medications)
            {
                Console.WriteLine(
                    $"ID: {medication.Id} | " +
                    $"{medication.Name}\t\t|" +
                    $"Price: Rs. {medication.Price}");
            }

            Console.WriteLine();

            int medicationId =
                Helper.ReadInt(
                    "Enter medication ID: ");

            Medication? selectedMedication =
                medicationService.GetMedicationById(
                    medicationId);

            if (selectedMedication == null)
            {
                Console.WriteLine(
                    "Medication not found.");

                Helper.Pause();
                return;
            }

            Console.WriteLine();
            Console.WriteLine(
                $"Selected: {selectedMedication.Name}");

            Console.WriteLine(
                $"Price: Rs. {selectedMedication.Price}");

            Console.WriteLine();
            Console.WriteLine(
                "Medicine ordered successfully.");

            Helper.Pause();
        }

        private int GetNextAppointmentId()
        {
            List<Appointment> appointments =
                appointmentService.GetAllAppointments();

            if (appointments.Count == 0)
            {
                return 1;
            }

            return appointments.Max(a => a.Id) + 1;
        }

    }
}