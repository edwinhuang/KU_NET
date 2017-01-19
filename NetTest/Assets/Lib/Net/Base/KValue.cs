using UnityEngine;
using System.Collections;
using System;
using System.Net.Sockets;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading;
using System.IO;

namespace Kubility
{

    public struct Bool8
    {
        byte value;

        public static Bool8 ToBool8 (byte[] bys)
        {
            Bool8 kvalue;
            kvalue.value = bys [0];
            return kvalue;
        }

        public override string ToString ()
        {

            if (value == 0)
                return "false";
            else
                return "true";
        }

        public byte[] GetBytes ()
        {

            return new byte[]{ value };
        }

        public static implicit  operator bool (Bool8 bvalue)
        {
            return bvalue.value == 0 ? false : true;
        }

        public static implicit  operator Bool8 (bool bvalue)
        {
            Bool8 newvalue;
            newvalue.value = bvalue ? (byte)1 : (byte)0;
            return newvalue;
        }

        public static explicit  operator Bool8 (Bool32 bvalue)
        {
            Bool8 newvalue;
            newvalue.value = bvalue ? (byte)1 : (byte)0;
            return newvalue;
        }
		
		
    }

    public struct Bool32
    {
        int value;

        public static Bool32 ToBool32 (byte[] bys)
        {
			
            Bool32 kvalue;
            kvalue.value = BitConverter.ToInt32 (bys, 0);
            return kvalue;
        }

        public override string ToString ()
        {

            if (value == 0)
                return "false";
            else
                return "true";
        }

        public byte[] GetBytes ()
        {
            return BitConverter.GetBytes (value);
        }

        public static implicit  operator bool (Bool32 bvalue)
        {
            return bvalue.value == 0 ? false : true;
        }

        public static implicit  operator Bool32 (bool bvalue)
        {
            Bool32 newvalue;
            newvalue.value = bvalue ? 1 : 0;
            return newvalue;
        }

        public static explicit  operator Bool32 (Bool8 bvalue)
        {
            Bool32 newvalue;
            newvalue.value = bvalue ? 1 : 0;
            return newvalue;
        }
    }

    [StructLayout (LayoutKind.Explicit)]
    public class Union<T,V>
    {
        [FieldOffset (0)]
        public T m_FirstValue;
		
        [FieldOffset (0)]
        public V m_SecondValue;
    }

    [StructLayout (LayoutKind.Explicit)]
    public class Union<T,V,K>
    {
        [FieldOffset (0)]
        public T m_FirstValue;
		
        [FieldOffset (0)]
        public V m_SecondValue;
		
        [FieldOffset (0)]
        public K m_ThirdValue;
    }

    [StructLayout (LayoutKind.Explicit)]
    public class Union<T,V,K,L>
    {
        [FieldOffset (0)]
        public T m_FirstValue;
		
        [FieldOffset (0)]
        public V m_SecondValue;
		
        [FieldOffset (0)]
        public K m_ThirdValue;
		
        [FieldOffset (0)]
        public L m_FourthValue;
    }

    /// <summary>
    /// class
    /// </summary>
    public class ClsTuple<T,V>:IDisposable
    {
        public T field0;
        public V field1;

        public ClsTuple ()
        {
						
        }

        public ClsTuple (T t, V v)
        {
            this.field0 = t;
            this.field1 = v;
        }

        public void Dispose ()
        {

            free (field0);
            free (field1);
            GC.SuppressFinalize (this);
        }

        void free (object obj)
        {
            if (obj != null && obj.GetType () == typeof(Stream)) {
                ((Stream)obj).Close ();
            }
        }
    }

    public class ClsTuple<T,V,U>:IDisposable
    {
        public T field0;
        public V field1;
        public U field2;

        public ClsTuple ()
        {

        }

        public ClsTuple (T t, V v, U u)
        {
            this.field0 = t;
            this.field1 = v;
            this.field2 = u;
        }

        public void Dispose ()
        {
            free (field0);
            free (field1);
            free (field2);
            GC.SuppressFinalize (this);
        }

