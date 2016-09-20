using System;
using System.Data.Entity;
using System.Threading.Tasks;
using MoneyManagement.Persistance.Interfaces;
using MoneyManagement.ServiceLayer.ClientPresentations;
using MoneyManagement.ServiceLayer.Interfaces;

namespace MoneyManagement.ServiceLayer.Queries
{
    public class GetCultureQuery : IQuery<GetCultureQuery.Result>
    {
        public class Result
        {
            public CulturePresentation CulturePresentation { get; set; }
        }
    }

    public class GetCultureQueryHandler : IQueryHandler<GetCultureQuery, GetCultureQuery.Result>
    {
        private readonly IMoneyManagementContext _db;

        public GetCultureQueryHandler(IMoneyManagementContext db)
        {
            _db = db;
        }

        public async Task<GetCultureQuery.Result> Execute(GetCultureQuery query)
        {
            var culture = await _db.Cultures.FirstOrDefaultAsync();
            return new GetCultureQuery.Result
            {
                CulturePresentation = new CulturePresentation
                {
                    KeyId = culture.KeyId,
                    IsPrimary = culture.IsPrimary,
                    ShortName = culture.ShortName
                }
            };
        }
    }
}
