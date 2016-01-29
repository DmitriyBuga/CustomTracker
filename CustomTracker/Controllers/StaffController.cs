using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomTracker.Models.Abstract;
using CustomTracker.Models;
using System.Net;
using CustomTracker.Models.Entities;

namespace CustomTracker.Controllers
{
    public class StaffController : Controller
    {
        public int PageSize = 10;

        IRepository repository;
        private IQueryProcessor queryProc;
        public StaffController(IRepository repo, IQueryProcessor proc)
        {
            repository = repo;
            queryProc = proc;
        }
        
        public string UpdateStatus(string statusName, string id)
        {

            if (id != null)
            {
                int nId = Convert.ToInt32(id);
                if (repository.Statuses.First(x => x.Id == nId) == null)
                    AddStatus(statusName, id);
                else
                {
                    StatusesJSON status = new StatusesJSON { Status = statusName, Id = Convert.ToInt32(id) };
                    repository.SaveStatus(status);
                }                    
                return "Ticket Updated";
        }
            
            else
            {
                return "Invalid Status";
            }

        }
        public string DeleteStatus(string id)
        {
            if(repository.DeleteStatus(Convert.ToInt32(id)))
                return "Status deleted";
            else
                return "Status not deleted";
        }
        public string AddStatus(string statusName, string id)
        {
            StatusesJSON status = new StatusesJSON { Status = statusName, Id = Convert.ToInt32(id) };
            if (status != null)
            {
                repository.CreateNewStatus(status);
                return "Status created";
            }
            else
                return "Invalid Status";
        }
        public JsonResult GetAll(string id)
        {
            IQueryable<Statuses> statuses = repository.Statuses;
            List<StatusesJSON> stat = new List<StatusesJSON>();
            //List<Statuses> statusList;
            foreach (Statuses p in statuses)
            {
                stat.Add(new StatusesJSON { Id = p.Id, Status = p.Status});
                
            }
                

            //statusList = stat;

            return Json(stat, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStatusById(string id)
        {
            int iD = Convert.ToInt32(id);
            Statuses status = repository.Statuses.FirstOrDefault(x => x.Id == iD);
            JsonResult json = Json(new { status.Id, status.Status }, JsonRequestBehavior.AllowGet);
            return json;
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Tickets ticket = repository.Tickets.FirstOrDefault(x=>x.Id == id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ticket.SendResponce = false;
            ticket.listDepartment = new SelectList(repository.Departments.OrderBy(x => x.Id), "Id", "Department");
            ticket.listUser = new SelectList(repository.Users.OrderBy(x => x.Id), "Id", "Name");
            ticket.listStatus = new SelectList(repository.Statuses.OrderBy(x => x.Id), "Id", "Status");
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tickets ticket)
        {
            if (ticket.SendResponce)
            {
                if (ticket.Email == null)
                    ModelState.AddModelError("", "Email is empty");
            }
            if (ModelState.IsValid)
            {
                if (ticket.SendResponce)
                {
                    string body = "Responce on your query №" + ticket.Reference + ": " + ticket.Responce;
                    queryProc.ProcessQuery(ticket, body);
                }
                    
                repository.SaveTicket(ticket);
                return RedirectToAction("ViewTickets");
            }

            //ticket.listDepartment = new SelectList(repository.Departments.OrderBy(x => x.Id), "Id", "Department");
            //ticket.listUser = new SelectList(repository.Users.OrderBy(x => x.Id), "Id", "Name");
            //ticket.listStatus = new SelectList(repository.Statuses.OrderBy(x => x.Id), "Id", "Status");
            return View(ticket);
        }
        private TicketsListViewModel CreateTicketListViewModel(IQueryable<Tickets> tickets, int page = 1)
        {
            TicketsListViewModel model = new TicketsListViewModel
            {
                Tickets = tickets.Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = tickets.Count()
                }
            };
            return model;

        }
        public ActionResult OpenTickets(int page = 1)
        {
            IQueryable<Tickets> tickets = repository.Tickets.Where(x => x.Statuses.GroupId != 0 && x.Statuses.GroupId < 3).OrderBy(x => x.Reference);
            ViewBag.Title = "Открытые тикеты";
            ViewBag.OpenTicket = "active";
            return View("ViewTickets", CreateTicketListViewModel(tickets, page));
        }
        public ActionResult NewTickets(int page = 1)
        {
            IQueryable<Tickets> tickets = repository.Tickets.Where(x => x.Statuses.GroupId == 0).OrderBy(x => x.Reference);
            ViewBag.Title = "New Tickets";
            ViewBag.NewTicket = "active";
            return View("ViewTickets", CreateTicketListViewModel(tickets, page));
        }
        public ActionResult OnHoldTickets(int page = 1)
        {
            IQueryable<Tickets> tickets = repository.Tickets.Where(x => x.Statuses.GroupId == 2).OrderBy(x => x.Reference);
            ViewBag.Title = "On Hold Tickets";
            ViewBag.OnHoldTicket = "active";
            return View("ViewTickets", CreateTicketListViewModel(tickets, page));
        }
        public ActionResult CompletedTickets(int page = 1)
        {
            IQueryable<Tickets> tickets = repository.Tickets.Where(x => x.Statuses.GroupId >= 3).OrderBy(x => x.Reference);
            ViewBag.Title = "Completed Tickets";
            ViewBag.CompletedTicket = "active";
            return View("ViewTickets", CreateTicketListViewModel(tickets, page));
        }
        public ActionResult ViewStatuses()
        {
            return View(repository.Statuses.OrderBy(x => x.Id));
        }

        public ActionResult TicketList(string searchReference, int selectedGroup =-1, int page = 1)
        {
            ViewBag.AllTicket = "active";
            
            IEnumerable<Tickets> tickets = null;
            if (selectedGroup == -1)
                tickets = repository.Tickets.OrderBy(x=>x.Reference);
            else
                tickets = repository.Tickets.Where(x=>x.Statuses.GroupId == selectedGroup).OrderBy(x => x.Reference);
            if (!String.IsNullOrEmpty(searchReference))
            {
                if (tickets.Where(s => s.Reference.ToUpper().Contains(searchReference.ToUpper())).Count() == 0)
                    tickets = tickets.Where(s => s.Subject != null && s.Subject.ToUpper().Contains(searchReference.ToUpper()));
                else
                    tickets = tickets.Where(s => s.Reference.ToUpper().Contains(searchReference.ToUpper()));
            }
            tickets.Skip((page - 1) * PageSize).Take(PageSize);
            return View(tickets);
        }
        
        public ActionResult ViewTickets(string searchReference, int selectedGroup = -1, int page = 1)
        {
            if (HttpContext != null)
                ViewBag.AllTicket = "active";

                IQueryable<Tickets> tickets = null;
            if (selectedGroup == -1)
                tickets = repository.Tickets.OrderBy(x => x.Reference);
            else
                tickets = repository.Tickets.Where(x => x.Statuses.GroupId == selectedGroup).OrderBy(x => x.Reference);
            if (!String.IsNullOrEmpty(searchReference))
            {
                if (tickets.Where(s => s.Reference.ToUpper().Contains(searchReference.ToUpper())).Count() == 0)
                    tickets = tickets.Where(s => s.Subject != null && s.Subject.ToUpper().Contains(searchReference.ToUpper()));
                else
                    tickets = tickets.Where(s => s.Reference.ToUpper().Contains(searchReference.ToUpper()));
            }
            tickets.Skip((page - 1) * PageSize).Take(PageSize);
            
            //return View(tickets);
            return View(CreateTicketListViewModel(tickets, page));
        }

        public PartialViewResult MenuGroup(int selectedGroup= -1)
        {
            ViewBag.SelectedGroup = selectedGroup;
            return PartialView(repository.StatusGroups.OrderBy(x=>x.Id));
        }
        // GET: Staff
        public ActionResult Index()
        {
            return View();
        }
    }
}