using System.Collections.Generic;
using MoneyManagement.ServiceLayer.ClientPresentations;

namespace MoneyManagement.Web.Models
{
    public class ExpenditureViewModel
    {
        public List<ExpenditurePresentation> ExpenditurePresentations { get; set; }
        public List<AccountPresentation> AccountPresentations { get; set; }
        public List<BudgetPresentation> BudgetPresentations { get; set; }
        public int EffectiveRows { get; set; }
    }
}