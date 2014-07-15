using System;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Moq;
using NUnit.Framework;

namespace CNISS_Tests.MotivoDespido_Test.Command
{
    [TestFixture]
    public class CommandUpdateMotivoDespido_Test
    {
        [Test]
        public void isExecutable_NotExistsTipoEmpleo_returnFalse()
        {
            var motivoDespido = new MotivoDespido("Actividad");

            var repositoryRead = Mock.Of<IMotivoDespidoRepositoryReadOnly>();
            var repositoryCommand = Mock.Of<IMotivoDespidoRepositoryCommands>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());
            Mock.Get(repositoryRead).Setup(x => x.exists(motivoDespido.Id)).Returns(false);

            var command = new CommandUpdateMotivoDespido(repositoryRead, repositoryCommand, uow);


            var respuesta = command.isExecutable(motivoDespido);


            Assert.IsFalse(respuesta);
        }
    }
}