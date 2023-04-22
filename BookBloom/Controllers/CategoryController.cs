﻿using BookBloom.Data;
using BookBloom.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookBloom.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BookBloomDbContext dbContext;

        public CategoryController(BookBloomDbContext bookBloomDbContext)
        {
                dbContext = bookBloomDbContext;
        }

        public IActionResult Index()
        {
            List<Category> categories = dbContext.Category.ToList();
            return View(categories);     
        }

        /// <summary>
        /// get method for create
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// post method
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Category category) 
        {
            if (ModelState.IsValid)
            {
                dbContext.Category.Add(category);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
           
        }
    }
}
