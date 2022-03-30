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
    public class RetailersController : Controller
    {
        private readonly BlessedMuslim_DBContext context = new BlessedMuslim_DBContext();

        public RetailersController()
        {
        }

        [AllowAnonymous]
        public async Task<ActionResult> Register()
        {
            var context = new BlessedMuslim_DBContext();

            ViewBag.BusinessCategoryId = new SelectList(await context.BusinessCategories.Where(x => x.IsActive == true).Select(x => new { x.Id, Ldesc = x.Ldesc }).ToListAsync(), "Id", "Ldesc");
            ViewBag.CityId = new SelectList(await context.Cities.Where(x => x.IsActive == true).Select(x => new { x.Id, CityName = x.CityName }).ToListAsync(), "Id", "CityName");
            ViewBag.TermsAndConditions = await context.Config.Where(x => x.ConfigCode == "T&CRetailer").Select(x => x.ConfigValue).FirstOrDefaultAsync();
            return View();
        }

        [AllowAnonymous]
        public ActionResult Thankyou()
        {
            return View();
        }

        // POST: AppicationFormController/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Retailers data)
        {
            var context = new BlessedMuslim_DBContext();
            try
            {
                if (data.TermsAgreement)
                {
                    data.CreatedDate = DateTime.Now;
                    context.Retailers.Add(data);
                    await context.SaveChangesAsync();
                    ViewBag.ThankyouMsg = await context.Config.Where(x => x.ConfigCode == "ThankyouMessageRetailer").Select(x => x.ConfigValue).FirstOrDefaultAsync();
                    ViewBag.result = "Registration has Been Done Successfully !";
                }
                else
                {
                    ViewBag.error = "Please Agree with Terms and Conditions";
                }

            }
            catch (Exception e)
            {
                var error = e;
                ViewBag.error = e.Message;
            }
            return View("Thankyou");
            //return RedirectToAction("Apply");

        }

        //// GET: Retailers
        //public async Task<IActionResult> Index()
        //{
        //    var blessedMuslim_DBContext = _context.Retailers.Include(r => r.CityCodeNavigation).Include(r => r.RegByNavigation);
        //    return View(await blessedMuslim_DBContext.ToListAsync());
        //}

        //// GET: Retailers/Details/5
        //public async Task<IActionResult> Details(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var retailers = await _context.Retailers
        //        .Include(r => r.CityCodeNavigation)
        //        .Include(r => r.RegByNavigation)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (retailers == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(retailers);
        //}

        //// GET: Retailers/Create
        //public IActionResult Create()
        //{
        //    ViewData["CityCode"] = new SelectList(_context.Cities, "Id", "Id");
        //    ViewData["RegBy"] = new SelectList(_context.Users, "Id", "Email");
        //    return View();
        //}

        //// POST: Retailers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,BusinessName,BusinessCategoryId,CityCode,AddressLine1,AddressLine2,PostCode,Email,ContactPerson,ShopPhone,MobileNumber,RegDate,RegBy,Comments,IsActive,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] Retailers retailers)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(retailers);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CityCode"] = new SelectList(_context.Cities, "Id", "Id", retailers.CityCode);
        //    ViewData["RegBy"] = new SelectList(_context.Users, "Id", "Email", retailers.RegBy);
        //    return View(retailers);
        //}

        //// GET: Retailers/Edit/5
        //public async Task<IActionResult> Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var retailers = await _context.Retailers.FindAsync(id);
        //    if (retailers == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CityCode"] = new SelectList(_context.Cities, "Id", "Id", retailers.CityCode);
        //    ViewData["RegBy"] = new SelectList(_context.Users, "Id", "Email", retailers.RegBy);
        //    return View(retailers);
        //}

        //// POST: Retailers/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(long id, [Bind("Id,BusinessName,BusinessCategoryId,CityCode,AddressLine1,AddressLine2,PostCode,Email,ContactPerson,ShopPhone,MobileNumber,RegDate,RegBy,Comments,IsActive,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] Retailers retailers)
        //{
        //    if (id != retailers.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(retailers);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!RetailersExists(retailers.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CityCode"] = new SelectList(_context.Cities, "Id", "Id", retailers.CityCode);
        //    ViewData["RegBy"] = new SelectList(_context.Users, "Id", "Email", retailers.RegBy);
        //    return View(retailers);
        //}

        //// GET: Retailers/Delete/5
        //public async Task<IActionResult> Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var retailers = await _context.Retailers
        //        .Include(r => r.CityCodeNavigation)
        //        .Include(r => r.RegByNavigation)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (retailers == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(retailers);
        //}

        //// POST: Retailers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(long id)
        //{
        //    var retailers = await _context.Retailers.FindAsync(id);
        //    _context.Retailers.Remove(retailers);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool RetailersExists(long id)
        //{
        //    return _context.Retailers.Any(e => e.Id == id);
        //}
    }
}
