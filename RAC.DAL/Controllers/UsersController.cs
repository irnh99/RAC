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
    public class UsersController : ApiController
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
            users =  new UserVM
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

        // GET: api/Users
        [ResponseType(typeof(List<UserVM>))]
        public IHttpActionResult GetUsers()
        {
            List<UserVM> users;
            IQueryable<User> usersDb = db.Users.Include(x => x.UserType);

            users = ToVM(usersDb);

            return Ok(users);
        }

        /// <summary>
        /// post method for log in
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(UserVM))]
        public IHttpActionResult PostUser(string username, string password)
        {
            User user = db.Users.FirstOrDefault(x => x.UserName == username && x.Pass == password);
            if (user == null)
            {
                return NotFound();
            }
            UserVM userVm = ToVM(user);
            return Ok(userVm);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, UserVM user)
        {
            User userDb = ToEntity(user);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userDb.IdUser)
            {
                return BadRequest();
            }

            db.Entry(userDb).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(UserVM))]
        public IHttpActionResult PostUser(UserVM user)
        {
            User userDb = ToEntity(user);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(userDb);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.IdUser }, user);
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