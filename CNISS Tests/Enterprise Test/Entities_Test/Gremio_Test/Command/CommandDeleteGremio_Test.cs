using System;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Gremio_Test.Command
{
    [TestFixture]
    public class CommandDeleteGremio_Test
    {
        [Test]
        public void isExecutable_invalidGremio_returnFalse()
        {
            var repositoryRead = Mock.Of<IGremioRepositoryReadOnly>();
            Mock.Get(repositoryRead).Setup(x => x.exists(It.IsAny<RTN>())).Returns(false);


            var repository = Mock.Of<IGremioRepositoryCommands>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            var command = new CommandDeleteGremio(repositoryRead, repository, uow);

            var rtnGremio = new RTN("08011985123960");

            var respuesta = command.isExecutable(rtnGremio);

            Assert.IsFalse(respuesta);

        }
        [Test]
        public void isExecutable_validGremio_returnTrue()
        {
            var repositoryRead = Mock.Of<IGremioRepositoryReadOnly>();
            Mock.Get(repositoryRead).Setup(x => x.exists(It.IsAny<RTN>())).Returns(true);


            var repository = Mock.Of<IGremioRepositoryCommands>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            var command = new CommandDeleteGremio(repositoryRead, repository, uow);

            var rtnGremio = new RTN("08011985123960");

            var respuesta = command.isExecutable(rtnGremio);

            Assert.IsTrue(respuesta);

        }

        [Test]
        public void execute_validGremio_deleteGremio()
        {
            var gremioDeleted = new Gremio(getRTN("08011985123960"), getRepresentanteLegal("0801198512396"),
               getDireccion("01", "01", "01", "Barrio"), "Camara");

            var repositoryRead = Mock.Of<IGremioRepositoryReadOnly>();
            Mock.Get(repositoryRead).Setup(x => x.exists(It.IsAny<RTN>())).Returns(true);
            Mock.Get(repositoryRead).Setup(x => x.get(It.IsAny<RTN>())).Returns(gremioDeleted);

            var repository = Mock.Of<IGremioRepositoryCommands>();
            
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            var command = new CommandDeleteGremio(repositoryRead, repository, uow);
            command.execute(gremioDeleted.Id);

            Mock.Get(repository).Verify(x => x.delete(gremioDeleted));




        }

        
        

        private RTN getRTN(string rtn)
        {
            return new RTN(rtn);
        }

        private RepresentanteLegal getRepresentanteLegal(string id)
        {
            var identidad = new Identidad(id);
            return new RepresentanteLegal(identidad, "representante");
        }
        private Direccion getDireccion(string idDepartamentoMunicipio, string idMunicipio, string idDepartamento, string descripcion)
        {
            var municipio = Builder<Municipio>.CreateNew().Build();
            municipio.departamentoId = idDepartamentoMunicipio;
            municipio.Id = idMunicipio;
            municipio.nombre = "municipio";
            var departamento = Builder<Departamento>.CreateNew().Build();
            departamento.Id = idDepartamento;
            departamento.nombre = "departamento";


            return new Direccion(departamento, municipio, descripcion);

        }
    }
}