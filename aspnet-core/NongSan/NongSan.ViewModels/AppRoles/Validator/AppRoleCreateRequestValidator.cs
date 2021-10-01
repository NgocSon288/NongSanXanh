using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NongSan.ViewModels.AppRoles.Validator
{
    public class AppRoleCreateRequestValidator : AbstractValidator<AppRoleCreateRequest>
    {
        public AppRoleCreateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không hợp lệ")
                .MaximumLength(200).WithMessage("Tên quá dài");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Mô tả không hợp lệ")
                .MaximumLength(200).WithMessage("Mô tả quá dài");
        }
    }
}