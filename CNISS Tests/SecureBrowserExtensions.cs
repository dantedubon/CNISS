using System.Linq;
using Nancy.Responses.Negotiation;
using Nancy.Testing;

namespace CNISS_Tests
{
    public static class SecureBrowserExtentions
    {
        public static BrowserResponse GetSecureJson(this Browser browser, string resource, object payload = null)
        {
            return browser.Get(resource, with =>
            {
                with.HttpsRequest();
                with.Accept(MediaRange.FromString("application/json"));
                if (payload != null)
                    payload.GetType().GetProperties().ToList()
                           .ForEach(
                               x => with.Query(x.Name, x.GetValue(payload).ToString()));
            });
        }

        public static BrowserResponse PostSecureJson(this Browser browser, string resource, object payload)
        {
            return browser.Post(resource, with =>
            {
                with.HttpsRequest();
                with.Accept(MediaRange.FromString("application/json"));
                with.JsonBody(payload);
            });
        }

        public static BrowserResponse PutSecureJson(this Browser browser, string resource, object payload)
        {
            return browser.Put(resource, with =>
            {
                with.HttpsRequest();
                with.Accept(MediaRange.FromString("application/json"));
                with.JsonBody(payload);
            });
        }
    }
}