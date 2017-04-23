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
    public class AccessesController : ApiController
    {
        private RACEntities db = new RACEntities();

        /// <summary>
        /// Method for pasing Iquerable to List of Area vm
        /// </summary>
        /// <param name="usersDb"></param>
        /// <returns></returns>
        private List<AccessVM> ToVM(IQueryable<Access> areasDb)
        {

            List<AccessVM> areasVm;
            areasVm = areasDb.Select(x => new AccessVM
            {
                Area = new AreaVM
                {
                    Descrition  = x.Area.Descrition,
                    IdArea = x.Area.IdArea,
                    Name = x.Area.Name,
                    Status = x.Area.Status
                },
                Date = x.Date,
                IdAccess = x.IdAccess,
                 User = new UserVM
                 {
                     IdUser = x.User.IdUser,
                     Name =  x.User.Name,
                     NoControl = x.User.NoControl,
                     Pass = x.User.Pass,
                     UserName = x.User.UserName,
                     UserType = new UserTypeVM
                     {
                         IdUserType = x.User.UserType.IdUserType,
                         Description =  x.User.UserType.Description
                     }
                 }
            }).ToList();
            return areasVm;
        }

        /// <summary>
        /// Method for pasing Area entity to of ARea vm
        /// </summary>
        /// <param name="usersDb"></param>
        /// <returns></returns>
        private AccessVM ToVM(Access accessDb)
        {

            AccessVM accessVm;
            accessVm = new AccessVM
            {
                Area = new AreaVM
                {
                    Descrition = accessDb.Area.Descrition,
                    IdArea = accessDb.Area.IdArea,
                    Name = accessDb.Area.Name,
                    Status = accessDb.Area.Status
                },
                Date = accessDb.Date,
                IdAccess = accessDb.IdAccess,
                User = new UserVM
                {
                    IdUser = accessDb.User.IdUser,
                    Name = accessDb.User.Name,
                    NoControl = accessDb.User.NoControl,
                    Pass = accessDb.User.Pass,
                    UserName = accessDb.User.UserName,
                    UserType = new UserTypeVM
                    {
                        IdUserType = accessDb.User.UserType.IdUserType,
                        Description = accessDb.User.UserType.Description
                    }
                }
            };
            return accessVm;
        }
        /// <summary>
        /// Method for pasing Area entity to of Area vm
        /// </summary>
        /// <param name="usersDb"></param>
        /// <returns></returns>
        private Access ToEntity(AccessVM accessVm)
        {

            Access accessDb;
            accessDb = new Access
            {
                Area = new Area
                {
                    Descrition = accessVm.Area.Descrition,
                    IdArea = accessVm.Area.IdArea,
                    Name = accessVm.Area.Name,
                    Status = accessVm.Area.Status
                },
                Date = accessVm.Date,
                IdAccess = accessVm.IdAccess,
                User = new User
                {
                    IdUser = accessVm.User.IdUser,
                    Name = accessVm.User.Name,
                    NoControl = accessVm.User.NoControl,
                    Pass = accessVm.User.Pass,
                    UserName = accessVm.User.UserName,
                    UserType = new UserType
                    {
                        IdUserType = accessVm.User.UserType.IdUserType,
                        Description = accessVm.User.UserType.Description
                    }
                }
            };
            return accessDb;
        }

        // GET: api/Accesses
        [ResponseType(typeof(List<AccessVM>))]
        public IHttpActionResult GetAccesses()
        {
            IQueryable<Access> accessDb = db.Accesses;
            List<AccessVM> accessVm = ToVM(accessDb);
            return Ok(accessVm);
        }

        // POST: api/Accesses
        [ResponseType(typeof(AccessVM))]
        public IHttpActionResult PostAccess(AccessVM accessVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Access accessDb = ToEntity(accessVm);

            db.Accesses.Add(accessDb);
            db.SaveChanges();

            accessVm = ToVM(accessDb);

            return CreatedAtRoute("DefaultApi", new { id = accessDb.IdAccess }, accessVm);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccessExists(int id)
        {
            return db.Accesses.Count(e => e.IdAccess == id) > 0;
        }
    }
}