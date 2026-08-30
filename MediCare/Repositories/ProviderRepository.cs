using System;
using System.Collections.Generic;
using System.Text;
using MediCare.Interfaces;
using MediCare.Models;

namespace MediCare.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly string filePath =
            Path.Combine(
                AppContext.BaseDirectory,
                "Data",
                "providers.txt");

        public ProviderRepository()
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

        public List<Provider> GetAll()
        {
            List<Provider> providers = new();

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] data = line.Split('|');

                Provider provider = new Provider
                {
                    Id = int.Parse(data[0]),
                    Name = data[1],
                    Specialty = data[2]
                };

                providers.Add(provider);
            }

            return providers;
        }

        public Provider? GetById(int id)
        {
            List<Provider> providers = GetAll();

            return providers.FirstOrDefault(p => p.Id == id);
        }
    }
}