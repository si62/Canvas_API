using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Canvas_API.Models._Api
{
    public class ApiUtils
    {
        private readonly IConfiguration apiconfig = new ConfigurationBuilder().AddJsonFile("apisetting.json").AddUserSecrets<Program>().Build();
        private readonly HttpClient client = new HttpClient();
        protected static string token;
        public ApiUtils(){
            token = apiconfig[$"Connection:Token"];
        }
        private bool IsJson(string source)
        {
            if (source == null)
                return false;
            try
            {
                JsonDocument.Parse(source);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }
        public string CallApi(string url,string method,string datajson=null, Dictionary<string, string> urlreplace =null)
        {
            ApiHeaderUtils ahu = new ApiHeaderUtils();
            ahu.initHttpHeaders(client.DefaultRequestHeaders, token);

            HttpMethod httpmethod;

            string apiurl = url;

            if(urlreplace!=null)
                foreach (var item in urlreplace)
                    apiurl = apiurl.Replace(item.Key, item.Value);

            if (datajson == null || !IsJson(datajson)) 
                datajson = "";

            switch (method) {
                case "GET":
                    httpmethod = HttpMethod.Get;
                    break;
                case "POST":
                    httpmethod = HttpMethod.Post;
                    break;
                case "DELETE":
                    httpmethod = HttpMethod.Delete;
                    break;
                default:
                    httpmethod = HttpMethod.Get;
                    break;
            }
            string baseurl = apiconfig[$"Connection:BaseURL"];
            
             var request = new HttpRequestMessage(httpmethod, baseurl + apiurl)
            {
                Content = new StringContent(datajson, Encoding.UTF8, "application/json")
            };
            var result = client.Send(request);
            var response_txt = "";
            //if (result.IsSuccessStatusCode)
           // {
                response_txt = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
           // }

            return response_txt;
        }


    }
}
