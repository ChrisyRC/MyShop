using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
    public class BasketItemViewModel
    {
        public string Id { get; set; }    // Id of basket item
        public int Quanity { get; set; }  // from the basket item
        public string ProductName { get; set; }  // from product table
        public decimal Price { get; set; }      // ditto
        public string Image { get; set; }      // Image URL ffrom product table+
    }
}
