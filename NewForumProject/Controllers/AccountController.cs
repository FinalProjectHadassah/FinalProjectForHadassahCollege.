using NewForumProject.DAL;
using NewForumProject.DAL.NewForumProject;
using NewForumProject.Models;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace NewForumProject.Controllers
{

    public class AccountController : BaseController
    {
        // It's here to make logging possible.
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly DataContext _dataContext;

        public AccountController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        //
        // GET: /Account/
        public ActionResult Login()
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    logger.Info("User: " + User.FirstName + " " + User.LastName + " got into Login page.");
            //}
            //else
            //{
            logger.Info("Unregistered user got into Login page.");
            //}
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var myPassword = model.Password;
                var user = _dataContext.Users.FirstOrDefault(u => u.Username == model.Username);
                if (user != null && user.Salt != null)
                {
                    int mySalt = user.Salt.Value;
                    Password pwd = new Password(myPassword, mySalt);
                    string strHashedPassword = pwd.ComputeSaltedHash();
                    var passCheck = slowEquals(user.Password, strHashedPassword);

                    if (passCheck)
                    {
                        var roles = user.Roles.Select(m => m.RoleName).ToArray();

                        CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel
                        {
                            UserID = user.UserID,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            roles = roles
                        };

                        string userData = JsonConvert.SerializeObject(serializeModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                            1,
                            user.Email,
                            DateTime.Now,
                            DateTime.Now.AddMinutes(15),
                            false,
                            userData);

                        string encTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                        Response.Cookies.Add(faCookie);

                        if (roles.Contains("Admin"))
                        {
                            logger.Info("Administratior: " + user.FirstName + " " + user.LastName + " Loged In.");
                            return RedirectToAction("Index", "Admin");
                        }
                        else if (roles.Contains("User"))
                        {
                            logger.Info("User: " + user.FirstName + " " + user.LastName + " Loged In.");
                            return RedirectToAction("Index", "User");
                        }
                        else
                        {
                            //if (User.Identity.IsAuthenticated)
                            //{
                            //    logger.Info("User: " + User.FirstName + " " + User.LastName + " was unsuccessfully trying to relogin as: " + model.Username + ".");
                            //}
                            //else
                            //{
                            logger.Info("Unregistered user was unsuccessfully trying to relogin as: " + model.Username + ".");
                            //}
                            return RedirectToAction("Login", "Account", null);
                        }
                    }
                }
                ModelState.AddModelError("", "Incorrect username and/or password.");
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            Response.Cookies.Clear();
            FormsAuthentication.SignOut();
            HttpCookie c = new HttpCookie("login") { Expires = DateTime.Now.AddDays(-1) };
            Response.Cookies.Add(c);
            Session.Clear();
            return RedirectToAction("Login", "Account", null);
        }

        [HttpGet]
        public ActionResult Register()
        {
            var academies = _dataContext.Academies.ToList();
            ViewBag.academieslist = (academies.Select(a => new SelectListItem
            {
                Value = a.AcademyID.ToString(),
                Text = a.AcademyName
            })).ToList();
            return View(new RegisterViewModel());
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var myPassword = model.Password;
                int mySalt = Password.CreateRandomSalt();
                Password pwd = new Password(myPassword, mySalt);
                string strHashedPassword = pwd.ComputeSaltedHash();
                using (DataContext dc = new DataContext())
                {
                    var user = new User()
                    {
                        Username = model.Username,
                        CreateDate = DateTime.Now,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        IsActive = true,
                        Password = strHashedPassword,
                        Salt = mySalt,
                        Roles = new List<Role> { new Role { RoleName = "User" } },
                        AcademyID = model.AcademyID

                    };
                    if (dc.Users.Any(e => e.Username == user.Username))
                    {
                        ModelState.AddModelError("", "User With this Username already exists.");
                    }
                    if (dc.Users.Any(e => e.Email == user.Email))
                    {
                        ModelState.AddModelError("", "User With this Email Address already exists.");
                    }
                    if (ModelState.IsValid)
                    {
                        dc.Users.Add(user);
                        ModelState.Clear();
                        ViewBag.Message = "Successfully Registration Done";
                        dc.SaveChanges();

                        var roles = user.Roles.Select(m => m.RoleName).ToArray();

                        CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel
                        {
                            UserID = user.UserID,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            roles = roles
                        };

                        string userData = JsonConvert.SerializeObject(serializeModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                 1,
                                 user.Email,
                                 DateTime.Now,
                                 DateTime.Now.AddMinutes(15),
                                 false,
                                 userData);

                        string encTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                        Response.Cookies.Add(faCookie);
                        return RedirectToAction("Index", "User");
                    }
                }
            }

            return View(model);
        }
        private static bool slowEquals(string str1, string str2)
        {
            byte[] a = new byte[str1.Length * sizeof(char)];
            System.Buffer.BlockCopy(str1.ToCharArray(), 0, a, 0, a.Length);
            byte[] b = new byte[str2.Length * sizeof(char)];
            System.Buffer.BlockCopy(str2.ToCharArray(), 0, b, 0, b.Length);
            int diff = a.Length ^ b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}




// var result = await UserManager.CreateAsync(user, model.Password);

//var roleStore = new RoleStore<IdentityRole>(context);
//var roleManager = new RoleManager<IdentityRole>(roleStore);

//var userStore = new UserStore<ApplicationUser>(context);
// var userManager = new UserManager<ApplicationUser>(userStore);
//userManager.AddToRole(user.Id, "User");

//if (result.Succeeded)
//{
//   await SignInAsync(user, isPersistent: false);
//    return RedirectToAction("Index", "Home");
//}
// else
//{
//    AddErrors(result);
//}