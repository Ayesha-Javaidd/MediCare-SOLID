using System;
using System.Collections.Generic;
using System.Text;
using MediCare.Models;

namespace MediCare.Interfaces
{
    public interface IProviderRepository
    {
        List<Provider> GetAll();
        Provider? GetById(int id);
    }
}