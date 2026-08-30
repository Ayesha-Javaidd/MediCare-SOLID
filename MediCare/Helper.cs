using System;
using System.Collections.Generic;
using System.Text;
namespace MediCare.UI
{
    public static class Helper
    {
        public static string ReadRequiredString(string message)
        {
            while (true)
            {
                Console.Write(message);

                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }

                Console.WriteLine("Input cannot be empty.");
            }
        }

        public static int ReadInt(string message)
        {
            while (true)
            {
                Console.Write(message);

                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    return value;
                }

                Console.WriteLine("Please enter a valid number.");
            }
        }

        public static decimal ReadDecimal(string message)
        {
            while (true)
            {
                Console.Write(message);

                if (decimal.TryParse(
                    Console.ReadLine(),
                    out decimal value))
                {
                    return value;
                }

                Console.WriteLine("Please enter a valid price.");
            }
        }

        public static DateTime ReadDate(string message)
        {
            while (true)
            {
                Console.Write(message);

                if (DateTime.TryParse(
                    Console.ReadLine(),
                    out DateTime date))
                {
                    return date;
                }

                Console.WriteLine("Please enter a valid date.");
            }
        }

        public static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}