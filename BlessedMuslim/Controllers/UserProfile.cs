﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlessedMuslim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;

namespace BlessedMuslim.Controllers
{

    [Authorize(Roles = "Admin")]
    public class UserProfile : Controller
    {
        private IHostingEnvironment _environment;
        public UserProfile(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        private readonly BlessedMuslim_DBContext context = new BlessedMuslim_DBContext();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> getUsers()
        {
            var UserInfo = await (from u in context.Users
                                  join r in context.Role on u.RoleId equals r.Id
                                  select new
                                  {
                                      Id = u.Id,
                                      Name = u.Name,
                                      Email = u.Email,
                                      ContactNumber = u.Phone,
                                      CreatedDate = u.CreatedDate == null ? "N/A" : Convert.ToDateTime(u.CreatedDate).ToString("yyyy-MM-dd"),
                                      Role = r.RoleName,
                                      Deleted = (u.IsDeleted) ? "Yes" : "No"
                                  }).ToListAsync();
            UserInfo = UserInfo.OrderByDescending(x => x.Id).ToList();
            return Json(new { data = UserInfo }, new Newtonsoft.Json.JsonSerializerSettings());
        }
        public async Task<IActionResult> Add()
        {
            ViewBag.result = "";
            ViewBag.error = "";
            int _min = 1000;
            int _max = 9999;
            Random rdm = new Random();
            ViewBag.ManagerId = new SelectList(await context.Users.Include(x => x.Role).Where(x => x.Role.RoleName == "Manager").Select(x => new { x.Id, ManagerName = x.Name }).ToListAsync(), "Id", "ManagerName");
            ViewBag.RoleId = new SelectList(await context.Role.Select(x => new { x.Id, RoleName = x.RoleName }).ToListAsync(), "Id", "RoleName");
            ViewBag.SalesRep = new SelectList(await context.DsrApplicationForm.Where(a => a.ApprovedDate != null && a.RejectedDate == null && a.IsUserCreated != true).Select(x => new { x.Id, salesRepName = x.FirstName + "" + x.LastName + rdm.Next(_min, _max) }).ToListAsync(), "Id", "salesRepName");
            ViewBag.HubId = new SelectList(await context.HubAreas.Select(x => new { x.HubId, HubName = x.HubId }).Distinct().ToListAsync(), "HubId", "HubName");

            return View(new Users());
        }
        [HttpPost]
        public async Task<IActionResult> Add(Users data, IFormFile files, IFormFile Documents)
        {
            try
            {
                string userimg = "";
                string Doc = "";
                if (files != null)
                {
                    if (files.Length > 0)
                    {
                        SaveImg(files, ref userimg);
                    }

                }
                if (Documents != null)
                {
                    if (Documents.Length > 0)
                    {
                        SaveImg(Documents, ref Doc);
                    }

                }
                var SaleRep = await context.DsrApplicationForm.Where(c => c.Id == data.Id).FirstOrDefaultAsync();
                data.CreatedDate = DateTime.Now;
                data.Id = 0;
                data.UserImage = userimg.Length > 0 ? userimg : null;
                data.Document = Doc.Length > 0 ? Doc : null;
                context.Add(data);
                if (SaleRep != null)
                {
                    SaleRep.IsUserCreated = true;
                    context.Update(SaleRep);
                }

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
        public void SaveImg(IFormFile files, ref string newFileName)
        {
            var allowedExtensions = new[] {
                        ".Jpg", ".png", ".jpg", ".jpeg",".PNG"
                    };
            var ext = Path.GetExtension(files.FileName);
            if (allowedExtensions.Contains(ext))
            {
                var fileName = Path.GetFileName(files.FileName);
                var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                var fileExtension = Path.GetExtension(fileName);
                newFileName = String.Concat(myUniqueFileName, fileExtension);
                var uploads = Path.Combine(_environment.WebRootPath, "Documents");
                var filepath = Path.Combine(uploads, newFileName);
                //var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Documents")).Root + $@"\{newFileName}";
                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    files.CopyTo(fs);
                    fs.Flush();
                }
            }
        }
        public async Task<IActionResult> Edit(int Id)
        {
            int _min = 1000;
            int _max = 9999;
            Random rdm = new Random();
            ViewBag.ManagerId = new SelectList(await context.Users.Include(x => x.Role).Where(x => x.Role.RoleName == "Manager").Select(x => new { x.Id, ManagerName = x.Name }).ToListAsync(), "Id", "ManagerName");
            ViewBag.RoleId = new SelectList(await context.Role.Select(x => new { x.Id, RoleName = x.RoleName }).ToListAsync(), "Id", "RoleName");
            ViewBag.SalesRep = new SelectList(await context.DsrApplicationForm.Where(a => a.ApprovedDate != null && a.RejectedDate == null && a.IsUserCreated != true).Select(x => new { x.Id, salesRepName = x.FirstName + "" + x.LastName + rdm.Next(_min, _max) }).ToListAsync(), "Id", "salesRepName");
            ViewBag.HubId = new SelectList(await context.HubAreas.Select(x => new { x.HubId, HubName = x.HubId }).Distinct().ToListAsync(), "HubId", "HubName");
            var std = await context.Users.Where(s => s.Id == Id).FirstOrDefaultAsync();
            return View(std);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Users data, IFormFile files, IFormFile Documents)
        {
            try
            {
                string userimg = "";
                string Doc = "";
                if (files != null)
                {
                    if (files.Length > 0)
                    {
                        SaveImg(files, ref userimg);
                    }

                }
                if (Documents != null)
                {
                    if (Documents.Length > 0)
                    {
                        SaveImg(Documents, ref Doc);
                    }

                }
                if ((data.UserImage == null && userimg.Length > 0) || (data.UserImage != null && userimg.Length > 0))
                {
                    data.UserImage = userimg;
                }
                if ((data.Document == null && Doc.Length > 0) || (data.Document != null && Doc.Length > 0))
                {
                    data.Document = Doc;
                }

                var dbEntry = context.Entry(data);
                dbEntry.Property("Name").IsModified = true;
                dbEntry.Property("Email").IsModified = true;
                dbEntry.Property("Phone").IsModified = true;
                dbEntry.Property("RoleId").IsModified = true;
                dbEntry.Property("ReportTo").IsModified = true;
                dbEntry.Property("HubId").IsModified = true;
                dbEntry.Property("UserImage").IsModified = true;
                dbEntry.Property("Document").IsModified = true;
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


        public async Task<ActionResult> Details(int Id)
        {
            Random rdm = new Random();
            int _min = 1000;
            int _max = 9999;
            ViewBag.ManagerId = new SelectList(await context.Users.Include(x => x.Role).Where(x => x.Role.RoleName == "Manager").Select(x => new { x.Id, ManagerName = x.Name }).ToListAsync(), "Id", "ManagerName");
            ViewBag.RoleId = new SelectList(await context.Role.Select(x => new { x.Id, RoleName = x.RoleName }).ToListAsync(), "Id", "RoleName");
            ViewBag.SalesRep = new SelectList(await context.DsrApplicationForm.Where(a => a.ApprovedDate != null && a.RejectedDate == null && a.IsUserCreated != true).Select(x => new { x.Id, salesRepName = x.FirstName + "" + x.LastName + rdm.Next(_min, _max) }).ToListAsync(), "Id", "salesRepName");
            ViewBag.HubId = new SelectList(await context.HubAreas.Select(x => new { x.HubId, HubName = x.HubId }).Distinct().ToListAsync(), "HubId", "HubName");
            var std = await context.Users.Where(s => s.Id == Id).FirstOrDefaultAsync();
            return View(std);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var dataUsers = await context.Users.Where(c => c.Id == id).FirstOrDefaultAsync();
                dataUsers.IsDeleted = true;
                context.Update(dataUsers);
                await context.SaveChangesAsync();
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