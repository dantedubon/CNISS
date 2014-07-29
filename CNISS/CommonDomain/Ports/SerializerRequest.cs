namespace CNISS.CommonDomain.Ports
{
    public class SerializerRequest:ISerializeJsonRequest
    {
        public string toJson<T>(T instance)
        {
            return JsonConvert.SerializeObject(instance,Formatting.Indented);
        }

        public T fromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
            
    
    }
}