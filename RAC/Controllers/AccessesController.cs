using Newtonsoft.Json;
using RAC.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace RAC.Controllers
{
    public class AccessesController : Controller
    {
        string connection;
        public AccessesController()
        {
            connection = "http://ec2-54-186-139-128.us-west-2.compute.amazonaws.com/Accesses/";

            /* to test post method
            connection = "http://localhost:50509/Accesses/";
            /**/
        }

        // GET: Accesses
        [HttpGet]
        public JsonResult Index()
        {
            using (WebClient wb = new WebClient())
            {

                string site = "";

                string response = wb.DownloadString(connection + site);

                List<AccessVM> accesses = JsonConvert.DeserializeObject<List<AccessVM>>(response);

                return Json(accesses, JsonRequestBehavior.AllowGet);
            }
        }
        // Create an access
        [HttpPost]
        public JsonResult Create(AccessVM accessVm)
        {
            /* to test post method
            accessVm.User = new UserVM()
            {
                IdUser = 0
            };
            accessVm.Area = new AreaVM()
            {
                IdArea = 1
            };
            /* */

            JavaScriptSerializer JSS = new JavaScriptSerializer();

            string site = "Create";
            var wr = (HttpWebRequest)WebRequest.Create(connection + site);
            wr.Method = "POST";
            wr.ContentType = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(wr.GetRequestStream()))
            {
                string json = JSS.Serialize(accessVm);

                streamWriter.Write(json);
                streamWriter.Flush();
            }
            var httpResponse = (HttpWebResponse)wr.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();

                responseText = JsonConvert.DeserializeObject<string>(responseText);

                return Json(responseText, JsonRequestBehavior.AllowGet);
            }

        }
    }
}