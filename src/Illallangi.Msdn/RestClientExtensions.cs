using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;

namespace Illallangi.Msdn
{
    public static class RestClientExtensions
    {
        public static T HttpGet<T>(this IRestClient client, string resource, IEnumerable<KeyValuePair<string, string>> parameters = null, string rootElement = null)
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
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public static T HttpPost<T>(this IRestClient client, string resource, object body = null, string rootElement = null)
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
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}