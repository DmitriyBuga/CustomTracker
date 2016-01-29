using CustomTracker.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomTracker.Models.Abstract
{
    public interface IRepository
    {
        
        IQueryable<Users> Users { get; }
        IQueryable<Tickets> Tickets { get; }
        IQueryable<Departments> Departments{ get; }
        IQueryable<Statuses> Statuses{ get; }
        IQueryable<StatusGroup> StatusGroups { get; }
        Users GetUser(string name);
        Users Login(string name, string password);
        string SaveTicket(Tickets ticket);
        void SaveStatus(StatusesJSON status);
        void CreateNewStatus(StatusesJSON status);
        bool DeleteStatus(int Id);

    }
}
