﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Threax.ModelGen
{
    public static class TypeExtensions
    {
        public static String GetTypeAsNullable(this Type t)
        {
            if (t.IsValueType)
            {
                return $"{t.Name}?";
            }
            else
            {
                return t.Name;
            }
        }

        private static HashSet<Type> NumericTypes = new HashSet<Type>()
        {
            typeof(byte), typeof(sbyte), typeof(ushort), typeof(uint), typeof(ulong), typeof(short), typeof(int), typeof(long), typeof(decimal), typeof(double), typeof(float)
        };

        public static bool IsNumeric(this Type t)
        {
            return NumericTypes.Contains(t);
        }

        public static String GetSchemaFormat(this Type t)
        {
            if (t.IsGenericType) //See if the type is a Nullable<T>, this will handle value types
            {
                var genericDef = t.GetGenericTypeDefinition();
                if (genericDef == typeof(Nullable<>))
                {
                    return t.GetGenericArguments()[0].Name + "?";
                }
                else
                {
                    var genericName = new StringBuilder(genericDef.Name.Substring(0, genericDef.Name.IndexOf('`')));
                    genericName.Append("<");
                    String format = "{0}";
                    foreach(var arg in t.GetGenericArguments())
                    {
                        genericName.AppendFormat(format, arg.GetSchemaFormat());
                        format = ", {0}";
                    }
                    genericName.Append(">");
                    return genericName.ToString();
                }
            }
            return t.Name;
        }
    }
}
