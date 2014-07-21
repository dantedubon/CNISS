using System;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Moq;
using NUnit.Framework;

namespace CNISS_Tests.Parentesco_Test.Command
{
    [TestFixture]
    public class CommandUpdateParentesco_Test
    {
        [Test]
        public void isExecutable_NotExistsTipoEmpleo_returnFalse()
        {
            var parentesco = new Parentesco("Abuela");

            var repositoryRead = Mock.Of<IParentescoReadOnlyRepository>();
            var repositoryCommand = Mock.Of<IParentescoRepositoryCommand>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());
            Mock.Get(repositoryRead).Setup(x => x.exists(parentesco.Id)).Returns(false);

            var command = new CommandUpdateParentesco(repositoryRead, repositoryCommand, uow);


            var respuesta = command.isExecutable(parentesco);


            Assert.IsFalse(respuesta);
        }
    }
}