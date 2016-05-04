using NewForumProject.DAL.NewForumProject;
using NewForumProject.Models;
using NewForumProject.Repositories;
using NLog;
using System.IO;
using System.Web;
using System.Web.Mvc;
using User = NewForumProject.Models.User;

namespace NewForumProject.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
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
                byte[] resizedImage = new byte[0];
                var jpeg = new byte[] { 255, 216, 255, 224 }; // jpeg
                if (file.ContentLength > 0)
                {
                    var filename = Path.GetFileName(file.FileName);
                    //using (var reader = new System.IO.BinaryReader(file.InputStream))
                    //{
                    //    if (!jpeg.SequenceEqual(reader.ReadBytes(file.ContentLength).Take(jpeg.Length)))
                    //    {
                    //        //file is not jpeg
                    //        return RedirectToAction("Index", "User");
                    //    }
                    //}
                    try
                    {
                        System.Drawing.Image sourceimage = System.Drawing.Image.FromStream(file.InputStream);
                        var BitmapResizedImage = ProjectTools.ResizeImage(sourceimage, 50, 50);
                        resizedImage = ProjectTools.imageToByteArray(BitmapResizedImage);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = ex.Message;
                        return this.View();
                    }

                }

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
                    avatar.Content = resizedImage;
                    repository.SavePicture(avatar, userid);
                }
                return RedirectToAction("Index", "User");
            }

            return RedirectToAction("Index", "User");
        }


        public ActionResult ManualSignToCourse()
        {
            SearchSubjectViewModel model = new SearchSubjectViewModel();
            ViewBag.AcademyID = new SelectList(repository.GetAllAcademies(), "AcademyID", "AcademyName", model.AcademyID);
            ViewBag.SubjectID = new SelectList(repository.GetAllSubjects(), "SubjectID", "SubjectName", model.SubjectID);
            return View(model);
        }


        //[HttpPost]
        public ActionResult SignToCourse(SearchSubjectViewModel model)
        {

            if (ModelState.IsValid)
            {
                var userId = User.UserID;
                repository.SignUserToSubject(model, userId);
            }

            return RedirectToAction("Courses", "User");
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
            string imageFile = System.Web.HttpContext.Current.Server.MapPath("~/Content/user-default-profile.jpg");
            if (User != null)
            {
                var userId = User.UserID;
                var photo = repository.GetPhoto(userId);
                if (photo != null && photo.Length > 0)
                {
                    return File(photo, "image/jpeg");
                }
            }
            return File(imageFile, "image/jpeg");
        }

        [HttpGet]
        public ActionResult EditUser(int? id)
        {
            User user = new User();
            if (id.HasValue)
            {
                user = (User)this.repository.GetUserById(id.Value);
            }
            else if (User != null)
            {
                user = (User)this.repository.GetUserById(User.UserID);
            }
            else
            {
                return HttpNotFound(); // change to some reasonable error
            }
            var model = new UserEditUserViewModel
            {
                UserID = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                AcademyID = user.AcademyID,
                Username = user.Username
            };
            ViewBag.AcademyID = new SelectList(repository.GetAllAcademies(), "AcademyID", "AcademyName", user.AcademyID);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditUser(UserEditUserViewModel model, HttpPostedFileBase file)
        {

            // ModelState.AddModelError(string.Empty, "An image file must be chosen.");

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    // Upload the file to Azure Blob Storage
                    bool savedComplete = await Task.Run(
                        () =>
                        {
                            var userid = User.UserID;
                            var user = repository.GetUserById(userid);
                            byte[] resizedImage = new byte[0];
                            var jpeg = new byte[] { 255, 216, 255, 224 }; // jpeg
                            if (file.ContentLength > 0)
                            {
                                var filename = Path.GetFileName(file.FileName);
                                try
                                {
                                    System.Drawing.Image sourceimage =
                                        System.Drawing.Image.FromStream(file.InputStream);
                                    var BitmapResizedImage = ProjectTools.ResizeImage(sourceimage, 256, 256);
                                    resizedImage = ProjectTools.imageToByteArray(BitmapResizedImage);
                                }
                                catch (Exception ex)
                                {
                                    ViewBag.ErrorMessage = ex.Message;
                                    ModelState.AddModelError(string.Empty, "An image file must be chosen.");
                                    return false;
                                }

                            }

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
                                avatar.Content = resizedImage;
                                repository.SavePicture(avatar, userid);
                            }
                            return true;

                        });
                }
                if (User == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                repository.UserEditUser(model);
                User.FirstName = model.FirstName;
                User.LastName = model.LastName;
                return RedirectToAction("Index");
            }
            ViewBag.AcademyID = new SelectList(repository.GetAllAcademies(), "AcademyID", "AcademyName", model.AcademyID);
            return View(model);
        }

        public ActionResult DeleteCourse()
        {
            return View();
        }
    }
}