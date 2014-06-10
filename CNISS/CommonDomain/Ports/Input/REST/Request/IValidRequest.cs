namespace CNISS.CommonDomain.Ports.Input.REST.Request
{
   public interface IValidRequest
   {
       bool isValidPost();
       bool isValidPut();
       bool isValidDelete();
       bool isValidGet();
   }
}
