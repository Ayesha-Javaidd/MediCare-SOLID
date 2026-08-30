using System;
using System.Collections.Generic;
using System.Text;
using MediCare.Interfaces;
using MediCare.Models;

namespace MediCare.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string filePath =
            Path.Combine(
                AppContext.BaseDirectory,
                "Data",
                "users.txt");

        public UserRepository()
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

        public void Add(User user)
        {
            string line =
                $"{user.Id}|{user.Name}|{user.Username}|{user.Password}|{user.Role}";

            File.AppendAllText(filePath, line + Environment.NewLine);
        }

        public void Remove(User user)
        {
            List<User> users = GetAll();

            User? existingUser = users
                .FirstOrDefault(u => u.Id == user.Id);

            if (existingUser == null)
            {
                return;
            }

            users.Remove(existingUser);

            SaveAll(users);
        }

        public void Update(User user)
        {
            List<User> users = GetAll();

            User? existingUser = users
                .FirstOrDefault(u => u.Id == user.Id);

            if (existingUser == null)
            {
                return;
            }

            existingUser.Name = user.Name;
            existingUser.Username = user.Username;
            existingUser.Password = user.Password;
            existingUser.Role = user.Role;

            SaveAll(users);
        }

        public User? GetById(int id)
        {
            List<User> users = GetAll();

            return users.FirstOrDefault(u => u.Id == id);
        }

        public User? GetByUsername(string username)
        {
            List<User> users = GetAll();

            return users.FirstOrDefault(
                u => u.Username.Equals(
                    username,
                    StringComparison.OrdinalIgnoreCase));
        }

        public List<User> GetAll()
        {
            List<User> users = new();

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] data = line.Split('|');

                User user = new User
                {
                    Id = int.Parse(data[0]),
                    Name = data[1],
                    Username = data[2],
                    Password = data[3],
                    Role = data[4]
                };

                users.Add(user);
            }

            return users;
        }

        private void SaveAll(List<User> users)
        {
            List<string> lines = new();

            foreach (User user in users)
            {
                string line =
                    $"{user.Id}|{user.Name}|{user.Username}|{user.Password}|{user.Role}";

                lines.Add(line);
            }

            File.WriteAllLines(filePath, lines);
        }
    }
}