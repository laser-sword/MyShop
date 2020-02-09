using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        IRepository<ProductCategory> context;

        public ProductCategoryManagerController(IRepository<ProductCategory> context)
        {
            this.context = context;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productsCategories = context.Collection().ToList();

            return View(productsCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productCategories = new ProductCategory();
            return View(productCategories);

        }
        [HttpPost]
        public ActionResult Create(ProductCategory productCategories)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategories);
            }
            else
            {
                context.Insert(productCategories);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productCategories = context.Find(Id);
            if (productCategories == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategories);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory product, string Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);

            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                productCategoryToEdit.Category = product.Category;
            

                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);

            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(productCategoryToDelete);
            }

        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }

            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }


        }

        // GET: ProductCategoryManager
       
    }
}