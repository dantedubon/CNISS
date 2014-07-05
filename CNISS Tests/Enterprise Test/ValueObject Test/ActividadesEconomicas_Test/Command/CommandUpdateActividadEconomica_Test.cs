using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using CNISS.EnterpriseDomain.Ports.Output.Database;
using Moq;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.ActividadesEconomicas_Test.Command
{
    [TestFixture]
    public class CommandUpdateActividadEconomica_Test
    {
        [Test]
        public void isExecutable_ActividadEconimicaNotExists_ReturnFalse()
        {
            var actividad = new ActividadEconomica("Actividad");

            var repositoryRead = Mock.Of<IActividadEconomicaRepositoryReadOnly>();
            var repositoryCommand = Mock.Of<IActividadEconomicaRepositoryCommands>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());
            Mock.Get(repositoryRead).Setup(x => x.exists(actividad.Id));

            var command = new CommandUpdateActividadEconomica(repositoryRead, repositoryCommand, uow);


            var respuesta = command.isExecutable(actividad);


            Assert.IsFalse(respuesta);

        }

    }
}