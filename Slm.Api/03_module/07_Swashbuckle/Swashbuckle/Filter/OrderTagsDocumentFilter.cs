using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Slm.DynamicApi.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Swashbuckle.Filter;

/// <summary>
/// 接口排序文档过滤器
/// </summary>
public class OrderTagsDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var orderTagList = new ConcurrentDictionary<string, int>();
        foreach (var apiDescription in context.ApiDescriptions)
        {
            var order = 0;
            var actionDescriptor = apiDescription.ActionDescriptor as ControllerActionDescriptor;
            var objOrderAttribute = actionDescriptor.EndpointMetadata.FirstOrDefault(x => x is OrderAttribute);
            if (objOrderAttribute != null)
            {
                var orderAttribute = objOrderAttribute as OrderAttribute;
                order = orderAttribute.Value;
            }
            orderTagList.TryAdd(actionDescriptor.ControllerName, order);
        }

        swaggerDoc.Tags = swaggerDoc.Tags
                                    .OrderBy(u => orderTagList.TryGetValue(u.Name, out int order) ? order : 0)
                                    .ThenBy(u => u.Name)
                                    .ToArray();
    }
}