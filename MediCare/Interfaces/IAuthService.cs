using System;
using System.Collections.Generic;
using System.Text;
using MediCare.Models;
public interface IAuthService
{
    User? Login(string username, string password);
}