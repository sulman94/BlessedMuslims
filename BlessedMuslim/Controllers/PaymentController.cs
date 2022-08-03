using BlessedMuslim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlessedMuslim.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class PaymentController : Controller
    {
        private IHostingEnvironment _environment;
        public PaymentController(IHostingEnvironment environment)
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
        public async Task<IActionResult> getPayments()
        {
            var dataPayments = await (from a in context.PaymentDetails
                                    join ci in context.Retailers on a.RetailerId equals ci.Id into aci
                                    from retailer in aci.DefaultIfEmpty()
                                    join s in context.Users on a.UserId equals s.Id into cis
                                    from users in cis.DefaultIfEmpty()
                                    where a.IsActive == true
                                    select new
                                    {
                                        Id = a.Id,
                                        TransactionNumber = a.TransactionNumber,
                                        RetailerName = retailer.BusinessName,
                                        PaymentMode = a.PaymentMode,
                                        PaymentStatus = a.PaymentStatus,
                                        AmountDue = a.AmountDue,
                                        AmountPaid = a.AmountPaid,
                                        Details = a.Details,
                                        Comments = a.Comments
                                    }).ToListAsync();

            return Json(new { data = dataPayments }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public async Task<IActionResult> Add()
        {

            ViewBag.result = "";
            ViewBag.error = "";
            ViewBag.RetailersId = new SelectList(await context.Retailers.Where(x => x.IsActive == true).ToListAsync(), "Id", "BusinessName");
            ViewBag.PaymentMode = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem { Selected = true, Text = "Cheque", Value = "Cheque"},
                    new SelectListItem { Selected = false, Text = "Cash", Value = "Cash"},
                    new SelectListItem { Selected = false, Text = "Bank Transfer", Value = "BankTransfer"},
                    new SelectListItem { Selected = false, Text = "Others", Value = "Others"},
                }, "Value", "Text");
            ViewBag.PaymentStatus = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem { Selected = true, Text = "Full Paid", Value = "FullPaid"},
                    new SelectListItem { Selected = false, Text = "Partial Paid", Value = "PartialPaid"},
                    new SelectListItem { Selected = false, Text = "Advance Paid", Value = "AdvancePaid"},
                    new SelectListItem { Selected = false, Text = "Others", Value = "Others"},
                }, "Value", "Text");
            return View(new PaymentDetails());
        }

        [HttpPost]
        public async Task<IActionResult> Add(PaymentDetails data,IFormFile files)
        {
            try
            {
                string userimg = "";
                if (files != null)
                {
                    if (files.Length > 0)
                    {
                        SaveImg(files, ref userimg);
                    }

                }
                int userId = Convert.ToInt32(HttpContext.User.Identity.Name);
                data.TransactionNumber = Guid.NewGuid().ToString("N").ToUpper();
                data.UserId = userId;
                data.IsActive = true;
                data.Attachment = userimg;
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
            ViewBag.RetailersId = new SelectList(await context.Retailers.Where(x => x.IsActive == true).ToListAsync(), "Id", "BusinessName");
            ViewBag.PaymentMode = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem { Selected = true, Text = "Cheque", Value = "Cheque"},
                    new SelectListItem { Selected = false, Text = "Cash", Value = "Cash"},
                    new SelectListItem { Selected = false, Text = "Bank Transfer", Value = "BankTransfer"},
                    new SelectListItem { Selected = false, Text = "Others", Value = "Others"},
                }, "Value", "Text");
            ViewBag.PaymentStatus = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem { Selected = true, Text = "Full Paid", Value = "FullPaid"},
                    new SelectListItem { Selected = false, Text = "Partial Paid", Value = "PartialPaid"},
                    new SelectListItem { Selected = false, Text = "Advance Paid", Value = "AdvancePaid"},
                    new SelectListItem { Selected = false, Text = "Others", Value = "Others"},
                }, "Value", "Text");
            var std = await context.PaymentDetails.Where(s => s.Id == Id).FirstOrDefaultAsync();

            return View(std);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PaymentDetails data, IFormFile files)
        {
            try
            {
                string userimg = "";
                if (files != null)
                {
                    if (files.Length > 0)
                    {
                        SaveImg(files, ref userimg);
                    }
                    data.Attachment = userimg;
                }
                var dbEntry = context.Entry(data);
                dbEntry.Property("RetailerId").IsModified = true;
                dbEntry.Property("Details").IsModified = true;
                dbEntry.Property("PaymentMode").IsModified = true;
                dbEntry.Property("PaymentStatus").IsModified = true;
                dbEntry.Property("AmountDue").IsModified = true;
                dbEntry.Property("AmountPaid").IsModified = true;
                dbEntry.Property("UserId").IsModified = true;
                dbEntry.Property("Attachment").IsModified = true;
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
                var dataStates = await context.PaymentDetails.Where(c => c.Id == id).FirstOrDefaultAsync();
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
            ViewBag.RetailersId = new SelectList(await context.Retailers.Where(x => x.IsActive == true).ToListAsync(), "Id", "BusinessName");
            ViewBag.PaymentMode = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem { Selected = true, Text = "Cheque", Value = "Cheque"},
                    new SelectListItem { Selected = false, Text = "Cash", Value = "Cash"},
                    new SelectListItem { Selected = false, Text = "Bank Transfer", Value = "BankTransfer"},
                    new SelectListItem { Selected = false, Text = "Others", Value = "Others"},
                }, "Value", "Text");
            ViewBag.PaymentStatus = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem { Selected = true, Text = "Full Paid", Value = "FullPaid"},
                    new SelectListItem { Selected = false, Text = "Partial Paid", Value = "PartialPaid"},
                    new SelectListItem { Selected = false, Text = "Advance Paid", Value = "AdvancePaid"},
                    new SelectListItem { Selected = false, Text = "Others", Value = "Others"},
                }, "Value", "Text");
            var std = await context.PaymentDetails.Where(s => s.Id == Id).FirstOrDefaultAsync();

            return View(std);
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
    }
}
