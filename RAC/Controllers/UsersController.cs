using Newtonsoft.Json;
using RAC.DAL.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace RAC.Controllers
{
    public class UsersController : Controller
    {
        string connection;
        public UsersController()
        {
            connection = "http://ec2-54-213-8-236.us-west-2.compute.amazonaws.com/Users/";

            /* to test post method 
             
            connection = "http://localhost:50509/Users/";
            /**/
        }

        // GET: Areas accesible by the user
        [HttpGet]
        public JsonResult Index()
        {
            using (WebClient wb = new WebClient())
            {
                string site = "";

                string response = wb.DownloadString(connection + site);

                List<AreaVM> accesses = JsonConvert.DeserializeObject<List<AreaVM>>(response);

                return Json(accesses, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult LogIn(UserVM user)
        {
            /* 
            user = new UserVM
            {
                UserName = "harp99",
                Pass = "leon"
            };
            /* */

            JavaScriptSerializer JSS = new JavaScriptSerializer();

            string site = "LogIn";
            var wr = (HttpWebRequest)WebRequest.Create(connection + site);
            wr.Method = "POST";
            wr.ContentType = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(wr.GetRequestStream()))
            {
                string json = JSS.Serialize(user);

                streamWriter.Write(json);
                streamWriter.Flush();
            }
            var httpResponse = (HttpWebResponse)wr.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();

                UserVM userDb = JsonConvert.DeserializeObject<UserVM>(responseText);

                return Json(userDb, JsonRequestBehavior.AllowGet);
            }

        }
    }
}