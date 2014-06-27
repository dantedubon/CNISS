using System.Configuration;

namespace CNISS.EnterpriseDomain.Application
{
    public class ProvideAllowedDaysForNewEmpleo:IProvideAllowedDaysForNewEmpleo
    {
        public int getDays()
        {
            var diasFromConfiguration = ConfigurationManager.AppSettings["diasParaNuevoEmpleo"];

            return int.Parse(diasFromConfiguration);
        }
    }
}