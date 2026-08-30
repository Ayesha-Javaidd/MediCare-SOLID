using MediCare.Interfaces;
using MediCare.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediCare.Services
{
   public class UserService : IUserService
   {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                throw new ArgumentException(
                    "Name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(user.Username))
            {
                throw new ArgumentException(
                    "Username cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(user.Password))
            {
                throw new ArgumentException(
                    "Password cannot be empty.");
            }

            if (_userRepository.GetByUsername(user.Username) != null)
            {
                throw new ArgumentException(
                    "Username already exists.");
            }
            _userRepository.Add(user);
        }

        public void EditUser(User user)
        {
            _userRepository.Remove(user);
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }
    }
}
