using Slm.Data.Abstractions;
using Slm.Utils.Core;
using Slm.Utils.Core.Extensions;
using SqlSugar;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;

namespace Slm.Data.Core.Extensions;

public static class EntityExtensions
{

    //public static string GetCorrsDbName(this Type type)
    //{
    //    var attr = type.GetCustomAttribute(typeof(TenantAttribute));
    //    if (attr == null) return string.Empty;
    //    string configId = ((TenantAttribute)attr).configId!.ToString();
    //    var dbs = App.GetService<List<DbOption>>();
    //    string libraryName = dbs.Where(a => a.ConfigId == configId).FirstOrDefault()?.LibraryName ?? "";
    //    if (libraryName.IsNull()) throw new Exception("多库读取配置不存在,请检查配置文件");
    //    attr = type.GetCustomAttribute(typeof(SugarTable));
    //    if (attr == null) return string.Empty;

    //    var sugarAttr = (SugarTable)attr;
    //    string tableName = sugarAttr?.TableName!;

    //    return $"{libraryName}.{tableName}";
    //}




    /// <summary>
    /// 获取实体特性中的表名
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string GetTableName(this Type type)
    {
        var attr = type.GetCustomAttribute(typeof(SugarTable));
        if (attr == null) return string.Empty;

        var sugarAttr = (SugarTable)attr;
        return sugarAttr?.TableName!;
    }



    /// <summary>
    /// 获取数据库字段名
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string GetColumnName(this Type type, string name)
    {
        var property = type.GetProperty(name);
        var sugarAttribute = (SugarColumn)property.GetCustomAttributes(true)
            .First(it => it is SugarColumn);
        return sugarAttribute?.ColumnName!;
    }

    /// <summary>
    /// 判断是否含有属性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool IsHasProperty<T>(this T t, string name)
    {
        Type type = t.GetType();
        return type.IsHasProperty(name);
    }

    public static bool IsHasProperty(this Type type, string name)
    {
        PropertyInfo p = type.GetProperty(name);
        if (p == null)
            return false;
        return true;
    }

    /// <summary>
    /// 根据属性名获取属性值
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <typeparam name="TValue">属性类型</typeparam>
    /// <param name="t">对象</param>
    /// <param name="name">属性名</param>
    /// <returns></returns>
    public static TValue GetPropertyValue<T, TValue>(this T t, string name)
    {
        Type type = t.GetType();
        PropertyInfo p = type.GetProperty(name);
        if (p == null) return default(TValue);

        var param_obj = Expression.Parameter(typeof(T));
        var param_val = Expression.Parameter(typeof(TValue));

        //转成真实类型，防止Dynamic类型转换成object
        var body_obj = Expression.Convert(param_obj, type);

        var body = Expression.Property(body_obj, p);
        var getValue = Expression.Lambda<Func<T, TValue>>(body, param_obj).Compile();
        return getValue(t);
    }

    /// <summary>
    /// 根据属性名称设置属性的值
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <typeparam name="TValue">属性类型</typeparam>
    /// <param name="t">对象</param>
    /// <param name="name">属性名</param>
    /// <param name="value">属性值</param>
    /// <returns></returns>
    public static bool SetPropertyValue<T, TValue>(this T t, string name, TValue value)
    {
        Type type = t.GetType();
        PropertyInfo p = type.GetProperty(name);
        if (p == null) return false;

        var param_obj = Expression.Parameter(type);
        var param_val = Expression.Parameter(typeof(TValue));
        var body_obj = Expression.Convert(param_obj, type);
        var body_val = Expression.Convert(param_val, p.PropertyType);

        //获取设置属性的值的方法
        var setMethod = p.GetSetMethod(true);

        //如果只是只读,则setMethod==null
        if (setMethod != null)
        {
            var body = Expression.Call(param_obj, p.GetSetMethod(), body_val);
            var setValue = Expression.Lambda<Action<T, TValue>>(body, param_obj, param_val).Compile();
            setValue(t, value);

            return true;
        }
        return false;
    }
}