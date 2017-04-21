using RAC.DAL.Entity;
using RAC.DAL.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace RAC.DAL.Controllers
{
    public class AreasController : ApiController
    {
        private RACEntities db = new RACEntities();
        
        /// <summary>
        /// Method for pasing Iquerable to List of Area vm
        /// </summary>
        /// <param name="usersDb"></param>
        /// <returns></returns>
        private List<AreaVM> ToVM(IQueryable<Area> areasDb)
        {

            List<AreaVM> areasVm;
            areasVm = areasDb.Select(x => new AreaVM
            {
                IdArea = x.IdArea,
                Descrition =  x.Descrition,
                Name = x.Name,
                Status = x.Status
            }).ToList();
            return areasVm;
        }

        /// <summary>
        /// Method for pasing Area entity to of ARea vm
        /// </summary>
        /// <param name="usersDb"></param>
        /// <returns></returns>
        private AreaVM ToVM(Area areaDb)
        {

            AreaVM areaVm;
            areaVm = new AreaVM
            {
                IdArea = areaDb.IdArea,
                Descrition = areaDb.Descrition,
                Name = areaDb.Name,
                Status = areaDb.Status
            };
            return areaVm;
        }
        /// <summary>
        /// Method for pasing Area entity to of Area vm
        /// </summary>
        /// <param name="usersDb"></param>
        /// <returns></returns>
        private Area ToEntity(AreaVM areaVm)
        {

            Area areaDb;
            areaDb = new Area
            {
                IdArea = areaVm.IdArea,
                Descrition = areaVm.Descrition,
                Name = areaVm.Name,
                Status = areaVm.Status
            };
            return areaDb;
        }

        // GET: api/Areas
        [ResponseType(typeof(List<AreaVM>))]
        public IHttpActionResult GetAreas()
        {
            IQueryable<Area> areaDb = db.Areas;
            List<AreaVM> areaVm = ToVM(areaDb);
            return Ok(areaVm);
        }

        // GET: api/Areas/5
        [ResponseType(typeof(AreaVM))]
        public IHttpActionResult GetArea(int id)
        {
            Area areaDb = db.Areas.Find(id);
            if (areaDb == null)
            {
                return NotFound();
            }
            AreaVM areaVm = ToVM(areaDb);
            return Ok(areaVm);
        }

        // PUT: api/Areas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArea(int id, AreaVM areaVm)
        {
            Area areaDb = ToEntity(areaVm);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != areaDb.IdArea)
            {
                return BadRequest();
            }

            db.Entry(areaDb).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AreaExists(int id)
        {
            return db.Areas.Count(e => e.IdArea == id) > 0;
        }
    }
}