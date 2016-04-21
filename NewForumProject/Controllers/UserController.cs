using NewForumProject.DAL.NewForumProject;
using NewForumProject.Models;
using NewForumProject.Repositories;
using NLog;
using System;
using System.Web;
using System.Web.Mvc;

namespace NewForumProject.Controllers
{

    // [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
    // [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
    [CustomAuthorize(Roles = "User")]
    // [CustomAuthorize(Users = "1,2")]
    public class UserController : BaseController
    {

        private DataContextRepository repository;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public UserController(DataContextRepository repository)
        {
            this.repository = repository;
        }
        //
        // GET: /User/
        [Authorize]
        public ActionResult Index()
        {
            string fullName = User != null ? User.FirstName + " " + User.LastName : "You are not supposed to be here";
            return View();
        }

        public ActionResult edit()
        {
            //string fullName = User != null ? User.FirstName + " " + User.LastName : "You are not supposed to be here";
            return View();
        }
        [HttpGet]
        public ActionResult Search(string searchSubject)
        {

            logger.Info("User gets the results of course basic info search");
            var subjects = repository.FindSubjectByName(searchSubject);
            return View(subjects);
        }
        public FileContentResult Photo(int userId)
        {
            // get EF Database (maybe different way in your applicaiton)
            var photo = repository.GetUserPicById(userId);
            return photo;

            //return File("C:/Users/Home/Dropbox/Security-Role-UserBased/NewForumProject/Content/user-default-profile.jpg", "image/jpeg");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadPhoto(HttpPostedFileBase Profile)
        {
            // get EF Database (maybe different way in your applicaiton)


            // find the user. I am skipping validations and other checks.
            var userid = User.UserID;
            var user = repository.GetUserById(userid);

            // convert image stream to byte array
            byte[] image = new byte[Profile.ContentLength];
            Profile.InputStream.Read(image, 0, Convert.ToInt32(Profile.ContentLength));

            user.ProfilePicture = image;

            // save changes to database
            repository.SaveChanges();

            return RedirectToAction("Index", "User");
        }

        //[HttpPost]
        public ActionResult SignToCourse(Subject subject)
        {
            var userid = User.UserID;
            repository.SignUserToSubject(subject, userid);
            return View();

        }
        public ActionResult Courses()
        {
            var userid = User.UserID;
            var subjects = repository.GetUserSubjectsById(userid);
            return View(subjects);
        }
    }
}