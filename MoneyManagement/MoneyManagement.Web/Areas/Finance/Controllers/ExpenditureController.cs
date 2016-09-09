using System.Web.Mvc;

namespace MoneyManagement.Web.Areas.Finance.Controllers
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