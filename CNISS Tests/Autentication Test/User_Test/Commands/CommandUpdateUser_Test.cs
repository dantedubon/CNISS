using System;
using CNISS.AutenticationDomain.Application.Comandos;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.CommonDomain.Domain;
using FizzWare.NBuilder;
using Moq;
using Nancy.Cryptography;
using NUnit.Framework;

namespace CNISS_Tests.User_Test.Commands
{
    public class CommandUpdateUser_Test
    {
        [Test]
        public void execute_givenAPassword_GenerateEncryptedPasswordAndKey()
        {
            Func<IUnitOfWork> _uow;
            var _repository = userRepositoryCommands(out _uow);

            var _userTest = Builder<User>.CreateNew().Build();
            var _passwordUser = _userTest.Password;
            var size = 128;
            var _keyGenerator = new UserKeyGenerator(new RandomKeyGenerator(), size);


            var encryptService = new CryptoService(_keyGenerator, (x) => new DefaultHmacProvider(x));

            var passwordEncrypted = encryptService.getEncryptedText(_passwordUser);
            var userKey = _keyGenerator.getKey();

            var command = new CommandUpdateUser(encryptService, _repository, _uow);

            command.execute(_userTest);

            Mock.Get(_repository).Verify(x => x.update(It.Is<User>(z => z.Password == passwordEncrypted
                && z.UserKey == userKey)));

        }

        private IUserRepositoryCommands userRepositoryCommands(out Func<IUnitOfWork> _uow)
        {
            var _repository = Mock.Of<IUserRepositoryCommands>();
            _uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(_uow).Setup(x => x()).Returns(new DummyUnitOfWork());
            return _repository;
        }
    }
}