using System.Linq;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;

namespace CNISS.EnterpriseDomain.Ports.Output
{
    public class EmpresaRepositoryCommands : NHibernateCommandRepository<Empresa, RTN>, IEmpresaRepositoryCommands
    {
        private readonly IGremioRepositoryReadOnly _repositoryReadGremio;

        public EmpresaRepositoryCommands(ISession session, IGremioRepositoryReadOnly repositoryReadGremio
            ) : base(session)
        {
            this._repositoryReadGremio = repositoryReadGremio;
        }

        public void save(Empresa entity)
        {
            var sucursales = entity.sucursales;
            if (entity.contrato != null)
                saveContrato(entity.contrato);

            base.save(entity);
            sucursales.ToList().ForEach(saveSucursal);

            


        }

        private void saveContrato(ContentFile contrato)
        {

            _session.Save(contrato);
        }

        public void update(Empresa entity)
        {
            var sucursales = entity.sucursales;
            
            
            sucursales.ToList().ForEach(updateSucursal);

            _session.SaveOrUpdate(entity);
        }

        public void updateContrato(RTN id, ContentFile nuevoContrato)
        {
            var empresa = _session.Get<Empresa>(id);
            empresa.contrato = nuevoContrato;
            _session.Save(empresa.contrato);
            _session.Update(empresa);

        }

        private Gremio getGremio(RTN rtnGremio)
        {
            return _repositoryReadGremio.get(rtnGremio);
        }

        private void updateSucursal(Sucursal sucursal)
        {
            var direccion = sucursal.direccion;
            var firma = sucursal.firma;
            _session.SaveOrUpdate(direccion);
            _session.SaveOrUpdate(firma);
            _session.SaveOrUpdate(sucursal);
        }
        private void saveSucursal(Sucursal sucursal)
        {
            var direccion = sucursal.direccion;
            var firma = sucursal.firma;
            _session.Save(direccion);
            _session.Save(firma);
            _session.Save(sucursal);
        }



       


    }
}