using System;
using System.Threading.Tasks;
using System.Web.Http;
using MoneyManagement.ServiceLayer.Commands;
using MoneyManagement.ServiceLayer.Interfaces;
using MoneyManagement.ServiceLayer.Queries;
using MoneyManagement.Web.Models;

namespace MoneyManagement.Web.Controllers.Finance
{
    public class ExpenditureApiController : ApiController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public ExpenditureApiController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        public async Task<ExpenditureViewModel> Post(SaveExpenditureCommand command)
        {
            await _commandDispatcher.Execute<SaveExpenditureCommand, SaveExpenditureCommand.Result>(command);
            var expenditures =
                await _queryDispatcher.Execute<GetExpenditureQuery, GetExpenditureQuery.Result>(new GetExpenditureQuery
                {
                    CultureId = command.CultureId,
                    UserId = command.UserId
                });
            var accounts = await _queryDispatcher.Execute<GetAccountQuery, GetAccountQuery.Result>(new GetAccountQuery
            {
                UserId = command.UserId,
                CultureId = command.CultureId
            });
            var budgets = await _queryDispatcher.Execute<GetBudgetQuery, GetBudgetQuery.Result>(new GetBudgetQuery
            {
                UserId = command.UserId,
                CultureId = command.CultureId
            });
            return new ExpenditureViewModel
            {
                AccountPresentations = accounts.AccountPresentations,
                BudgetPresentations = budgets.BudgetPresentations,
                ExpenditurePresentations = expenditures.ExpenditurePresentations
            };
        }

        public async Task<ExpenditureViewModel> Put(DepositToExistingAccountCommand command)
        {
            var result = await _commandDispatcher.Execute<DepositToExistingAccountCommand, DepositToExistingAccountCommand.Result>(command);

            var accounts = await _queryDispatcher.Execute<GetAccountQuery, GetAccountQuery.Result>(new GetAccountQuery
            {
                UserId = command.UserId,
                CultureId = command.CultureId
            });

            var expenditures = await _queryDispatcher.Execute<GetExpenditureQuery, GetExpenditureQuery.Result>(new GetExpenditureQuery
            {
                UserId = command.UserId,
                CultureId = command.CultureId
            });
            return new ExpenditureViewModel
            {
                AccountPresentations = accounts.AccountPresentations,
                ExpenditurePresentations = expenditures.ExpenditurePresentations,
                EffectiveRows = result.EffectiveRows
            };
        }

        [HttpPut]
        [Route("api/ExpenditureApi/Edit")]
        public async Task<ExpenditureViewModel> Edit(UpdateExpenditureCommand command)
        {
            var result = await _commandDispatcher.Execute<UpdateExpenditureCommand, UpdateExpenditureCommand.Result>(command);

            var accounts = await _queryDispatcher.Execute<GetAccountQuery, GetAccountQuery.Result>(new GetAccountQuery
            {
                UserId = command.UserId,
                CultureId = command.CultureId
            });

            var expenditures = await _queryDispatcher.Execute<GetExpenditureQuery, GetExpenditureQuery.Result>(new GetExpenditureQuery
            {
                UserId = command.UserId,
                CultureId = command.CultureId
            });

            var budgets = await _queryDispatcher.Execute<GetBudgetQuery, GetBudgetQuery.Result>(new GetBudgetQuery
            {
                UserId = command.UserId,
                CultureId = command.CultureId
            });
            return new ExpenditureViewModel
            {
                AccountPresentations = accounts.AccountPresentations,
                ExpenditurePresentations = expenditures.ExpenditurePresentations,
                BudgetPresentations = budgets.BudgetPresentations,
                EffectiveRows = result.EffectiveRows
            };
        }

        public async Task<GetExpenditureQuery.Result> Get([FromUri]GetExpenditureQuery query)
        {
            return await _queryDispatcher.Execute<GetExpenditureQuery, GetExpenditureQuery.Result>(query);
        }

        public async Task<ExpenditureViewModel> Delete([FromUri]RemoveExpenditureCommand command)
        {
            var result = await _commandDispatcher.Execute<RemoveExpenditureCommand, RemoveExpenditureCommand.Result>(command);

            var expenditures =
                await _queryDispatcher.Execute<GetExpenditureQuery, GetExpenditureQuery.Result>(new GetExpenditureQuery
                {
                    CultureId = command.CultureId,
                    UserId = command.UserId
                });

            var accounts = await _queryDispatcher.Execute<GetAccountQuery, GetAccountQuery.Result>(new GetAccountQuery
            {
                UserId = command.UserId,
                CultureId = command.CultureId
            });

            var budgets = await _queryDispatcher.Execute<GetBudgetQuery, GetBudgetQuery.Result>(new GetBudgetQuery
            {
                CultureId = command.CultureId,
                UserId = command.UserId
            });
            return new ExpenditureViewModel
            {
                AccountPresentations = accounts.AccountPresentations,
                BudgetPresentations = budgets.BudgetPresentations,
                ExpenditurePresentations = expenditures.ExpenditurePresentations,
                EffectiveRows = result.EffectiveRows
            };
        }
    }
}
