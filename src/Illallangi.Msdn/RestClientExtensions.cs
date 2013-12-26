using System.Collections.Generic;
using System.Security.AccessControl;
using Newtonsoft.Json;
using RestSharp;

namespace Illallangi.Msdn
{
    public static class RestClientExtensions
    {
        public static RestResult<T> HttpGet<T>(this IRestClient client, string resource, IEnumerable<KeyValuePair<string, string>> parameters = null, string rootElement = null) where T : class
        {
            var request = new RestRequest(resource, Method.GET);

            if (!string.IsNullOrWhiteSpace(rootElement))
            {
                request.RootElement = rootElement;
            }

            if (null != parameters)
            {
                foreach (var kvp in parameters)
                {
                    request.AddParameter(kvp.Key, kvp.Value, ParameterType.QueryString);
                }
            }

            var response = client.Execute(request);
            return new RestResult<T>(response.Content);
        }

        public static RestResult<T> HttpPost<T>(this IRestClient client, string resource, object body = null, string rootElement = null) where T : class
        {
            var request = new RestRequest(resource, Method.POST);

            if (!string.IsNullOrWhiteSpace(rootElement))
            {
                request.RootElement = rootElement;
            }

            if (null != body)
            {
                request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(body), ParameterType.RequestBody);
                request.RequestFormat = DataFormat.Json;
            }

            var response = client.Execute(request);
            return new RestResult<T>(response.Content);
        }
    }

    public class RestResult<T> where T: class
    {
        private T currentResult;
        private readonly string currentRaw;

        public RestResult(string raw)
        {
            this.currentRaw = raw;
        }

        public T Result
        {
            get { return this.currentResult ?? (this.currentResult = JsonConvert.DeserializeObject<T>(this.Raw)); }
        }

        public string Raw
        {
            get { return this.currentRaw; }
        }
    }
}