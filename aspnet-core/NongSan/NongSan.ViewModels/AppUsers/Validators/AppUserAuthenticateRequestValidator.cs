using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NongSan.ViewModels.AppUsers.Validators
{
    public class AppUserAuthenticateRequestValidator : AbstractValidator<AppUserAuthenticateRequest>
    {
        public AppUserAuthenticateRequestValidator()
        {
            RuleFor(x => x.Token).NotEmpty().WithMessage("Token không hợp lệ"); 
        }
    }
}