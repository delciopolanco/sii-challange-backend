using System.Security.Cryptography;
using System.Text;

namespace addCard_backend.Services
{
    public class SecurityService : ISecurityService
    {
        public IConfiguration configuration;

        public SecurityService(IConfiguration _configutation)
        {
            configuration = _configutation;
        }

        public string Decrypt(string maskedAndEncryptedCardNumber)
        {
            if (string.IsNullOrEmpty(maskedAndEncryptedCardNumber)) throw new Exception("You must provide a value to Decrypt");

            string keyFromConfig = configuration["Encrypt:Key"];
            string vectorFromConfig = configuration["Encrypt:Vector"];

            byte[] key = Encoding.UTF8.GetBytes(keyFromConfig.PadRight(32, '\0').Substring(0, 32));
            byte[] iv = Encoding.UTF8.GetBytes(vectorFromConfig.PadRight(16, '\0'));

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Extraer la parte cifrada de la cadena
                string encryptedCardNumber = maskedAndEncryptedCardNumber.Substring(2, maskedAndEncryptedCardNumber.Length - 6);

                byte[] encryptedBytes = Convert.FromBase64String(encryptedCardNumber);

                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                string decryptedCardNumber = Encoding.UTF8.GetString(decryptedBytes);

                return decryptedCardNumber;
            }
        }

        public string Encrypt(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber)) throw new Exception("You must provide a value Encrypt");

            string keyFromConfig = configuration["Encrypt:Key"];
            string vectorFromConfig = configuration["Encrypt:Vector"];

            byte[] key = Encoding.UTF8.GetBytes(keyFromConfig.PadRight(32, '\0').Substring(0, 32));
            byte[] iv = Encoding.UTF8.GetBytes(vectorFromConfig.PadRight(16, '\0'));

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                byte[] cardNumberBytes = Encoding.UTF8.GetBytes(cardNumber);

                byte[] encryptedBytes = encryptor.TransformFinalBlock(cardNumberBytes, 0, cardNumberBytes.Length);

                string encryptedCardNumber = Convert.ToBase64String(encryptedBytes);
                string maskedCardNumber = cardNumber.Substring(0, 2) + encryptedCardNumber + cardNumber.Substring(cardNumber.Length - 4);


                return maskedCardNumber;
            }
        }
    }
}
