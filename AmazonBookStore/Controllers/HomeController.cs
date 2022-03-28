using AmazonBookStore.Models;
using AmazonBookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonBookStore.Controllers
{
    public class HomeController : Controller
    {
        private IBookstoreRepository repo;

        public HomeController (IBookstoreRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index(string bookCategory, int pageNum = 1)
        {
            int resultsPerPage = 5;

            var x = new BooksViewModel
            {
                Books = repo.Books
                .Where(b => b.Category == bookCategory || bookCategory == null)
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * resultsPerPage)
                .Take(resultsPerPage),

                PageInfo = new PageInfo
                {
                    //if the book category we are looking at is null(aka the regular home index) or then the page num will be inherited
                    TotalNumBooks = (bookCategory == null 
                                        ? repo.Books.Count() 
                                        : repo.Books.Where(x => x.Category == bookCategory).Count()),
                    BooksPerPage = resultsPerPage,
                    CurrentPage = pageNum
                }
            };
            return View(x);
        }

    }
}
