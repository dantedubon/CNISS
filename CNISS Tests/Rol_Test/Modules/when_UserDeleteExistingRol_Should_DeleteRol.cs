using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.RolModule.RolCommand;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using FizzWare.NBuilder;
using Machine.Specifications;
using Moq;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Rol_Test.Modules
{
    [Subject(typeof (RolModuleCommandDelete))]
    public class when_UserDeleteExistingRol_Should_DeleteRol
    {
        static Browser _browser;
        static BrowserResponse _response;
        static ICommandDeleteIdentity<Rol> _commandDelete;
        static IRolRepositoryReadOnly _repositoryRead;
        static RolRequest _rolRequest;

        Establish context = () =>
        {
            _rolRequest = Builder<RolRequest>.CreateNew().Build();
            _commandDelete = Mock.Of<ICommandDeleteIdentity<Rol>>();
            _repositoryRead = Mock.Of<IRolRepositoryReadOnly>();
            Mock.Get(_repositoryRead).Setup(x => x.exists(Moq.It.IsAny<Guid>())).Returns(true);

            _browser = new Browser(
                x =>
                {
                    x.Module<RolModuleCommandDelete>();
                    x.Dependencies(_repositoryRead, _commandDelete);

                }
                );
        };

        Because of = () => _browser.DeleteSecureJson("/rol", _rolRequest);

        It should_delete_rol = () => Mock.Get(_commandDelete)
            .Verify(x => x.execute(Moq.It.Is<Rol>(z => z.idKey == _rolRequest.idGuid)));


    }
}