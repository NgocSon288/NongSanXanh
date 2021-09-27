using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NongSan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private List<int> data = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        [HttpGet]
        public ActionResult Gets()
        {
            return Ok(data);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            try
            {
                return Ok(data[id]);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        } 
    }
}
