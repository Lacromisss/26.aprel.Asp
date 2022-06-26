using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.DAL;
using WebApplication4.Models;
using WebApplication4.Vm;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;

        }

        public IActionResult Index( )
        {
           
          
            return View();
        }
      
      
    }
}
