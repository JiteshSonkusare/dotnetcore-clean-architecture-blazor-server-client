using System;
using System.Linq.Expressions;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Language;
using HotChocolate.Types;

namespace WebAPI.GraphQL.CustomFilters
{
    public class QueryableStringInvariantEqualsHandler : QueryableStringOperationHandler
    {
        public QueryableStringInvariantEqualsHandler(InputParser inputParser) : base(inputParser) { }

        protected override int Operation => DefaultFilterOperations.Contains;
        public override Expression HandleOperation(QueryableFilterContext context, IFilterOperationField field, IValueNode value, object parsedValue)
        {
            Expression property = context.GetInstance();
            if (parsedValue is string str)
            {
                var toLower = Expression.Call(property, typeof(string).GetMethod("ToLower", Type.EmptyTypes));
                return Expression.Call(toLower, typeof(string).GetMethod(nameof(string.Contains), new Type[] { typeof(string) }), Expression.Constant(str.ToLower()));
            }
            throw new InvalidOperationException();
        }
    }
}
