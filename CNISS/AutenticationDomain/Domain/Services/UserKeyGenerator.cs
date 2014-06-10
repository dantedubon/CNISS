using System;
using Nancy.Cryptography;

namespace CNISS.AutenticationDomain.Domain.Services
{
    public class UserKeyGenerator : IKeyGenerator
    {
        private readonly byte[] _key;

        public UserKeyGenerator(IKeyGenerator generator, int size = 128)
        {
            _key = generator.GetBytes(size);
        }
        public byte[] GetBytes(int count)
        {
            return _key;
        }

        public string getKey()
        {
            return Convert.ToBase64String(_key);
        }
    }
}