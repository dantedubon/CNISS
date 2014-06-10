using System;
using System.Net;
using CNISS.AutenticationDomain.Application.Comandos;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.AutenticationDomain.Ports.Output.Database;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.UserModule.UserCommands;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS_Integration_Test.Unit_Of_Work;
using CNISS_Tests;
using FizzWare.NBuilder;
using Machine.Specifications;
using Nancy.Cryptography;
using Nancy.Testing;
using NHibernate;

namespace CNISS_Integration_Test.Modules.Users
{
    [Subject(typeof (UserModuleCommandInsert))]
    public class when_UserPostNewUser_Should_SaveNewUser
    {
         static InFileDataBaseTest _databaseTest;
         static ISessionFactory _sessionFactory;
         static IUserRepositoryReadOnly _repositoryRead;
         static IUserRepositoryCommands _repositoryCommands;
         
         static ICommandInsertIdentity<User> _commandInsert;
         static Browser _browser;
         static BrowserResponse _response;
         static UserRequest _userRequest;
         static RolRequest _userRol;

         Establish context = () =>
         {
             configureDataBase();

             _userRol = Builder<RolRequest>.CreateNew().Build();
             _userRol.idGuid = Guid.NewGuid();


             Rol _rol;
             prepareRol(out _rol);


             _userRequest = Builder<UserRequest>.CreateNew().Build();
              _userRequest.userRol = _userRol;

             
           
              var _session2 = _sessionFactory.OpenSession();
             _repositoryRead = new UserRepositoryReadOnly(_session2);
             _repositoryCommands = new UserRepositoryCommands(_session2);


         
             var _keyGenerator = new UserKeyGenerator(new RandomKeyGenerator());


             var encryptService = new CryptoService(_keyGenerator, (x) => new DefaultHmacProvider(x));


             _commandInsert = new CommandInsertUser(encryptService,_repositoryCommands, 
                 () => new NHibernateUnitOfWork(_session2));
             _browser = new Browser(
                 x =>
                 {
                     x.Module<UserModuleCommandInsert>();
                     x.Dependencies(_repositoryRead, _commandInsert);
                 }
                     
                      
             );



         };

        private static void prepareRol(out Rol _rol)
        {
            _rol = new Rol(_userRol.name, _userRol.description);
            _rol.Id = _userRol.idGuid;

            Utils.insertEntity(_userRol.idGuid, _rol, _sessionFactory);
        }

        private static void configureDataBase()
        {
            _databaseTest = new InFileDataBaseTest();
            _sessionFactory = _databaseTest.sessionFactory;
        }


        Because of = () => _response = _browser.PostSecureJson("/user", _userRequest);

        It should_saveNewUser = () => {_response.StatusCode.Equals(HttpStatusCode.OK);};
    }
}
