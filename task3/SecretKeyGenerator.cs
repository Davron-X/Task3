using System.Security.Cryptography;

namespace task3
{
    public class SecretKeyGenerator
    {
        public static byte[] GenerateSecretKey()
        {
            byte[] secretKey = new byte[32];
            RandomNumberGenerator.Fill(secretKey);
            return secretKey;
        }

        public static string ConvertKeyToString(byte[] secretKey)
        {
           return BitConverter.ToString(secretKey).Replace("-", "");
        }
    }
}
