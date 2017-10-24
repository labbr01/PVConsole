﻿using System;
using System.Linq.Expressions;

namespace LinqToDB.Linq
{
	class ExpressionQueryImpl<T> : ExpressionQuery<T>, IExpressionQuery
	{
		public ExpressionQueryImpl(IDataContextInfo dataContext, Expression expression)
		{
			Init(dataContext, expression);
		}

		public override string ToString()
		{
			return SqlText;
		}
	}
}
