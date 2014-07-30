using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Services;

namespace CNISS_Tests
{
    public class DummyEncrytRequestProvider:IEncrytRequestProvider
    {
        readonly string _token;

        public DummyEncrytRequestProvider()
        {
            
        }

        public string encryptString(string plainMessage)
        {
            return plainMessage;
        }

        public string decryptString(string encryptedMessage)
        {
            return encryptedMessage;
        }
    }
}