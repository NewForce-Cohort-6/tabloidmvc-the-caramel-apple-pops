using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        // ASP.NET will give us an instance of our category Repository. This is called "Dependency Injection"
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: CategoryController

        public ActionResult Index()
       
        {
            var categories = _categoryRepository.GetAll();

            return View(categories);
        }


        //GET: CategoryController/Details/5
                public ActionResult Details(int id)
        {
            return View();
        }


        //GET: CategoryController/Create
                public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                _categoryRepository.AddCategory(category);
                return RedirectToAction("Index");
                
            }
            catch (Exception ex)
            {
                return View(category);
            }
        }

        //GET: CategoryController/Delete/5
                public ActionResult Delete(int id)
        {
            Category category = _categoryRepository.GetCategoryById(id);

            return View(category);
        }


        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                category = _categoryRepository.GetCategoryById(id);
                _categoryRepository.DeleteCategory(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("NoDelete");
            }
        }

        // GET: CategoryController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            Category category = _categoryRepository.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category category)
        {
            try
            {
                _categoryRepository.UpdateCategory(category);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(category);
            }
        }

    }
}


