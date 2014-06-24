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
    public class CommandInsertGremio_Test
    {
        [Test]
        public void execute_BadDireccion_throwExcepcion()
        {
            var rtn = getRTN("08011985123960");
            var representante = getRepresentanteLegal("0801198512396");
            var badDireccion = getDireccion("01", "01", "02", "B Abajo");
            var servicioValidadorDireccion = Mock.Of<IServiceDireccionValidator>();
            Mock.Get(servicioValidadorDireccion)
                .Setup(x => x.isValidDireccion(badDireccion))
                .Returns(false);

            var repositorioRead = Mock.Of<IGremioRepositoryReadOnly>();
            var repositorio = Mock.Of<IGremioRepositoryCommands>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            var gremio = new Gremio(rtn, representante, badDireccion, "Camara");

            var comando = new CommandInsertGremio(servicioValidadorDireccion, repositorioRead, repositorio, uow);

            Assert.Throws<ArgumentException>(
                ()=> comando.execute(gremio),"Direccion mala"
                )
            ;



        }

        [Test]
        public void execute_DataValid_saveGremio()
        {
            var rtn = getRTN("08011985123960");
            var representante = getRepresentanteLegal("0801198512396");
            var direccion = getDireccion("01", "01", "02", "B Abajo");
            var servicioValidadorDireccion = Mock.Of<IServiceDireccionValidator>();
            Mock.Get(servicioValidadorDireccion)
                .Setup(x => x.isValidDireccion(direccion))
                .Returns(true);


            var repositorio = Mock.Of<IGremioRepositoryCommands>();
            var repositorioRead = Mock.Of<IGremioRepositoryReadOnly>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            var gremio = new Gremio(rtn, representante, direccion, "Camara");

            var comando = new CommandInsertGremio(servicioValidadorDireccion,repositorioRead, repositorio, uow);

            comando.execute(gremio);

            Mock.Get(repositorio).Verify(x => x.save(gremio));
        }

        [Test]
        public void isExecutable_GremioAlreadyExist_returnsFalse()
        {
            var rtn = getRTN("08011985123960");
            var representante = getRepresentanteLegal("0801198512396");
            var direccion = getDireccion("01", "01", "02", "B Abajo");
            var servicioValidadorDireccion = Mock.Of<IServiceDireccionValidator>();
            Mock.Get(servicioValidadorDireccion)
                .Setup(x => x.isValidDireccion(direccion))
                .Returns(true);


            var repositorio = Mock.Of<IGremioRepositoryCommands>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            var repositorioRead = Mock.Of<IGremioRepositoryReadOnly>();
            Mock.Get(repositorioRead).Setup(x => x.exists(It.IsAny<RTN>())).Returns(true);

            var gremio = new Gremio(rtn, representante, direccion, "Camara");

            var comando = new CommandInsertGremio(servicioValidadorDireccion,repositorioRead, repositorio, uow);

            Assert.IsFalse(comando.isExecutable(gremio));

            
        }

        [Test]
        public void isExecutable_GremioNotExist_returnsTrue()
        {
            var rtn = getRTN("08011985123960");
            var representante = getRepresentanteLegal("0801198512396");
            var direccion = getDireccion("01", "01", "02", "B Abajo");
            var servicioValidadorDireccion = Mock.Of<IServiceDireccionValidator>();
            Mock.Get(servicioValidadorDireccion)
                .Setup(x => x.isValidDireccion(direccion))
                .Returns(true);


            var repositorio = Mock.Of<IGremioRepositoryCommands>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            var repositorioRead = Mock.Of<IGremioRepositoryReadOnly>();
            Mock.Get(repositorioRead).Setup(x => x.exists(It.IsAny<RTN>())).Returns(false);

            var gremio = new Gremio(rtn, representante, direccion, "Camara");

            var comando = new CommandInsertGremio(servicioValidadorDireccion, repositorioRead, repositorio, uow);

            Assert.IsTrue(comando.isExecutable(gremio));


        }

        private RTN getRTN(string rtn)
        {
            return new RTN(rtn);
        }

        private  RepresentanteLegal getRepresentanteLegal(string id)
        {
            var identidad = new Identidad(id);
            return new RepresentanteLegal(identidad, "representante");
        }
        private  Direccion getDireccion(string idDepartamentoMunicipio, string idMunicipio, string idDepartamento, string descripcion)
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