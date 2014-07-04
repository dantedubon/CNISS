using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Moq;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Commands
{
    [TestFixture]
    public class CommandUpdateEmpleoContrato_Test
    {
        [Test]
        public void isExecutable_empleoNotExists_returnFalse()
        {
            var empleoid = Guid.NewGuid();
            var repositoryRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var repositoryCommands = Mock.Of<IEmpleoRepositoryCommands>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            Mock.Get(repositoryRead).Setup(x => x.exists(empleoid)).Returns(false);

            var command = new CommandUpdateEmpleoContrato(repositoryRead,repositoryCommands, uow);

            var response = command.isExecutable(empleoid);

            Assert.IsFalse(response);

        }

        [Test]
        public void isExecutable_empleoExists_returnTrue()
        {
            var empleoid = Guid.NewGuid();
            var repositoryRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var repositoryCommands = Mock.Of<IEmpleoRepositoryCommands>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            Mock.Get(repositoryRead).Setup(x => x.exists(empleoid)).Returns(true);

            var command = new CommandUpdateEmpleoContrato(repositoryRead, repositoryCommands, uow);

            var response = command.isExecutable(empleoid);

            Assert.IsTrue(response);

        }

        [Test]
        public void execute_correctEmpleoIDWithContentFile_CallRepository()
        {
            var empleoid = Guid.NewGuid();
            var contentFile = new ContentFile(new byte[] {0, 1});
            var repositoryRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var repositoryCommands = Mock.Of<IEmpleoRepositoryCommands>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            Mock.Get(repositoryRead).Setup(x => x.exists(empleoid)).Returns(true);

            var command = new CommandUpdateEmpleoContrato(repositoryRead, repositoryCommands, uow);
            command.execute(empleoid,contentFile);

            Mock.Get(repositoryCommands).Verify(x => x.updateContratoEmpleo(empleoid,contentFile));

        }
    }
}