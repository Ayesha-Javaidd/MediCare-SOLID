using MediCare.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MediCare.Models;

namespace MediCare.Interfaces
{
    public interface IMedicationRepository
    {
        void Add(Medication medication);
        void Remove(Medication medication);
        Medication? GetById(int id);
        List<Medication> GetAll();
    }
}
