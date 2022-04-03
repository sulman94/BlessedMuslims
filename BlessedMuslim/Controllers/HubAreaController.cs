using BlessedMuslim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlessedMuslim.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class HubAreaController : Controller
    {
        private readonly BlessedMuslim_DBContext context = new BlessedMuslim_DBContext();
        public async Task<IActionResult> Add()
        {
            ViewBag.AreaId = new SelectList(await context.Areas.Where(e => e.IsActive == true && !context.HubAreas.Select(m => m.AreaId)
                                                   .Contains(e.Id))
                                   .Select(x => new { x.Id, AreaName = x.AreaName }).ToListAsync(), "Id", "AreaName");

            // ViewBag.AreaId = new SelectList(await context.Areas.Where(x => x.IsActive == true).Select(x => new { x.Id, AreaName = x.AreaName }).ToListAsync(), "Id", "AreaName");
            return View();
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> getHubs()
        {
            var HubData = await (from hm in context.HubMaster
                                 select new HubMaster
                                 {
                                     Id = hm.Id,
                                     HubId = hm.HubId,
                                     HubDesc = hm.HubDesc,
                                 }).ToListAsync();
            HubData = HubData.OrderByDescending(x => x.Id).ToList();
            return Json(new { data = HubData }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        [HttpPost]
        public async Task<ActionResult> CreateHubAreas(string HubName, string HubDescription, int[] SelectedAreas)
        {

            var _HubName = HubName;
            var _HubDescription = HubDescription;

            bool exists = context.HubMaster.Any(r => r.HubId == HubName);
            if (!exists)
            {
                HubMaster obj = new HubMaster()
                {
                    HubId = HubName,
                    HubDesc = HubDescription
                };

                context.Update(obj);
                await context.SaveChangesAsync();

                //var HubAreas = await context.HubAreas.Where(x => x.HubId == HubName).FirstOrDefaultAsync();
                //if (HubAreas != null)
                //{
                //    context.Update(HubAreas);
                //    await context.SaveChangesAsync();
                //}

                for (int i = 0; i < SelectedAreas.Length; i++)
                {
                    HubAreas objAreas = new HubAreas()
                    {
                        HubId = HubName,
                        AreaId = SelectedAreas[i]
                    };

                    context.Add(objAreas);
                }
                await context.SaveChangesAsync();
            }
            else
            {

            }

            return View("Index");
        }

        [HttpPost]
        public async Task<ActionResult> UpdateHubAreas(string HubId, string HubDesc, int[] SelectedAreas)
        {
            var HubAreas = context.HubAreas.Where(x => x.HubId == HubId);
            if (HubAreas.Any())
            {
                context.RemoveRange(HubAreas);
                context.SaveChanges();
            }

            for (int i = 0; i < SelectedAreas.Length; i++)
            {
                HubAreas objAreas = new HubAreas()
                {
                    HubId = HubId,
                    AreaId = SelectedAreas[i]
                };

                context.Add(objAreas);
            }
            await context.SaveChangesAsync();
            return View("Index");
        }
        public async Task<IActionResult> Edit(int Id)
        {
            ViewBag.AreaId = new SelectList(await context.Areas.Where(e => e.IsActive == true && !context.HubAreas.Select(m => m.AreaId)
                                       .Contains(e.Id))
                       .Select(x => new { x.Id, AreaName = x.AreaName }).ToListAsync(), "Id", "AreaName");

            var HubData = await (from hm in context.HubMaster
                                 where hm.Id == Id
                                 select new HubMaster
                                 {
                                     Id = hm.Id,
                                     HubId = hm.HubId,
                                     HubDesc = hm.HubDesc,
                                 }).FirstOrDefaultAsync();
            if (HubData != null)
            {
                ViewBag.SelectedAreaId = new SelectList(await context.Areas.Where(e => e.IsActive == true && context.HubAreas.Where(a => a.HubId == HubData.HubId).Select(m => m.AreaId)
                                      .Contains(e.Id))
                       .Select(x => new { x.Id, AreaName = x.AreaName }).ToListAsync(), "Id", "AreaName");
            }

            return View(HubData);
        }
    }
}

