using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.TipoEmpleoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.TipoEmpleo_Test.Module
{
    [Subject(typeof(TipoEmpleoModuleInsert))]
    public class when_UserPostNewTipoEmpleoValidData_Should_SaveTipoEmpleo
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static TipoEmpleoRequest _tipoEmpleoRequest;
        private static TipoEmpleo _tipoEmpleoExpected;
        private static ICommandInsertIdentity<TipoEmpleo> _command; 


        private Establish context = () =>
        {
            _tipoEmpleoRequest = new TipoEmpleoRequest()
            {
                descripcion = "tipoEmpleo",
                auditoriaRequest = new AuditoriaRequest()
                {
                    usuarioCreo = "usuarioCreo",
                    fechaCreo = new DateTime(2014, 8, 1),
                    fechaModifico = new DateTime(2014, 8, 1),
                    usuarioModifico = "usuarioModifico"
                }
            };

             _command = Mock.Of<ICommandInsertIdentity<TipoEmpleo>>();
            _tipoEmpleoExpected = getTipoEmpleo(_tipoEmpleoRequest);


            _browser = new Browser(
                x =>
                {
                    x.Module<TipoEmpleoModuleInsert>();
                    x.Dependencies(_command);
                }

                );
        };

        private Because of = () => { _response = _browser.PostSecureJson("/enterprise/tipoEmpleo", _tipoEmpleoRequest); };



         It should_save_tipoEmpleo = () => Mock.Get(_command)
            .Verify(x => x.execute(Moq.It.Is<TipoEmpleo>(z => z.descripcion == _tipoEmpleoExpected.descripcion)));

        private static TipoEmpleo getTipoEmpleo(TipoEmpleoRequest tipoEmpleoRequest)
        {
            var tipoEmpleo = new TipoEmpleo(tipoEmpleoRequest.descripcion)
            {
                auditoria = new CNISS.CommonDomain.Domain.Auditoria(
                    tipoEmpleoRequest.auditoriaRequest.usuarioCreo,
                    tipoEmpleoRequest.auditoriaRequest.fechaCreo,
                    tipoEmpleoRequest.auditoriaRequest.usuarioModifico,
                    tipoEmpleoRequest.auditoriaRequest.fechaModifico

                    )
            };
            return tipoEmpleo;
        }
    }
}