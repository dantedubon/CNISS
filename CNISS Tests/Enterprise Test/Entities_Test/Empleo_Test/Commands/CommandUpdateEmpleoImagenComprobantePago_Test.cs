using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Moq;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Commands
{
    [TestFixture]
    public class CommandUpdateEmpleoImagenComprobantePago_Test
    {
        [Test]
        public void isExecutable_empleoNotExists_returnFalse()
        {
            var empleoid = Guid.NewGuid();
            var comprobanteId = Guid.NewGuid();
            var repositoryRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var repositoryCommands = Mock.Of<IEmpleoRepositoryCommands>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            Mock.Get(repositoryRead).Setup(x => x.existsComprobante(empleoid,comprobanteId)).Returns(false);

            var command = new CommandUpdateEmpleoImagenComprobantePago(repositoryRead, repositoryCommands, uow);

            var response = command.isExecutable(empleoid,comprobanteId);

            Assert.IsFalse(response);

        }

        [Test]
        public void isExecutable_comprobanteNotExists_returnFalse()
        {
            var empleoid = Guid.NewGuid();
            var comprobanteId = Guid.NewGuid();
            var repositoryRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var repositoryCommands = Mock.Of<IEmpleoRepositoryCommands>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

          
            Mock.Get(repositoryRead).Setup(x => x.existsComprobante(empleoid, comprobanteId)).Returns(false);

            var command = new CommandUpdateEmpleoImagenComprobantePago(repositoryRead, repositoryCommands, uow);

            var response = command.isExecutable(empleoid, comprobanteId);

            Assert.IsFalse(response);

        }

        [Test]
        public void isExecutable_comprobanteExistsEmpleoExists_returnTrue()
        {
            var empleoid = Guid.NewGuid();
            var comprobanteId = Guid.NewGuid();
            var repositoryRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var repositoryCommands = Mock.Of<IEmpleoRepositoryCommands>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());


            Mock.Get(repositoryRead).Setup(x => x.existsComprobante(empleoid, comprobanteId)).Returns(true);

            var command = new CommandUpdateEmpleoImagenComprobantePago(repositoryRead, repositoryCommands, uow);

            var response = command.isExecutable(empleoid, comprobanteId);

            Assert.IsTrue(response);

        }

        [Test]
        public void execute_empleoAndComprobanteExists_updateEmpleoComprobante()
        {
            var data = new byte[] {0, 1};
            var contentFile = new ContentFile(data);

            var empleoid = Guid.NewGuid();
            var comprobanteId = Guid.NewGuid();

            var repositoryRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var repositoryCommands = Mock.Of<IEmpleoRepositoryCommands>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());


            Mock.Get(repositoryRead).Setup(x => x.existsComprobante(empleoid, comprobanteId)).Returns(true);

            var command = new CommandUpdateEmpleoImagenComprobantePago(repositoryRead, repositoryCommands, uow);

           command.execute(empleoid, comprobanteId,contentFile);

            Mock.Get(repositoryCommands).Verify(x => x.updateImagenComprobante(empleoid,comprobanteId,contentFile));
        }
    }
}