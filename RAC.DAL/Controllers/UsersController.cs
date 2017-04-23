using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RAC.DAL.Entity;
using RAC.DAL.Models;
using System.Data.Entity;

namespace RAC.DAL.Controllers
{
    public class UsersController : Controller
    {

        private RACEntities db = new RACEntities();

        /// <summary>
        /// Method for pasing Iquerable to List of user vm
        /// </summary>
        /// <param name="usersDb"></param>
        /// <returns></returns>
        private List<UserVM> ToVM(IQueryable<User> usersDb)
        {

            List<UserVM> users;
            users = usersDb.Select(x => new UserVM
            {
                IdUser = x.IdUser,
                Name = x.Name,
                NoControl = x.NoControl,
                Pass = x.Pass,
                UserName = x.UserName,
                UserType = new UserTypeVM
                {
                    IdUserType = x.UserType.IdUserType,
                    Description = x.UserType.Description
                }
            }).ToList();
            return users;
        }

        /// <summary>
        /// Method for pasing user entity to of user vm
        /// </summary>
        /// <param name="usersDb"></param>
        /// <returns></returns>
        private UserVM ToVM(User usersDb)
        {

            UserVM users;
            users = new UserVM
            {
                IdUser = usersDb.IdUser,
                Name = usersDb.Name,
                NoControl = usersDb.NoControl,
                Pass = usersDb.Pass,
                UserName = usersDb.UserName,
                UserType = new UserTypeVM
                {
                    IdUserType = usersDb.UserType.IdUserType,
                    Description = usersDb.UserType.Description
                }
            };
            return users;
        }
        /// <summary>
        /// Method for pasing user entity to of user vm
        /// </summary>
        /// <param name="usersDb"></param>
        /// <returns></returns>
        private User ToEntity(UserVM usersVm)
        {

            User users;
            users = new User
            {
                IdUser = usersVm.IdUser,
                Name = usersVm.Name,
                NoControl = usersVm.NoControl,
                Pass = usersVm.Pass,
                UserName = usersVm.UserName,
                UserType = new UserType
                {
                    IdUserType = usersVm.UserType.IdUserType,
                    Description = usersVm.UserType.Description
                }
            };
            return users;
        }

        // GET: Users
        [HttpGet]
        public JsonResult Index()
        {
            List<UserVM> users;
            IQueryable<User> usersDb = db.Users
                .Include(x => x.UserType);

            users = ToVM(usersDb);

            return Json(users, JsonRequestBehavior.AllowGet);
        }

        // GET: Users/Details/5
        [HttpPost]
        public JsonResult LogIn(string username, string password)
        {
            User user = db.Users.FirstOrDefault(x => x.UserName == username && x.Pass == password);
            if (user == null)
            {
                return Json("Incorrect user or password");
            }
            UserVM userVm = ToVM(user);
            return Json(userVm);
        }

        // GET: Users/Create
        [HttpPut]
        public JsonResult Update(int id, UserVM user)
        {
            User userDb = ToEntity(user);
            if (!ModelState.IsValid)
            {
                return Json("Bad request");
            }

            if (id != userDb.IdUser)
            {
                return Json("Bad request");
            }

            db.Entry(userDb).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                if (!UserExists(id))
                {
                    return Json("User not found");
                }
                else
                {
                    throw;
                }
            }

            return Json("Success");
        }
        
        // POST: api/Users
        [HttpPost]
        public JsonResult PostUser(UserVM user)
        {
            User userDb = ToEntity(user);
            if (!ModelState.IsValid)
            {
                return Json("Bad request");
            }

            db.Users.Add(userDb);
            db.SaveChanges();

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

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.IdUser == id) > 0;
        }
    }
}
