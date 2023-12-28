using Microsoft.AspNetCore.Builder;
using Slm.Modularity.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder Initialize(this IApplicationBuilder builder)
    {
        var runner =builder.ApplicationServices.GetRequiredService<IAppApplication>();
        runner.Initialize();
        return builder;
    }
}
