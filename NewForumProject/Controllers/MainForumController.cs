using NewForumProject.DAL;
using NewForumProject.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace NewForumProject.Controllers
{
    public class MainForumController : Controller
    {
        private DataContext db = new DataContext();

        // GET: MainForum
        public ActionResult Index()
        {
            var topics = db.Topics.Include(t => t.Category).Include(t => t.LastPost).Include(t => t.Poll).Include(t => t.User);
            return View(topics.ToList());
        }

        // GET: MainForum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: MainForum/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.LastPostID = new SelectList(db.Posts, "PostID", "PostContent");
            ViewBag.PollID = new SelectList(db.Polls, "PollID", "PollID");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username");
            return View();
        }

        // POST: MainForum/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TopicID,Name,CreateDate,Solved,SolvedReminderSent,Slug,Views,IsSticky,IsLocked,LastPostID,CategoryID,UserID,PollID,Pending")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Topics.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", topic.CategoryID);
            ViewBag.LastPostID = new SelectList(db.Posts, "PostID", "PostContent", topic.LastPostID);
            ViewBag.PollID = new SelectList(db.Polls, "PollID", "PollID", topic.PollID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", topic.UserID);
            return View(topic);
        }

        // GET: MainForum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", topic.CategoryID);
            ViewBag.LastPostID = new SelectList(db.Posts, "PostID", "PostContent", topic.LastPostID);
            ViewBag.PollID = new SelectList(db.Polls, "PollID", "PollID", topic.PollID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", topic.UserID);
            return View(topic);
        }

        // POST: MainForum/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TopicID,Name,CreateDate,Solved,SolvedReminderSent,Slug,Views,IsSticky,IsLocked,LastPostID,CategoryID,UserID,PollID,Pending")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", topic.CategoryID);
            ViewBag.LastPostID = new SelectList(db.Posts, "PostID", "PostContent", topic.LastPostID);
            ViewBag.PollID = new SelectList(db.Polls, "PollID", "PollID", topic.PollID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", topic.UserID);
            return View(topic);
        }

        // GET: MainForum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: MainForum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Topic topic = db.Topics.Find(id);
            db.Topics.Remove(topic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ListMainCategories()
        {
            return View();
        }
    }
}
