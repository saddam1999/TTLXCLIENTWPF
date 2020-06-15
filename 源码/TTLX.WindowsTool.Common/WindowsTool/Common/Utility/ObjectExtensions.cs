using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Common.Utility
{
	// Token: 0x02000013 RID: 19
	public static class ObjectExtensions
	{
		// Token: 0x0600008D RID: 141 RVA: 0x000039A0 File Offset: 0x00001BA0
		public static string ToJsonString(this object obj)
		{
			JsonSerializerSettings settings = new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore
			};
			return JsonConvert.SerializeObject(obj, Formatting.None, settings);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000039C2 File Offset: 0x00001BC2
		public static bool IsPrimitive(this Type type)
		{
			return type == typeof(string) || (type.IsValueType & type.IsPrimitive);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000039E5 File Offset: 0x00001BE5
		public static object Copy(this object originalObject)
		{
			return ObjectExtensions.InternalCopy(originalObject, new Dictionary<object, object>(new ReferenceEqualityComparer()));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000039F8 File Offset: 0x00001BF8
		private static object InternalCopy(object originalObject, IDictionary<object, object> visited)
		{
			if (originalObject == null)
			{
				return null;
			}
			Type typeToReflect = originalObject.GetType();
			if (typeToReflect.IsPrimitive())
			{
				return originalObject;
			}
			if (visited.ContainsKey(originalObject))
			{
				return visited[originalObject];
			}
			if (typeof(Delegate).IsAssignableFrom(typeToReflect))
			{
				return null;
			}
			object cloneObject = ObjectExtensions.CloneMethod.Invoke(originalObject, null);
			if (typeToReflect.IsArray && !typeToReflect.GetElementType().IsPrimitive())
			{
				Array clonedArray = (Array)cloneObject;
				clonedArray.ForEach(delegate(Array array, int[] indices)
				{
					array.SetValue(ObjectExtensions.InternalCopy(clonedArray.GetValue(indices), visited), indices);
				});
			}
			visited.Add(originalObject, cloneObject);
			ObjectExtensions.CopyFields(originalObject, visited, cloneObject, typeToReflect, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy, null);
			ObjectExtensions.RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect);
			return cloneObject;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003AD8 File Offset: 0x00001CD8
		private static void RecursiveCopyBaseTypePrivateFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect)
		{
			if (typeToReflect.BaseType != null)
			{
				ObjectExtensions.RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
				ObjectExtensions.CopyFields(originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, (FieldInfo info) => info.IsPrivate);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003B30 File Offset: 0x00001D30
		private static void CopyFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> filter = null)
		{
			foreach (FieldInfo fieldInfo in typeToReflect.GetFields(bindingFlags))
			{
				if ((filter == null || filter(fieldInfo)) && !fieldInfo.FieldType.IsPrimitive())
				{
					object clonedFieldValue = ObjectExtensions.InternalCopy(fieldInfo.GetValue(originalObject), visited);
					fieldInfo.SetValue(cloneObject, clonedFieldValue);
				}
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003B89 File Offset: 0x00001D89
		public static T Copy<T>(this T original)
		{
			return (T)((object)original.Copy());
		}

		// Token: 0x0400001E RID: 30
		private static readonly MethodInfo CloneMethod = typeof(object).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);
	}
}
