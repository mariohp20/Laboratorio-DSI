using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Security.Cryptography;

namespace MiBotica.SolPedido.Utiles.Helpers
{
    public static class EncriptacionHelper
    {
        public static byte[] EncriptarByte(string rawText)
        {
            var rijndaelCipher = new RijndaelManaged();
            byte[] rawTextData = Encoding.UTF8.GetBytes(rawText);
            Rfc2898DeriveBytes secretKey = GetSecretKey();

            using (var encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(rawTextData, 0, rawTextData.Length);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }
        }

        private static Rfc2898DeriveBytes GetSecretKey()
        {
            const string encryptionKey = "T@ll3rN3t2018";
            byte[] salt = Encoding.UTF8.GetBytes(encryptionKey);
            var secretKey = new Rfc2898DeriveBytes(encryptionKey, salt);
            return secretKey;
        }
    }
}