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


    [Subject(typeof (RolModuleCommandInsert))]
    public class when_UserPostNewRol_RolInserted
    {

         static Browser _browser;
         static BrowserResponse _response;
         static ICommandInsertIdentity<Rol> _commandInsert; 
         static RolRequest _rolRequest;
        

         Establish context = () =>
         {

             _rolRequest = Builder<RolRequest>.CreateNew().Build();
             _commandInsert = Mock.Of<ICommandInsertIdentity<Rol>>();

             _browser = new Browser(
                 x =>
                 {
                     x.Module<RolModuleCommandInsert>();
                     x.Dependencies(_commandInsert);
                    
                 }
                 );

         };

         Because of = () => _browser.PostSecureJson("/rol",_rolRequest);

        It should_insertNewRol = () => Mock.Get(_commandInsert).Verify( x => x.execute(Moq.It.Is<Rol>( z =>z.name == _rolRequest.name)));
    }
}