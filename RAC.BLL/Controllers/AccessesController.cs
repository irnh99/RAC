using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RAC.DAL.Models;
using System.IO;

namespace RAC.BLL.Controllers
{
    public class AccessesController : Controller
    {
        string connection;
        public AccessesController()
        {
            connection = "ec2-34-210-81-196.us-west-2.compute.amazonaws.com/Accesses/";
        }

        // GET: Accesses
        [HttpGet]
        public JsonResult Index()
        {
            using (WebClient wb = new WebClient())
            {

                string site = "";

                string response = wb.DownloadString(connection + site);

                List<UserVM> users = JsonConvert.DeserializeObject<List<UserVM>>(response);

                return Json(users, JsonRequestBehavior.AllowGet);
            }
        }
        // Create an access
        [HttpPost]
        public JsonResult Create(AccessVM accessVm)
        {
            string site = "Create";
            WebRequest wr = WebRequest.Create(connection + site);
            wr.Method = "POST";

            string data = string.Format("accessVm={0}", JsonConvert.SerializeObject(accessVm));

            using (StreamWriter sw = new StreamWriter(wr.GetRequestStream()))
            {
                sw.WriteLine(data);
            }

            string response = wr.GetResponse().ToString();

            string webResponse = JsonConvert.DeserializeObject<string>(response);

            return Json(webResponse, JsonRequestBehavior.AllowGet);
        }
    }
}