﻿using BlessedMuslim.Models;
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
    public class AppicationFormController : Controller
    {
        private readonly BlessedMuslim_DBContext context = new BlessedMuslim_DBContext();
        // GET: AppicationFormController

        [AllowAnonymous]
        public async Task<ActionResult> Apply()
        {
            var context = new BlessedMuslim_DBContext();

            ViewBag.AreaId = new SelectList(await context.Areas.Where(x=>x.IsActive == true).Select(x=>new {x.Id, AreaName = x.AreaName}).ToListAsync(), "Id", "AreaName");
            ViewBag.TermsAndConditions = await context.Config.Where(x=>x.ConfigCode == "T&C").Select(x=>x.ConfigValue).FirstOrDefaultAsync();
            return View();
        }

        [AllowAnonymous]
        public  ActionResult Thankyou()
        {
            return View();
        }

        // POST: AppicationFormController/Save
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Apply(DsrApplicationForm data)
        {
            var context = new BlessedMuslim_DBContext();
            try
            {
                if (data.TermsAgreement)
                {
                    data.SubmitDate = DateTime.Now;
                    context.DsrApplicationForm.Add(data);
                    await context.SaveChangesAsync();
                    ViewBag.ThankyouMsg = await context.Config.Where(x => x.ConfigCode == "ThankyouMessage").Select(x => x.ConfigValue).FirstOrDefaultAsync();
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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> getApplications()
        {
            var isManager = HttpContext.User.IsInRole("Manager");
            long userId;
            if (isManager)
            {
                userId = Convert.ToInt64(HttpContext.User.Identity.Name);
                var dataApplications = await (from u in context.Users
                                              join
                      ha in context.HubAreas on u.HubId equals ha.HubId
                                              join
           ar in context.Areas on ha.AreaId equals ar.Id
                                              join
           a in context.DsrApplicationForm on ar.Id equals a.AreaId
                                              where u.Id == userId && ar.IsActive== true
                                              select new
                                              {
                                                  Id = a.Id,
                                                  Name = a.FirstName + " " + a.LastName,
                                                  DOB = a.Dob,
                                                  ContactNumber = a.ContactNumber,
                                                  SubmitDate = a.SubmitDate == null ? "N/A" : Convert.ToDateTime(a.SubmitDate).ToString("yyyy-MM-dd"),
                                                  Area = ar.AreaCode + " - " + ar.AreaName,
                                                  Status = a.RejectedDate != null ? "Rejected" : a.ApprovedDate != null ? "Approved" : "Submitted"
                                              }).ToListAsync();
                dataApplications = dataApplications.OrderByDescending(x => x.SubmitDate).ToList();
                return Json(new { data = dataApplications }, new Newtonsoft.Json.JsonSerializerSettings());
            }
            else
            {

                var dataApplications = await (from a in context.DsrApplicationForm
                                              join ci in context.Areas on a.AreaId equals ci.Id
                                              select new
                                              {
                                                  Id = a.Id,
                                                  Name = a.FirstName + " " + a.LastName,
                                                  DOB = a.Dob,
                                                  ContactNumber = a.ContactNumber,
                                                  SubmitDate = a.SubmitDate == null ? "N/A" : Convert.ToDateTime(a.SubmitDate).ToString("yyyy-MM-dd"),
                                                  Area = ci.AreaCode + " - " + ci.AreaName,
                                                  Status = a.RejectedDate != null ? "Rejected" : a.ApprovedDate != null ? "Approved" : "Submitted"
                                              }).ToListAsync();
                dataApplications = dataApplications.OrderByDescending(x => x.SubmitDate).ToList();
                return Json(new { data = dataApplications }, new Newtonsoft.Json.JsonSerializerSettings());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Approve(int id)
        {
            try
            {
                var dataApplication = await context.DsrApplicationForm.Where(c => c.Id == id).FirstOrDefaultAsync();
                if (dataApplication!= null)
                {
                    //dataApplication.ApprovedBy = "";
                    dataApplication.RejectedBy = null;
                    dataApplication.RejectedDate = null;
                    dataApplication.ApprovedDate = DateTime.Now;
                    context.Update(dataApplication);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var error = e;
                ViewBag.error = e.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Reject(int id)
        {
            try
            {
                var dataApplication = await context.DsrApplicationForm.Where(c => c.Id == id).FirstOrDefaultAsync();
                if (dataApplication != null)
                {
                    dataApplication.ApprovedBy = null;
                    dataApplication.ApprovedDate = null;
                    dataApplication.RejectedDate = DateTime.Now;
                    context.Update(dataApplication);
                    await context.SaveChangesAsync();
                }
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
            var dataApplications = await (from a in context.DsrApplicationForm
                                          join ci in context.Areas on a.AreaId equals ci.Id
                                          where a.Id == Id
                                          select new DsrApplicationFormView
                                          {
                                              Id = a.Id,
                                              FirstName = a.FirstName,
                                              LastName = a.LastName,
                                              Dob = Convert.ToDateTime(a.Dob).ToString("yyyy-MM-dd"),
                                              Gender = a.Gender,
                                              AccountNo = a.AccountNo,
                                              ApprovedDate= a.ApprovedDate,
                                              DateOfJoining = a.DateOfJoining,
                                              Phone1 = a.Phone1, 
                                              Phone2 = a.Phone2,
                                              ReferenceCode = a.ReferenceCode,
                                              RejectedDate = a.RejectedDate,
                                              SortCode = a.SortCode,
                                              ContactNumber = a.ContactNumber,
                                              AddressLine1 = a.AddressLine1,
                                              Email = a.Email,
                                              AddressLine2 = a.AddressLine2,
                                              PostCode = a.PostCode,
                                              Remarks  = a.Remarks,
                                              AreaName = ci.AreaCode + " - " + ci.AreaName,
                                              SubmitDate = a.SubmitDate == null ? "N/A" : Convert.ToDateTime(a.SubmitDate).ToString()
                                          }).ToListAsync();
            return View(dataApplications[0]);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            ViewBag.AreaId = new SelectList(await context.Areas.Where(x => x.IsActive == true).Select(x => new { x.Id, AreaName = x.AreaName }).ToListAsync(), "Id", "AreaName");
            var std = await context.DsrApplicationForm.Where(s => s.Id == Id).FirstOrDefaultAsync();
            return View(std);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DsrApplicationForm data)
        {
            try
            {
                data.ModifiedDate = DateTime.Now;
                var dbEntry = context.Entry(data);
                dbEntry.Property("FirstName").IsModified = true;
                dbEntry.Property("LastName").IsModified = true;
                dbEntry.Property("DOB").IsModified = true;
                dbEntry.Property("AddressLine1").IsModified = true;
                dbEntry.Property("AddressLine2").IsModified = true;
                dbEntry.Property("PostCode").IsModified = true;
                dbEntry.Property("ContactNumber").IsModified = true;
                dbEntry.Property("AreaId").IsModified = true;
                dbEntry.Property("ApprovedDate").IsModified = true;
                dbEntry.Property("RejectedDate").IsModified = true;
                dbEntry.Property("Remarks").IsModified = true;
                dbEntry.Property("ModifiedDate").IsModified = true;
                dbEntry.Property("Email").IsModified = true;
                dbEntry.Property("Gender").IsModified = true;
                dbEntry.Property("Phone1").IsModified = true;
                dbEntry.Property("Phone2").IsModified = true;
                dbEntry.Property("DateOfJoining").IsModified = true;
                dbEntry.Property("AccountNo").IsModified = true;
                dbEntry.Property("SortCode").IsModified = true;
                dbEntry.Property("ReferenceCode").IsModified = true;
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
    }
}
