using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CustomTracker.Models.Abstract;
using CustomTracker.Models;

namespace CustomTracker.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private IRepository repository;
        public HomeController(IRepository repo)
        {
            repository = repo;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session["UserName"] = null;
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult LogIn(string name, string password)
        {

            Users repo = repository.Users.FirstOrDefault(x => x.Name == name && x.Password == password);

            if (repo != null)
            {
                if (HttpContext != null)
                    Session["UserName"] = name;

                ViewBag.UserName = name;
                return RedirectToAction("ViewTickets", "Staff");
            }
            return View(new LogOnModel {Name = name, Password = password });
        }
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

    }
}