using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using CNISS.AutenticationDomain.Domain.Services;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.Cryptography;
using NUnit.Framework;

namespace CNISS_Tests.Autentication_Test.User_Test.Cryptography
{
    [TestFixture]
    public class RequestSecurity_Test
    {

        [Test]
        public void encryptRequest_objectRecieved_encryptAndDecrypAreEqual()
        {
            var tokenizer = getTokenizer(TimeSpan.FromHours(6));
            var token = getToken(tokenizer, new DummyUserIdentityMovil("DRCD"));
            var data = "data to encrypt";
            var encrypter = new RequestEncrypterRijndael(token);
            var decrypter = new RequestEncrypterRijndael(token);
            var dataEncryted = encrypter.encryptString(data);
            var dataDecrypt = decrypter.decryptString(dataEncryted);

            Assert.IsTrue(dataDecrypt.Equals(data));
        }

        private string getToken(Tokenizer tokenizer, DummyUserIdentityMovil userIdentity)
        {

            NancyContext context = new NancyContext();
            var request = new FakeRequest("GET", "/",
                                      new Dictionary<string, IEnumerable<string>>
                                      {
                                          {"User-Agent", new[] {"a fake user agent"}}
                                      });
            context.Request = request;

            return tokenizer.Tokenize(userIdentity,context);
        }

        private Tokenizer getTokenizer(TimeSpan time)
        {
            return new Tokenizer(configurator => configurator.TokenExpiration(() =>time ));
        }
       

    
    }
}