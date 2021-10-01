using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NongSan.ViewModels.AppUsers.Validators
{
    public class AppUserLoginRequestValidator : AbstractValidator<AppUserLoginRequest>
    {
        public AppUserLoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Tài khoản không hợp lệ");

            RuleFor(x => x.PassWord).NotEmpty().WithMessage("Mật khẩu không hợp lệ")
                .MinimumLength(3).WithMessage("Mật khẩu phải có ít nhật 3 ký tự");
        }
    }
}