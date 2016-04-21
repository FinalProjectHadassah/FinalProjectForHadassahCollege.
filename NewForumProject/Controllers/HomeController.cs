using NewForumProject.Repositories;
using NLog;
using System.Web.Mvc;

namespace NewForumProject.Controllers
{
    public class HomeController : BaseController
    {
        private DataContextRepository repository;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public HomeController(DataContextRepository repository)
        {
            this.repository = repository;
        }

        //
        // GET: /Home/
        public ActionResult Index()
        {

            logger.Info("You have visited the Index view");
            return View();
        }
        public ActionResult contact()
        {

            logger.Info("You have visited the Index view");
            return View();
        }
        //[HttpGet]
        //public ActionResult SearchCourse()
        //{

        //    logger.Info("User opened the search course page");

        //    return View();
        //}
        [HttpGet]
        public ActionResult SearchCourse(string searchSubject)
        {

            logger.Info("User gets the results of course basic info search");
            var subjects = repository.FindSubjectByName(searchSubject);
            return View(subjects);
        }


    }
}