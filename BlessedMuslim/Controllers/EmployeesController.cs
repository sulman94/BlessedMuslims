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
    public class EmployeesController : Controller
    {
        private readonly BlessedMuslim_DBContext context = new BlessedMuslim_DBContext();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> getEmployees()
        {
            var dataEmployees = await context.Employee.Where(c => c.IsActive == true).ToListAsync();
            dataEmployees = dataEmployees.OrderByDescending(x => x.Id).ToList();
            return Json(new { data = dataEmployees }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public async Task<IActionResult> Add()
        {
            ViewBag.result = "";
            ViewBag.error = "";
            ViewBag.AreaId = new SelectList(await context.Areas.Where(x => x.IsActive == true).ToListAsync(), "Id", "AreaName");
            return View(new Employee());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Employee data)
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
            var std = await context.Employee.Where(s => s.Id == Id).FirstOrDefaultAsync();
            return View(std);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee data)
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
                var dataCountry = await context.Employee.Where(c => c.Id == id).FirstOrDefaultAsync();
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
            var std = await context.Employee.Where(s => s.Id == Id).FirstOrDefaultAsync();
            return View(std);
        }
    }
}

