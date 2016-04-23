using NewForumProject.DAL.NewForumProject;
using NewForumProject.Models;
using NewForumProject.Repositories;
using NLog;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;

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
            return View();
        }
        [HttpGet]
        public ActionResult Search(string searchSubject)
        {

            logger.Info("User gets the results of course basic info search");
            var subjects = repository.FindSubjectByName(searchSubject);
            return View(subjects);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadPhoto(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var userid = User.UserID;
                var user = repository.GetUserById(userid);
                repository.DeleteAllAvatarsFromUser(userid);

                if (file != null && file.ContentLength > 0)
                {
                    var avatar = new Picture
                    {
                        User = user,
                        PictureName = System.IO.Path.GetFileName(file.FileName),
                        Type = FileType.Avatar,
                        ContentType = file.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(file.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(file.ContentLength);
                    }
                    repository.SavePicture(avatar, userid);
                }
                return RedirectToAction("Index", "User");
            }

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

        [Authorize]
        public ActionResult GetPhoto()
        {
            string user = Session["UserName"] as string;
            var userId = User.UserID;
            var photo = repository.GetPhoto(userId);
            return File(photo, "image/jpeg");
        }
    }
}