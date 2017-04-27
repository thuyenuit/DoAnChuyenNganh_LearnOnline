using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnOnline.Web.Models
{
    public class UserViewModel
    {
        public int ID { get; set; }

        public string Avatar { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }

        public DateTime? NgaySinh { get; set; }

        public bool? Sex { get; set; }

        public string SSN { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int ProvincesID { get; set; }

        public int DistrictID { get; set; }

        public string UnitName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool? Status { get; set; }

        public int UserGroupID { get; set; }


        public string UserGroupName { get; set; }
        public string Gender { get; set; }
        public string ProvincesName { get; set; }
        public string DistrictName { get; set; }

    }
}