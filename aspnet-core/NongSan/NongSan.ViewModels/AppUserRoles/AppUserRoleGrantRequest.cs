using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NongSan.ViewModels.AppUserRoles
{
    public class AppUserRoleGrantRequest
    {
        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }
    }
}
