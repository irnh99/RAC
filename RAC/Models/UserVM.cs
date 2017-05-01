using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAC.DAL.Models
{
    [Serializable]
    public class UserVM
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }
        public string UserName { get; set; }
        public int? NoControl { get; set; }
        public UserTypeVM UserType { get; set; }
    }
}