﻿using Slm.Modularity.Abstractions;

namespace Slm.Modularity.Core;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DependsOnAttribute : Attribute, IDependedTypesProvider
{
    public Type[] DependedTypes { get; }

    public DependsOnAttribute(params Type[] dependedTypes)
    {
        DependedTypes = dependedTypes ?? new Type[0];
    }

    public virtual Type[] GetDependedTypes()
    {
        return DependedTypes;
    }
}
