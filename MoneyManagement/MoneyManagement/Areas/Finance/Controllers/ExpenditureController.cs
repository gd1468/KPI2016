using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoneyManagement.Areas.Finance.Controllers
{
    public class ExpenditureController : Controller
    {
        // GET: Finance/Expenditure
        public ActionResult Index()
        {
            return View();
        }
    }
}