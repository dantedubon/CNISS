using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.ParentescoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Modules.TipoEmpleoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
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
    [Subject(typeof(ParentescoModuleInsert))]
    public class when_UserPostNewParentescoValidData_Should_SaveParentesco
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static ParentescoRequest _parentescoRequest;
        private static Parentesco _tipoEmpleoExpected;
        private static ICommandInsertIdentity<Parentesco> _command; 


        private Establish context = () =>
        {
            _parentescoRequest = new ParentescoRequest()
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

             _command = Mock.Of<ICommandInsertIdentity<Parentesco>>();
            _tipoEmpleoExpected = getParentesco(_parentescoRequest);


            _browser = new Browser(
                x =>
                {
                    x.Module<ParentescoModuleInsert>();
                    x.Dependencies(_command);
                }

                );
        };

        private Because of = () => { _response = _browser.PostSecureJson("/enterprise/beneficiarios/parentescos", _parentescoRequest); };



         It should_save_parentesco = () => Mock.Get(_command)
            .Verify(x => x.execute(Moq.It.Is<Parentesco>(z => z.descripcion == _tipoEmpleoExpected.descripcion)));

        private static Parentesco getParentesco(ParentescoRequest parentescoRequest)
        {
            var parentesco = new Parentesco(parentescoRequest.descripcion)
            {
                auditoria = new CNISS.CommonDomain.Domain.Auditoria(
                    parentescoRequest.auditoriaRequest.usuarioCreo,
                    parentescoRequest.auditoriaRequest.fechaCreo,
                    parentescoRequest.auditoriaRequest.usuarioModifico,
                    parentescoRequest.auditoriaRequest.fechaModifico

                    )
            };
            return parentesco;
        }
    }
}