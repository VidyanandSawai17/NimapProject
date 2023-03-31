using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NimapProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NimapProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IConfiguration configuration;
        CategoryDAL db;
        public CategoryController(IConfiguration configuration)
        {
            this.configuration = configuration;
            db = new CategoryDAL(configuration);
        }
        public IActionResult List()
        {
            var model = db.CategoryList();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddCat()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCat(Category c)
        {
            try
            {
                int res = db.AddCat(c);
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

        public IActionResult DeleteCat(int id)
        {
            var cat = db.GetCatById(id);
            return View(cat);
        }
        [HttpPost]
        [ActionName("DeleteCat")]
        public IActionResult DeleteCategoryConfiorm(int id)
        {
            try
            {
                int res = db.DeleteCat(id);
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
        public IActionResult EditCat(int id)
        {
            var cat = db.GetCatById(id);
            return View(cat);
        }

        [HttpPost]
        public IActionResult EditCat(Category c)
        {
            try
            {
                int res = db.UpdateCat(c);
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
