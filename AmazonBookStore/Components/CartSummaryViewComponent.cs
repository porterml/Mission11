using AmazonBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AmazonBookStore.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private Basket basket;
        public CartSummaryViewComponent(Basket ba)
        {
            basket = ba; 
        }
        public IViewComponentResult Invoke()
        {
            return View(basket);
        }
    }
}