        void free (object obj)
        {
            if (obj != null && obj.GetType () == typeof(Stream)) {
                ((Stream)obj).Close ();
            }
        }
    }

    public class ClsTuple<T,V,U,K>:IDisposable
    {
        public T field0;
        public V field1;
        public U field2;
        public K field3;

        public ClsTuple ()
        {

        }

        public ClsTuple (T t, V v, U u, K k)
        {
            this.field0 = t;
            this.field1 = v;
            this.field2 = u;
            this.field3 = k;
        }

        public void Dispose ()
        {
            free (field0);
            free (field1);
            free (field2);
            free (field3);
            GC.SuppressFinalize (this);
        }

        void free (object obj)
        {
            if (obj != null && obj.GetType () == typeof(Stream)) {
                ((Stream)obj).Close ();
            }
        }
    }

    public class ClsTuple<T,V,U,K,P>:IDisposable
    {
        public T field0;
        public V field1;
        public U field2;
        public K field3;
        public P field4;

        public ClsTuple ()
        {

        }

        public ClsTuple (T t, V v, U u, K k, P p)
        {
            this.field0 = t;
            this.field1 = v;
            this.field2 = u;
            this.field3 = k;
            this.field4 = p;
        }

        public void Dispose ()
        {
            free (field0);
            free (field1);
            free (field2);
            free (field3);
            free (field4);
            GC.SuppressFinalize (this);
        }

        void free (object obj)
        {
            if (obj != null && obj.GetType () == typeof(Stream)) {
                ((Stream)obj).Close ();
            }
        }
    }

    public class ClsTuple<T,V,U,K,P,L>:IDisposable
    {
        public T field0;
        public V field1;
        public U field2;
        public K field3;
        public P field4;
        public L field5;

        public ClsTuple ()
        {

        }

        public ClsTuple (T t, V v, U u, K k, P p, L l)
        {
            this.field0 = t;
            this.field1 = v;
            this.field2 = u;
            this.field3 = k;
            this.field4 = p;
            this.field5 = l;
        }

        public void Dispose ()
        {
            free (field0);
            free (field1);
            free (field2);
            free (field3);
            free (field4);
            free (field5);
            GC.SuppressFinalize (this);
        }

        void free (object obj)
        {
            if (obj.GetType () == typeof(Stream)) {
                ((Stream)obj).Close ();
            }
        }
    }



	
    /// <summary>
    /// struct
    /// </summary>
	
    public struct MiniTuple<T,V>
    {
        public T field0;
        public V field1;
    }

	
    public struct MiniTuple<T,V,K>
    {
        public T field0;
        public V field1;
        public K field2;
		
    }

    public struct MiniTuple<T,V,K,U>
    {
        public T field0;
        public V field1;
        public K field2;
        public U field3;
    }

    public struct MiniTuple<T,V,K,U,G>
    {
        public T field0;
        public V field1;
        public K field2;
        public U field3;
        public G field4;
    }

    public struct MiniTuple<T,V,K,U,G,L>
    {
        public T field0;
        public V field1;
        public K field2;
        public U field3;
        public G field4;
        public L field5;
    }


    public class Trustee<T>
    {
        private T t;

        public delegate bool enbleHandler ();

        private enbleHandler enbleEvent;
		
        private object obj;
        private ValueType intValue;
		
        //		public static implicit operator Trustee<T> (T value)
        //		{
        //			return new Trustee<T>(value);
        //		}
        //
        //		public static implicit operator bool (Trustee<T> value)
        //		{
        //			return value.Get() == null;
        //		}
        //
        //		public static explicit operator T (Trustee<T> value)
        //		{
        //			return value.Get();
        //		}
		
        public Trustee (T value)
        {
            this.t = value;
        }

        public void PushStruct<V> (V v) where V:struct
        {
            this.intValue = v;
        }

        public V GetStruct<V> () where V:struct
        {
            return (V)intValue;
        }

        public void PushObject<V> (V  v)
        {
            obj = v;
        }

        public V GetObject<V> ()
        {
            return (V)obj;
        }

		
        public void  RegisterEvent (enbleHandler act)
        {
            this.enbleEvent += act;
        }

