﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using CNISS.EnterpriseDomain.Ports.Output;
using CNISS.EnterpriseDomain.Ports.Output.Database;
using CNISS_Integration_Test.Unit_Of_Work;
using FluentAssertions;
using Machine.Specifications;
using NHibernate;

namespace CNISS_Integration_Test.Repositories.EmpleoRepository.Commands
{
    [Subject(typeof(EmpleoRepositoryCommands))]
    public class when_UserUpdatesContrato_Should_UpdatesEmpleo
    {
        private static InFileDataBaseTest _dataBaseTest;
        private static ISessionFactory _sessionFactory;
        private static ISession _session;
        private static Empleo _expectedEmpleo;
        private static Empleo _responseEmpleo;
       
        private static ContentFile _contrato;

        private Establish context = () =>
        {

            _dataBaseTest = new InFileDataBaseTest();
            _sessionFactory = _dataBaseTest.sessionFactory;

            var empresa = getEmpresa();
        
            var beneficiario = getBeneficiario();
           
            prepareEmpresa(empresa);

            var sucursal = empresa.Sucursales.First();
            var horario = new HorarioLaboral(new Hora(7, 0, "AM"), new Hora(5, 30, "PM"), new DiasLaborables()
            {
                Lunes = true,
                Martes = true,
                Miercoles = true,
                Jueves = true,
                Viernes = true
            });
            _contrato = new ContentFile(new byte[] { 1, 1, 1 });
            var tipoEmpleo = new TipoEmpleo("Empleo Mensual");
            _expectedEmpleo = new Empleo(empresa, sucursal, beneficiario, horario, "Ingeniero", 25000, tipoEmpleo, new DateTime(2014, 8, 2));
            _expectedEmpleo.addComprobante(getComprobantePago());

            prepareBeneficiario(beneficiario);
            prepareTipoEmpleo(tipoEmpleo);


            using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
            {
                var repository = new EmpleoRepositoryCommands(uow.Session);
                repository.save(_expectedEmpleo);
                uow.commit();
            }

        };

        private Because of = () =>
        {
            _expectedEmpleo.Contrato = _contrato;
            using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
            {
                var repository = new EmpleoRepositoryCommands(uow.Session);
                repository.updateContratoEmpleo(_expectedEmpleo.Id,_contrato);
                uow.commit();
            }

        };

        It should_update_empleo = () =>
        {

            _session = _sessionFactory.OpenSession();

            using (var tx = _session.BeginTransaction())
            {
                _responseEmpleo = _session.Get<Empleo>(_expectedEmpleo.Id);
                _responseEmpleo.Empresa.Gremial.Empresas = new List<Empresa>();
                _responseEmpleo.Empresa.Sucursales = new List<Sucursal>();
                _expectedEmpleo.Empresa.Sucursales = new List<Sucursal>();
                _responseEmpleo.ShouldBeEquivalentTo(_expectedEmpleo);
            }
        };


        private static ComprobantePago getComprobantePago()
        {
            return new ComprobantePago(new DateTime(2014, 8, 2), 10, 20, 10);
        }


        private static void prepareParentesco(Parentesco parentesco)
        {
            _session = _sessionFactory.OpenSession();

            using (var tx = _session.BeginTransaction())
            {
                _session.Save(parentesco);
                tx.Commit();
            }
            _session.Close();
        }

        private static Parentesco getParentescoHijo()
        {
            return new Parentesco("Hijo");
        }

        private static Parentesco getParentescoMadre()
        {
            return new Parentesco("Madre");
        }

        private static Dependiente getDependiente(Identidad identidad, Nombre nombre, Parentesco parentesco)
        {
            return new Dependiente(identidad, nombre, parentesco, new DateTime(1984, 8, 2));
        }

        private static Beneficiario getBeneficiario()
        {
            var beneficiario = new Beneficiario(new Identidad("0801198512396"),
                new Nombre("Dante", "Castillo", "Rubén"), new DateTime(1984, 8, 2));

            return beneficiario;
        }

        private static void prepareTipoEmpleo(TipoEmpleo tipoEmpleo)
        {
            _session = _sessionFactory.OpenSession();
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(tipoEmpleo);
                tx.Commit();
            }
            _session.Close();
        }

