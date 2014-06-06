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

namespace CNISS_Tests.Rol_Test.Modules
{
    [Subject(subjectType: typeof(RolModuleQuery))]
    public class when_a_user_list_all_rol
    {
        static Browser _browser;
        static IEnumerable<Rol> _rolCollection;
        static IRolRepositoryReadOnly _repositoryReadOnly;
        static BrowserResponse _response;
        static IEnumerable<Rol> _rolResponse;
        

        Establish context = () =>
        {
            _rolCollection = Builder<Rol>.CreateListOfSize(10).Build();
            var rolRepositoryReadOnlyMock = new Mock<IRolRepositoryReadOnly>();
            rolRepositoryReadOnlyMock.Setup(x => x.getAll()).Returns(_rolCollection);

            _repositoryReadOnly = rolRepositoryReadOnlyMock.Object;



            _browser = new Browser(
                x =>
                {
                    x.Module<RolModuleQuery>();
                    x.Dependency<IRolRepositoryReadOnly>(_repositoryReadOnly);

                }
            );



        };

        Because of = () =>
        {
            _rolResponse = _browser.GetSecureJson("/rol").Body.DeserializeJson<IEnumerable<Rol>>();
        };

         It should_list_all_the_rol =
            () => _rolResponse.ShouldBeEquivalentTo(_rolCollection);

    }
}