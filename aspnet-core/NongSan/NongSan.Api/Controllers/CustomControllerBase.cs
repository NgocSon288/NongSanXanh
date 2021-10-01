using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NongSan.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NongSan.Api.Controllers
{
    public class CustomControllerBase : ControllerBase
    {
        protected string[] ModelStateErrors(ModelStateDictionary modelState)
        {
            return modelState.ToList().Select(x => x.Value.ToString()).ToArray();
        }
    }
}
