using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAC.DAL.Models
{
    public class AccessVM
    {
        public int IdAccess { get; set; }
        public UserVM User { get; set; }
        public AreaVM Area { get; set; }
        public DateTime? Date { get; set; }
    }
}