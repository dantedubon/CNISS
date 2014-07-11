using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using Moq;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Visita_Test.Commands
{
    [TestFixture]
    public class CommandUpdateVisita_Test
    {
        [Test]
        public void isExecutable_VisitaNotExists_ReturnFalse()
        {
            var visita = new Visita("Visita Prueba", new DateTime(2014, 8, 1), new DateTime(2014, 8, 30));

            var repositoryRead = Mock.Of<IVisitaRepositoryReadOnly>();
            Mock.Get(repositoryRead).Setup(x => x.exists(visita.Id)).Returns(false);
            var repositoryCommand = Mock.Of<IVisitaRepositoryCommand>();

            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());


            var command = new CommandUpdateVisita(repositoryRead, repositoryCommand, uow);

            var response = command.isExecutable(visita);

            Assert.IsFalse(response);

        }

        [Test]
        public void isExecutable_fechaInicialMenorFechaFinal_ReturnFalse()
        {
            var visita = new Visita("Visita Prueba", new DateTime(2014, 8, 30), new DateTime(2014, 8, 1));

            var repositoryRead = Mock.Of<IVisitaRepositoryReadOnly>();
            Mock.Get(repositoryRead).Setup(x => x.exists(visita.Id)).Returns(false);
            var repositoryCommand = Mock.Of<IVisitaRepositoryCommand>();

            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());


            var command = new CommandUpdateVisita(repositoryRead, repositoryCommand, uow);

            var response = command.isExecutable(visita);

            Assert.IsFalse(response);

        }

        [Test]
        public void isExecutable_datosCorrectos_ReturnTrue()
        {
            var visita = new Visita("Visita Prueba", new DateTime(2014, 8, 1), new DateTime(2014, 8, 30));

            var repositoryRead = Mock.Of<IVisitaRepositoryReadOnly>();
            Mock.Get(repositoryRead).Setup(x => x.exists(visita.Id)).Returns(true);
            var repositoryCommand = Mock.Of<IVisitaRepositoryCommand>();

            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());


            var command = new CommandUpdateVisita(repositoryRead, repositoryCommand, uow);

            var response = command.isExecutable(visita);

            Assert.IsTrue(response);

        }
    }
}