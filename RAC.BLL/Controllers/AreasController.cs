using Newtonsoft.Json;
using RAC.DAL.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace RAC.BLL.Controllers
{
    public class AreasController : Controller
    {
        string connection;
        public AreasController()
        {
            connection = "http://ec2-34-210-81-196.us-west-2.compute.amazonaws.com/Areas/";

            /* to test post method
             
            connection = "http://localhost:59781/Areas/";
            /**/
        }

        // GET: Areas accesible by the user
        [HttpGet]
        public JsonResult Index(int idUserType)
        {
            using (WebClient wb = new WebClient())
            {
                /* 
                idUserType = 1;
                /**/

                string site = "?idUserType="+idUserType.ToString();

                string response = wb.DownloadString(connection + site);

                List<AreaVM> areas = JsonConvert.DeserializeObject<List<AreaVM>>(response);
                IEnumerable<AreaVM> iAreas = areas.AsEnumerable<AreaVM>();

                areas = iAreas.Where(x => 
                    x.HasAccess != null && 
                    x.HasAccess.Where(y => 
                        y.IdUserType == idUserType).Any()).ToList();
                
                return Json(areas, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult Details(int idArea)
        {
            /* 
            idArea = 1;
            /* */
            WebClient wb = new WebClient();

            string site = "Details/" + idArea.ToString();

            string response = wb.DownloadString(connection + site);

            AreaVM accesses = JsonConvert.DeserializeObject<AreaVM>(response);
            
            return Json(accesses, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OpenClose(AreaVM area)
        {
            /* 
            area = new AreaVM
            {
                IdArea = 1,
                Descrition = "",
                Name = "Laboratorio B",
                Status = false
            };
            /* */

            area.Status = !area.Status;

            JavaScriptSerializer JSS = new JavaScriptSerializer();

            string site = "Update";
            var wr = (HttpWebRequest)WebRequest.Create(connection + site);
            wr.Method = "POST";
            wr.ContentType = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(wr.GetRequestStream()))
            {
                string json = JSS.Serialize(area);

                streamWriter.Write(json);
                streamWriter.Flush();
            }
            var httpResponse = (HttpWebResponse)wr.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string responseText = streamReader.ReadToEnd();

                responseText = JsonConvert.DeserializeObject<string>(responseText);
                
                return Json(responseText, JsonRequestBehavior.AllowGet);
            }

        }
    }
}