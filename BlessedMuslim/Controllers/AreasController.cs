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
    public class AreasController : Controller
    {
        private readonly BlessedMuslim_DBContext context = new BlessedMuslim_DBContext();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> getAreas()
        {
            var dataCities = await (from a in context.Areas
                                    join ci in context.Cities on a.CityId equals ci.Id into aci
                                    from areaCity in aci.DefaultIfEmpty()
                                    join s in context.States on a.StateId equals s.Id into cis
                                    from citystate in cis.DefaultIfEmpty()
                                    join co in context.Country on a.CountryId equals co.Id into cicp
                                    from citycountry in cicp.DefaultIfEmpty()
                                    where a.IsActive == true
                                    select new
                                    {
                                        Id = a.Id,
                                        CountryName = citycountry.CountryCode + " - " + citycountry.CountryName,
                                        StateName = citystate.StateName,
                                        CityName = areaCity.CityName,
                                        AreaName = a.AreaCode + " - " + a.AreaName,
                                    }).ToListAsync();

            return Json(new { data = dataCities }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public async Task<IActionResult> Add()
        {

            ViewBag.result = "";
            ViewBag.error = "";
            ViewBag.CountryId = new SelectList(await context.Country.Where(x => x.IsActive == true).ToListAsync(), "Id", "CountryName");
            ViewBag.StateId = new SelectList(await context.States.Where(x => x.IsActive == true).ToListAsync(), "Id", "StateName");
            ViewBag.CityId = new SelectList(await context.Cities.Where(x => x.IsActive == true).ToListAsync(), "Id", "CityName");
            return View(new Areas());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Areas data)
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
            ViewBag.CityId = new SelectList(await context.Cities.Where(x => x.IsActive == true).ToListAsync(), "Id", "CityName");
            var std = await context.Areas.Where(s => s.Id == Id).FirstOrDefaultAsync();

            return View(std);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Areas data)
        {
            try
            {
                var dbEntry = context.Entry(data);
                dbEntry.Property("CountryId").IsModified = true;
                dbEntry.Property("StateId").IsModified = true;
                dbEntry.Property("CityId").IsModified = true;
                dbEntry.Property("AreaCode").IsModified = true;
                dbEntry.Property("AreaName").IsModified = true;
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
                var dataStates = await context.Areas.Where(c => c.Id == id).FirstOrDefaultAsync();
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
            ViewBag.CityId = new SelectList(await context.Cities.Where(x => x.IsActive == true).ToListAsync(), "Id", "CityName");

            var std = await context.Areas.Where(s => s.Id == Id).FirstOrDefaultAsync();

            return View(std);
        }
    }
}
