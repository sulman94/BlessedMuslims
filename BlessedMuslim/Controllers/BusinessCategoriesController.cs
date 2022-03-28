using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlessedMuslim.Models;

namespace BlessedMuslim.Controllers
{
    public class BusinessCategoriesController : Controller
    {
        private readonly BlessedMuslim_DBContext context = new BlessedMuslim_DBContext();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> getBusinessCategories()
        {
            var dataCategories = await context.BusinessCategories.Where(c => c.IsActive == true).ToListAsync();
            dataCategories = dataCategories.OrderByDescending(x => x.Id).ToList();
            return Json(new { data = dataCategories }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult Add()
        {
            ViewBag.result = "";
            ViewBag.error = "";
            return View(new BusinessCategories());
        }

        [HttpPost]
        public async Task<IActionResult> Add(BusinessCategories data)
        {
            try
            {
                data.IsActive = true;
                context.Add(data);
                await context.SaveChangesAsync();
                ViewBag.result = "Record Saved Successfully!";
            }
            catch (Exception e)
            {
                var error = e;
                ViewBag.error = e.Message;
            }
            ModelState.Clear();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var std = await context.BusinessCategories.Where(s => s.Id == Id).FirstOrDefaultAsync();
            return View(std);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BusinessCategories data)
        {
            try
            {
                var dbEntry = context.Entry(data);
                dbEntry.Property("Sdesc").IsModified = true;
                dbEntry.Property("Ldesc").IsModified = true;
                await context.SaveChangesAsync();
                ViewBag.result = "Record Updated Successfully!";
            }
            catch (Exception e)
            {
                var error = e;
                ViewBag.error = e.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var dataCountry = await context.BusinessCategories.Where(c => c.Id == id).FirstOrDefaultAsync();
                dataCountry.IsActive = false;
                context.Update(dataCountry);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                var error = e;
                ViewBag.error = e.Message;
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int Id)
        {
            var std = await context.BusinessCategories.Where(s => s.Id == Id).FirstOrDefaultAsync();
            return View(std);
        }
    }
}
