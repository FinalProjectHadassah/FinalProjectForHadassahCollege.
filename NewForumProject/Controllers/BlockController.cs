using NewForumProject.DAL;
using NewForumProject.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace NewForumProject.Controllers
{
    [Authorize]
    public class BlockController : BaseController
    {
        private readonly DataContext _db;
        public BlockController(DataContext dataContext)
        {
            _db = dataContext;
        }
        //
        // GET: /Block/

        public ActionResult Index()
        {
            var block = _db.Blocks.Include(b => b.BlockerUser).Include(b => b.BlockedUser);
            return View(block.ToList());
        }

        //
        // GET: /Block/Details/5

        public ActionResult Details(int id = 0)
        {
            Block block = _db.Blocks.Find(id);
            if (block == null)
            {
                return HttpNotFound();
            }
            return View(block);
        }

        //
        // GET: /Block/Create

        public ActionResult Create()
        {
            ViewBag.BlockerUserID = new SelectList(_db.Users, "UserID", "Username");
            ViewBag.BlockedUserID = new SelectList(_db.Users, "UserID", "Username");
            return View();
        }

        //
        // POST: /Block/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlockViewModel blockVM)
        {
            Block block = new Block();
            if (ModelState.IsValid)
            {
                block = new Block
                {
                    Date = DateTime.Now,
                    BlockerUserID = _db.Users.ToList<User>().FirstOrDefault(e => e.Email == User.Identity.Name).UserID,
                    BlockedUserID = blockVM.BlockedUserID
                };

                _db.Blocks.Add(block);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BlockerUserID = new SelectList(_db.Users, "UserID", "Username", block.BlockerUserID);
            ViewBag.BlockedUserID = new SelectList(_db.Users, "UserID", "Username", block.BlockedUserID);
            return View(block);
        }

        //
        // GET: /Block/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Block block = _db.Blocks.Find(id);
            if (block == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlockerUserID = new SelectList(_db.Users, "UserID", "Username", block.BlockerUserID);
            ViewBag.BlockedUserID = new SelectList(_db.Users, "UserID", "Username", block.BlockedUserID);
            return View(block);
        }

        //
        // POST: /Block/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Block block)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(block).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlockerUserID = new SelectList(_db.Users, "UserID", "Username", block.BlockerUserID);
            ViewBag.BlockedUserID = new SelectList(_db.Users, "UserID", "Username", block.BlockedUserID);
            return View(block);
        }

        //
        // GET: /Block/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Block block = _db.Blocks.Find(id);
            if (block == null)
            {
                return HttpNotFound();
            }
            return View(block);
        }

        //
        // POST: /Block/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Block block = _db.Blocks.Find(id);
            _db.Blocks.Remove(block);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}