using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomTracker.Models
{
    public class TicketsListViewModel
    {
        public IQueryable<Tickets> Tickets { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}