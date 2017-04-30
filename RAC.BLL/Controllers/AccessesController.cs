using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RAC.DAL.Models;

namespace RAC.BLL.Controllers
{
    public class AccessesController : Controller
    {
        string connection;
        public AccessesController()
        {
            connection = "http://ec2-34-210-68-39.us-west-2.compute.amazonaws.com/Accesses/";
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

                return Json(users,JsonRequestBehavior.AllowGet);
            }
        }
    }
}