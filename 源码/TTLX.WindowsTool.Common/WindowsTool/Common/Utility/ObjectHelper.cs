using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using KellermanSoftware.CompareNetObjects;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Common.Utility
{
	// Token: 0x02000012 RID: 18
	public class ObjectHelper
	{
		// Token: 0x0600007E RID: 126 RVA: 0x000032E4 File Offset: 0x000014E4
		public static async Task<T> DeepCopyAsync<T>(T obj)
		{
			T r = default(T);
			await TaskEx.Run(delegate()
			{
				r = obj.Copy<T>();
			});
			return r;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000332C File Offset: 0x0000152C
		public static T DeepCopy<T>(T obj)
		{
			if (obj is string || obj.GetType().IsValueType)
			{
				return obj;
			}
			object retval = Activator.CreateInstance(obj.GetType());
			foreach (FieldInfo field in obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				try
				{
					field.SetValue(retval, ObjectHelper.DeepCopy<object>(field.GetValue(obj)));
				}
				catch
				{
				}
			}
			return (T)((object)retval);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000033CC File Offset: 0x000015CC
		public static bool AreEqual(object o1, object o2)
		{
			string text = JsonConvert.SerializeObject(o1);
			string s2 = JsonConvert.SerializeObject(o2);
			return text.Equals(s2);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000033EC File Offset: 0x000015EC
		public static bool AreEqual(string a, string b)
		{
			if (string.IsNullOrEmpty(a))
			{
				return string.IsNullOrEmpty(b);
			}
			return string.Equals(a, b);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003404 File Offset: 0x00001604
		public static bool AreEquals(object o1, object o2)
		{
			if (o1 == null || o2 == null)
			{
				return false;
			}
			CompareLogic compareLogic = new CompareLogic();
			compareLogic.Config.SkipInvalidIndexers = true;
			compareLogic.Config.IgnoreUnknownObjectTypes = true;
			compareLogic.Compare(o1, o2);
			return compareLogic.Compare(o1, o2).AreEqual;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003440 File Offset: 0x00001640
		public static bool Compare(object obj1, object obj2)
		{
			if (obj1 == null || obj2 == null)
			{
				return false;
			}
			if (!obj1.GetType().Equals(obj2.GetType()))
			{
				return false;
			}
			Type type = obj1.GetType();
			if (type.IsPrimitive || typeof(string).Equals(type))
			{
				return obj1.Equals(obj2);
			}
			if (type.IsArray)
			{
				Array array = obj1 as Array;
				Array second = obj2 as Array;
				IEnumerator en = array.GetEnumerator();
				int i = 0;
				while (en.MoveNext())
				{
					if (!ObjectHelper.Compare(en.Current, second.GetValue(i)))
					{
						return false;
					}
					i++;
				}
			}
			else
			{
				foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					object val = propertyInfo.GetValue(obj1, null);
					object tval = propertyInfo.GetValue(obj2, null);
					if (!ObjectHelper.Compare(val, tval))
					{
						return false;
					}
				}
				foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					object val2 = fieldInfo.GetValue(obj1);
					object tval2 = fieldInfo.GetValue(obj2);
					if (!ObjectHelper.Compare(val2, tval2))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000355C File Offset: 0x0000175C
		public static bool IsALike(object original, object comparedToObject)
		{
			if (original.GetType() != comparedToObject.GetType())
			{
				return false;
			}
			MethodInfo[] originalMethods = original.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (comparedToObject.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Length != originalMethods.Length)
			{
				return false;
			}
			PropertyInfo[] originalProperties = original.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (comparedToObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Length != originalProperties.Length)
			{
				return false;
			}
			FieldInfo[] originalFields = original.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (comparedToObject.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Length != originalFields.Length)
			{
				return false;
			}
			foreach (FieldInfo fi in originalFields)
			{
				FieldInfo fiComparedObj = comparedToObject.GetType().GetField(fi.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (fiComparedObj == null)
				{
					return false;
				}
				object srcValue = original.GetType().InvokeMember(fi.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField, null, original, null);
				object comparedObjFieldValue = comparedToObject.GetType().InvokeMember(fiComparedObj.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField, null, comparedToObject, null);
				if (srcValue == null)
				{
					return comparedObjFieldValue == null;
				}
				if (srcValue.GetType() != comparedObjFieldValue.GetType())
				{
					return false;
				}
				if (!srcValue.ToString().Equals(comparedObjFieldValue.ToString()))
				{
					return false;
				}
			}
			foreach (PropertyInfo pi in originalProperties)
			{
				PropertyInfo piComparedObj = comparedToObject.GetType().GetProperty(pi.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (piComparedObj == null)
				{
					return false;
				}
				object srcValue2 = original.GetType().InvokeMember(pi.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty, null, original, null);
				object comparedObjValue = comparedToObject.GetType().InvokeMember(piComparedObj.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty, null, comparedToObject, null);
				if (srcValue2.GetType() != comparedObjValue.GetType())
				{
					return false;
				}
				if (!srcValue2.ToString().Equals(comparedObjValue.ToString()))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000374C File Offset: 0x0000194C
		public static bool CompareObj(object inputObjectA, object inputObjectB, string[] ignorePropertiesList)
		{
			bool areObjectsEqual = true;
			if (inputObjectA != null && inputObjectB != null)
			{
				foreach (PropertyInfo propertyInfo in inputObjectA.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
				{
					if (propertyInfo.CanRead && !ignorePropertiesList.Contains(propertyInfo.Name))
					{
						object value = propertyInfo.GetValue(inputObjectA, null);
						object value2 = propertyInfo.GetValue(inputObjectB, null);
						if (ObjectHelper.IsAssignableFrom(propertyInfo.PropertyType) || ObjectHelper.IsPrimitiveType(propertyInfo.PropertyType) || ObjectHelper.IsValueType(propertyInfo.PropertyType))
						{
							if (!ObjectHelper.CompareValues(value, value2))
							{
								Console.WriteLine("Property Name {0}", propertyInfo.Name);
								areObjectsEqual = false;
							}
						}
						else if (ObjectHelper.IsEnumerableType(propertyInfo.PropertyType))
						{
							Console.WriteLine("Property Name {0}", propertyInfo.Name);
							ObjectHelper.CompareEnumerations(value, value2, ignorePropertiesList);
						}
						else if (propertyInfo.PropertyType.IsClass)
						{
							if (!ObjectHelper.CompareObj(propertyInfo.GetValue(inputObjectA, null), propertyInfo.GetValue(inputObjectB, null), ignorePropertiesList))
							{
								areObjectsEqual = false;
							}
						}
						else
						{
							areObjectsEqual = false;
						}
					}
				}
			}
			else
			{
				areObjectsEqual = false;
			}
			return areObjectsEqual;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000386A File Offset: 0x00001A6A
		private static bool IsAssignableFrom(Type type)
		{
			return typeof(IComparable).IsAssignableFrom(type);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000387C File Offset: 0x00001A7C
		private static bool IsPrimitiveType(Type type)
		{
			return type.IsPrimitive;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003884 File Offset: 0x00001A84
		private static bool IsValueType(Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000388C File Offset: 0x00001A8C
		private static bool IsEnumerableType(Type type)
		{
			return typeof(IEnumerable).IsAssignableFrom(type);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000038A0 File Offset: 0x00001AA0
		private static bool CompareValues(object value1, object value2)
		{
			bool areValuesEqual = true;
			IComparable selfValueComparer = value1 as IComparable;
			if ((value1 == null && value2 != null) || (value1 != null && value2 == null))
			{
				areValuesEqual = false;
			}
			else if (selfValueComparer != null && selfValueComparer.CompareTo(value2) != 0)
			{
				areValuesEqual = false;
			}
			else if (!object.Equals(value1, value2))
			{
				areValuesEqual = false;
			}
			return areValuesEqual;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000038E4 File Offset: 0x00001AE4
		private static bool CompareEnumerations(object value1, object value2, string[] ignorePropertiesList)
		{
			if ((value1 == null && value2 != null) || (value1 != null && value2 == null))
			{
				return false;
			}
			if (value1 != null && value2 != null)
			{
				IEnumerable<object> enumValue = ((IEnumerable)value1).Cast<object>();
				IEnumerable<object> enumValue2 = ((IEnumerable)value2).Cast<object>();
				if (enumValue.Count<object>() != enumValue2.Count<object>())
				{
					return false;
				}
				for (int itemIndex = 0; itemIndex < enumValue.Count<object>(); itemIndex++)
				{
					object enumValue1Item = enumValue.ElementAt(itemIndex);
					object enumValue2Item = enumValue2.ElementAt(itemIndex);
					Type enumValue1ItemType = enumValue1Item.GetType();
					if (ObjectHelper.IsAssignableFrom(enumValue1ItemType) || ObjectHelper.IsPrimitiveType(enumValue1ItemType) || ObjectHelper.IsValueType(enumValue1ItemType))
					{
						if (!ObjectHelper.CompareValues(enumValue1Item, enumValue2Item))
						{
							return false;
						}
					}
					else if (!ObjectHelper.CompareObj(enumValue1Item, enumValue2Item, ignorePropertiesList))
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}
