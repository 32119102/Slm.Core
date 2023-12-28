using System;
using System.Collections.Generic;
using System.Text;

namespace Slm.Data.Abstractions.Service;

public interface IService
{



}




/// <summary>
/// 接口基类
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TEntityInput"></typeparam>
/// <typeparam name="TEntityOutput"></typeparam>
/// <typeparam name="TEntitySearch"></typeparam>
/// <typeparam name="TTableDto"></typeparam>
public interface IBaseService<TEntity, TEntityInput, TEntityOutput, TEntitySearch, TTableDto> :
    IService,
    IRepositoryAdd<TEntity>,
    IRepositoryUpdate<TEntity>,
    IRepositoryDel<TEntity>,
    IRepositoryFind<TEntity>
{ }


