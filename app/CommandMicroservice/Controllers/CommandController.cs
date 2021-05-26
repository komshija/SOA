using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandMicroservice.Controllers
{
    [ApiController]
    [Route("/")]
    public class CommandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
