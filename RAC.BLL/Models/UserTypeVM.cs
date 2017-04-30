using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAC.DAL.Models
{
    [Serializable]
    public class UserTypeVM
    {
        public int IdUserType { get; set; }
        public string Description { get; set; }
        public List<UserVM> Users { get; set; }
    }
}