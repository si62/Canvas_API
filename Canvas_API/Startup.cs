using Canvas_API.Models._Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Canvas_API
{
    public class Startup
    {
        private readonly IConfiguration apiconfig = new ConfigurationBuilder().AddJsonFile("apisetting.json").AddUserSecrets<Program>().Build();


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    ApiUtils api = new ApiUtils();
                    string response = "";
                    string datajson = null;
                    LogFile logfile = new LogFile();
                    Dictionary<string, string> urlreplace=null;

                    //List courses
                     response = api.CallApi(apiconfig[$"API:Courses:List:URL"],
                                            apiconfig[$"API:Courses:List:Method"], datajson, urlreplace);

                    await context.Response.WriteAsync("<p>Courses List:<br/>"+response+"</p>");
                    logfile.Log(response);

                    //Create course
                    /*
                     datajson ="";
                    urlreplace = new Dictionary<string, string>() {
                        [":account_id"] = "1" //account 1 for canvas "Macao Polytechnic University"
                    } ;

                    datajson = @"{ 
                        ""course"":{
                            ""name"":""Test API Course"",
                            ""course_code"":""TESTAPI01"",
                            ""license"":""private"",
                            ""is_public"":false,
                            ""sis_course_id"":""TESTAPI01""
                        }
                    }
                    ";

                    response = api.CallApi(apiconfig[$"API:Courses:Create:URL"],
                                           apiconfig[$"API:Courses:Create:Method"], datajson, urlreplace
                                           );

                    await context.Response.WriteAsync($"<p>Courses Create:<br/>{ response }</p>");
                    logfile.Log(response);
                    */

                    //List Enrollments
                    /*urlreplace = new Dictionary<string, string>()
                    {
                        [":course_id"] = "2391" //account 1 for canvas "Macao Polytechnic University"
                    };

                    response = api.CallApi(apiconfig[$"API:Enrollments:List:URL"],
                       apiconfig[$"API:Enrollments:List:Method"], datajson, urlreplace);



                    await context.Response.WriteAsync($"<p>Enrollments List:<br/>{ response }</p>");
                    logfile.Log(response);
                    */
                    //Enroll a user
                    /*
                     urlreplace = new Dictionary<string, string>()
                    {
                        [":course_id"] = "2391" 
                    };

                    datajson = @"{ 
                        ""enrollment"":{
                            ""user_id"":""510"",
                            ""type"":""StudentEnrollment"",
                            ""enrollment_state"":""active""
                        }
                    }
                    ";

                    response = api.CallApi(apiconfig[$"API:Enrollments:Create:URL"],
                                           apiconfig[$"API:Enrollments:Create:Method"], datajson, urlreplace
                                           );

                    await context.Response.WriteAsync($"<p>Enrollments Create:<br/>{ response }</p>");
                    logfile.Log(response);

                    dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response);
                    var enrollment_id = jsonObject.id;
                    
                    */

                    //Delete Enrollment
                    /*
                    urlreplace = new Dictionary<string, string>()
                    {
                        [":course_id"] = "2391",
                        [":id"] = enrollment_id,
                    };

                    datajson = @"{ 
                        ""task"":""delete""
                    }
                    ";

                    response = api.CallApi(apiconfig[$"API:Enrollments:Delete:URL"],
                                           apiconfig[$"API:Enrollments:Delete:Method"], datajson, urlreplace
                                           );

                    await context.Response.WriteAsync($"<p>Enrollments Delete:<br/>{ response }</p>");
                    logfile.Log(response);
                    */
                });
            });
        }

    }
}
