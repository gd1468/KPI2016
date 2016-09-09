﻿using System;
using System.Management.Instrumentation;
using System.Threading.Tasks;
using System.Web.Mvc;
using MoneyManagement.ServiceLayer.Interfaces;

namespace MoneyManagement.ServiceLayer.CQRS
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IDependencyResolver _resolver;

        public QueryDispatcher(IDependencyResolver resolver)
        {
            _resolver = resolver;
        }

        public Task<TResult> Execute<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            var handler = _resolver.GetService<IQueryHandler<TQuery, TResult>>();

            if (handler == null)
            {
                throw new InstanceNotFoundException("Query Handler Not Found");
            }

            return handler.Execute(query);
        }
    }
}
