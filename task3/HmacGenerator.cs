using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
namespace task3
{
    public class HmacGenerator
    {
        public static string GenerateHmac(byte[] secretKey, int message)
        {
            byte[] messageBytes= BitConverter.GetBytes(message);                        
            HMac hmac = new(new Sha3Digest(256));
            hmac.Init(new KeyParameter(secretKey));
            hmac.BlockUpdate(messageBytes, 0, messageBytes.Length);
            byte[] result = new byte[hmac.GetMacSize()];
            hmac.DoFinal(result, 0);
            return BitConverter.ToString(result).Replace("-", "");             
        }
    }
}
