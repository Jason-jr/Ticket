using System;
using System.Linq;
using System.Reflection;
using Application.Interfaces;
using AutoMapper;

namespace Application.Common;

public class Mapping : Profile
{
    public Mapping()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(i => i == typeof(IMap)
            || (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMap<>))))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type.IsGenericType
                ? type.MakeGenericType(type.GetGenericArguments()[0].BaseType)
                : type);

            var methodInfo = type.GetMethod("Mapping") ?? type.GetInterface("IMap`1").GetMethod("Mapping");

            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
}