using System.Collections.Generic;
using System.Linq;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.UserModule.UserQuery
{
    public class UserMapping
    {
        public UserMapping()
        {
        }

        public IEnumerable<UserRequest> convertToRequest(IEnumerable<User> users)
        {
            return users.Select(convertToRequest);
        }

        public UserRequest convertToRequest(User user)
        {
            return new UserRequest
            {
                firstName = user.firstName,
                secondName = user.secondName,
                Id = user.Id,
                mail = user.mail,
                password = "",
                userRol = new RolRequest
                {
                    description = user.userRol.description,
                    name = user.userRol.name,
                    idGuid = user.userRol.Id,
                    nivel = user.userRol.nivel,
                    auditoriaRequest = new AuditoriaRequest()
                    {
                        fechaCreo = user.userRol.auditoria.fechaCreo,
                        fechaModifico = user.userRol.auditoria.fechaModifico,
                        usuarioCreo = user.userRol.auditoria.usuarioCreo,
                        usuarioModifico = user.userRol.auditoria.usuarioModifico
                    }

                },
                auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo =  user.auditoria.fechaCreo,
                    fechaModifico = user.auditoria.fechaModifico,
                    usuarioCreo = user.auditoria.usuarioCreo,
                    usuarioModifico = user.auditoria.usuarioModifico
                }
            };
        }
    }
}