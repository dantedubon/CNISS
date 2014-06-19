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