using System;
using System.Collections.Generic;
using System.Text;
using MediCare.Interfaces;
using MediCare.Models;

namespace MediCare.Repositories
{
    public class MedicationRepository : IMedicationRepository
    {
        private readonly string filePath =
             Path.Combine(
                 AppContext.BaseDirectory,
                 "Data",
                 "medications.txt");
        public MedicationRepository()
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

        public void Add(Medication medication)
        {
            string line =
                $"{medication.Id}|{medication.Name}|{medication.Price}";

            File.AppendAllText(
                filePath,
                line + Environment.NewLine);
        }

        public void Remove(Medication medication)
        {
            List<Medication> medications = GetAll();

            Medication? existingMedication = medications
                .FirstOrDefault(m => m.Id == medication.Id);

            if (existingMedication == null)
            {
                return;
            }

            medications.Remove(existingMedication);

            SaveAll(medications);
        }

        public Medication? GetById(int id)
        {
            List<Medication> medications = GetAll();

            return medications.FirstOrDefault(
                m => m.Id == id);
        }

        public List<Medication> GetAll()
        {
            List<Medication> medications = new();

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] data = line.Split('|');

                Medication medication = new Medication
                {
                    Id = int.Parse(data[0]),
                    Name = data[1],
                    Price = decimal.Parse(data[2])
                };

                medications.Add(medication);
            }

            return medications;
        }

        private void SaveAll(List<Medication> medications)
        {
            List<string> lines = new();

            foreach (Medication medication in medications)
            {
                lines.Add(
                    $"{medication.Id}|{medication.Name}|{medication.Price}");
            }

            File.WriteAllLines(filePath, lines);
        }
    }
}
