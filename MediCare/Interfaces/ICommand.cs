using System;
using System.Collections.Generic;
using System.Text;
namespace MediCare.Interfaces
{
    public interface ICommand
    {
        void Execute();

        void Undo();
    }
}