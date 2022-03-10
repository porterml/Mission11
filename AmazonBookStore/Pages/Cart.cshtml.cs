using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmazonBookStore.Infrastructure;
using AmazonBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AmazonBookStore.Pages
{
    public class CartModel : PageModel
    {
        private IBookstoreRepository repo { get; set; }

        public CartModel (IBookstoreRepository temp)
        {
            repo = temp;
        }

        //create instances to be used later
        public Basket basket { get; set; }
        public string ReturnUrl { get; set; }


        //this is the get "controller" 
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
        }

        //this is the post "controller"
        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();

            basket.AddItem(b, 1);

            HttpContext.Session.SetJson("basket", basket);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
