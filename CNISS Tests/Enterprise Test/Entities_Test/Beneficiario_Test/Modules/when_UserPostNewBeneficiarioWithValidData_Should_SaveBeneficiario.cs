using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.BeneficiarioModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Beneficiario_Test.Modules
{
    [Subject(typeof(BeneficiarioModuleInsert))]
    public class when_UserPostNewBeneficiarioWithValidData_Should_SaveBeneficiario
    {
        static Browser _browser;
        static BrowserResponse _response;
        private static BeneficiarioRequest _beneficiarioRequest;
        private static ICommandInsertIdentity<Beneficiario> _command; 

        private Establish context = () =>
        {
            _beneficiarioRequest = new BeneficiarioRequest()
            {
                dependienteRequests = getDependienteRequest(),
                fechaNacimiento = DateTime.Now,
                identidadRequest = getIdentidadRequest(),
                nombreRequest = getNombreRequest()
            };

            _command = Mock.Of<ICommandInsertIdentity<Beneficiario>>();
            Mock.Get(_command).Setup(x => x.isExecutable(Moq.It.IsAny<Beneficiario>())).Returns(true);

            _browser = new Browser(
                x =>
                {
                    x.Module<BeneficiarioModuleInsert>();
                    x.Dependencies(_command);
                }

                );
        };

        private Because of = () =>
        {
            _response = _browser.PostSecureJson("/enterprise/beneficiarios", _beneficiarioRequest);
        };

        private It should_return_error = () => Mock.Get(_command)
            .Verify(
                x =>
                    x.execute(
                        Moq.It.Is<Beneficiario>(
                            z => z.Id.identidad == _beneficiarioRequest.identidadRequest.identidad)));

        private static IEnumerable<DependienteRequest> getDependienteRequest()
        {
            return new List<DependienteRequest>()
            {
                new DependienteRequest()
                {
                    identidadRequest = getIdentidadRequest(),
                    nombreRequest = getNombreRequest(),
                    parentescoRequest = getParentescoRequest()
                }
            };
        }

        private static NombreRequest getNombreRequest()
        {
            return new NombreRequest()
            {
                nombres = "Dante Ruben",
                primerApellido = "Castillo",
                segundoApellido = "Dubon"
            };
        }

        private static IdentidadRequest getIdentidadRequest()
        {
            return new IdentidadRequest()
            {
                identidad = "0801198512396"
            };
        }

        private static ParentescoRequest getParentescoRequest()
        {
            return new ParentescoRequest()
            {
                descripcion = "Esposo"
            };
        }

    }
}