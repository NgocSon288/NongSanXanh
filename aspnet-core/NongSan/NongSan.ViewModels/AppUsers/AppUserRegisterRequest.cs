using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NongSan.ViewModels.AppUsers
{
    public class AppUserRegisterRequest
    {
        public string FullName { get; set; }

        public DateTime Dob { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public string PassWordConfirm { get; set; }
    }
}
