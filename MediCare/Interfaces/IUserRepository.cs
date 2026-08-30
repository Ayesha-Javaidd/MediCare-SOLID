using MediCare.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MediCare.Models;

namespace MediCare.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        void Remove(User user);
        void Update(User user);

        User? GetById(int id);
        User? GetByUsername(string username);

        List<User> GetAll();
    }
}
