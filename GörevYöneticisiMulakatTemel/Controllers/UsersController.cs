using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GörevYöneticisiMulakatTemel.Models;
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
        public ActionResult Login(Users objUser)
        {
            if (ModelState.IsValid)
            {
                var obj = db.Users.Where(a => a.UserName == objUser.UserName && a.UserPassword == objUser.UserPassword).FirstOrDefault();
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
            return View(objUser);
        }

        // GET: Users/Register
        public ActionResult Register()
        {

            return View();
        }

        // POST: Users/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users users)
        {

            // Kullanıcı adı bulunmasındaki hata
            if (db.Users.Any(model => model.UserName == users.UserName))
            {
                ModelState.AddModelError("UserName", "Bu kullanıcı adı bulunmaktadır.");
            }
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                string r = "../Users/Login";
                return RedirectToAction(r);
            }

            return View(users);
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
