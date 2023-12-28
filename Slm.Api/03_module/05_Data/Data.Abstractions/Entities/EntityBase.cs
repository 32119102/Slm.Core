using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Slm.Data.Abstractions.Attributes;
using SqlSugar;
using System.Runtime.InteropServices;

namespace Slm.Data.Abstractions.Entities;



/// <summary>
/// 包含指定类型主键的实体
/// </summary>
public abstract class Entity<TKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    [SugarColumn(IsPrimaryKey = true)]
    public virtual TKey Id { get; set; }
}

/// <summary>
/// 含有自增主键的实体
/// </summary>
public class Entity : Entity<long>
{

}

/// <summary>
/// 框架实体基类
/// </summary>
/// <typeparam name="TKey"></typeparam>
public abstract class EntityBase<TKey> : Entity<TKey>, IDeletedFilter
{
    /// <summary>
    /// 创建人编号
    /// </summary>
    [SugarColumn(ColumnDescription = "创建人编号", IsOnlyIgnoreUpdate = true)]
    [TableField(HandleType = HandleTypeEnum.Add, TableType = TableTypeEnum.UserId)]
    public virtual long CreatId { get; set; }

    /// <summary>
    /// 创建人名称
    /// </summary>
    [SugarColumn(ColumnDescription = "创建人名称", IsOnlyIgnoreUpdate = true)]
    [TableField(HandleType = HandleTypeEnum.Add, TableType = TableTypeEnum.UserName)]
    public virtual string Creator { get; set; } = String.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn(ColumnDescription = "创建时间", IsOnlyIgnoreUpdate = true)]
    public virtual DateTime? Created { get; set; } = DateTime.Now;

    /// <summary>
    /// 修改人编号
    /// </summary>
    [SugarColumn(ColumnDescription = "修改人编号", IsOnlyIgnoreInsert = true)]
    [TableField(HandleType = HandleTypeEnum.Edit, TableType = TableTypeEnum.UserId)]
    public virtual long? LastmodifiId { get; set; }

    /// <summary>
    /// 修改人名称
    /// </summary>
    [TableField(HandleType = HandleTypeEnum.Edit, TableType = TableTypeEnum.UserName)]
    [SugarColumn(ColumnDescription = "修改人名称", IsOnlyIgnoreInsert = true)]
    public virtual string? Lastmodifier { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    [TableField(HandleType = HandleTypeEnum.Edit, TableType = TableTypeEnum.Dt)]
    [SugarColumn(ColumnDescription = "修改时间", IsOnlyIgnoreInsert = true)]
    public virtual DateTime? Lastmodified { get; set; }


    /// <summary>
    /// 已删除的
    /// </summary>
    public virtual bool IsDelete { get; set; } = false;

}




/// <summary>
/// 业务数据实体基类(数据权限)
/// </summary>
public abstract class EntityBaseData<TKey> : EntityBase<TKey>, IOrgIdFilter
{
    /// <summary>
    /// 创建者部门Id
    /// </summary>
    [SugarColumn(ColumnDescription = "创建者部门Id", IsOnlyIgnoreUpdate = true)]
    [TableField(HandleType = HandleTypeEnum.Add, TableType = TableTypeEnum.CreatOrgId)]
    public virtual long? CreatOrgId { get; set; }
}


/// <summary>
/// 租户基类实体
/// </summary>
public abstract class EntityTenant<TKey> : EntityBase<TKey>, ITenantIdFilter
{
    /// <summary>
    /// 租户Id
    /// </summary>
    [SugarColumn(ColumnDescription = "租户Id", IsOnlyIgnoreUpdate = true)]
    [TableField(HandleType = HandleTypeEnum.Add, TableType = TableTypeEnum.TenantId)]
    public virtual long? TenantId { get; set; }
}













