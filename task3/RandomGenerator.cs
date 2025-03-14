

using System.Security.Cryptography;

namespace task3
{
    public class RandomGenerator
    {
        public static int GenerateRandomNumber(int ceil)
        {
            return RandomNumberGenerator.GetInt32(0, ceil);
        }
    }
}
