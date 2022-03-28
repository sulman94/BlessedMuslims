﻿using System;
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
    public class CharitiesController : Controller
    {
        private readonly BlessedMuslim_DBContext context = new BlessedMuslim_DBContext();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> getCharities()
        {
            var dataCountries = await context.Charity.Where(c => c.IsActive == true).ToListAsync();
            dataCountries = dataCountries.OrderByDescending(x => x.Id).ToList();
            return Json(new { data = dataCountries }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult Add()
        {
            ViewBag.result = "";
            ViewBag.error = "";
            return View(new Charity());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Charity data)
        {
            if (data.AddressLine1.Length > 256)
            {
                ViewBag.result = "Address Line 1 is greater than 256 characters";
            }
            if (data.AddressLine2.Length > 256)
            {
                ViewBag.result = "Address Line 1 is greater than 256 characters";
            }
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
            var std = await context.Charity.Where(s => s.Id == Id).FirstOrDefaultAsync();
            return View(std);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Charity data)
        {
            try
            {
                var dbEntry = context.Entry(data);
                dbEntry.Property("CharityId").IsModified = true;
                dbEntry.Property("BusinessName").IsModified = true;
                dbEntry.Property("AddressLine1").IsModified = true;
                dbEntry.Property("AddressLine2").IsModified = true;
                dbEntry.Property("PostCode").IsModified = true;
                dbEntry.Property("Email").IsModified = true;
                dbEntry.Property("ContactPerson").IsModified = true;
                dbEntry.Property("Landline").IsModified = true;
                dbEntry.Property("MobileNumber").IsModified = true;
                dbEntry.Property("JoiningDate").IsModified = true;
                dbEntry.Property("BankName").IsModified = true;
                dbEntry.Property("AccountNumber").IsModified = true;
                dbEntry.Property("SortCode").IsModified = true;
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
                var dataCountry = await context.Charity.Where(c => c.Id == id).FirstOrDefaultAsync();
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
            var std = await context.Charity.Where(s => s.Id == Id).FirstOrDefaultAsync();
            return View(std);
        }
    }
}