using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAC.DAL.Models
{
    [Serializable]
    public class AreaVM
    {
        public int IdArea { get; set; }
        public string Name { get; set; }
        public string Descrition { get; set; }
        public bool? Status { get; set; }
        public List<HasAccessVM> HasAccess { get; set; }
    }
}