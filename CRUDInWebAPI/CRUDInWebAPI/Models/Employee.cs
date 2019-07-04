using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDInWebAPI.Models
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}