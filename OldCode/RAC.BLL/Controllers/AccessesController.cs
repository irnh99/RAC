using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace RAC.BLL.Controllers
{
    public class AccessesController : ApiController
    {
        string connection;
        public AccessesController()
        {
            connection = "http://ec2-54-149-159-249.us-west-2.compute.amazonaws.com/home";
        }

        // GET: api/Accesses
        [ResponseType(typeof(string))]
        public IHttpActionResult GetAccesses()
        {
            using (WebClient wb = new WebClient())
            {

                string site = "/checkmate";

                var response = wb.DownloadString(connection + site);

                return Ok(response);
            }
        }

        // POST: api/Accesses
        //[ResponseType(typeof(AccessVM))]
        //public IHttpActionResult PostAccess(AccessVM accessVm)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Access accessDb = ToEntity(accessVm);

        //    db.Accesses.Add(accessDb);
        //    db.SaveChanges();

        //    accessVm = ToVM(accessDb);

        //    return CreatedAtRoute("DefaultApi", new { id = accessDb.IdAccess }, accessVm);
        //}
    }
}
