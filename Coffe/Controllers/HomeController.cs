using Coffe.Dal;
using Coffe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coffe.Controllers
{
    public class HomeController : Controller
    {
        private UserDal db = new UserDal();
        private ItemsDal pd = new ItemsDal();
        public ActionResult Index()
        {
            return View(pd.Items.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                User a = db.Users.Where(x => x.Username == user.Username).FirstOrDefault();
                if (a != null)
                {
                    ViewBag.massage = "Username already exist!";
                    return View("Register");
                }
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View("Register");
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult AdminPage()
        {
            
            return View("adminPage",pd.Items.ToList());
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return View("Index",pd.Items.ToList());
        }



        public ActionResult SubmitLogin(User u) 
        {
            User user = db.Users.Where(x => x.Username == u.Username && x.Password == u.Password).FirstOrDefault();
            
            if(user == null)
            {
                ViewBag.massage = "Password or username is not correct.";
                return View("Login");
            }
            
            else if (user.isAdmin)
            {
                Session["name"] = u.Username;
                Session["id"] = user.Uid;
                return View("AdminPage", pd.Items.ToList().OrderBy(x =>x.price).Reverse());
            }
            else if (user.isBarista)
            {
                Session["name"] = u.Username;
                Session["id"] = user.Uid;
                return View("Index", pd.Items.ToList());
            }
            Session["name"] = u.Username;
            Session["id"] = user.Uid;
            return View("Index", pd.Items.ToList());
        }

    }


}