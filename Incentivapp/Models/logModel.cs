using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Incentivapp.Models
{
    public class LogModel
    {
        public string Action { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}