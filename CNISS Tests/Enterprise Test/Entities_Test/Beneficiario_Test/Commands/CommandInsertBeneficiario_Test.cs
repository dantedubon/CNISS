using System;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Moq;
using NUnit.Framework;

namespace CNISS_Tests.Entities_Test.Beneficiario_Test.Commands
{
    [TestFixture]
    public class CommandInsertBeneficiario_Test
    {
        [Test]
        public void isExecutable_BeneficiarioAlreadyExists_ReturnFalse()
        {

            var beneficiario = getBeneficiario();
            var repositoryReadBeneficiario = Mock.Of<IBeneficiarioRepositoryReadOnly>();
            Mock.Get(repositoryReadBeneficiario).Setup(x => x.exists(It.IsAny<Identidad>())).Returns(true);

            var repositoryCommandBeneficiario = Mock.Of<IBeneficiarioRepositoryCommands>();
            var repositoryReadParentesco = Mock.Of<IParentescoReadOnlyRepository>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            var command = new CommandInsertBeneficiario(repositoryReadBeneficiario, repositoryReadParentesco,
                repositoryCommandBeneficiario,uow);

            var respuesta = command.isExecutable(beneficiario);

            Assert.IsFalse(respuesta);

        }

        [Test]
        public void isExecutable_ParentescoDependienteNotExists_ReturnFalse()
        {

            var beneficiario = getBeneficiario();
            var repositoryReadBeneficiario = Mock.Of<IBeneficiarioRepositoryReadOnly>();
            Mock.Get(repositoryReadBeneficiario).Setup(x => x.exists(It.IsAny<Identidad>())).Returns(false);

            var parentescoNoExistente = getParentescoPadre();
            var dependienteWithParentescoNoExistente = getDependiente(new Identidad("0501198512498"),
                new Nombre("", "", "", ""), parentescoNoExistente);
            beneficiario.addDependiente(dependienteWithParentescoNoExistente);

            var repositoryCommandBeneficiario = Mock.Of<IBeneficiarioRepositoryCommands>();
            var repositoryReadParentesco = Mock.Of<IParentescoReadOnlyRepository>();
            Mock.Get(repositoryReadParentesco).Setup(x => x.exists(parentescoNoExistente.Id)).Returns(false);

            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            var command = new CommandInsertBeneficiario(repositoryReadBeneficiario, repositoryReadParentesco,
                repositoryCommandBeneficiario, uow);

            var respuesta = command.isExecutable(beneficiario);

            Assert.IsFalse(respuesta);

        }

        [Test]
        public void isExecutable_ValidData_ReturnTrue()
        {

            var beneficiario = getBeneficiario();
            var repositoryReadBeneficiario = Mock.Of<IBeneficiarioRepositoryReadOnly>();
            Mock.Get(repositoryReadBeneficiario).Setup(x => x.exists(It.IsAny<Identidad>())).Returns(false);

            var parentescoNoExistente = getParentescoPadre();
            var dependienteWithParentescoNoExistente = getDependiente(new Identidad("0501198512498"),
                new Nombre("", "", "", ""), parentescoNoExistente);
            beneficiario.addDependiente(dependienteWithParentescoNoExistente);

            var repositoryCommandBeneficiario = Mock.Of<IBeneficiarioRepositoryCommands>();
            var repositoryReadParentesco = Mock.Of<IParentescoReadOnlyRepository>();
            Mock.Get(repositoryReadParentesco).Setup(x => x.exists(It.IsAny<Guid>())).Returns(true);

            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            var command = new CommandInsertBeneficiario(repositoryReadBeneficiario, repositoryReadParentesco,
                repositoryCommandBeneficiario, uow);

            var respuesta = command.isExecutable(beneficiario);

            Assert.IsTrue(respuesta);

        }
       
        private  Parentesco getParentescoHijo()
        {
            return new Parentesco("Hijo");
        }

        private  Parentesco getParentescoMadre()
        {
            return new Parentesco("Madre");
        }

        private Parentesco getParentescoPadre()
        {
            return new Parentesco("Padre");
        }



        private  Dependiente getDependiente(Identidad identidad, Nombre nombre, Parentesco parentesco)
        {
            return new Dependiente(identidad, nombre, parentesco, 30);
        }

        private  Beneficiario getBeneficiario()
        {
            var beneficiario = new Beneficiario(new Identidad("0801198512396"),
                new Nombre("Dante", "Dubon", "Castillo", "Rubén"), new DateTime(1984, 8, 2));


            var parentescoHijo = getParentescoHijo();
            var parentescoMadre = getParentescoMadre();

          
            beneficiario.addDependiente(getDependiente(new Identidad("0801196712396"), new Nombre("Lavinia", "", "Dubon", "Fajardo"), parentescoMadre));
            beneficiario.addDependiente(getDependiente(new Identidad("0801196712395"), new Nombre("Daniel", "", "Castillo", "Velasquez"), parentescoHijo));
            return beneficiario;
        }
    }
}