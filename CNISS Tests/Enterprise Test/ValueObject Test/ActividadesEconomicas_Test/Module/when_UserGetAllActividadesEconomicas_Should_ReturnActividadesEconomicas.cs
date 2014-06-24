using System.Collections.Generic;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpresaModule;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.ActividadesEconomicas_Test.Module
{
    [Subject(typeof (ActividadesEconomicasModule))]
    public class when_UserGetAllActividadesEconomicas_Should_ReturnActividadesEconomicas
    {
        private static Browser _browser;
        private static IEnumerable<ActividadEconomica> _expected;
        private static IEnumerable<ActividadEconomica> _result;
        private Establish context = () =>
        {
            _expected = Builder<ActividadEconomica>.CreateListOfSize(10).Build();
            var repository = Mock.Of<IActividadEconomicaRepositoryReadOnly>();
            Mock.Get(repository).Setup(x => x.getAll()).Returns(_expected);

            _browser = new Browser(
                x =>
                {
                    x.Module<ActividadesEconomicasModule>();
                    x.Dependencies(repository);
                }
                );
        };

        private Because of = () =>
        {
            _result =
                _browser.GetSecureJson("/empresa/actividades").Body.DeserializeJson<IEnumerable<ActividadEconomica>>();
        };

        It should_return_actividades = () => _result.ShouldBeEquivalentTo(_expected);
    }
}