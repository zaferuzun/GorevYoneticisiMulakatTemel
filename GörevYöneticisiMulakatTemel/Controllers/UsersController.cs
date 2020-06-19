using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GörevYöneticisiMulakatTemel.Models;
using GörevYöneticisiMulakatTemel.ViewModel;

namespace GörevYöneticisiMulakatTemel.Controllers
{
    public class UsersController : Controller
    {
        private DB_A633FB_gorevtakipEntities1 db = new DB_A633FB_gorevtakipEntities1();
        // POST: Users/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UsersVM G_users)
        {
            if (ModelState.IsValid)
            {
                //Users P_users = new Users();
                //P_users.UserName = G_users.UserName;
                //P_users.UserPassword = G_users.UserPassword;

                var obj = db.Users.Where(a => a.UserName == G_users.UserName && a.UserPassword == G_users.UserPassword).FirstOrDefault();
                if (obj != null)
                {
                    string r = "../Jobs/Index/" + obj.UserId.ToString();
                    return RedirectToAction(r);
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifreyi yanlış girdiniz.");
                }
            }
            return View(G_users);
        }

        // GET: Users/Register
        public ActionResult Register()
        {
            UsersVM P_users = new UsersVM();
            return View(P_users);
        }

        // POST: Users/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UsersVM G_users)
        {

            // Kullanıcı adı bulunmasındaki hata
            if (db.Users.Any(model => model.UserName == G_users.UserName))
            {
                ModelState.AddModelError("UserName", "Bu kullanıcı adı bulunmaktadır.");
            }
            if (ModelState.IsValid)
            {
                Users P_users = new Users();
                P_users.UserName = G_users.UserName;
                P_users.UserPassword = G_users.UserPassword;

                db.Users.Add(P_users);
                db.SaveChanges();
                string r = "../Users/Login";
                return RedirectToAction(r);
            }

            return View(G_users);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
