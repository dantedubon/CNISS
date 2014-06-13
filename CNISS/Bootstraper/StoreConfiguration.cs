using System;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FluentNHibernate.Automapping;

namespace CNISS.Bootstraper
{
    public class StoreConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.Namespace == typeof(Rol).Namespace || type.Namespace == typeof(User).Namespace  ;
        }
    }
}