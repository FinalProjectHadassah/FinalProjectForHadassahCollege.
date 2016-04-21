using NewForumProject.DAL.NewForumProject;
using System.Web.Mvc;

namespace NewForumProject.Controllers
{
    //[CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
    // [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
    [CustomAuthorize(Roles = "Admin")]
    // [CustomAuthorize(Users = "1")]
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/
        [Authorize]
        public ActionResult Index()
        {
            string FullName = User.FirstName + " " + User.LastName;
            return View();
        }

    }
}