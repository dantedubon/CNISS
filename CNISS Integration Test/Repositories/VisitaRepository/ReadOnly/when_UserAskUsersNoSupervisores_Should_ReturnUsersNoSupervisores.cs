using System;
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

namespace CNISS_Integration_Test.Repositories.VisitaRepository.ReadOnly
{
    [Subject(typeof(VisitaRepositoryReadOnly))]
    public class when_UserAskUsersNoSupervisores_Should_ReturnUsersNoSupervisores
    {
        private static InFileDataBaseTest _dataBaseTest;
        private static ISessionFactory _sessionFactory;
        private static ISession _session;
        private static bool _respuesta;
        private static Visita _expectedVisita;
        private static User _expectedUserSupervisor;
        private static IEnumerable<User> _usersResponse; 

        private Establish context = () =>
        {
            _dataBaseTest = new InFileDataBaseTest();
            _sessionFactory = _dataBaseTest.sessionFactory;

            var usuarioSupervisor = new User("Supervisor", "Supervisor", "Supervisor", "xxx", "xxx", new Rol("Rol Supervisor", "Rol Supervisor"){nivel = 2});
            _expectedUserSupervisor = new User("SupervisorEsperado", "SupervisorEsperado", "Supervisor", "xxx", "xxx", new Rol("Rol Supervisor", "Rol Supervisor") { nivel = 2 });
            var fechaInicial = new DateTime(2014, 5, 1);
            var fechaFinal = new DateTime(2014, 5, 30);
            var supervisor = new Supervisor(usuarioSupervisor);
            var empresaVisita = getEmpresa();
            var lugarVisita = new LugarVisita(empresaVisita, empresaVisita.sucursales.FirstOrDefault());
            supervisor.addLugarVisita(lugarVisita);

            _expectedVisita = new Visita("Visita de Prueba", fechaInicial, fechaFinal);
            _expectedVisita.addSupervisor(supervisor);

            prepareUser(usuarioSupervisor);
            prepareUser(_expectedUserSupervisor);
            prepareEmpresa(empresaVisita);

            using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
            {

                var repository = new VisitaRepositoryCommand(uow.Session);
                repository.save(_expectedVisita);
                uow.commit();
            }





        };

        private Because of = () =>
        {

            using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
            {

                var repository = new VisitaRepositoryReadOnly(uow.Session);

               
                _usersResponse = repository.usuariosSinVisitaAgendada(_expectedVisita.fechaInicial, _expectedVisita.fechaFinal).ToList();
                

            }
        };

         It should_return_usuariosNoAdministradores = () =>
        {
            var usuarioNoAdministrador = _usersResponse.FirstOrDefault();
            usuarioNoAdministrador.ShouldBeEquivalentTo(_expectedUserSupervisor);
        };



        private static void prepareEmpresa(Empresa empresa)
        {
            var gremio = empresa.gremial;
            var sucursales = empresa.sucursales;
            var actividades = empresa.actividadesEconomicas;
            prepareGremio(gremio);
            sucursales.ToList().ForEach(x => prepareUser(x.firma.user));
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
            var direccion = gremio.direccion;
            saveDepartamentoMunicipio(direccion.departamento, direccion.municipio);

            using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
            {
                var representanteRepository = new RepresentanteLegalRepositoryReadOnly(uow.Session);
                var direccionRepository = new DireccionRepositoryReadOnly(uow.Session);


                var gremioRepository = new GremioRepositoryCommands(uow.Session, representanteRepository,
                    direccionRepository);
                gremioRepository.save(gremio);
                uow.commit();
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
            var rol = user.userRol;
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

            empresa.actividadesEconomicas = actividades;
            empresa.sucursales = sucursales;
            empresa.contrato = getContrato();
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
            var departamento = new Departamento() { Id = "01", municipios = new List<Municipio>() { municipio }, nombre = "Departamento" };
            var direccion = new Direccion(departamento, municipio, "direccion gremio");

            var RTN = new RTN("08011985123960");
            var representante = new RepresentanteLegal(new Identidad("0801198512396"), "Dante");

            var gremio = new Gremio(RTN, representante, direccion, "Camara");
            return gremio;

        }

        private static IList<Sucursal> getSucursales()
        {
            var municipio = new Municipio("01", "01", "Municipio");
            var departamento = new Departamento() { Id = "01", municipios = new List<Municipio>() { municipio }, nombre = "Departamento" };
            var direccion = new Direccion(departamento, municipio, "direccion");
            var fechaDeCreacionFirma = DateTime.ParseExact(DateTime.Now.ToString("g"), "g", null);

            var firma1 = new FirmaAutorizada(new User("Usuario1", "Dante", "Ruben", "SDSD", "as", new Rol("rol", "rol"){nivel = 1}), fechaDeCreacionFirma);
            var firma2 = new FirmaAutorizada(new User("Usuario2", "Angela", "Castillo", "SSS", "SS", new Rol("rol", "rol"){nivel = 1}), fechaDeCreacionFirma);

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
