using System.Security.Cryptography;
using System.Text;

namespace SpinningTrainer.Repositories
{
    internal class CryptographyData
    {
        private static string Key = "876543210FEDCBA9876543210FEDCBA9"; // 32 bytes (256 bits) para AES-256
        private static string IV = "1234567890ABCDEF"; 

        /// <summary>
        /// Metodo de encriptación de datos usando un formato AES-256
        /// </summary>
        /// <param name="plainText">Texto plano que se quiere cifrar.</param>
        /// <returns>Texto cifrado</returns>
        public string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.IV = Encoding.UTF8.GetBytes(IV);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                { 
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        /// <summary>
        /// Metodo de desencriptación de datos usando un formato AES-256
        /// </summary>
        /// <param name="cipherText">Texto cifrado que se quiere descifrar.</param>
        /// <returns>Texto descifrado</returns>
        public string Decrypt(string cipherText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.IV = Encoding.UTF8.GetBytes(IV);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
