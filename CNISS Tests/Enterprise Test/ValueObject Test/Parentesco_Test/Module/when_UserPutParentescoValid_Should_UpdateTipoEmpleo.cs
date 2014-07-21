using System;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.ParentescoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Modules.TipoEmpleoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Machine.Specifications;
using Moq;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Parentesco_Test.Module
{
    [Subject(typeof(ParentescoModuleUpdate))]
    public class when_UserPutParentescoValid_Should_UpdateTipoEmpleo
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static ParentescoRequest parentescoRequest;
        private static Parentesco _expectedParentesco;
        private static ICommandUpdateIdentity<Parentesco> _command;

        private Establish context = () =>
        {
            parentescoRequest = new ParentescoRequest()
            {

                guid = Guid.NewGuid(),
                descripcion = "tipo",
                auditoriaRequest = new AuditoriaRequest()
                {
                    usuarioCreo = "usuarioCreo",
                    fechaCreo = new DateTime(2014, 8, 1),
                    fechaModifico = new DateTime(2014, 8, 1),
                    usuarioModifico = "usuarioModifico"
                }
            };

            _command = Mock.Of<ICommandUpdateIdentity<Parentesco>>();

            _expectedParentesco = getParentesco(parentescoRequest);

            Mock.Get(_command).Setup(x => x.isExecutable(Moq.It.Is<Parentesco>(z => z.Id==_expectedParentesco.Id))).Returns(true);

            _browser = new Browser(
                x =>
                {
                    x.Module<ParentescoModuleUpdate>();
                    x.Dependencies(_command);
                }

                );

        };

        private Because of = () =>
        {
            _response = _browser.PutSecureJson("/enterprise/beneficiarios/parentescos", parentescoRequest);
        };

        It should_update_tipoEmpleo =
            () =>
                Mock.Get(_command)
                    .Verify(
                        x =>
                            x.execute(
                                Moq.It.Is<Parentesco>(
                                    z =>
                                        z.Id == _expectedParentesco.Id &&
                                        z.descripcion == _expectedParentesco.descripcion)));

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
            parentesco.Id = parentescoRequest.guid;
            return parentesco;
        }
    }
}