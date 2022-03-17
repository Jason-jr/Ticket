using System;
using System.ComponentModel;
using System.Reflection;
using Application.Models;

namespace Application.Common;

public static class EnumToKeyValue
{
    /// <summary>
    /// Parse enum to KeyValueVM with specific type id and description
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static KeyValueVM<T> Parse<T>(this Enum source)
    {
        return new()
        {
            Id = (T)Convert.ChangeType(source, typeof(T)),
            Value = source.GetType().GetField(source.ToString()).GetCustomAttribute<DescriptionAttribute>()?.Description
        };
    }
}