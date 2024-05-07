using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace build9ja.admin.Controllers
{
    public class ReportController : Controller
    {
        // GET: /<controller>/
        public async Task<ActionResult> StockReport()
        {
            return View();
        }
    }
}

