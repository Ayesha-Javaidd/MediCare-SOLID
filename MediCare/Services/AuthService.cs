using System;
using System.Collections.Generic;
using System.Text;

using MediCare.Interfaces;
using MediCare.Models;

namespace MediCare.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepository;

        public AuthService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User? Login(
            string username,
            string password)
        {
            User? user =
                userRepository.GetByUsername(username);

            if (user != null &&
                user.Password == password)
            {
                return user;
            }

            return null;
        }
    }
}