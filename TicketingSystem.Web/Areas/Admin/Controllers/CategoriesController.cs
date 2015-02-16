using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TicketingSystem.Data;
using TicketingSystem.Models;
using TicketingSystem.Web.Models;
using TicketingSystem.Web.Areas.Admin.ViewModels;
using TicketingSystem.Web.Areas.Admin.Models;

using PagedList;
using PagedList.Mvc;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Net;

namespace TicketingSystem.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : BaseController
    {
        public CategoriesController(ITicketingSystemData data)
            : base(data)
        {
        }

        // GET: Admin/Categories
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult Index(int? page)
        {
            var categoriesModel = Data.Categories.All()
                    .OrderBy(c => c.Name)
                    .Project().To<CategoryIndex>()
                    .ToPagedList(page ?? 1, 3);

                return View(categoriesModel);
        }

        // GET: Admin/Categories/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryInput category)
        {
            if (ModelState.IsValid)
            {
                var newCategory = Mapper.Map<Category>(category);
                Data.Categories.Add(newCategory);
                try
                {
                    Data.SaveChanges();
                    TempData["message"] = "Category successfully added";
                    TempData["color"] = "alert-success";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "An error occurred.Maybe there is a category with that name.Try different name");
                    return View(category);
                }
            }

            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = Data.Categories.GetById(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<CategoryEdit>(category);
            return View(model);
        }

        // POST: Admin/Categories/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CategoryEdit category)
        {
            var editCategory = Mapper.Map<Category>(category);

            try
            {
                Data.Categories.Update(editCategory);
                Data.SaveChanges();

                TempData["message"] = "Category successfully edited";
                TempData["color"] = "alert-success";
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                ModelState.AddModelError("", "An error occurred.Maybe there is a category with that name.Try different name");
                return View(category);
            }
        }

        // GET: Admin/Categories/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = Data.Categories.GetById(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<CategoryIndex>(category);
            return View(model);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Data.Categories.Delete(id);
                Data.SaveChanges();
                TempData["message"] = "Category successfully deleted";
                TempData["color"] = "alert-success";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["message"] = "Some problem appears.Maybe category is already deleted";
                TempData["color"] = "alert-danger";
                return RedirectToAction("Index");
            }
        }
    }
}
