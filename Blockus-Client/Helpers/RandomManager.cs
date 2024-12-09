using System;

namespace Blockus_Client.Helpers
{
    public static class RandomManager
    {

        private static readonly Random _random = new Random();

        public static string GenerateRandomName()
        {
            const int lenght = 9;
            const string prefix = "Guest-";

            string randomDigits = GenerateRandomDigits(lenght);

            return prefix + randomDigits;
        }

        private static string GenerateRandomDigits(int lenght)
        {
            const string chars = "0123456789";
            char[] buffer = new char[lenght];

            for (int i = 0; i < lenght; i++)
            {
                buffer[i] = chars[_random.Next(chars.Length)];
            }

            return new string(buffer);
        }
        
    }
}
