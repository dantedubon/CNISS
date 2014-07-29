namespace CNISS.CommonDomain.Ports
{
    public interface ISerializeJsonRequest
    {
        string toJson<T>(T instance);
        T fromJson<T>(string json);
    }
}