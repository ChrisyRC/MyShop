using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Services
{
    public class BasketService
    {
        IRepository<Product> productContext;           // add using statements 
        IRepository<Basket> basketContext;

        public const string BasketSessionName = "eCommerceBase"; 

        public BasketService(IRepository<Product> ProductContext,  IRepository<Basket> BasketContext)  // Constructor
        {
            this.basketContext = BasketContext;
            this.productContext = ProductContext;
        }
        
        private Basket GetBasket(HttpContextBase httpContext, bool createIfNull)   // Load getbasket method - private
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);  // read the cookie

            Basket basket = new Basket();

            if (cookie != null)
            {
                string basketId = cookie.Value;
                if (!string.IsNullOrEmpty(basketId))   // check if null value or empty string
                {
                    basket = basketContext.Find(basketId);
                }
                else
                {
                    if (createIfNull)
                    {
                        basket = CreateNewBasket(httpContext);    // CreateNewBAsket method does not esist yet
                    }
                }
            }
            else
            {
                if (createIfNull)
                {
                    basket = CreateNewBasket(httpContext);
                }
            }
            return basket;
        }

        private Basket CreateNewBasket(HttpContextBase httpContext)   // CreateNewBasket method
        {
            Basket basket = new Basket();
            basketContext.Insert(basket);   // inserting basket into the dataabase
            basketContext.Commit();

            HttpCookie cookie = new HttpCookie(BasketSessionName);  //  write cookie back to user
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return basket;
        }

        public void AddToBasket (HttpContextBase httpContext, string productId)  // using the internal method above
        {
            Basket basket = GetBasket(httpContext, true);  // Get basketId Becasue we are inserting an item need to create basket (true) if is does not exist
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);

            if (item == null)
            {
                item = new BasketItem()
                {
                    BasketId = basket.Id,
                    ProductId = productId,
                    Quanity = 1

                };

                basket.BasketItems.Add(item);
            }
            else
            {
                item.Quanity = item.Quanity + 1;
            }
            basketContext.Commit();
        }

        public void RemoveFromBasket(HttpContextBase httpContext, string itemId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                basket.BasketItems.Remove(item);
                basketContext.Commit();
            }
        }
    }
}
