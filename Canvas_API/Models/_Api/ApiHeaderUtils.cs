using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Canvas_API.Models._Api
{
    public class ApiHeaderUtils
    {
        private static readonly string CONTENT_TYPE = "Content-Type";
        protected static readonly IConfiguration apiconfig = new ConfigurationBuilder().AddJsonFile("apisetting.json").Build();

        
        public HttpHeaders initHttpHeaders(HttpRequestHeaders headers,string token)
        {
            headers.Clear();
            headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            headers.Add("Authorization", "Bearer " + token);
            return headers;
        }
    }

 }