        public void ClearEvent ()
        {
            this.enbleEvent = null;
        }

        public T Get ()
        {
            if (t == null || (enbleEvent != null && !enbleEvent ())) {
                throw new NullReferenceException ();
            }
            return t;
        }

        public void Set (Trustee<T> trustee)
        {
            this.t = trustee.Get ();
        }
		
		
    }

    //		public class BaseEnum : IComparable<BaseEnum>, IEquatable<BaseEnum>
    //		{
    //				protected volatile short derviced = -1;
    //				static volatile int counter = -1;
    //
    //				protected static Dictionary<System.Type,Dictionary<int, PriorityQueue<BaseEnum>>> hashTable  = new Dictionary<System.Type, Dictionary<int, PriorityQueue<BaseEnum>>>();
    //
    //				protected static List<BaseEnum> members = new List<BaseEnum> ();
    //
    //				private string Name { get; set; }
    //
    //				public int Value { get; protected set; }
    //
    //				sealed class Comparer : IComparer<BaseEnum>
    //				{
    //						public int Compare (BaseEnum x, BaseEnum y)
    //						{
    //								return y.Value - x.Value;
    //						}
    //				}
    //
    //				/// <summary>
    //				/// 不指定数值构造实例
    //				/// </summary>
    //				protected BaseEnum (string name)
    //				{
    //						derviced++;
    //						this.Name = name;
    //						Type type = this.GetType();
    //						if(hashTable.ContainsKey(type))
    //						{
    //								++counter;
    //								List<int> keys = new List<int>(hashTable[type].Keys);
    //								if(keys.Count >0 && hashTable[type][keys[keys.Count -1]].Count >0)
    //								{
    //										this.Value = ++hashTable[type][keys[keys.Count -1]].Top().Value;
    //								}
    //								else
    //								{
    //										this.Value = counter;
    //								}
    //
    //						}
    //						else
    //						{
    //								this.Value = ++counter;
    //						}
    //
    //						members.Add (this);
    //
    //
    //						if (!hashTable.ContainsKey (type))
    //						{
    //								Dictionary<int, PriorityQueue<BaseEnum>> tuple = new Dictionary<int, PriorityQueue<BaseEnum>>();
    //
    //								var queue  =new PriorityQueue<BaseEnum>(32,new Comparer());
    //								tuple.Add(this.derviced,queue);
    //
    //								hashTable.Add (type, tuple);
    //
    //						}
    //						else
    //						{
    //								Dictionary<int, PriorityQueue<BaseEnum>> dict =hashTable[type];
    //								if(!dict.ContainsKey(this.derviced))
    //								{
    //										var queue  =new PriorityQueue<BaseEnum>(32,new Comparer());
    //										dict.Add(this.derviced,queue);
    //								}
    //								else
    //								{
    //										dict[this.derviced].Push(this);
    //								}
    //						}
    //
    //
    //
    //				}
    //
    //
    //
    //				/// <summary>
    //				/// 指定数值构造实例
    //				/// </summary>
    //				protected BaseEnum (string name, int value)
    //						: this (name)
    //				{
    //						derviced++;
    //						this.Value = value;
    //						counter = value;
    //
    //						System.Type type= this.GetType();
    //
    //						Dictionary<int, PriorityQueue<BaseEnum>> dict =hashTable[type];
    //						if(!dict.ContainsKey(this.derviced))
    //						{
    //								var queue  =new PriorityQueue<BaseEnum>(32,new Comparer());
    //								dict.Add(this.derviced,queue);
    //						}
    //						else
    //						{
    //								dict[this.derviced].Push(this);
    //						}
    //
    //				}

    public class BaseEnum : IComparable<BaseEnum>, IEquatable<BaseEnum>
    {
        static int counter = -1;
        //默认数值计数器
        private static Hashtable hashTable = new Hashtable ();
        //不重复数值集合
        protected static List<BaseEnum> members = new List<BaseEnum> ();
        //所有实例集合
        private string Name { get; set; }

        public int Value { get; set; }

