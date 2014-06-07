using System;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Domain;

namespace CNISS.AutenticationDomain.Domain.Repositories
{
    public interface IUserRepositoryReadOnly : IRepositoryReadOnly<User, string>
    {
    }
}