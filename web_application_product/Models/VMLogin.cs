using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_application_product.Models
{
    public class VMLogin
    {
        public string Email { get; set; }
        public string PassWord { get; set; }
        public bool KeepLoggedIn { get; set; }
    }
}
