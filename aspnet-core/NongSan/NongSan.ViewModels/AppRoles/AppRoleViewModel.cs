using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NongSan.ViewModels.AppRoles
{
    public class AppRoleViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public string Description { get; set; }
    }
}
