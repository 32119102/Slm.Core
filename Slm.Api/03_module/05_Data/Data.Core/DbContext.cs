using SqlSugar;
using System.Linq.Expressions;

namespace Slm.Data.Core;

public class DbContext<T> : SimpleClient<T> where T : class, new()
{
    public ISugarQueryable<T> Find()
    {

        return Context.Queryable<T>();
    }

    public ISugarQueryable<T> Find(Expression<Func<T, bool>> expression)
    {
        return Context.Queryable<T>().Where(expression);
    }


}
