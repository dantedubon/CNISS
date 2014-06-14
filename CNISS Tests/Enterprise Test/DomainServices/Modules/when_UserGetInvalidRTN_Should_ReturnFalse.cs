using CNISS.CommonDomain.Ports.Input.REST.Modules.EnterpriseServiceModule;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Service;
using Machine.Specifications;
using Nancy.Testing;
using Should;

namespace CNISS_Tests.DomainServices.Modules
{
    [Subject(typeof(EnterpriseServiceModule))]
    public class when_UserGetInvalidRTN_Should_ReturnFalse
    {
        static Browser _browser;
        static bool _response;
        static IServiceValidatorRTN _service;
        static string validRTN = "08011985123961";
        Establish context = () =>
        {

            _service = new ServiceValidatorRTN(new ContribuyenteDomainService());
            _browser = new Browser(x =>
            {
                x.Module<EnterpriseServiceModule>();
                x.Dependencies(_service);
            });



        };

        Because of = () => { _response = _browser.GetSecureJson("/enterprise/isValidRTN=" + validRTN).Body.DeserializeJson<bool>(); };

        It should_return_false = () => _response.ShouldBeFalse();
    }
}