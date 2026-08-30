using System;
using System.Collections.Generic;
using System.Text;
using MediCare.Models;

namespace MediCare.Interfaces
{
    public interface IUserService
    {
        void AddUser(User user);

        void EditUser(User user);

        List<User> GetUsers();
    }
}