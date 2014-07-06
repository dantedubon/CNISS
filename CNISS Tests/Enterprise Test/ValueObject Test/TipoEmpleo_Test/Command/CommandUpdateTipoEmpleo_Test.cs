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

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.TipoEmpleo_Test.Command
{
    [TestFixture]
    public class CommandUpdateTipoEmpleo_Test
    {
        [Test]
        public void isExecutable_NotExistsTipoEmpleo_returnFalse()
        {
            var tipoEmpleo = new TipoEmpleo("Actividad");

            var repositoryRead = Mock.Of<ITipoDeEmpleoReadOnlyRepository>();
            var repositoryCommand = Mock.Of<ITipoDeEmpleoRepositoryCommand>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());
            Mock.Get(repositoryRead).Setup(x => x.exists(tipoEmpleo.Id)).Returns(false);

            var command = new CommandUpdateTipoEmpleo(repositoryRead, repositoryCommand, uow);


            var respuesta = command.isExecutable(tipoEmpleo);


            Assert.IsFalse(respuesta);
        }
    }
}