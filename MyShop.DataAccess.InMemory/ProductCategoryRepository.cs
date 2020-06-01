using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;                   // cache object set to default inmemory cache
        List<ProductCategory> productCategories;                                    // internal list

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();

            }
        }


        public void Commit()                                                     // Method
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory p)                                               // Method for adding a product to the list
        {
            productCategories.Add(p);
        }

        public void Update(ProductCategory productCategory)                                     // Method to update produc
        {
            ProductCategory productCategoryToUpdate = productCategories.Find(p => p.Id == productCategory.Id);
            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product Category Not Found");
            }
        }

        public ProductCategory Find(string Id)                                          // <ethod to find a Product - Product in line specifies return type
        {
            ProductCategory productCategory = productCategories.Find(p => p.Id == Id);
            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }

        public IQueryable<ProductCategory> Collection()                                 // Method to return a lis of Product which is queryable
        {
            return productCategories.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.Id == Id);
            if (productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product Category Not Found");
            }

        }
    }
}
