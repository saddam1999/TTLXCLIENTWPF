namespace TTLX.WindowsTool.Common.Utility
{
    using KellermanSoftware.CompareNetObjects;
    using Microsoft.Runtime.CompilerServices;
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class ObjectHelper
    {
        public static bool AreEqual(object o1, object o2) => 
            JsonConvert.SerializeObject(o1).Equals(JsonConvert.SerializeObject(o2));

        public static bool AreEqual(string a, string b)
        {
            if (string.IsNullOrEmpty(a))
            {
                return string.IsNullOrEmpty(b);
            }
            return string.Equals(a, b);
        }

        public static bool AreEquals(object o1, object o2)
        {
            if ((o1 == null) || (o2 == null))
            {
                return false;
            }
            CompareLogic logic1 = new CompareLogic {
                Config = { 
                    SkipInvalidIndexers = true,
                    IgnoreUnknownObjectTypes = true
                }
            };
            logic1.Compare(o1, o2);
            return logic1.Compare(o1, o2).AreEqual;
        }

        public static bool Compare(object obj1, object obj2)
        {
            if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            if (!obj1.GetType().Equals(obj2.GetType()))
            {
                return false;
            }
            Type o = obj1.GetType();
            if (o.IsPrimitive || typeof(string).Equals(o))
            {
                return obj1.Equals(obj2);
            }
            if (o.IsArray)
            {
                Array array = obj2 as Array;
                IEnumerator enumerator = (obj1 as Array).GetEnumerator();
                for (int i = 0; enumerator.MoveNext(); i++)
                {
                    if (!Compare(enumerator.Current, array.GetValue(i)))
                    {
                        return false;
                    }
                }
            }
            else
            {
                foreach (PropertyInfo info1 in o.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
                {
                    object obj3 = info1.GetValue(obj1, null);
                    object obj4 = info1.GetValue(obj2, null);
                    if (!Compare(obj3, obj4))
                    {
                        return false;
                    }
                }
                foreach (FieldInfo info2 in o.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
                {
                    object obj5 = info2.GetValue(obj1);
                    object obj6 = info2.GetValue(obj2);
                    if (!Compare(obj5, obj6))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool CompareEnumerations(object value1, object value2, string[] ignorePropertiesList)
        {
            if (((value1 == null) && (value2 != null)) || ((value1 != null) && (value2 == null)))
            {
                return false;
            }
            if ((value1 != null) && (value2 != null))
            {
                IEnumerable<object> source = ((IEnumerable) value1).Cast<object>();
                IEnumerable<object> enumerable2 = ((IEnumerable) value2).Cast<object>();
                if (source.Count<object>() != enumerable2.Count<object>())
                {
                    return false;
                }
                for (int i = 0; i < source.Count<object>(); i++)
                {
                    object obj2 = source.ElementAt<object>(i);
                    object obj3 = enumerable2.ElementAt<object>(i);
                    Type type = obj2.GetType();
                    if ((IsAssignableFrom(type) || IsPrimitiveType(type)) || IsValueType(type))
                    {
                        if (!CompareValues(obj2, obj3))
                        {
                            return false;
                        }
                    }
                    else if (!CompareObj(obj2, obj3, ignorePropertiesList))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool CompareObj(object inputObjectA, object inputObjectB, string[] ignorePropertiesList)
        {
            bool flag = true;
            if ((inputObjectA != null) && (inputObjectB != null))
            {
                foreach (PropertyInfo info in inputObjectA.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (info.CanRead && !ignorePropertiesList.Contains<string>(info.Name))
                    {
                        object obj2 = info.GetValue(inputObjectA, null);
                        object obj3 = info.GetValue(inputObjectB, null);
                        if ((IsAssignableFrom(info.PropertyType) || IsPrimitiveType(info.PropertyType)) || IsValueType(info.PropertyType))
                        {
                            if (!CompareValues(obj2, obj3))
                            {
                                Console.WriteLine("Property Name {0}", info.Name);
                                flag = false;
                            }
                        }
                        else if (IsEnumerableType(info.PropertyType))
                        {
                            Console.WriteLine("Property Name {0}", info.Name);
                            CompareEnumerations(obj2, obj3, ignorePropertiesList);
                        }
                        else if (info.PropertyType.IsClass)
                        {
                            if (!CompareObj(info.GetValue(inputObjectA, null), info.GetValue(inputObjectB, null), ignorePropertiesList))
                            {
                                flag = false;
                            }
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                }
                return flag;
            }
            return false;
        }

        private static bool CompareValues(object value1, object value2)
        {
            bool flag = true;
            IComparable comparable = value1 as IComparable;
            if (((value1 == null) && (value2 != null)) || ((value1 != null) && (value2 == null)))
            {
                return false;
            }
            if ((comparable != null) && (comparable.CompareTo(value2) != 0))
            {
                return false;
            }
            if (!object.Equals(value1, value2))
            {
                flag = false;
            }
            return flag;
        }

        public static T DeepCopy<T>(T obj)
        {
            if ((obj is string) || obj.GetType().IsValueType)
            {
                return obj;
            }
            object obj2 = Activator.CreateInstance(obj.GetType());
            foreach (FieldInfo info in obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
            {
                try
                {
                    info.SetValue(obj2, DeepCopy<object>(info.GetValue(obj)));
                }
                catch
                {
                }
            }
            return (T) obj2;
        }

        [AsyncStateMachine(typeof(<DeepCopyAsync>d__0))]
        public static Task<T> DeepCopyAsync<T>(T obj)
        {
            <DeepCopyAsync>d__0<T> d__;
            d__.obj = obj;
            d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<DeepCopyAsync>d__0<T>>(ref d__);
            return d__.<>t__builder.Task;
        }

        public static bool IsALike(object original, object comparedToObject)
        {
            if (original.GetType() != comparedToObject.GetType())
            {
                return false;
            }
            MethodInfo[] methods = original.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (comparedToObject.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Length != methods.Length)
            {
                return false;
            }
            PropertyInfo[] properties = original.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (comparedToObject.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Length != properties.Length)
            {
                return false;
            }
            FieldInfo[] fields = original.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (comparedToObject.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Length != fields.Length)
            {
                return false;
            }
            foreach (FieldInfo info in fields)
            {
                FieldInfo field = comparedToObject.GetType().GetField(info.Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (field == null)
                {
                    return false;
                }
                object obj2 = original.GetType().InvokeMember(info.Name, BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, original, null);
                object obj3 = comparedToObject.GetType().InvokeMember(field.Name, BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, comparedToObject, null);
                if (obj2 == null)
                {
                    if (obj3 != null)
                    {
                        return false;
                    }
                    return true;
                }
                if (obj2.GetType() != obj3.GetType())
                {
                    return false;
                }
                if (!obj2.ToString().Equals(obj3.ToString()))
                {
                    return false;
                }
            }
            foreach (PropertyInfo info3 in properties)
            {
                PropertyInfo property = comparedToObject.GetType().GetProperty(info3.Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (property == null)
                {
                    return false;
                }
                object obj4 = original.GetType().InvokeMember(info3.Name, BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, original, null);
                object obj5 = comparedToObject.GetType().InvokeMember(property.Name, BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, comparedToObject, null);
                if (obj4.GetType() != obj5.GetType())
                {
                    return false;
                }
                if (!obj4.ToString().Equals(obj5.ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsAssignableFrom(Type type) => 
            typeof(IComparable).IsAssignableFrom(type);

        private static bool IsEnumerableType(Type type) => 
            typeof(IEnumerable).IsAssignableFrom(type);

        private static bool IsPrimitiveType(Type type) => 
            type.IsPrimitive;

        private static bool IsValueType(Type type) => 
            type.IsValueType;

        [CompilerGenerated]
        private struct <DeepCopyAsync>d__0<T> : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<T> <>t__builder;
            public T obj;
            private ObjectHelper.<>c__DisplayClass0_0<T> <>8__1;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter <>u__1;

            private void MoveNext()
            {
                T r;
                int num = this.<>1__state;
                try
                {
                    Microsoft.Runtime.CompilerServices.TaskAwaiter awaiter;
                    if (num != 0)
                    {
                        this.<>8__1 = new ObjectHelper.<>c__DisplayClass0_0<T>();
                        this.<>8__1.obj = this.obj;
                        this.<>8__1.r = default(T);
                        awaiter = AwaitExtensions.GetAwaiter(TaskEx.Run(new Action(this.<>8__1.<DeepCopyAsync>b__0)));
                        if (!awaiter.IsCompleted)
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter, ObjectHelper.<DeepCopyAsync>d__0<T>>(ref awaiter, ref (ObjectHelper.<DeepCopyAsync>d__0<T>) ref this);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter();
                        this.<>1__state = num = -1;
                    }
                    awaiter.GetResult();
                    awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter();
                    r = this.<>8__1.r;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult(r);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }
    }
}

