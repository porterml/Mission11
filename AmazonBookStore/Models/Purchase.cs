using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonBookStore.Models
{
    public class Purchase
    {
        [Key]
        [BindNever]
        public int PurchaseId { get; set; }

        [BindNever]
        public ICollection<BasketLineItem> Lines { get; set; }

        [Required(ErrorMessage = "Name Field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a valid Address")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
    
        [Required(ErrorMessage = "Please enter a valid City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter a valid State")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please enter a valid Zip")]
        public string Zip { get; set; }
        [Required(ErrorMessage = "Please enter a valid Country")]
        public string Country { get; set; }

        public bool SubscribeFlag { get; set; }

    }
}
