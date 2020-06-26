using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Galchenko.TestTask.ApplicationLayer.Common.Extensions
{
    public static class TypeExtensions
    {
        public static string? GetHashed(  this IDictionary< PropertyInfo, string > cipherData,  string propertyName,  string? defaultValue )
        {
            PropertyInfo? key = cipherData.Keys.FirstOrDefault( pi => pi.Name.StartsWith( propertyName ));

            if ( key != null ) {
                return cipherData[key];
            }
            else {
                return defaultValue;
            }
        }

        public static string GetHashed( this IDictionary< PropertyInfo, string > cipherData, string propertyName )
        {
            PropertyInfo key = cipherData.Keys.First( pi => pi.Name.StartsWith( propertyName ));
            return cipherData[key];
        }


        public static bool IsImplementAsyncEnumerable( this object obj )
        {
            return 
                obj.GetType().GetInterfaces().Any(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(IAsyncEnumerable<>));
        }

        public static bool IsImplementInterface( this object obj, Type interfaceType )
        {
            return 
                obj.GetType().GetInterfaces().Any(x =>
                    (x.IsGenericType && x.GetGenericTypeDefinition() == interfaceType) 
                    || (!x.IsGenericType && x == interfaceType) );
        }

        public static bool IsImplementInterface( this Type type, Type interfaceType )
        {
            return 
                type.GetInterfaces().Any(x =>
                    (x.IsGenericType && x.GetGenericTypeDefinition() == interfaceType) 
                    || (!x.IsGenericType && x == interfaceType) );
        }

        public static bool IsSubclassOfRawGeneric( this object obj, Type generic ) 
        {
            Type toCheck = obj.GetType();

            while (toCheck != null && toCheck != typeof(object)) {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur) {
                    return true;
                }
                toCheck = toCheck.BaseType!;
            }
            return false;
        }
    }
}
