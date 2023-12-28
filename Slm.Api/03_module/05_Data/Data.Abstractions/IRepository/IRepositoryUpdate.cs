using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Data.Abstractions;

public interface IRepositoryUpdate<TEntity>
{
    Task<bool> UpdateAsync(TEntity updateObj);



    Task<bool> UpdateRangeAsync(List<TEntity> updateObjs);



    Task<bool> UpdateAsync(Expression<Func<TEntity, TEntity>> columns, Expression<Func<TEntity, bool>> whereExpression);

    Task<bool> UpdateSetColumnsTrueAsync(Expression<Func<TEntity, TEntity>> columns, Expression<Func<TEntity, bool>> whereExpression);




}
