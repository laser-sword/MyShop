using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        //create the cache
        
        ObjectCache cache = MemoryCache.Default;
        //create the list fo rthe products
        List<Product> products;

        public ProductRepository()
        {
            //look to see in the cache for list of products
            products = cache["products"] as List<Product>;
            if (products == null) {
                products = new List<Product>();
            }
        }

        //before adding to the list, we will store current list of products in the cache
        public void Commit() {
            cache["products"] = products;
        
        }

        public void Insert(Product p) {
            products.Add(p);
        }
        public void Update(Product product) {
            //find what product to update
            Product productToUpdate = products.Find(p => p.Id == product.Id);
            //if we recived product
            if (productToUpdate != null)
            {
                productToUpdate = product;

            }
            else {
                throw new Exception("Product Not Found!");
            }
        
        }

        public Product Find(string Id) {
            Product product = products.Find(p => p.Id == p.Id);
            //if we recived product
            if (product != null)
            {
                return product;

            }
            else
            {
                throw new Exception("Product Not Found!");
            }

        }

        public IQueryable<Product> Collection() {
            return products.AsQueryable();
        }

        public void Delete(string Id) {
            Product productToDelete = products.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else {
                throw new Exception("No Product Found!");
            }

        }


    }
}
