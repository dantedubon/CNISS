using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.RolModule.RolCommand;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Rol_Test.Modules
{
    [Subject(typeof (RolModuleCommandInsert))]
    public class when_UserPostIncompleteRol_Should_ReturnBadRequest
    {
        static Browser _browser;
        static BrowserResponse _response;
        static ICommandInsertIdentity<Rol> _commandInsert;
        static RolRequest _badRolRequest;


        Establish context = () =>
        {

            _badRolRequest = new RolRequest();

            _commandInsert = Mock.Of<ICommandInsertIdentity<Rol>>();

            _browser = new Browser(
                x =>
                {
                    x.Module<RolModuleCommandInsert>();
                    x.Dependencies(_commandInsert);

                }
                );

        };

         Because of = () => { _response = _browser.PostSecureJson("/rol", _badRolRequest); };

        It should_ReturnBadRequest = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
    }
}