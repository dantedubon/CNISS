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
    public class CommandUpdateGremioRepresentante_Test
    {
       [Test]
        public void isExecutable_invalidGremio_should_returnFalse()
        {
            var repositoryReadOnly = Mock.Of<IGremioRepositoryReadOnly>();
            Mock.Get(repositoryReadOnly).Setup(x => x.exists(Moq.It.IsAny<RTN>())).Returns(false);

            var representanteRepository = Mock.Of<IRepresentanteLegalRepositoryReadOnly>();
           
            var repository = Mock.Of<IGremioRepositoryCommands>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            var command = new CommandUpdateGremioRepresentante(repositoryReadOnly, repository, representanteRepository,
                uow);

            var gremio = new Gremio(getRTN("08011985123960"), getRepresentanteLegal("0801198512396"),
                getDireccion("01", "01", "01", "Barrio"), "Camara");

            var respuesta = command.isExecutable(gremio);

            Assert.IsFalse(respuesta);



        }

       [Test]
       public void isExecutable_validGremio_should_returnTrue()
       {
           var repositoryReadOnly = Mock.Of<IGremioRepositoryReadOnly>();
           Mock.Get(repositoryReadOnly).Setup(x => x.exists(Moq.It.IsAny<RTN>())).Returns(true);

           var representanteRepository = Mock.Of<IRepresentanteLegalRepositoryReadOnly>();
           var repository = Mock.Of<IGremioRepositoryCommands>();
           var uow = Mock.Of<Func<IUnitOfWork>>();
           Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

           var command = new CommandUpdateGremioRepresentante(repositoryReadOnly, repository, representanteRepository,
               uow);

           var gremio = new Gremio(getRTN("08011985123960"), getRepresentanteLegal("0801198512396"),
               getDireccion("01", "01", "01", "Barrio"), "Camara");

           var respuesta = command.isExecutable(gremio);

           Assert.IsTrue(respuesta);



       }

        [Test]
        public void execute_RepresentanteNuevo_UpdateGremio()
        {
            var gremioOriginal = new Gremio(getRTN("08011985123960"), getRepresentanteLegal("0801198512396"),
               getDireccion("01", "01", "01", "Barrio"), "Camara");

            var nuevoGremio = new Gremio(getRTN("08011985123960"), getRepresentanteLegal("0801198411111"),
               getDireccion("01", "01", "01", "Barrio"), "Camara");

            var repositoryReadOnly = Mock.Of<IGremioRepositoryReadOnly>();
            Mock.Get(repositoryReadOnly).Setup(x => x.get(It.IsAny<RTN>())).Returns(gremioOriginal);

            var representanteRepository = Mock.Of<IRepresentanteLegalRepositoryReadOnly>();
            Mock.Get(representanteRepository).Setup(x => x.exists(It.IsAny<Identidad>())).Returns(false);

            var repository = Mock.Of<IGremioRepositoryCommands>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            var command = new CommandUpdateGremioRepresentante(repositoryReadOnly, repository, representanteRepository,
              uow);

            
           

            command.execute(nuevoGremio);

          
            Mock.Get(repository).Verify(x => x.updateRepresentante(It.Is<Gremio>( z=> z.representanteLegal == nuevoGremio.representanteLegal)));


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