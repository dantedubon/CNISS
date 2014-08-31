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
                firstName = user.FirstName,
                secondName = user.SecondName,
                Id = user.Id,
                mail = user.Mail,
                password = "",
                userRol = new RolRequest
                {
                    description = user.UserRol.Description,
                    name = user.UserRol.Name,
                    idGuid = user.UserRol.Id,
                    nivel = user.UserRol.Nivel,
                    auditoriaRequest = new AuditoriaRequest()
                    {
                        fechaCreo = user.UserRol.Auditoria.FechaCreacion,
                        fechaModifico = user.UserRol.Auditoria.FechaActualizacion,
                        usuarioCreo = user.UserRol.Auditoria.CreadoPor,
                        usuarioModifico = user.UserRol.Auditoria.ActualizadoPor
                    }

                },
                auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo =  user.Auditoria.FechaCreacion,
                    fechaModifico = user.Auditoria.FechaActualizacion,
                    usuarioCreo = user.Auditoria.CreadoPor,
                    usuarioModifico = user.Auditoria.ActualizadoPor
                }
            };
        }
    }
}