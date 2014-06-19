using System;
using CNISS.EnterpriseDomain.Domain.Service;

namespace CNISS.EnterpriseDomain.Application
{
    public class ServiceValidatorRTN : IServiceValidatorRTN
    {
        private ContribuyenteDomainService _service;
        public ServiceValidatorRTN(ContribuyenteDomainService contribuyenteDomainService)
        {
            _service = contribuyenteDomainService;
        }

        public bool isValidRTN(string RTN)
        {
            if(String.IsNullOrEmpty(RTN))
                return false;
            return _service.validarRtn(RTN);
        }
    }
}