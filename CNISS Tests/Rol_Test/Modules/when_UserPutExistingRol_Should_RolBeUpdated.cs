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
    [Subject(typeof (RolModuleCommandUpdate))]
    public class when_UserPutExistingRol_Should_RolBeUpdated
    {
        static Browser _browser;
        static BrowserResponse _response;
        static ICommandUpdateIdentity<Rol> _commandUpdate;
        static RolRequest _rolRequest;

         Establish context = () =>
         {
             _rolRequest = Builder<RolRequest>.CreateNew().Build();
             _commandUpdate = Mock.Of<ICommandUpdateIdentity<Rol>>();

             _browser = new Browser(
                 x =>
                 {
                     x.Module<RolModuleCommandUpdate>();
                     x.Dependencies(_commandUpdate);

                 }
                 );
         };

        Because of = () => _browser.PutSecureJson("/rol", _rolRequest);

        It should_update_rol = () => Mock.Get(_commandUpdate).Verify( x=> x.execute(Moq.It.Is<Rol>( z=> z.idKey == _rolRequest.idGuid)));
    }
}