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
    public class ContractPaymentsController : Controller
    {
        private IHostingEnvironment _environment;
        private readonly BlessedMuslim_DBContext context = new BlessedMuslim_DBContext();

        public ContractPaymentsController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> getContractPayments()
        {
            var dataPayments = await (from a in context.ContractPayments
                                      join rc in context.RetailerContracts on a.ContractId equals rc.ContractId.ToString()
                                      join ci in context.Retailers on a.RetailerId equals ci.Id into aci
                                    from retailer in aci.DefaultIfEmpty()
                                    join s in context.Users on a.UserId equals s.Id into cis
                                    from users in cis.DefaultIfEmpty()
                                    where a.IsActive == true
                                    select new
                                    {
                                        Id = a.Id,
                                        RetailerName = retailer.BusinessName,
                                        ContractAmount = rc.ContractAmount,
                                        RefNumber = a.RefNumber,
                                        Comments = a.Comments
                                    }).ToListAsync();

            return Json(new { data = dataPayments }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public async Task<IActionResult> Add()
        {
            List<long> contractswalyRetailers = await (from rc in context.ContractPayments
                                      join ci in context.Retailers on rc.RetailerId equals ci.Id into aci
                                      from retailer in aci.DefaultIfEmpty()
                                      where rc.IsActive == true
                                      select retailer.Id).ToListAsync();

            ViewBag.result = "";
            ViewBag.error = "";
            ViewBag.RetailersId = new SelectList(await context.Retailers.Where(x => x.IsActive == true && !contractswalyRetailers.Contains(x.Id)).ToListAsync(), "Id", "BusinessName");
            return View(new ContractPaymentViewModel() { TransactionDate = DateTime.Now});
        }

        [HttpGet]
        public async Task<IActionResult> getContractDetails(long RetailerId)
        {
            var dataPayments = await (from rc in context.RetailerContracts
                                      join ci in context.Retailers on rc.RetailerId equals ci.Id into aci
                                      from retailer in aci.DefaultIfEmpty()
                                      where rc.IsActive == true && retailer.Id == RetailerId
                                      select new
                                      {
                                          ContractName = rc.ContractId,
                                          RetailerName = retailer.BusinessName,
                                          ContractPeriod = rc.ContractPeriod,
                                          ContractAmount = rc.ContractAmount
                                      }).FirstOrDefaultAsync();

            return Json(new { data = dataPayments }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        [HttpPost]
        public async Task<IActionResult> Add(ContractPaymentViewModel data)
        {
            try
            {
                var dataPayments = await (from rc in context.RetailerContracts
                                          join ci in context.Retailers on rc.RetailerId equals ci.Id into aci
                                          from retailer in aci.DefaultIfEmpty()
                                          where rc.IsActive == true && retailer.Id == data.RetailerId
                                          select new
                                          {
                                              ContractName = rc.ContractId,
                                              RetailerName = retailer.BusinessName,
                                              ContractPeriod = rc.ContractPeriod,
                                              ContractAmount = rc.ContractAmount
                                          }).FirstOrDefaultAsync();
                ContractPayments contractPayments = new ContractPayments();
                int userId = Convert.ToInt32(HttpContext.User.Identity.Name);
                contractPayments.RefNumber = data.RefNumber;
                contractPayments.Comments = data.Comments;
                contractPayments.SaleRepId = userId;
                contractPayments.RetailerId = data.RetailerId;
                contractPayments.ContractId = dataPayments.ContractName;
                contractPayments.IsActive = true;
                contractPayments.TransactionDate = data.TransactionDate;
                contractPayments.UserId = userId;
                context.ContractPayments.Add(contractPayments);
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

        //public async Task<IActionResult> Edit(int Id)
        //{
        //    ViewBag.RetailersId = new SelectList(await context.Retailers.Where(x => x.IsActive == true).ToListAsync(), "Id", "BusinessName");
        //    ViewBag.PaymentMode = new SelectList(new List<SelectListItem>
        //        {
        //            new SelectListItem { Selected = true, Text = "Cheque", Value = "Cheque"},
        //            new SelectListItem { Selected = false, Text = "Cash", Value = "Cash"},
        //            new SelectListItem { Selected = false, Text = "Bank Transfer", Value = "BankTransfer"},
        //            new SelectListItem { Selected = false, Text = "Others", Value = "Others"},
        //        }, "Value", "Text");
        //    ViewBag.PaymentStatus = new SelectList(new List<SelectListItem>
        //        {
        //            new SelectListItem { Selected = true, Text = "Full Paid", Value = "FullPaid"},
        //            new SelectListItem { Selected = false, Text = "Partial Paid", Value = "PartialPaid"},
        //            new SelectListItem { Selected = false, Text = "Advance Paid", Value = "AdvancePaid"},
        //            new SelectListItem { Selected = false, Text = "Others", Value = "Others"},
        //        }, "Value", "Text");
        //    var std = await context.PaymentDetails.Where(s => s.Id == Id).FirstOrDefaultAsync();

        //    return View(std);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(PaymentDetails data, IFormFile files)
        //{
        //    try
        //    {
        //        string userimg = "";
        //        if (files != null)
        //        {
        //            if (files.Length > 0)
        //            {
        //                SaveImg(files, ref userimg);
        //            }
        //            data.Attachment = userimg;
        //        }
        //        var dbEntry = context.Entry(data);
        //        dbEntry.Property("RetailerId").IsModified = true;
        //        dbEntry.Property("Details").IsModified = true;
        //        dbEntry.Property("PaymentMode").IsModified = true;
        //        dbEntry.Property("PaymentStatus").IsModified = true;
        //        dbEntry.Property("AmountDue").IsModified = true;
        //        dbEntry.Property("AmountPaid").IsModified = true;
        //        dbEntry.Property("UserId").IsModified = true;
        //        dbEntry.Property("Attachment").IsModified = true;
        //        await context.SaveChangesAsync();
        //        ViewBag.result = "Record Updated Successfully!";
        //    }
        //    catch (Exception e)
        //    {
        //        var error = e;
        //        ViewBag.error = e.Message;
        //    }
        //    return RedirectToAction("Index");
        //}

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var dataStates = await context.ContractPayments.Where(c => c.Id == id).FirstOrDefaultAsync();
                dataStates.IsActive = false;
                context.ContractPayments.Update(dataStates);
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
            
             var dataPayments = await (from cp in context.ContractPayments 
                                       join rc in context.RetailerContracts on cp.ContractId equals rc.ContractId
                                       join ci in context.Retailers on rc.RetailerId equals ci.Id into aci
                                       from retailer in aci.DefaultIfEmpty()
                                       where rc.IsActive == true && cp.Id == Id
                                       select new ContractPaymentViewModel
                                       {
                                           ContractName = cp.ContractId,
                                           RetailerName = retailer.BusinessName,
                                           Comments = cp.Comments,
                                           RefNumber = cp.RefNumber,
                                           TransactionDate = Convert.ToDateTime(cp.TransactionDate),
                                           RetailerId = cp.RetailerId,
                                           SaleRepId = cp.SaleRepId,
                                           ContractPeriod = rc.ContractPeriod,
                                           ContractAmount = Convert.ToDecimal(rc.ContractAmount)
                                       }).FirstOrDefaultAsync();

            return View(dataPayments);
        }
    }
}
