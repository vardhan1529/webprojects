using ElmahMvc.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Xml;

namespace ElmahMvc.Controllers
{
    public class Site
    {
        public string ContentUrl { get; set; }

        public string Id { get; set; }
    }
    public class Credentials
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public Site Site { get; set; }
    }
    public class SignIn
    {
        public Credentials Credentials { get; set; }
    }
    public static class SiteManagement
    {
        static Dictionary<string, string> Tokens { get; set; }

        static Dictionary<string, string> SiteIds { get; set; }

        static Dictionary<string, DateTime> TokenLifeTime { get; set; }

        public static async Task<string> GetToken(string siteContentUrl)
        {
            if (Tokens.ContainsKey(siteContentUrl) && TokenLifeTime[siteContentUrl] > DateTime.Now)
            {
                return Tokens[siteContentUrl];
            }

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://us-east-1.online.tableau.com");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var cred = JsonConvert.SerializeObject(new SignIn
            {
                Credentials = new Credentials()
                {
                    Name = "vardhan1529@gmail.com",
                    Password = "Tableau@1529",
                    Site = new Site() { ContentUrl = "demo1529" }
                }
            }, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var res = await client.PostAsync("/api/2.7/auth/signin", new StringContent(cred, System.Text.Encoding.UTF8, "application/json"));

            var con = await res.Content.ReadAsAsync<SignIn>();
            Tokens[siteContentUrl] = con.Credentials.Token;
            TokenLifeTime[siteContentUrl] = DateTime.Now.AddMinutes(55);
            SiteIds[siteContentUrl] = con.Credentials.Site.Id;

            return con.Credentials.Token;
        }

        public async static Task<string> GetSiteId(string siteContentUrl)
        {
            if (!SiteIds.ContainsKey(siteContentUrl))
            {
                await GetToken(siteContentUrl);
            }

            return SiteIds[siteContentUrl];
        }

        public static byte[] GetViewPreviewImage()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://us-east-1.online.tableau.com");
            client.DefaultRequestHeaders.Add("X-Tableau-Auth", "I5eqARHI3Z75NPxaYJXhGwPlDlak9Ibt");
            var r = client.GetAsync("/api/2.7/sites/57ab922b-ce9b-478b-86e7-80a9f8e87754/workbooks/0fe9df90-be7c-4342-9ed5-e70358bc9c23/views/6060e5f5-d4d5-4886-bde9-19abfdd54469/previewimage").Result;
            var i = r.Content.ReadAsByteArrayAsync().Result;
            return i;
        }

        public async static Task UploadWorkBook()
        {
            using (var content = new MultipartFormDataContent())
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://us-east-1.online.tableau.com");
                client.DefaultRequestHeaders.Add("X-Tableau-Auth", "SJtZhqANny9QCdzry4RUWza9mvrwm8UO");
                var doc = new XmlDocument();
                var root = doc.CreateElement("tsRequest");
                var workbook = doc.CreateElement("workbook");
                workbook.Attributes.Append(doc.CreateAttribute("name", "test"));
                var project = doc.CreateElement("project");
                project.Attributes.Append(doc.CreateAttribute("id", "54e0d2fd-e322-4aa1-b0f6-bad5494515c0"));
                workbook.AppendChild(project);
                root.AppendChild(workbook);
                doc.AppendChild(root);
                content.Add(new StringContent(doc.OuterXml, System.Text.Encoding.UTF8, "text/xml"));
                using (var fileStream = new FileStream(@"D://Analyze Superstore.twbx", FileMode.Open))
                {
                    var fileContent = new StreamContent(fileStream);
                    //fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("application/octet-stream")
                    //{
                    //    FileName = "test.twbx",
                    //    Name = "tableau_workbook"
                    //};
                    fileContent.Headers.Add("content-type", "application/octet-stream");
                    content.Add(fileContent, "tableau_workbook", "Analyze Superstore.twbx");


                var requestUri = "/api/2.7/sites/57ab922b-ce9b-478b-86e7-80a9f8e87754/workbooks";

                var result = await client.PostAsync(requestUri, content);

                var res = result.Content.ReadAsStringAsync().Result;
                }
            }
        }
    }
        public class HomeController : Controller
        {
            public async Task<ActionResult> Index()
            {
                //SqlConnection s = new SqlConnection(ConfigurationManager.ConnectionStrings["test"].ToString());
                //s.Open();
                var b = Request.Browser.Id;
                var s = Request.ServerVariables;
                var x = ApplicationManager.GetApplicationManager().GetRunningApplications();
                ViewBag.HostingEnvironmentID = HostingEnvironment.ApplicationID;
                ViewBag.HostEnvironmentSiteName = HostingEnvironment.SiteName;
                ViewBag.PThreadId = Thread.CurrentThread.ManagedThreadId;
                ViewBag.Context = System.Web.HttpContext.Current == null;
                ViewBag.ThreadId = Thread.CurrentThread.ManagedThreadId;
                ViewBag.AppDoamin = AppDomain.CurrentDomain.FriendlyName;
                ViewBag.AppDoaminId = AppDomain.CurrentDomain.Id;
            //await SiteManagement.UploadWorkBook();
            //var img = Convert.ToBase64String(SiteManagement.GetViewPreviewImage());
            //ViewBag.Img = "data:image/png;base64," + img;

                return View();
            }

        public void OledbConnectionSample()
        {
            using (var dbConnection = new OleDbConnection())
            {
                var sw = new Stopwatch();
                try
                {
                    dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["TimeOutTest"].ToString();
                    dbConnection.Open();
                    var sqlCommand = new OleDbCommand();
                    sqlCommand.Connection = dbConnection;
                    sqlCommand.CommandTimeout = 40;
                    sqlCommand.CommandText = "select * from Test where Id =" + "'" + "0' or '1'='1" + "'" ;
                    sw.Start();
                    var c = sqlCommand.ExecuteReader();
                    while(c.Read())
                    {
                        var y = c[0];
                    }
                    sw.Stop();
                }

                catch (Exception ex)
                {
                    sw.Stop();
                    var ep = sw.ElapsedMilliseconds;
                }
            }
        }
        public void OdbcConnectionSample()
        {
            using (var con = new OdbcConnection("Driver={ODBC Driver 13 for SQL Server};Server=ggku2ser6;Database=vardhan;Trusted_Connection=Yes"))
            {
                var sw = new Stopwatch();
                try
                {
                    con.Open();
                    var odbcc = new OdbcCommand();
                    odbcc.Connection = con;
                    odbcc.CommandTimeout = 1;
                    //odbcc.CommandText = "declare @c int; " +
                    //                    "set @c = 0; " +
                    //                    "WHILE @c < 200000 " +
                    //                    "BEGIN " +
                    //                        " insert into Test values(CAST(@c as Varchar(8)) + 'Test' + CAST(@c + 1 as Varchar(8))) " +
                    //                        " set @c = @c + 1; " +
                    //                    " END";

                    odbcc.CommandText = "select UName from Test";

                    sw.Start();
                    var c = odbcc.ExecuteReader();
                    var li = new List<string>();
                    while (c.Read())
                    {
                        li.Add(c[0].ToString());
                    }
                    sw.Stop();

                    con.Close();
                }
                catch (Exception ex)
                {
                    sw.Stop();
                    var ep = sw.ElapsedMilliseconds;
                    con.Close();
                }
            }
        }

        public ActionResult About()
            {
                ViewBag.Message = "Your application description page.";

                return View();
            }

            public ActionResult Contact()
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }
        }
    }