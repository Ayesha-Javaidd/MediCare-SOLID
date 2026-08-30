using MediCare.Interfaces;
using MediCare.Models;
using MediCare.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediCare.UI
{
    public class LoginUI
    {
        private readonly IAuthService authService;
        private readonly AdminUI adminUI;
        private readonly UserUI userUI;

        public LoginUI(
            IAuthService authService,
            AdminUI adminUI,
            UserUI userUI)
        {
            this.authService = authService;
            this.adminUI = adminUI;
            this.userUI = userUI;
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("MEDICARE SYSTEM");
                Console.WriteLine();

                string username =
                    Helper.ReadRequiredString(
                        "Username: ");

                string password =
                    Helper.ReadRequiredString(
                        "Password: ");

                User? user =
                    authService.Login(username, password);

                if (user == null)
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        "Invalid username or password.");

                    Helper.Pause();
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine(
                    $"Welcome, {user.Name}!");

                Helper.Pause();

                if (user.Role.Equals(
                    "Admin",
                    StringComparison.OrdinalIgnoreCase))
                {
                    adminUI.Show(user);
                }
                else
                {
                    userUI.Show(user);
                }
            }
        }
    }

}