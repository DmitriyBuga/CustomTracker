using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CustomTracker.Models.Abstract;
using CustomTracker.Models.Entities;
using System.Web.Mvc;

namespace CustomTracker.Models.Concrete
{
    public class EFRepository : IRepository
    {
        //private TrackerDBEntities;// = new TrackerDBEntities();
        private EFDbContext dbContext;// = new TrackerDBEntities();
        public EFRepository(EFDbContext context)
        {
            dbContext = context;
        }
        
        public IQueryable<StatusGroup> StatusGroups
        {
            get { return dbContext.StatusGroups; }
        }
        public IQueryable<Users> Users
        {
            get { return dbContext.Users; }
        }
        public IQueryable<Tickets> Tickets
        {
            get { return dbContext.Tickets; }
        }
        public IQueryable<Departments> Departments
        {
            get { return dbContext.Departments; }
        }
        public IQueryable<Statuses> Statuses
        {
            get { return dbContext.Statuses; }
        }
        public Users GetUser(string name)
        {
            return dbContext.Users.FirstOrDefault(p => string.Compare(p.Name, name, true) == 0);
        }
        public Users Login(string name, string password)
        {
            return dbContext.Users.FirstOrDefault(p => string.Compare(p.Name, name, true) == 0 && p.Password == password);
        }
        private String GetReference(Tickets ticket)
        {
            string sReference;
            Tickets dbEntry = Tickets.OrderByDescending(x => x.Reference).FirstOrDefault() ;
            if (dbEntry != null)
            {
                
                sReference = dbEntry.Reference;
                int nRef = Convert.ToInt16(sReference.Substring(4,6));
                if (nRef != 999999)
                {
                    nRef += 1;
                    sReference = sReference.Substring(0,3) + "-" + nRef.ToString().PadLeft(6, '0');
                }
                else
                {

                }

            }
            else
            {
                sReference = "AAA-000001";
            }
            return sReference;
        }
        public bool DeleteStatus(int Id)
        {
            
            if (dbContext.Tickets.Where(x => x.Statuses.Id == Id).Count() == 0)
            {
                Statuses dbEntry = dbContext.Statuses.First(x => x.Id == Id);
                if (dbEntry != null)
                {
                    dbContext.Statuses.Remove(dbEntry);
                    dbContext.SaveChanges();
                    return true;
                }
                    
            }

            return false;

        }
        public void CreateNewStatus(StatusesJSON status)
        {
            {
                Statuses newStatus = new Statuses
                {
                    Status = status.Status,
                    GroupId = 0,
                };
                dbContext.Statuses.Add(newStatus);
                dbContext.SaveChanges();
            }
        }
        public  void SaveStatus(StatusesJSON status)
        {
            Statuses dbEntry = dbContext.Statuses.FirstOrDefault(x => x.Id == status.Id);
            dbEntry.Status = status.Status;
            dbContext.SaveChanges();
            //if (dbEntry == null)


        }
        public string SaveTicket(Tickets ticket)
        {
            Tickets dbEntry = null;
            if (ticket.Reference == null || (dbEntry = dbContext.Tickets.FirstOrDefault(x => x.Reference == ticket.Reference)) == null)
            {
                ticket.Responce = "";
                ticket.SendResponce = false;
                ticket.Reference = GetReference(ticket);
                ticket.Statuses = dbContext.Statuses.FirstOrDefault(x => x.Id == 0);
                ticket.Users = dbContext.Users.FirstOrDefault(x => x.Id == 0);
                ticket.DepartmentId = ticket.Departments.Id;
                ticket.Departments = dbContext.Departments.FirstOrDefault(x => x.Id == ticket.DepartmentId);
                //ticket.listDepartment = new SelectList(dbContext.Departments.OrderBy(x => x.Id), "Id", "Department");
                //ticket.listUser = new SelectList(dbContext.Users.OrderBy(x => x.Id), "Id", "Name");
                //ticket.listStatus = new SelectList(dbContext.Statuses.OrderBy(x => x.Id), "Id", "Status");
                dbContext.Tickets.Add(ticket);
            }
            else
            {
                //dbEntry.DepartmentId = 1;
                ticket.Statuses = dbContext.Statuses.FirstOrDefault(x => x.Id == ticket.Statuses.Id);
                ticket.Users = dbContext.Users.FirstOrDefault(x => x.Id == ticket.Users.Id);
                ticket.Departments = dbContext.Departments.FirstOrDefault(x => x.Id == ticket.Departments.Id);
                dbEntry = dbContext.Tickets.FirstOrDefault(x => x.Id == ticket.Id);
                dbEntry.Departments = ticket.Departments;
                dbEntry.Statuses = ticket.Statuses;
                dbEntry.Users = ticket.Users;
                dbEntry.Email = ticket.Email;
                if(dbEntry.DataMail == null)
                    dbEntry.DataMail = DateTime.Today;
                dbEntry.Subject = ticket.Subject;
                dbEntry.Responce = ticket.Responce;
                dbEntry.Reference = ticket.Reference;
                //dbContext.Tickets.Add(dbEntry);
            }            
            dbContext.SaveChanges();
            return ticket.Reference;
        }

    }
}