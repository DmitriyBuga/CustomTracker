using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomTracker.Models
{
    public partial class Tickets
    {
        public int DepartmentId { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }

        public SelectList listDepartment { get; set; }
        public SelectList listUser { get; set; }
        public SelectList listStatus { get; set; }
        public bool SendResponce { get; set; }

        
    }
}