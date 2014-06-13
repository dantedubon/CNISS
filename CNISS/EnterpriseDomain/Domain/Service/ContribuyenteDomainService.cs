using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CNISS.EnterpriseDomain.Domain.Service
{
    /*Legacy Code*/
    public class ContribuyenteDomainService
    {
        public virtual bool validarRtn(string rtn)
        {
            var _rtn = new RTN(rtn);
            return _rtn.isRTNValid();
        }


    }
}