using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using CNISS_Tests.Enterprise_Test.Entities_Test.Beneficiario_Test.Modules;
using Moq;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empresas_Test.Command
{
    [TestFixture]
    public class CommandUpdateEmpresaContrato_Test
    {
        [Test]
        public void isExecutable_EmpresaNotExists_ReturnFalse()
        {
            var rtn = new RTN("08011985123960");
            var repositoryReadOnly = Mock.Of<IEmpresaRepositoryReadOnly>();
            var repositoryCommand = Mock.Of<IEmpresaRepositoryCommands>();
            var uof = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uof).Setup(x => x()).Returns(new DummyUnitOfWork());
            Mock.Get(repositoryReadOnly).Setup(x => x.exists(rtn)).Returns(false);

            var command = new CommandUpdateEmpresaContrato(repositoryReadOnly, repositoryCommand, uof);

            var response = command.isExecutable(rtn);

            Assert.IsFalse(response);
        }

        [Test]
        public void execute_EmpresaExists_UpdatedEmpresa()
        {
            var contentFile = new ContentFile(new byte[] {1, 1});
            var rtn = new RTN("08011985123960");
            var repositoryReadOnly = Mock.Of<IEmpresaRepositoryReadOnly>();
            var repositoryCommand = Mock.Of<IEmpresaRepositoryCommands>();
            var uof = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uof).Setup(x => x()).Returns(new DummyUnitOfWork());
           

            var command = new CommandUpdateEmpresaContrato(repositoryReadOnly, repositoryCommand, uof);

             command.execute(rtn, contentFile);

            Mock.Get(repositoryCommand).Verify(x => x.updateContrato(rtn,contentFile));
        }
    }
}