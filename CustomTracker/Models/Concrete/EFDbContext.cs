using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CustomTracker.Models.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base("name=TrackerDBEntities") { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<Statuses> Statuses { get; set; }
        public DbSet<StatusGroup> StatusGroups { get; set; }
        public DbSet<Departments> Departments { get; set; }
        
    }
}