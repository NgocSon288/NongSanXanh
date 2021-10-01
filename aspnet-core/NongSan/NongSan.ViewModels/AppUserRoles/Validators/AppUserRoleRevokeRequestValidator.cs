using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NongSan.ViewModels.AppUserRoles.Validators
{
    public class AppUserRoleRevokeRequestValidator : AbstractValidator<AppUserRoleRevokeRequest>
    {
        public AppUserRoleRevokeRequestValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId không hợp lệ");
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("RoleId không hợp lệ");
        }
    }
}