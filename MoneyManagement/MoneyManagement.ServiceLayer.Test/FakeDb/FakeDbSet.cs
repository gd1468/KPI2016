using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Moq;
using System.Linq.Expressions;

namespace MoneyManagement.ServiceLayer.Test.FakeDb
{
    public class FakeDbSet<T> : DbSet<T>, IDbAsyncEnumerable<T>, IQueryable<T> where T : class
    {
        protected ObservableCollection<T> Data { get; private set; }

        private readonly IQueryable _queryable;
        private readonly Action<T> itemProcessor;

        public FakeDbSet(params T[] data)
            : this(new List<T>(data))
        { }

        public FakeDbSet(IEnumerable<T> data = null) : this(data, null)
        {

        }
        public FakeDbSet(IEnumerable<T> data, Action<T> processItem)
        {
            Data = data != null ? new ObservableCollection<T>(data) : new ObservableCollection<T>();
            _queryable = Data.AsQueryable();

            Action<T> defaultAction = i => { };

            itemProcessor = processItem ?? defaultAction;
        }

        public override T Add(T item)
        {
            itemProcessor(item);
            Data.Add(item);
            return item;
        }

        public override T Remove(T item)
        {
            Data.Remove(item);
            return item;
        }

        IQueryProvider IQueryable.Provider => new AsyncQueryProviderWrapper<T>(_queryable.Provider);

        Expression IQueryable.Expression => _queryable.Expression;

        public int Count => Data.Count;
    }

    public class AsyncEnumeratorWrapper<T> : IDbAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public AsyncEnumeratorWrapper(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_inner.MoveNext());
        }

        public T Current
        {
            get { return _inner.Current; }
        }

        object IDbAsyncEnumerator.Current
        {
            get { return Current; }
        }
    }

    public class AsyncEnumerableQuery<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable
    {
        public AsyncEnumerableQuery(IEnumerable<T> enumerable)
            : base(enumerable)
        {
        }

        public AsyncEnumerableQuery(Expression expression)
            : base(expression)
        {
        }

        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return new AsyncEnumeratorWrapper<T>(this.AsEnumerable().GetEnumerator());
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return GetAsyncEnumerator();
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new AsyncQueryProviderWrapper<T>(this); }
        }
    }

    internal class AsyncQueryProviderWrapper<T> : IDbAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal AsyncQueryProviderWrapper(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new AsyncEnumerableQuery<T>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new AsyncEnumerableQuery<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute(expression));
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }
    }

    public static class DbContextExtensions
    {
        public static Mock<FakeDbSet<T>> MockDbSet<T>(this Mock<DbSet<T>> dbSet, List<T> sourceList) where T : class
        {
            return sourceList.ToMockDbSet();
        }

        public static Mock<FakeDbSet<T>> ToMockDbSet<T>(this IEnumerable<T> data) where T : class
        {
            var queryable = data.AsQueryable();
            var result = new Mock<FakeDbSet<T>>(queryable);

            result.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new AsyncQueryProviderWrapper<T>(queryable.Provider));
            result.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            result.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            result.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            result.Setup(m => m.AsNoTracking()).Returns(result.Object);
            result.Setup(m => m.Include(It.IsAny<String>())).Returns(result.Object);
            result.Setup(m => m.Local).Returns(new ObservableCollection<T>(data));

            return result;
        }
    }
}
