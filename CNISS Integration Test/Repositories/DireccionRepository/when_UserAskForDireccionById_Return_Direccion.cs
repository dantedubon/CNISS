using System.Collections.Generic;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using CNISS.EnterpriseDomain.Ports.Output.Database;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using NHibernate;

namespace CNISS_Integration_Test.Repositories.DireccionRepository
{
    [Subject(typeof (DireccionRepositoryReadOnly))]
    public class when_UserAskForDireccionById_Return_Direccion
    {
        static InMemoryDatabaseTest _databaseTest;
        static ISession _session;
        static DireccionRepositoryReadOnly _repository;
        static Direccion _response;
        static Direccion _expecteDireccion;

         Establish context = () =>
         {
             _databaseTest = new InMemoryDatabaseTest(typeof(Departamento).Assembly);
             _databaseTest.openSession();

             _session = _databaseTest.session;

             _repository = new DireccionRepositoryReadOnly(_session);
             var idMunicipio = "municipio1";
             var idDepartamento = "departamento1";
             var municipio = Builder<Municipio>.CreateNew()
                 .With(x => x.Id = idMunicipio)
                 .With(x => x.DepartamentoId = idDepartamento)
                 .Build();
             var departamento = Builder<Departamento>.CreateNew()
                 .With(x => x.Id = idDepartamento)
                 .With(x => x.Municipios = new List<Municipio>
                 {
                     municipio
                 })
                 .Build();

             _expecteDireccion = new Direccion(departamento, municipio, "Barrio Abajo");


             using (var tx = _session.BeginTransaction())
             {
                 _session.Save(departamento);
                 _session.Save(municipio);
                 _session.Save(_expecteDireccion);
                 tx.Commit();
             }
             _session.Clear();
         };

         Because of = () => { _response = _repository.get(_expecteDireccion.Id); };

        It should_return_direccion = () => _response.ShouldBeEquivalentTo(_expecteDireccion);
    }
}
