using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomTracker.Models;
using CustomTracker.Models.Abstract;
using System.Text.RegularExpressions;

namespace CustomTracker.Controllers
{
    public class CustomController : Controller
    {
        private IRepository repository;
        private IQueryProcessor queryProc;
        public CustomController(IRepository repo, IQueryProcessor proc)
        {
            repository = repo;
            queryProc = proc;
        }
        // GET: Custom
        [HttpPost]
        public ActionResult CreateQuery(Tickets ticket)
        {
            
            //RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrect email")]
            if (ticket.Email == null || !Regex.IsMatch(ticket.Email, @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"))
                ModelState.AddModelError("","Incorrect email");
            if (ticket.Subject == null)
                ModelState.AddModelError("", "Enter a subject, please");
            if (ticket.Body == null)
                ModelState.AddModelError("", "Enter a query, please");
            if (ModelState.IsValid)
            {
                
                
                ticket.DataMail = DateTime.Today;
                ticket.Reference = repository.SaveTicket(ticket);
                queryProc.ProcessQuery(ticket,null);
                return RedirectToAction("Index","Home");
            }
            Tickets queryModel = new Tickets();
            queryModel.listDepartment = new SelectList(repository.Departments.OrderBy(x => x.Id), "Id", "Department");
            return View(queryModel);
        }
        [HttpGet]
        public ActionResult CreateQuery()
        {
            Tickets ticket = new Tickets();
            ticket.listDepartment = new SelectList(repository.Departments.OrderBy(x => x.Id),"Id","Department");
            return View(ticket);
        }
        public ActionResult Index()
        {
            return View();
        }
       
    }
}