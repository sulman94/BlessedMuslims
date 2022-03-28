using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlessedMuslim.Models;
using Microsoft.AspNetCore.Authorization;

namespace BlessedMuslim.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class CountriesController : Controller
    {
        private readonly BlessedMuslim_DBContext context = new BlessedMuslim_DBContext();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> getCountries()
        {
            var dataCountries = await context.Country.Where(c => c.IsActive == true).ToListAsync();
            dataCountries = dataCountries.OrderByDescending(x => x.Id).ToList();
            return Json(new { data = dataCountries }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult Add()
        {
            ViewBag.result = "";
            ViewBag.error = "";
            return View(new Country());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Country data)
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
            var std = await context.Country.Where(s => s.Id == Id).FirstOrDefaultAsync();
            return View(std);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Country data)
        {
            try
            {
                var dbEntry = context.Entry(data);
                dbEntry.Property("CountryCode").IsModified = true;
                dbEntry.Property("CountryName").IsModified = true;

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
                var dataCountry = await context.Country.Where(c => c.Id == id).FirstOrDefaultAsync();
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
            var std = await context.Country.Where(s => s.Id == Id).FirstOrDefaultAsync();
            return View(std);
        }
    }
}
