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
    public class RetailersContractController : Controller
    {
        private readonly BlessedMuslim_DBContext context = new BlessedMuslim_DBContext();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> getContracts()
        {
            var dataContracts = await (from ci in context.RetailerContracts
                                    join s in context.Retailers on ci.RetailerId equals s.Id 
                                    join co in context.DsrApplicationForm on ci.SaleRepId equals co.Id
                                    where ci.IsActive == true
                                    select new
                                    {
                                        Id = ci.Id,
                                        ContractId = ci.ContractId,
                                        BusinessName = s.BusinessName,
                                        ContractDate = ci.ContractDate == null ? "N/A" : Convert.ToDateTime(ci.ContractDate).ToString("yyyy-MM-dd"),
                                        EndDate = ci.EndDate == null ? "N/A" : Convert.ToDateTime(ci.EndDate).ToString("yyyy-MM-dd"),
                                        SaleRep = co.FirstName + " " + co.LastName
                                    }).ToListAsync();

            return Json(new { data = dataContracts }, new Newtonsoft.Json.JsonSerializerSettings());
        }
        public async Task<IActionResult> Add()
        {
            RetailerContracts contracts = new RetailerContracts();
            string ContractId = "CONT-";
            var retailerContracts = await context.RetailerContracts.OrderByDescending(x=>x.Id).FirstOrDefaultAsync();
            if (retailerContracts!=null)
            {
                ContractId += (retailerContracts.Id + 1).ToString().PadLeft(4, '0');
            }
            else
            {
                ContractId += (1).ToString().PadLeft(4, '0');
            }
            
            contracts.ContractId = ContractId;
            ViewBag.RetailerId = new SelectList(await context.Retailers.Where(x => x.IsActive == true).Select(x => new { x.Id, Name = x.Id + " - " + x.BusinessName }).ToListAsync(), "Id", "Name");
            ViewBag.SaleRepId = new SelectList(await context.DsrApplicationForm.Where(x => x.IsActive == true).Select(x => new { x.Id, Name = x.Id + " - " + x.FirstName +" " + x.LastName }).ToListAsync(), "Id", "Name");
            return View(contracts);
        }
        [HttpPost]
        public async Task<IActionResult> Add(RetailerContracts data)
        {
            try
            {
                string ContractId = "CONT-";
                var retailerContracts = await context.RetailerContracts.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                if (retailerContracts != null)
                {
                    ContractId += (retailerContracts.Id + 1).ToString().PadLeft(4, '0');
                }
                else
                {
                    ContractId += (1).ToString().PadLeft(4, '0');
                }

                data.ContractId = ContractId;
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
            RetailerContracts contracts = await context.RetailerContracts.Where(x => x.Id == Id).FirstOrDefaultAsync();
            ViewBag.RetailerId = new SelectList(await context.Retailers.Where(x => x.IsActive == true).Select(x => new { x.Id, Name = x.Id + " - " + x.BusinessName }).ToListAsync(), "Id", "Name");
            ViewBag.SaleRepId = new SelectList(await context.DsrApplicationForm.Where(x => x.IsActive == true).Select(x => new { x.Id, Name = x.Id + " - " + x.FirstName + " " + x.LastName }).ToListAsync(), "Id", "Name");
            return View(contracts);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RetailerContracts data)
        {
            try
            {
                var dbEntry = context.Entry(data);
                dbEntry.Property("ContractDate").IsModified = true;
                dbEntry.Property("ContractPeriod").IsModified = true;
                dbEntry.Property("StartDate").IsModified = true;
                dbEntry.Property("EndDate").IsModified = true;
                dbEntry.Property("AccNo").IsModified = true;
                dbEntry.Property("SortCode").IsModified = true;
                dbEntry.Property("CommissionPercentage").IsModified = true;
                dbEntry.Property("ContractAmount").IsModified = true;
                dbEntry.Property("ContractSignedBy").IsModified = true;
                dbEntry.Property("SaleRepId").IsModified = true;
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

        public async Task<IActionResult> Details(int Id)
        {
            RetailerContracts contracts = await context.RetailerContracts.Where(x => x.Id == Id).FirstOrDefaultAsync();
            ViewBag.RetailerId = new SelectList(await context.Retailers.Where(x => x.IsActive == true).Select(x => new { x.Id, Name = x.Id + " - " + x.BusinessName }).ToListAsync(), "Id", "Name");
            ViewBag.SaleRepId = new SelectList(await context.DsrApplicationForm.Where(x => x.IsActive == true).Select(x => new { x.Id, Name = x.Id + " - " + x.FirstName + " " + x.LastName }).ToListAsync(), "Id", "Name");
            return View(contracts);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                RetailerContracts contracts = await context.RetailerContracts.Where(x => x.Id == Id).FirstOrDefaultAsync();
                contracts.IsActive = false;
                context.RetailerContracts.Update(contracts);
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
