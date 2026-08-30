using MediCare.Interfaces;
using MediCare.Models;
using MediCare.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediCare.UI
{
    public class AdminUI
    {
        private readonly IUserService userService;
        private readonly IMedicationService medicationService;

        public AdminUI(
            IUserService userService,
            IMedicationService medicationService)
        {
            this.userService = userService;
            this.medicationService = medicationService;
        }

        public void Show(User admin)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("ADMIN MENU");
                Console.WriteLine();
                Console.WriteLine("1. Add User");
                Console.WriteLine("2. Edit User");
                Console.WriteLine("3. Add Medication");
                Console.WriteLine("4. Remove Medication");
                Console.WriteLine("5. Logout");
                Console.WriteLine();

                int choice =
                    
                    Helper.ReadInt("Enter choice: ");

                switch (choice)
                {
                    case 1:
                        AddUser();
                        break;

                    case 2:
                        EditUser();
                        break;

                    case 3:
                        AddMedication();
                        break;

                    case 4:
                        RemoveMedication();
                        break;

                    case 5:
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        Helper.Pause();
                        break;
                }
            }
        }

        private void AddUser()
        {
            Console.Clear();

            Console.WriteLine("ADD USER");
            Console.WriteLine();

            int id = GetNextUserId();

            string name =
                Helper.ReadRequiredString("Enter name: ");

            string username =
                Helper.ReadRequiredString(
                    "Enter username: ");

            string password =
                Helper.ReadRequiredString(
                    "Enter password: ");

            User user = new User
            {
                Id = id,
                Name = name,
                Username = username,
                Password = password,
                Role = "User"
            };

            try
            {
                userService.AddUser(user);

                Console.WriteLine();
                Console.WriteLine(
                    "User added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }

            Helper.Pause();
        }

        private void EditUser()
        {
            Console.Clear();

            Console.WriteLine("EDIT USER");
            Console.WriteLine();

            List<User> users =
                userService.GetUsers();

            if (users.Count == 0)
            {
                Console.WriteLine("No users found.");
                Helper.Pause();
                return;
            }

            foreach (User user in users)
            {
                Console.WriteLine(
                    $"ID: {user.Id} | " +
                    $"Name: {user.Name} | " +
                    $"Username: {user.Username} | " +
                    $"Role: {user.Role}");
            }

            Console.WriteLine();

            int id =
                Helper.ReadInt(
                    "Enter user ID to edit: ");

            User? existingUser =
                userService.GetUserById(id);

            if (existingUser == null)
            {
                Console.WriteLine("User not found.");
                Helper.Pause();
                return;
            }

            Console.WriteLine();
            Console.WriteLine(
                $"Editing: {existingUser.Name}");

            string name =
                Helper.ReadRequiredString(
                    "Enter new name: ");

            string username =
                Helper.ReadRequiredString(
                    "Enter new username: ");

            string password =
                Helper.ReadRequiredString(
                    "Enter new password: ");

            existingUser.Name = name;
            existingUser.Username = username;
            existingUser.Password = password;

            try
            {
                userService.EditUser(existingUser);

                Console.WriteLine();
                Console.WriteLine(
                    "User updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }

            Helper.Pause();
        }

        private void AddMedication()
        {
            Console.Clear();

            Console.WriteLine(
                "ADD MEDICATION");
            Console.WriteLine();

            int id =
                GetNextMedicationId();

            string name =
                Helper.ReadRequiredString(
                    "Enter medication name: ");

            decimal price =
                Helper.ReadDecimal(
                    "Enter medication price: ");

            Medication medication = new Medication
            {
                Id = id,
                Name = name,
                Price = price
            };

            try
            {
                medicationService.AddMedication(
                    medication);

                Console.WriteLine();
                Console.WriteLine(
                    "Medication added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }

            Helper.Pause();
        }

        private void RemoveMedication()
        {
            Console.Clear();

            Console.WriteLine(
                "REMOVE MEDICATION");
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

            foreach (Medication medication in medications)
            {
                Console.WriteLine(
                    $"ID: {medication.Id} | " +
                    $"Name: {medication.Name} \t| " +
                    $"Price: {medication.Price}");
            }

            Console.WriteLine();

            int id =
                Helper.ReadInt(
                    "Enter medication ID to remove: ");

            Medication? medicationToRemove =
                medicationService.GetMedicationById(id);

            if (medicationToRemove == null)
            {
                Console.WriteLine(
                    "Medication not found.");

                Helper.Pause();
                return;
            }

            medicationService.RemoveMedication(
                medicationToRemove);

            Console.WriteLine();
            Console.WriteLine(
                "Medication removed successfully.");

            Helper.Pause();
        }

        private int GetNextMedicationId()
        {
            List<Medication> medications =
                medicationService.GetMedications();

            if (medications.Count == 0)
            {
                return 1;
            }

            return medications.Max(a => a.Id) + 1;
        }

        private int GetNextUserId()
        {
            List<User> users =
                userService.GetUsers();

            if (users.Count == 0)
            {
                return 1;
            }

            return users.Max(a => a.Id) + 1;
        }
    }
}