using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NongSan.ViewModels.AppUserRoles.Validators
{
    public class AppUserRoleGrantRequestValidator : AbstractValidator<AppUserRoleGrantRequest>
    {
        public AppUserRoleGrantRequestValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId không hợp lệ");
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("RoleId không hợp lệ");
        }
    }
}