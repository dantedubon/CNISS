using System.Collections.Generic;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Ports.Input.REST.Modules.RolModule.RolQuery;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Rol_Test
{
    [Subject(subjectType: typeof (RolModuleQuery))]
    public class when_UserGetSpecificRol_Should_ReturnRol
    {
        static Browser _browser;
        static Rol _rolExpected;
        static IRolRepositoryReadOnly _repositoryReadOnly;
        static BrowserResponse _response;
        static Rol _rolResponse;

        private Establish context = () =>
        {
            _rolExpected = Builder<Rol>.CreateNew().Build();

            var rolRepositoryReadOnlyMock = new Mock<IRolRepositoryReadOnly>();

            rolRepositoryReadOnlyMock.Setup(x => x.get(_rolExpected.idKey)).Returns(_rolExpected);

            _repositoryReadOnly = rolRepositoryReadOnlyMock.Object;

            _browser = new Browser(
                x =>
                {
                    x.Module<RolModuleQuery>();
                    x.Dependency<IRolRepositoryReadOnly>(_repositoryReadOnly);

                }
            );
        };

        private Because of = () =>
        {
            _rolResponse = _browser.GetSecureJson("/rol/id=" + _rolExpected.idKey).Body.DeserializeJson<Rol>();

        };

        It should_return_rol= () => _rolResponse.ShouldBeEquivalentTo(_rolExpected);
    }
}