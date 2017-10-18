using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SAM.Handlers
{
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

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            Console.WriteLine("Response: StatusCode " + response.IsSuccessStatusCode);

            return response;
        }
    }
}