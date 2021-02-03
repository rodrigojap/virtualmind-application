using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VirtuaMind.Infrastructure.RestServices.Template
{
    public abstract class RestService
    {
        private RestClient BaseClient;
        private RestRequest Request;
        private dynamic Response;        

        public RestService()
        {
            BaseClient = new RestClient();
            Request = new RestRequest();            
        }

        protected async Task<dynamic> Execute(Dictionary<string, object> keyValueParameters, string requestUri, Method method)
        {
            InitializeParametersRequest(requestUri, method, keyValueParameters);

            await SendRequestAsync();
            
            return Response.Content;
        }
      
        protected void SetClientRequest(string baseURL)
        {
            BaseClient = new RestClient(baseURL);
        }

        private void InitializeParametersRequest(string requestUri,
                                                 Method method,
                                                 Dictionary<string, object> keyValueParameters)
        {
            SetResource(requestUri);
            SetHttpMethod(method);
            AddParameters(keyValueParameters);            
        }

        private void SetResource(string requestUri)
        {
            Request.Resource = requestUri;
        }

        private void SetHttpMethod(Method method)
        {
            Request.Method = method;
        }

        private void AddParameters(Dictionary<string, object> keyValueParameters)
        {
            if (keyValueParameters == null)
                return;

            foreach (var (key, value) in keyValueParameters)
            {
                Request.AddParameter(key, value);
            }
        }

        private async Task SendRequestAsync<T>()
        {
            Response = await BaseClient.ExecuteAsync<T>(this.Request);
        }

        private async Task SendRequestAsync()
        {
            Response = await BaseClient.ExecuteAsync(this.Request);
        }
      
        public void Dispose()
        {
            BaseClient = null;
            Request = null;
            Response = null;
        }
    }
}
