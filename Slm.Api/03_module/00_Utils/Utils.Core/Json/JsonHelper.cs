using Slm.Utils.Core.DependencyInjection;
using Slm.Utils.Core.Json;
using Slm.Utils.Core.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace Slm.Utils.Core;

public class JsonHelper : ISingletonDependency
{
    //todo:升级 new();
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions();

    public JsonHelper()
    {
        //不区分大小写的反序列化
        _options.PropertyNameCaseInsensitive = true;
        //属性名称使用 camel 大小写
        _options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        //最大限度减少字符转义
        _options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        //添加日期转换器
        _options.Converters.Add(new DateTimeConverter());

        //添加多态嵌套序列化
        _options.AddPolymorphism();
    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public string Serialize<T>(T obj)
    {
        return JsonSerializer.Serialize(obj, _options);
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json"></param>
    /// <returns></returns>
    public T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, _options);
    }
}
