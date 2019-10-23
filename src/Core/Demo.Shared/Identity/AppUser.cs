using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class AppUser : IdentityUser
    {
        public string Idcard { get; set; }
        public string FullName { get; set; }
    }

    public class AppRole : IdentityRole
    {
    }
}
