using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.AutenticationDomain.Domain.ValueObjects;
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
    public class CommandInsertNotaDespido_Test
    {
        [Test]
        public void isExecutable_empleoNotExists_returnFalse()
        {
            var idEmpleo = Guid.NewGuid();
            var nota = getNotaDespido();
            var repositoryEmpleoRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var repositoryEmpleoCommand = Mock.Of<IEmpleoRepositoryCommands>();

            var authenticateUser = Mock.Of<IAuthenticateUser>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());
            Mock.Get(repositoryEmpleoRead).Setup(x => x.existsEmpleoForNotaDespido(idEmpleo,nota.fechaDespido)).Returns(false);

            var comando = new CommandInsertNotaDespido(repositoryEmpleoRead,repositoryEmpleoCommand, authenticateUser, uow);

            var respuesta = comando.isExecutable(idEmpleo, nota);

            Assert.IsFalse(respuesta);



        }

        [Test]
        public void isExecutable_invalidFirma_returnFalse()
        {
            var idEmpleo = Guid.NewGuid();
            var nota = getNotaDespido();
            var repositoryEmpleoRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var repositoryEmpleoCommand = Mock.Of<IEmpleoRepositoryCommands>();
            const int nivelFirma = 1;

            var authenticateUser = Mock.Of<IAuthenticateUser>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());
            Mock.Get(authenticateUser).Setup(x => x.isValidUser(nota.firmaAutorizada.user, nivelFirma)).Returns(false);
            Mock.Get(repositoryEmpleoRead).Setup(x => x.existsEmpleoForNotaDespido(idEmpleo, nota.fechaDespido)).Returns(true);

            var comando = new CommandInsertNotaDespido(repositoryEmpleoRead, repositoryEmpleoCommand, authenticateUser, uow);

            var respuesta = comando.isExecutable(idEmpleo, nota);

            Assert.IsFalse(respuesta);



        }

        [Test]
        public void execute_methodCalled_empleoUpdate()
        {
            var idEmpleo = Guid.NewGuid();
            var nota = getNotaDespido();
            var repositoryEmpleoRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var repositoryEmpleoCommand = Mock.Of<IEmpleoRepositoryCommands>();
        

            var authenticateUser = Mock.Of<IAuthenticateUser>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());
         
            var comando = new CommandInsertNotaDespido(repositoryEmpleoRead, repositoryEmpleoCommand, authenticateUser, uow);

           comando.execute(idEmpleo, nota);

            Mock.Get(repositoryEmpleoCommand).Verify(x => x.updateFromMovilNotaDespido(idEmpleo,nota));
        }

        private NotaDespido getNotaDespido()
        {
            return new NotaDespido(getMotivoDespido(),new DateTime(2014,1,1),"posicionGPS",getSupervisor(),getFirmaAutorizada());
        }

        private MotivoDespido getMotivoDespido()
        {
            return new MotivoDespido("Llegada Tarde");
        }

        private Supervisor getSupervisor()
        {
            return new Supervisor(new User("User", "User", "", "XXX", "mail", new RolNull()));
        }

        private FirmaAutorizada getFirmaAutorizada()
        {
            return new FirmaAutorizada(new User("User", "User", "", "XXX", "mail", new RolNull()), DateTime.Now.Date);
        }
    }
}