using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using Nancy.Cryptography;
using NHibernate.Hql.Ast.ANTLR;

namespace CNISS.AutenticationDomain.Domain.Services
{
    public class AuthenticateUser : IAuthenticateUser
    {

        private readonly IUserRepositoryReadOnly _userRepositoryRead;
        private readonly Func<byte[], IKeyGenerator> _keyGeneratorFactory;
        private readonly Func<IKeyGenerator, IHmacProvider> _factoryHmac;
        private readonly Func<IKeyGenerator, Func<IKeyGenerator, IHmacProvider>, ICryptoService> _crytoServiceFactory;


        public AuthenticateUser(IUserRepositoryReadOnly userRepositoryRead, Func<byte[],IKeyGenerator> keyGeneratorFactory, Func<IKeyGenerator,IHmacProvider> factoryHmac,
            Func<IKeyGenerator, Func<IKeyGenerator, IHmacProvider>, ICryptoService> crytoServiceFactory
            
            )
        {
            _userRepositoryRead = userRepositoryRead;
            _keyGeneratorFactory = keyGeneratorFactory;
            _factoryHmac = factoryHmac;
            _crytoServiceFactory = crytoServiceFactory;
        }

        public bool isValidUser(User user, int nivel)
        {

            var existingUser = _userRepositoryRead.get(user.Id);
            if (existingUser == null)
            {
                return false;
            }
            if (existingUser.UserRol.Nivel != nivel)
            {
                return false;
            }

            var key = Convert.FromBase64String(existingUser.UserKey);
            var keyGenerator = _keyGeneratorFactory(key);
           

          
            var cryto = _crytoServiceFactory(keyGenerator,_factoryHmac);

            user.Password = cryto.getEncryptedText(user.Password);           
           
            return user.Password.Equals(existingUser.Password);
        }

      

       
    }
}