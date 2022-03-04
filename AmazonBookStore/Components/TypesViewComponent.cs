using AmazonBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonBookStore.Components
{
    public class TypesViewComponent : ViewComponent
    {
        private IBookstoreRepository repo { get; set; }

        public TypesViewComponent (IBookstoreRepository temp)
        {
            repo = temp;
        }

        // use this to search/filter by category
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["bookCategory"];

            var types = repo.Books
                .Select(x => x.Category) // category is the filterable field
                .Distinct()
                .OrderBy(x => x);
            
            return View(types);
        }
    }
}
