using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq;

public static class DynamicLinqExpressions
{
    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                     Expression<Func<T, bool>> expr2)
    {
        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
        return Expression.Lambda<Func<T, bool>>
              (Expression.Or(expr1.Body, invokedExpr), expr1.Parameters);
    }

    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                         Expression<Func<T, bool>> expr2, bool contain)
    {
        if (contain)
        {
            var body = Expression.AndAlso(expr1.Body, expr2.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(body, expr1.Parameters[0]);
            return lambda;
        }
        else
        {
            return expr1;
        }
        //var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
        //var result = Expression.Lambda<Func<T, bool>>(Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
        //return result;
    }

    public static Expression<Func<T, T2, bool>> And<T, T2>(this Expression<Func<T, T2, bool>> expr1,
                                                     Expression<Func<T, T2, bool>> expr2, bool contain)
    {
        if (contain)
        {
            var body = Expression.AndAlso(expr1.Body, expr2.Body);
            var lambda = Expression.Lambda<Func<T, T2, bool>>(body, expr1.Parameters);
            return lambda;
        }
        else
        {
            return expr1;
        }
    }

    public static Expression<Func<T, T2, T3, bool>> And<T, T2, T3>(this Expression<Func<T, T2, T3, bool>> expr1,
                                                      Expression<Func<T, T2, T3, bool>> expr2, bool contain)
    {
        if (contain)
        {
            var body = Expression.AndAlso(expr1.Body, expr2.Body);
            var lambda = Expression.Lambda<Func<T, T2, T3, bool>>(body, expr1.Parameters);
            return lambda;
        }
        else
        {
            return expr1;
        }
    }

    public static Expression<Func<T, T2, T3, T4, bool>> And<T, T2, T3, T4>(this Expression<Func<T, T2, T3, T4, bool>> expr1,
                                                       Expression<Func<T, T2, T3, T4, bool>> expr2, bool contain)
    {
        if (contain)
        {
            var body = Expression.AndAlso(expr1.Body, expr2.Body);
            var lambda = Expression.Lambda<Func<T, T2, T3, T4, bool>>(body, expr1.Parameters);
            return lambda;
        }
        else
        {
            return expr1;
        }
    }

    public static Expression<Func<T, T2, T3, T4, T5, bool>> And<T, T2, T3, T4, T5>(this Expression<Func<T, T2, T3, T4, T5, bool>> expr1,
                                                    Expression<Func<T, T2, T3, T4, T5, bool>> expr2, bool contain)
    {
        if (contain)
        {
            var body = Expression.AndAlso(expr1.Body, expr2.Body);
            var lambda = Expression.Lambda<Func<T, T2, T3, T4, T5, bool>>(body, expr1.Parameters);
            return lambda;
        }
        else
        {
            return expr1;
        }
    }


    public static Expression<Func<T, T2, T3, T4, T5, T6, bool>> And<T, T2, T3, T4, T5, T6>(this Expression<Func<T, T2, T3, T4, T5, T6, bool>> expr1,
                                                    Expression<Func<T, T2, T3, T4, T5, T6, bool>> expr2, bool contain)
    {
        if (contain)
        {
            var body = Expression.AndAlso(expr1.Body, expr2.Body);
            var lambda = Expression.Lambda<Func<T, T2, T3, T4, T5, T6, bool>>(body, expr1.Parameters);
            return lambda;
        }
        else
        {
            return expr1;
        }
    }


    public static Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> And<T, T2, T3, T4, T5, T6, T7>(this Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> expr1,
                                                 Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> expr2, bool contain)
    {
        if (contain)
        {
            var body = Expression.AndAlso(expr1.Body, expr2.Body);
            var lambda = Expression.Lambda<Func<T, T2, T3, T4, T5, T6, T7, bool>>(body, expr1.Parameters);
            return lambda;
        }
        else
        {
            return expr1;
        }
    }

    public static Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, bool>> And<T, T2, T3, T4, T5, T6, T7, T8>(this Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, bool>> expr1,
                                              Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, bool>> expr2, bool contain)
    {
        if (contain)
        {
            var body = Expression.AndAlso(expr1.Body, expr2.Body);
            var lambda = Expression.Lambda<Func<T, T2, T3, T4, T5, T6, T7, T8, bool>>(body, expr1.Parameters);
            return lambda;
        }
        else
        {
            return expr1;
        }
    }

    public static Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> And<T, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> expr1,
                                                 Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> expr2, bool contain)
    {
        if (contain)
        {
            var body = Expression.AndAlso(expr1.Body, expr2.Body);
            var lambda = Expression.Lambda<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>>(body, expr1.Parameters);
            return lambda;
        }
        else
        {
            return expr1;
        }
    }
}
