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
    public class CitiesController : Controller
    {
        private readonly BlessedMuslim_DBContext context = new BlessedMuslim_DBContext();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> getCities()
        {
            var dataCities = await (from ci in context.Cities
                                    join s in context.States on ci.StateId equals s.Id into cis
                                    from citystate in cis.DefaultIfEmpty()
                                    join co in context.Country on ci.CountryId  equals co.Id into cicp
                                    from citycountry in cicp.DefaultIfEmpty()
                                    where ci.IsActive == true
                                    select new {
                                        Id = ci.Id,
                                        CountryName = citycountry.CountryCode + " - " + citycountry.CountryName,
                                        StateName = citystate.StateName,
                                        CityName = ci.CityName
                                    }).ToListAsync();
           
            return Json(new { data = dataCities }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public async Task<IActionResult> Add()
        {

            ViewBag.result = "";
            ViewBag.error = "";
            ViewBag.CountryId = new SelectList(await context.Country.Where(x => x.IsActive == true).ToListAsync(), "Id", "CountryName");
            ViewBag.StateId = new SelectList(await context.States.Where(x => x.IsActive == true).ToListAsync(), "Id", "StateName");
            return View(new Cities());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Cities data)
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
            ViewBag.StateId = new SelectList(await context.States.Where(x => x.IsActive == true).ToListAsync(), "Id", "StateName");
            var std = await context.Cities.Where(s => s.Id == Id).FirstOrDefaultAsync();

            return View(std);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Cities data)
        {
            try
            {
                var dbEntry = context.Entry(data);
                dbEntry.Property("CountryId").IsModified = true;
                dbEntry.Property("StateId").IsModified = true;
                dbEntry.Property("CityName").IsModified = true;
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
                var dataStates = await context.Cities.Where(c => c.Id == id).FirstOrDefaultAsync();
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
            ViewBag.StateId = new SelectList(await context.States.Where(x => x.IsActive == true).ToListAsync(), "Id", "StateName");

            var std = await context.Cities.Where(s => s.Id == Id).FirstOrDefaultAsync();

            return View(std);
        }
    }
}
