using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CNISS.AutenticationDomain.Domain.Services
{
    public class RequestEncrypterRijndael:IEncrytRequestProvider
    {
        private readonly string _token;

        public RequestEncrypterRijndael(string token)
        {
            _token = token;
        }

        public  string encryptString(String plainMessage)
        {
            var tokenComponents = _token.Split(new[] { ":" }, StringSplitOptions.None);

            byte[] key = Convert.FromBase64String(tokenComponents[0]);
            byte[] iv = Convert.FromBase64String(tokenComponents[1]);
            int keySize = 32;
            int ivSize = 16;
            Array.Resize(ref key, keySize);
            Array.Resize(ref iv,ivSize);

            Rijndael RijndaelAlg = Rijndael.Create();


            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                RijndaelAlg.CreateEncryptor(key, iv),
                CryptoStreamMode.Write);

            byte[] plainMessageBytes = UTF8Encoding.UTF8.GetBytes(plainMessage);

            cryptoStream.Write(plainMessageBytes, 0, plainMessageBytes.Length);

            cryptoStream.FlushFinalBlock();

            byte[] cipherMessageBytes = memoryStream.ToArray();

 
            memoryStream.Close();
            cryptoStream.Close();


            return Convert.ToBase64String(cipherMessageBytes);
        }

        public  string decryptString(String encryptedMessage)
        {
            // Obtener la representación en bytes del texto cifrado
            var tokenComponents = _token.Split(new[] { ":" }, StringSplitOptions.None);

            byte[] key = Convert.FromBase64String(tokenComponents[0]);
            byte[] iv = Convert.FromBase64String(tokenComponents[1]);
            int keySize = 32;
            int ivSize = 16;
            Array.Resize(ref key, keySize);
            Array.Resize(ref iv, ivSize);

            byte[] cipherTextBytes = Convert.FromBase64String(encryptedMessage);

            // Crear un arreglo de bytes para almacenar los datos descifrados

            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            // Crear una instancia del algoritmo de Rijndael

            Rijndael RijndaelAlg = Rijndael.Create();
       
            // Crear un flujo en memoria con la representación de bytes de la información cifrada

            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

            // Crear un flujo de descifrado basado en el flujo de los datos

            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                RijndaelAlg.CreateDecryptor(key, iv),
                CryptoStreamMode.Read);

            // Obtener los datos descifrados obteniéndolos del flujo de descifrado

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            // Cerrar los flujos utilizados

            memoryStream.Close();
            cryptoStream.Close();

            // Retornar la representación de texto de los datos descifrados

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    }
}