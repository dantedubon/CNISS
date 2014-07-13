using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.Cryptography;

namespace CNISS.AutenticationDomain.Domain.Services
{
    public class UserKeyRecovery:IKeyGenerator
    {
        private readonly byte[] _key;


        public UserKeyRecovery(byte[] key)
        {
            _key = key;
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