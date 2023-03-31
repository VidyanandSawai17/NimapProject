using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NimapProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NimapProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IConfiguration configuration;
        ProductDAL db;
        public ProductController(IConfiguration configuration)
        {
            this.configuration = configuration;
            db = new ProductDAL(configuration);
        }
        public IActionResult List()
        {
            var model = db.ProductList();
            return View(model);
        }
        [HttpGet]
        public IActionResult AddProd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProd(Product p)
        {
            try
            {
                int res = db.AddProd(p);
                if (res == 1)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public IActionResult DeleteProd(int id)
        {
            var cat = db.GetProdById(id);
            return View(cat);
        }
        [HttpPost]
        [ActionName("DeleteProd")]
        public IActionResult DeleteProductConfiorm(int id)
        {
            try
            {
                int res = db.DeleteProd(id);
                if (res == 1)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        [HttpGet]
        public IActionResult EditProd(int id)
        {
            var cat = db.GetProdById(id);
            return View(cat);
        }
            [HttpPost]
            public IActionResult EditProd(Product p)
            {
                try
                {
                    int res = db.UpdateProd(p);
                    if (res == 1)
                    {
                        return RedirectToAction("List");
                    }
                    else
                    {
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    return View();
                }
            }
    }
    
}
