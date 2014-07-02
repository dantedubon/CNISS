using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNISS.CommonDomain.Domain
{
    public class Auditoria
    {
        public virtual string usuarioCreo { get; set; }
        public virtual DateTime  fechaCreo { get; set; }
        public virtual string usuarioModifico { get; set; }
        public virtual DateTime fechaModifico { get; set; }

        public Auditoria(string usuarioCreo, DateTime fechaCreo, string usuarioModifico, DateTime fechaModifico)
        {
            this.usuarioCreo = usuarioCreo;
            this.fechaCreo = fechaCreo;
            this.usuarioModifico = usuarioModifico;
            this.fechaModifico = fechaModifico;
        }

        public Auditoria()
        {
            
        }
    }
}