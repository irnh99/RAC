using RAC.DAL.Entity;
using RAC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace RAC.DAL.Controllers
{
    public class AreasController : Controller
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
                Descrition = x.Descrition,
                Name = x.Name,
                Status = x.Status,
                HasAccess = x.HasAccesses.Select(y => new HasAccessVM()
                {
                    IdUserType = y.UserType.IdUserType
                }).ToList()
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
                Status = areaDb.Status,
                HasAccess = areaDb.HasAccesses.Select(y => new HasAccessVM()
                {
                    IdUserType = y.UserType.IdUserType
                }).ToList()
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
                Status = areaVm.Status,
                HasAccesses = areaVm.HasAccess.Select(y => new HasAccess()
                {
                    IdUserType = y.IdUserType
                }).ToList()
            };
            return areaDb;
        }

        // GET: Areas
        [HttpGet]
        public JsonResult Index()
        {
            IQueryable<Area> areaDb = db.Areas
                .Include(x => x.HasAccesses);
            List<AreaVM> areaVm = ToVM(areaDb);
            return Json(areaVm, JsonRequestBehavior.AllowGet);
        }

        // GET: Areas/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            Area areaDb = db.Areas.Find(id);
            if (areaDb == null)
            {
                return Json("Area doesn't exit");
            }
            AreaVM areaVm = ToVM(areaDb);
            return Json(areaVm, JsonRequestBehavior.AllowGet);
        }

        // GET: Areas/Create
        [HttpPost]
        public ActionResult Update(AreaVM areaVm)
        {
            Area areaDb = ToEntity(areaVm);
            if (!ModelState.IsValid)
            {
                return Json("Bad Request");
            }

            areaDb.HasAccesses = null;
            db.Areas.Attach(areaDb);
            db.Entry(areaDb).Property(x => x.Status).IsModified = true;

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                if (!AreaExists(areaVm.IdArea))
                {
                    return Json("Area not found");
                }
                else
                {
                    throw;
                }
            }

            return Json("Success");
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
