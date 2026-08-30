using System;
using System.Collections.Generic;
using System.Text;
using MediCare.Models;

namespace MediCare.Interfaces
{
    public interface IMedicationService
    {
        void AddMedication(Medication medication);

        void RemoveMedication(Medication medication);

        List<Medication> GetMedications();

        Medication GetMedicationById(int id);
    }
}