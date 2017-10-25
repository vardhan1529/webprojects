using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SAM.Handlers
{
    public static class HttpClientFactory
    {
        private static readonly bool CustomActionFilterEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableCustomActionFilter"].ToString());
        public static HttpClient GetHttpClient()
        {
            if(CustomActionFilterEnabled)
            {
                return new HttpClient(new SAM.Handlers.HttpClientCustomHandler(new HttpClientHandler()));
            }

            return new HttpClient();
        }
    }
    public class HttpClientCustomHandler : DelegatingHandler
    {
        //Accessed by setting the custom handler as the messagehandler for the client request
        //HttpClient client = new HttpClient(new HttpClientCustomHandler(new HttpClientHandler()));
        public HttpClientCustomHandler(HttpMessageHandler defaultHandler)
                : base(defaultHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Request:");
            Thread.Sleep(3000);
            var response =  await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            //Console.WriteLine("Response: StatusCode " + response.IsSuccessStatusCode);

            return response;
        }
    }
}