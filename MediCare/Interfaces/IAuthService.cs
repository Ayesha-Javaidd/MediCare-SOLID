using System;
using System.Collections.Generic;
using System.Text;
public interface IAuthService
{
    object? Login(string username, string password);
}