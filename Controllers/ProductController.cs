using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        CategoryDAL cat;
        public ProductController(IConfiguration configuration)
        {
            this.configuration = configuration;
            db = new ProductDAL(configuration);
            cat=new CategoryDAL(configuration); 
        }
        public IActionResult List(int pg=1)
        {
            var model = db.ProductList();
            var catlist=cat.CategoryList();
            foreach (var p in model)
            {
                foreach (var c in catlist)
                {
                    if (c.CategoryId == p.CategoryId)
                    {
                        p.CategoryName=c.CategoryName;
                    }
                }
            }
            const int pagesize = 5;
            if (pg < 1)
            {
                pg = 1;
            }
            int recscount = model.Count();

            var pager = new Pager(recscount, pg, pagesize);

            int recskip = (pg - 1) * pagesize;

            var data = model.Skip(recskip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;
            return View(data);
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
