using NewForumProject.Models;
using NLog;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace NewForumProject.Controllers
{
    using NewForumProject.Repositories;
    using System.Collections.Generic;

    public class UsersController : Controller
    {
        private DataContextRepository repository;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public UsersController(DataContextRepository repository)
        {
            this.repository = repository;
        }

        // GET: Users
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            logger.Info("You have visited the Index view");
            var users = repository.GetAllUsers();
            //db.Users.Include(u => u.Academy);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = repository.GetUserById(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            logger.Info("Entered to Details Section.");
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.AcademyID = new SelectList(repository.GetAllAcademies(), "AcademyID", "AcademyName");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Username,Email,Password,FirstName,LastName,IsActive,CreateDate,AcademyID,Salt")] User user)
        {
            if (ModelState.IsValid)
            {
                repository.AddUser(user);
                return RedirectToAction("Index");
            }

            ViewBag.AcademyID = new SelectList(repository.GetAllAcademies(), "AcademyID", "AcademyName", user.AcademyID);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = repository.GetUserById(id.Value);

            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.AcademyID = new SelectList(repository.GetAllAcademies(), "AcademyID", "AcademyName", user.AcademyID);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Username,Email,FirstName,LastName,IsActive,CreateDate,AcademyID")] User user)
        {
            if (ModelState.IsValid)
            {
                if (user == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                repository.EditUser(user);
                return RedirectToAction("Index");
            }
            ViewBag.AcademyID = new SelectList(repository.GetAllAcademies(), "AcademyID", "AcademyName", user.AcademyID);
            return View(user);
        }

        //// GET: Users/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user = repository.GetUserById(id.Value);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}

        //// POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    User user = db.Users.Find(id);
        //    db.Users.Remove(user);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}


        public ActionResult Search(string Name)
        {
            if (User.Identity.IsAuthenticated)
            {
                logger.Info("Registrered User: " + User.Identity.Name + ", searched for:" + Name + ".");
            }
            else
            {
                logger.Info("Unregistered user was searching for: " + Name);
            }
            if (Request.IsAjaxRequest())
            {
                var content = this.repository.SearchUsers(Name);
                return this.PartialView("_SearchedUsersListPartial", content);
            }
            return this.View();
        }
    }
}