        /// <summary>
        /// 不指定数值构造实例
        /// </summary>
        protected BaseEnum (string name)
        {
            this.Name = name;
            this.Value = ++counter;
            members.Add (this);
            if (!hashTable.ContainsKey (this.Value)) {
                hashTable.Add (this.Value, this);
            }
        }

        /// <summary>
        /// 指定数值构造实例
        /// </summary>
        protected BaseEnum (string name, int value)
            : this (name)
        {
            this.Value = value;
            counter = value;
        }

        public override int GetHashCode ()
        {
            return base.GetHashCode ();
        }

        /// <summary>
        /// 向string转换
        /// </summary>
        /// <returns></returns>
        public override string ToString ()
        {
            return this.Name.ToString () + " 值为 " + this.Value.ToString ();
        }

        /// <summary>
        /// 显式强制从int转换
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static explicit operator BaseEnum (int i)
        {
            var m = members.Find (p => p.Value == i);
            if (m != null) {
                return m;
            }

            return new BaseEnum (i.ToString (), i);

        }

        public static explicit operator BaseEnum (ushort i)
        {
            int value = i;

            var m = members.Find (p => p.Value == value);
            if (m != null) {
                return m;
            }

            return new BaseEnum (i.ToString (), value);
        }

        public static implicit operator int (BaseEnum e)
        {
            return e.Value;
        }

        public static implicit operator ushort (BaseEnum e)
        {
            return (ushort)e.Value;
        }

        public static bool TryParse (string name, out BaseEnum em)
        {
			
            if (string.IsNullOrEmpty (name)) {
                em = default(BaseEnum);
                return false;
            }
			
			
            for (int i = 0; i < members.Count; ++i) {
                BaseEnum sub = (BaseEnum)members [i];
                if (name.Equals (sub.Name)) {
                    em = sub;
                    return true;
					
                }
            }
            em = default(BaseEnum);
            return false;
        }


        public static void ForEach (Action<BaseEnum> action)
        {
            foreach (BaseEnum item in members) {
                action (item);
            }
        }

        public int CompareTo (BaseEnum other)
        {
            return this.Value.CompareTo (other.Value);
        }

        public bool Equals (BaseEnum other)
        {
            return this.Value.Equals (other.Value);
        }

        public override bool Equals (object obj)
        {
            if (!(obj is BaseEnum))
                return false;
            return this.Value == ((BaseEnum)obj).Value;
        }


        public static bool operator != (BaseEnum e1, BaseEnum e2)
        {
            if (!(e2 is BaseEnum))
                return true;
            return e1.Value != e2.Value;
        }

        public static bool operator < (BaseEnum e1, BaseEnum e2)
        {

            return e1.Value < e2.Value;
        }

        public static bool operator <= (BaseEnum e1, BaseEnum e2)
        {
            return e1.Value <= e2.Value;
        }



        public static bool operator == (BaseEnum e1, BaseEnum e2)
        {
            if (!(e2 is BaseEnum))
                return false;

            return e1.Value == e2.Value;
        }

        public static bool operator > (BaseEnum e1, BaseEnum e2)
        {

            return e1.Value > e2.Value;
        }

        public static bool operator >= (BaseEnum e1, BaseEnum e2)
        {

            return e1.Value >= e2.Value;
        }
    }

    [Serializable]
    public class SingleArray<T> where T :new()
    {
        [SerializeField]
        private T[] array;
        [SerializeField]
        private int mIndex;
        [SerializeField]
        private T singleValue;

        public SingleArray (int size, T single)
        {
            array = new T[size];
            singleValue = single;
            mIndex = -1;
        }

        public T Get (int index = 0)
        {
            if (index < array.Length) {
                return array [index];
            } else {
                throw new ArgumentException ();
            }
			
        }

        public void Set (int index, T value)
        {
            if (index < array.Length) {
                if (index == this.mIndex && value.Equals (singleValue)) {
                    mIndex = -1;
                    array [index] = value;
                } else if (mIndex == -1 && !value.Equals (singleValue)) {
                    mIndex = index;
                    array [index] = value;
					
                }
				
				
            } else {
                throw new ArgumentException ();
            }
        }
    }
	
}


