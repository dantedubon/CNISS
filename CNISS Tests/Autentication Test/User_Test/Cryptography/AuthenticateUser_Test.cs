using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using Moq;
using Nancy.Cryptography;
using NUnit.Framework;

namespace CNISS_Tests.Autentication_Test.User_Test.Cryptography
{
    [TestFixture]
    public class AuthenticateUser_Test
    {
        [Test]
        public void isValidUser_UserExists_returnTrue()
        {
            var userExisting = new User("User","Dante","Castillo","Password","mail",new Rol("Rol Prueba","Rol Prueba"){nivel = 1});
            var nivel = 1;
            var repositoryRead = Mock.Of<IUserRepositoryReadOnly>();
            var cryptoService = getCryptoService();
            userExisting.password = cryptoService.getEncryptedText(userExisting.password);
            userExisting.userKey = cryptoService.getKey();

            var repository = Mock.Of<IUserRepositoryReadOnly>();
            Mock.Get(repository).Setup(x => x.get(userExisting.Id)).Returns(userExisting);

            var userToValidate = new User("User", "Dante", "Castillo", "Password", "mail", new Rol("Rol Prueba", "Rol Prueba"));

            var authenticateUser = new AuthenticateUser(repository, (x) => new UserKeyRecovery(x),
                generator => new DefaultHmacProvider(generator), (generator, func) => new CryptoService(generator,func));

            var respuesta = authenticateUser.isValidUser(userToValidate,nivel);

            Assert.IsTrue(respuesta);



        }
        [Test]
        public void isValidUser_UserNivelError_returnFalse()
        {
            var userExisting = new User("User", "Dante", "Castillo", "Password", "mail", new Rol("Rol Prueba", "Rol Prueba") { nivel = 1 });
            var nivel = 2;
            var repositoryRead = Mock.Of<IUserRepositoryReadOnly>();
            var cryptoService = getCryptoService();
            userExisting.password = cryptoService.getEncryptedText(userExisting.password);
            userExisting.userKey = cryptoService.getKey();

            var repository = Mock.Of<IUserRepositoryReadOnly>();
            Mock.Get(repository).Setup(x => x.get(userExisting.Id)).Returns(userExisting);

            var userToValidate = new User("User", "Dante", "Castillo", "Password", "mail", new Rol("Rol Prueba", "Rol Prueba"));

            var authenticateUser = new AuthenticateUser(repository, (x) => new UserKeyRecovery(x),
                generator => new DefaultHmacProvider(generator), (generator, func) => new CryptoService(generator, func));

            var respuesta = authenticateUser.isValidUser(userToValidate, nivel);

            Assert.IsFalse(respuesta);



        }


        [Test]
        public void isValidUser_UserNotExists_returnFalse()
        {
            var userExisting = new User("User", "Dante", "Castillo", "Password", "mail", new Rol("Rol Prueba", "Rol Prueba"){nivel = 1});
            int nivel = 1;
            var repositoryRead = Mock.Of<IUserRepositoryReadOnly>();
            var cryptoService = getCryptoService();
            userExisting.password = cryptoService.getEncryptedText(userExisting.password);
            userExisting.userKey = cryptoService.getKey();

            var repository = Mock.Of<IUserRepositoryReadOnly>();
       

            var userToValidate = new User("User", "Dante", "Castillo", "Password", "mail", new Rol("Rol Prueba", "Rol Prueba"));

            var authenticateUser = new AuthenticateUser(repository, (x) => new UserKeyRecovery(x),
                generator => new DefaultHmacProvider(generator), (generator, func) => new CryptoService(generator, func));

            var respuesta = authenticateUser.isValidUser(userToValidate,nivel);

            Assert.IsFalse(respuesta);



        }

        [Test]
        public void isValidUser_UserNotValidPassword_returnFalse()
        {
            var userExisting = new User("User", "Dante", "Castillo", "Password", "mail", new Rol("Rol Prueba", "Rol Prueba"){nivel = 1});
            var nivel = 1;
            var repositoryRead = Mock.Of<IUserRepositoryReadOnly>();
            var cryptoService = getCryptoService();
            userExisting.password = cryptoService.getEncryptedText(userExisting.password);
            userExisting.userKey = cryptoService.getKey();

            var repository = Mock.Of<IUserRepositoryReadOnly>();
            Mock.Get(repository).Setup(x => x.get(userExisting.Id)).Returns(userExisting);

            var userToValidate = new User("User", "Dante", "Castillo", "PasswordDiferente", "mail", new Rol("Rol Prueba", "Rol Prueba"));

            var authenticateUser = new AuthenticateUser(repository, (x) => new UserKeyRecovery(x),
                generator => new DefaultHmacProvider(generator), (generator, func) => new CryptoService(generator, func));

            var respuesta = authenticateUser.isValidUser(userToValidate, nivel);

            Assert.IsFalse(respuesta);



        }

   

        private ICryptoService getCryptoService()
        {
            var size = 128;
            var _keyGenerator = new UserKeyGenerator(new RandomKeyGenerator(), size);
            var encryptService = new CryptoService(_keyGenerator, (x) => new DefaultHmacProvider(x));

            return encryptService;
        }


      
       
    }
}