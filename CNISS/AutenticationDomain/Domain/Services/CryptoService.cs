using System;
using Nancy.Cryptography;

namespace CNISS.AutenticationDomain.Domain.Services
{
    public class CryptoService : ICryptoService
    {

        private IHmacProvider _hmacProvider;
        private IKeyGenerator _keyGenerator;
     
        public CryptoService(IKeyGenerator keyGenerator, Func<IKeyGenerator,IHmacProvider> hmacProvider)
        {
            _keyGenerator = keyGenerator;
            _hmacProvider = hmacProvider(_keyGenerator);
            
        }

      

        public string getEncryptedText(string text)
        {
            var data = _hmacProvider.GenerateHmac(text);
            return Convert.ToBase64String(data);
        }

        public string getKey()
        {
            return Convert.ToBase64String(_keyGenerator.GetBytes(1));
        }
    }
}