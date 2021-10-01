using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NongSan.ViewModels.AppUserRoles
{
    public class AppUserRoleRevokeRequest
    {
        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }
    }
}
