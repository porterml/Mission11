using AmazonBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonBookStore.Controllers
{
    public class HomeController : Controller
    {
        private BookstoreContext context { get; set; }

        public HomeController (BookstoreContext temp)
        {
            context = temp;
        }

        public IActionResult Index()
        {
            var blah = context.Books.ToList();

            return View(blah);
        }

    }
}
