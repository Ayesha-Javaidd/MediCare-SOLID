using System;
using System.Collections.Generic;
using System.Text;
using MediCare.Interfaces;
using MediCare.Models;

namespace MediCare.Services
{
    public class MedicationService : IMedicationService
    {
        private readonly IMedicationRepository _medicationRepository;

        public MedicationService(
            IMedicationRepository repository)
        {
            _medicationRepository = repository;
        }

        public void AddMedication(Medication medication)
        {
            _medicationRepository.Add(medication);
        }

        public void RemoveMedication(Medication medication)
        {
            _medicationRepository.Remove(medication);
        }

        public List<Medication> GetMedications()
        {
            return _medicationRepository.GetAll();
        }

        public Medication? GetMedicationById(int id)
        {
            return _medicationRepository.GetById(id);
        }
    }
}