        private static void prepareBeneficiario(Beneficiario beneficiario)
        {
            var parentescoHijo = getParentescoHijo();
            var parentescoMadre = getParentescoMadre();
            beneficiario.addDependiente(getDependiente(new Identidad("0801196712396"), new Nombre("Lavinia", "Dubon", "Fajardo"), parentescoMadre));
            beneficiario.addDependiente(getDependiente(new Identidad("0801196712395"), new Nombre("Daniel", "Castillo", "Velasquez"), parentescoHijo));

            prepareParentesco(parentescoHijo);
            prepareParentesco(parentescoMadre);
            _session = _sessionFactory.OpenSession();
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(beneficiario);
                tx.Commit();
            }
            _session.Close();
        }

        private static void prepareEmpresa(Empresa empresa)
        {
            var gremio = empresa.Gremial;
            var sucursales = empresa.Sucursales;
            var actividades = empresa.ActividadesEconomicas;
            prepareGremio(gremio);
            sucursales.ToList().ForEach(x => prepareUser(x.Firma.User));
            actividades.ToList().ForEach(prepareActividades);


            _session = _sessionFactory.OpenSession();
            using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
            {
                var repositoryGremios = new GremioRepositoryReadOnly(uow.Session);
                var repository = new EmpresaRepositoryCommands(uow.Session, repositoryGremios);
                repository.save(empresa);
                uow.commit();
            }

        }

        private static void prepareActividades(ActividadEconomica actividadEconomica)
        {
            _session = _sessionFactory.OpenSession();
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(actividadEconomica);
                tx.Commit();
            }
            _session.Close();
        }

        private static void prepareGremio(Gremio gremio)
        {
            var direccion = gremio.Direccion;
            saveDepartamentoMunicipio(direccion.Departamento, direccion.Municipio);

            using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
            {
                var representanteRepository = new RepresentanteLegalRepositoryReadOnly(uow.Session);
                var direccionRepository = new DireccionRepositoryReadOnly(uow.Session);
                var gremioRead = new GremioRepositoryReadOnly(uow.Session);
                if (!gremioRead.exists(gremio.Id))
                {
                    var gremioRepository = new GremioRepositoryCommands(uow.Session, representanteRepository,
                   direccionRepository);
                    gremioRepository.save(gremio);
                    uow.commit();
                }


            }

        }


        private static void saveDepartamentoMunicipio(Departamento departamento, Municipio municipio)
        {
            _session = _sessionFactory.OpenSession();
            using (var tx = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(departamento);
                _session.SaveOrUpdate(municipio);
                tx.Commit();
            }
            _session.Close();
        }

        private static void prepareUser(User user)
        {
            var rol = user.UserRol;
            _session = _sessionFactory.OpenSession();
            using (var tx = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(rol);
                _session.SaveOrUpdate(user);
                tx.Commit();
            }
            _session.Close();
        }

        private static Empresa getEmpresa()
        {
            var actividades = getActividadEconomicas();
            var sucursales = getSucursales();
            var gremio = getGremio();
            var fechaIngreso = DateTime.ParseExact(DateTime.Now.ToString("g"), "g", null);
            var rtn = new RTN("08011985123960");
            var empresa = new Empresa(rtn, "La Holgazana", fechaIngreso, gremio);

            empresa.ActividadesEconomicas = actividades;
            empresa.Sucursales = sucursales;
            empresa.Contrato = getContrato();
            return empresa;
        }

      

        private static IEnumerable<ActividadEconomica> getActividadEconomicas()
        {
            return new List<ActividadEconomica>()
            {
                new ActividadEconomica("Camaronera"),
                new ActividadEconomica("Arrocera")
            };
        }

        private static Gremio getGremio()
        {
            var municipio = new Municipio("01", "01", "Municipio");
            var departamento = new Departamento() { Id = "01", Municipios = new List<Municipio>() { municipio }, Nombre = "Departamento" };
            var direccion = new Direccion(departamento, municipio, "direccion gremio");

            var RTN = new RTN("08011985123960");
            var representante = new RepresentanteLegal(new Identidad("0801198512396"), "Dante");

            var gremio = new Gremio(RTN, representante, direccion, "Camara");
            return gremio;

        }

        private static IList<Sucursal> getSucursales()
        {
            var municipio = new Municipio("01", "01", "Municipio");
            var departamento = new Departamento() { Id = "01", Municipios = new List<Municipio>() { municipio }, Nombre = "Departamento" };
            var direccion = new Direccion(departamento, municipio, "direccion");
            var fechaDeCreacionFirma = DateTime.ParseExact(DateTime.Now.ToString("g"), "g", null);

            var firma1 = new FirmaAutorizada(new User("DRCD", "Dante", "Ruben", "SDSD", "as", new Rol("rol", "rol")), fechaDeCreacionFirma);
            var firma2 = new FirmaAutorizada(new User("Angela", "Angela", "Castillo", "SSS", "SS", new Rol("rol", "rol")), fechaDeCreacionFirma);

            var sucursal1 = new Sucursal("El Centro", direccion, firma1);
            var sucursal2 = new Sucursal("Barrio Abajo", direccion, firma2);


            return new List<Sucursal>() { sucursal1, sucursal2 };

        }

        private static ContentFile getContrato()
        {
            var data = new byte[5];
            return new ContentFile(data);
        }

    }
}
