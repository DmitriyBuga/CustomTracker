using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomTracker.Models.Abstract
{
    public interface IQueryProcessor
    {
        void ProcessQuery(Tickets query, string body);
    }
}
