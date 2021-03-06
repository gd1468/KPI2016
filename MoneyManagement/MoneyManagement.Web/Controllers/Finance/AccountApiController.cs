﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using MoneyManagement.ServiceLayer.ClientPresentations;
using MoneyManagement.ServiceLayer.Commands;
using MoneyManagement.ServiceLayer.Interfaces;
using MoneyManagement.ServiceLayer.Queries;

namespace MoneyManagement.Web.Controllers.Finance
{
    public class AccountApiController : ApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        public AccountApiController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        public async Task<GetAccountByIdQuery.Result> Account(Guid id)
        {
            return await _queryDispatcher.Execute<GetAccountByIdQuery, GetAccountByIdQuery.Result>(new GetAccountByIdQuery
            {
                KeyId = id
            });
        }

        [HttpGet]
        public async Task<GetAccountQuery.Result> Account([FromUri]GetAccountQuery query)
        {
            return await _queryDispatcher.Execute<GetAccountQuery, GetAccountQuery.Result>(query);
        }

        [HttpPost]
        public async Task<GetAccountQuery.Result> Post([FromBody]SaveAccountCommand command)
        {
            await _commandDispatcher.Execute<SaveAccountCommand, SaveAccountCommand.Result>(command);
            var accounts = await _queryDispatcher.Execute<GetAccountQuery, GetAccountQuery.Result>(new GetAccountQuery
            {
                UserId = command.UserId,
                CultureId = command.CultureId
            });
            return accounts;
        }

        public async Task<RemoveAccountCommand.Result> Delete([FromUri]RemoveAccountCommand command)
        {
            return await _commandDispatcher.Execute<RemoveAccountCommand, RemoveAccountCommand.Result>(command);
        }
    }
}
