using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Slm.Utils.Core.Extensions;


namespace Slm.Swashbuckle.Filter;

/// <summary>
/// 枚举架构过滤器
/// </summary>
public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var type = context.Type;
        if (type.IsEnum)
        {
            var enumValueType = type.GetField("value__").FieldType;
            var items = Enum.GetValues(type).Cast<Enum>()
            .Where(m => !m.ToString().Equals("Null")).Select(x =>
            $"{x.ToDescription()}={Convert.ChangeType(x, enumValueType)}").ToList();

            if (items?.Count > 0)
            {
                string description = string.Join(",", items);
                schema.Description = string.IsNullOrEmpty(schema.Description) ? description : $"{schema.Description}:{description}";
            }
        }
    }
}