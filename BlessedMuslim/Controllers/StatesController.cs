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
    public class StatesController : Controller
    {
        private readonly BlessedMuslim_DBContext context = new BlessedMuslim_DBContext();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> getStates()
        {
            //context = new BlessedMuslim_DBContext();
            // var dataStates = await context.States.Where(c => c.IsActive == true).ToListAsync();

            var dataStates = await context.States.Join(context.Country,
                c => c.CountryId,
                oa => oa.Id,
                (c, oa) => new
                {
                    Id = c.Id,
                    CountryName = oa.CountryCode + " - "+oa.CountryName,
                    StateName = c.StateName,
                    IsActive = c.IsActive
                }).Where(c => c.IsActive == true).ToListAsync();

            dataStates = dataStates.OrderByDescending(x => x.Id).ToList();
            return Json(new { data = dataStates }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public async Task<IActionResult> Add()
        {
            
            ViewBag.result = "";
            ViewBag.error = "";
            ViewBag.CountryId = new SelectList(await context.Country.Where(x => x.IsActive == true).ToListAsync(), "Id", "CountryName");
            return View(new States());
        }

        [HttpPost]
        public async Task<IActionResult> Add(States data)
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
            ViewBag.CountryId = new SelectList(await context.Country.Where(x => x.IsActive == true).ToListAsync(), "Id", "CountryName");
            var std = await context.States.Where(s => s.Id == Id).FirstOrDefaultAsync();

            return View(std);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(States data)
        {
            try
            {
                var dbEntry = context.Entry(data);
                dbEntry.Property("CountryId").IsModified = true;
                dbEntry.Property("StateName").IsModified = true;
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
                var dataStates = await context.States.Where(c => c.Id == id).FirstOrDefaultAsync();
                dataStates.IsActive = false;
                context.Update(dataStates);
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
            ViewBag.CountryId = new SelectList(await context.Country.Where(x => x.IsActive == true).ToListAsync(), "Id", "CountryName");
            var std = await context.States.Where(s => s.Id == Id).FirstOrDefaultAsync();

            return View(std);
        }
    }
}
