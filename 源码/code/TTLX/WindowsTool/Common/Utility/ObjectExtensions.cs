namespace TTLX.WindowsTool.Common.Utility
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class ObjectExtensions
    {
        private static readonly MethodInfo CloneMethod = typeof(object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

        public static object Copy(this object originalObject) => 
            InternalCopy(originalObject, new Dictionary<object, object>(new ReferenceEqualityComparer()));

        public static T Copy<T>(this T original) => 
            ((T) original.Copy());

        private static void CopyFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect, BindingFlags bindingFlags = 0x74, Func<FieldInfo, bool> filter = null)
        {
            foreach (FieldInfo info in typeToReflect.GetFields(bindingFlags))
            {
                if (((filter == null) || filter(info)) && !info.FieldType.IsPrimitive())
                {
                    object obj2 = InternalCopy(info.GetValue(originalObject), visited);
                    info.SetValue(cloneObject, obj2);
                }
            }
        }

        private static object InternalCopy(object originalObject, IDictionary<object, object> visited)
        {
            if (originalObject == null)
            {
                return null;
            }
            Type type = originalObject.GetType();
            if (type.IsPrimitive())
            {
                return originalObject;
            }
            if (visited.ContainsKey(originalObject))
            {
                return visited[originalObject];
            }
            if (typeof(Delegate).IsAssignableFrom(type))
            {
                return null;
            }
            object obj2 = CloneMethod.Invoke(originalObject, null);
            if (type.IsArray && !type.GetElementType().IsPrimitive())
            {
                Array clonedArray = (Array) obj2;
                clonedArray.ForEach(delegate (Array array, int[] indices) {
                    array.SetValue(InternalCopy(clonedArray.GetValue(indices), visited), indices);
                });
            }
            visited.Add(originalObject, obj2);
            CopyFields(originalObject, visited, obj2, type, BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null);
            RecursiveCopyBaseTypePrivateFields(originalObject, visited, obj2, type);
            return obj2;
        }

        public static bool IsPrimitive(this Type type) => 
            ((type == typeof(string)) || (type.IsValueType & type.IsPrimitive));

        private static void RecursiveCopyBaseTypePrivateFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect)
        {
            if (typeToReflect.BaseType != null)
            {
                RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
                CopyFields(originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.NonPublic | BindingFlags.Instance, info => info.IsPrivate);
            }
        }

        public static string ToJsonString(this object obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings {
                NullValueHandling = NullValueHandling.Ignore
            };
            return JsonConvert.SerializeObject(obj, Formatting.None, settings);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ObjectExtensions.<>c <>9 = new ObjectExtensions.<>c();
            public static Func<FieldInfo, bool> <>9__5_0;

            internal bool <RecursiveCopyBaseTypePrivateFields>b__5_0(FieldInfo info) => 
                info.IsPrivate;
        }
    }
}

