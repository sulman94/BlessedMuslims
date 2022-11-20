using Microsoft.AspNetCore.Mvc;
using System;

namespace BlessedMuslim.Controllers
{
    public class ValidationController : Controller
    {
        [HttpPost]
        public JsonResult IsTrxDateValid(string TransactionDate)
        {
            var min = DateTime.Now.AddMonths(-1);
            var max = DateTime.Now;
            var msg = string.Format("Please enter a value between {0:MM/dd/yyyy} and {1:MM/dd/yyyy}", min, max);
            try
            {
                var date = DateTime.Parse(TransactionDate);
                if (date > min || date < max)
                    return Json(true);
                else
                    return Json(msg);
            }
            catch (Exception)
            {
                return Json(msg);
            }
        }
    }
}
