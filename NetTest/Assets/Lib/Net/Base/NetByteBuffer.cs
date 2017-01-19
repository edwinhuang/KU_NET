#define JAVA

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Kubility
{
    /// <summary>
    /// 性能不高 后期再优化
    /// </summary>
    public class NetByteBuffer : ByteBuffer
    {

        bool error;

        public bool HasError
        {
            get
            {
                return error;
            }
        }

        public NetByteBuffer(int size) : base(size)
        {

        }

        public NetByteBuffer(byte[] bys) : base(bys)
        {

        }

        public override byte[] Read(int begin, int len)
        {
            if (len > DataCount - begin)
                len = DataCount - begin;

            byte[] bys = new byte[len];

            int i = 0, j = 0;
            for (i = begin; i < begin + len; ++i, ++j)
            {
                bys[j] = this.buffer[i];

            }
            if (DataCount - i - 1 > 0)
            {
                System.Array.Copy(buffer, i, buffer, begin, DataCount - len);
                Position -= len;
            }
            else
            {
                ReSize(0);
            }

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bys);

            return bys;
        }

        #region operater

        public static NetByteBuffer operator +(NetByteBuffer left, NetByteBuffer right)
        {
            if (right != null)
            {
                int nextsize = right.DataCount + left.Position;
                if (nextsize > left.buffer.Length)
                {
                    left.IncreaseCapacity(nextsize);
                }

                int endpos = left.Position + right.DataCount;
                for (int i = left.Position, j = 0; i < endpos; ++i, ++j)
                {
                    left.buffer[i] = right.buffer[j];
                }
                right.Position = 0;

                left.Position += right.DataCount;
            }

            return left;
        }

        public static NetByteBuffer operator +(NetByteBuffer left, byte[] right)
        {
            if (right != null)
            {
#if debug
								LogMgr.Log("原来的数据大小为  "+left.DataCount.ToString()+"  新数据大小为 "+right.Length.ToString());
#endif

                int nextsize = right.Length + left.Position;
                if (nextsize > left.buffer.Length)
                {
                    left.IncreaseCapacity(nextsize);
                }

#if JAVA
                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(right);
#else
								if (BitConverter.IsLittleEndian)
										Array.Reverse (right);
#endif

                Array.Copy(right, 0, left.buffer, left.Position, right.Length);
                left.Position += right.Length;
            }

            return left;
        }

        public static NetByteBuffer operator +(NetByteBuffer left, int right)
        {
            left += BitConverter.GetBytes(right);
            return left;
        }

        public static NetByteBuffer operator +(NetByteBuffer left, Bool8 right)
        {
            byte[] bys = right.GetBytes();
            left += bys;
            return left;
        }

        public static NetByteBuffer operator +(NetByteBuffer left, Bool32 right)
        {
            byte[] bys = right.GetBytes();
            left += bys;
            return left;
        }

        public static NetByteBuffer operator +(NetByteBuffer left, float right)
        {
            left += BitConverter.GetBytes(right);
            return left;
        }

        public static NetByteBuffer operator +(NetByteBuffer left, double right)
        {
            left += BitConverter.GetBytes(right);
            return left;
        }

        public static NetByteBuffer operator +(NetByteBuffer left, short right)
        {
            left += BitConverter.GetBytes(right);
            return left;
        }

        public static NetByteBuffer operator +(NetByteBuffer left, long right)
        {
            left += BitConverter.GetBytes(right);
            return left;
        }

        public static NetByteBuffer operator +(NetByteBuffer left, uint right)
        {
            left += BitConverter.GetBytes(right);
            return left;
        }

        public static NetByteBuffer operator +(NetByteBuffer left, ulong right)
        {
            left += BitConverter.GetBytes(right);
            return left;
        }

        public static NetByteBuffer operator +(NetByteBuffer left, ushort right)
        {
            left += BitConverter.GetBytes(right);
            return left;
        }

        public static NetByteBuffer operator +(NetByteBuffer left, string right)
        {
            if (string.IsNullOrEmpty(right))
            {
                left += (short)0;
            }
            else
            {
                byte[] bs = System.Text.Encoding.UTF8.GetBytes(right);
                left += (short)bs.Length;
                if (bs.Length > 0)
                    left += bs;
            }


            return left;
        }

        public static NetByteBuffer operator +(NetByteBuffer left, List<int> right)
        {

            left += (short)right.Count;
            for (int i = 0; i < right.Count; ++i)
            {
                left += right[i];
            }

            return left;
        }

        public static NetByteBuffer operator +(NetByteBuffer left, List<short> right)
        {

            left += (short)right.Count;
            for (int i = 0; i < right.Count; ++i)
            {
                left += right[i];
            }

            return left;
        }


        public static NetByteBuffer operator +(NetByteBuffer left, List<long> right)
        {

            left += (short)right.Count;
            for (int i = 0; i < right.Count; ++i)
            {
                left += right[i];
            }

            return left;
        }

        public static NetByteBuffer operator +(NetByteBuffer left, List<ushort> right)
        {

            left += (short)right.Count;
            for (int i = 0; i < right.Count; ++i)
            {
                left += right[i];
            }

            return left;
        }

        public static NetByteBuffer operator +(NetByteBuffer left, List<string> right)
        {

            left += (short)right.Count;
            for (int i = 0; i < right.Count; ++i)
            {
                left += right[i];
            }

            return left;
        }

        public static NetByteBuffer operator +(NetByteBuffer left, List<uint> right)
        {

            left += (short)right.Count;
            for (int i = 0; i < right.Count; ++i)
            {
                left += right[i];
            }

            return left;
        }

        public static NetByteBuffer operator +(NetByteBuffer left, List<ulong> right)
        {

            left += (short)right.Count;
            for (int i = 0; i < right.Count; ++i)
            {
                left += right[i];
            }

            return left;
        }



        //public static NetByteBuffer operator  + (NetByteBuffer left, List<NetDataSubReqInterface> right)
        //{

        //        left += (short)right.Count;
        //        for(int i =0; i < right.Count;++i)
        //        {
        //                right[i].Serialize(left);
        //        }

        //        return left;
        //}



        #region Read

        public static explicit operator Bool8(NetByteBuffer left)
        {
            if (left != null && left.DataCount >= ByteStream.BYTE_LEN)
            {
                byte[] tempBys = new byte[ByteStream.BYTE_LEN];
                Array.Copy(left.buffer, 0, tempBys, 0, ByteStream.BYTE_LEN);
                left.Clear(ByteStream.BYTE_LEN);
#if JAVA
                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(tempBys);
#else
								if (BitConverter.IsLittleEndian)
										Array.Reverse (tempBys);
#endif
                return Bool8.ToBool8(tempBys);
            }
            LogMgr.LogError("Bool8 Read from ByteBuffer Error");
            left.error = true;
            return false;
        }

        public static explicit operator Bool32(NetByteBuffer left)
        {
            if (left != null && left.DataCount >= ByteStream.INT32_LEN)
            {
                byte[] tempBys = new byte[ByteStream.INT32_LEN];
                Array.Copy(left.buffer, 0, tempBys, 0, ByteStream.INT32_LEN);
                left.Clear(ByteStream.INT32_LEN);
#if JAVA
                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(tempBys);
#else
								if (BitConverter.IsLittleEndian)
								Array.Reverse (tempBys);
#endif
                return Bool32.ToBool32(tempBys);
            }
            LogMgr.LogError("Bool32 Read from ByteBuffer Error");
            left.error = true;
            return false;
        }


        public static explicit operator int(NetByteBuffer left)
        {
            if (left != null && left.DataCount >= ByteStream.INT32_LEN)
            {
                byte[] tempBys = new byte[ByteStream.INT32_LEN];
                Array.Copy(left.buffer, 0, tempBys, 0, ByteStream.INT32_LEN);
                left.Clear(ByteStream.INT32_LEN);
#if JAVA
                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(tempBys);
#else
								if (BitConverter.IsLittleEndian)
								Array.Reverse (tempBys);
#endif
                return BitConverter.ToInt32(tempBys, 0);
            }
            LogMgr.LogError("int Read from ByteBuffer Error");
            left.error = true;
            return default(int);
        }

        public static explicit operator uint(NetByteBuffer left)
        {
            if (left != null && left.DataCount >= ByteStream.INT32_LEN)
            {
                byte[] tempBys = new byte[ByteStream.INT32_LEN];
                Array.Copy(left.buffer, 0, tempBys, 0, ByteStream.INT32_LEN);
                left.Clear(ByteStream.INT32_LEN);
#if JAVA
                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(tempBys);
#else
								if (BitConverter.IsLittleEndian)
								Array.Reverse (tempBys);
#endif
                return BitConverter.ToUInt32(tempBys, 0);
            }
            LogMgr.LogError("uint Read from ByteBuffer Error");
            left.error = true;
            return default(UInt32);
        }

        public static explicit operator short(NetByteBuffer left)
        {
            if (left != null && left.DataCount >= ByteStream.SHORT16_LEN)
            {
                byte[] tempBys = new byte[ByteStream.SHORT16_LEN];
                Array.Copy(left.buffer, 0, tempBys, 0, ByteStream.SHORT16_LEN);
                left.Clear(ByteStream.SHORT16_LEN);
#if JAVA
                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(tempBys);
#else
								if (BitConverter.IsLittleEndian)
								Array.Reverse (tempBys);
#endif
                return BitConverter.ToInt16(tempBys, 0);
            }
            LogMgr.LogError("short Read from ByteBuffer Error");
            left.error = true;
            return default(short);
        }

        public static explicit operator float(NetByteBuffer left)
        {
            if (left != null && left.DataCount >= ByteStream.FLOAT_LEN)
            {
                byte[] tempBys = new byte[ByteStream.FLOAT_LEN];
                Array.Copy(left.buffer, 0, tempBys, 0, ByteStream.FLOAT_LEN);
                left.Clear(ByteStream.FLOAT_LEN);
#if JAVA
                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(tempBys);
#else
								if (BitConverter.IsLittleEndian)
								Array.Reverse (tempBys);
#endif
                return BitConverter.ToSingle(tempBys, 0);
            }
            LogMgr.LogError("float Read from ByteBuffer Error");
            left.error = true;
            return default(float);
        }

        public static explicit operator double(NetByteBuffer left)
        {
            if (left != null && left.DataCount >= ByteStream.DOUBLE_LEN)
            {
                byte[] tempBys = new byte[ByteStream.DOUBLE_LEN];
                Array.Copy(left.buffer, 0, tempBys, 0, ByteStream.DOUBLE_LEN);
                left.Clear(ByteStream.DOUBLE_LEN);
#if JAVA
                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(tempBys);
#else
								if (BitConverter.IsLittleEndian)
								Array.Reverse (tempBys);
#endif
                return BitConverter.ToDouble(tempBys, 0);
            }
            LogMgr.LogError("double Read from ByteBuffer Error");
            left.error = true;
            return default(double);
        }

        public static explicit operator ushort(NetByteBuffer left)
        {
            if (left != null && left.DataCount >= ByteStream.SHORT16_LEN)
            {
                byte[] tempBys = new byte[ByteStream.SHORT16_LEN];
                Array.Copy(left.buffer, 0, tempBys, 0, ByteStream.SHORT16_LEN);
                left.Clear(ByteStream.SHORT16_LEN);
#if JAVA
                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(tempBys);
#else
								if (BitConverter.IsLittleEndian)
								Array.Reverse (tempBys);
#endif
                return BitConverter.ToUInt16(tempBys, 0);
            }
            LogMgr.LogError("ushort Read from ByteBuffer Error");
            left.error = true;
            return default(ushort);
        }

        public static explicit operator ulong(NetByteBuffer left)
        {
            if (left != null && left.DataCount >= ByteStream.LONG_LEN)
            {
                byte[] tempBys = new byte[ByteStream.LONG_LEN];
                Array.Copy(left.buffer, 0, tempBys, 0, ByteStream.LONG_LEN);
                left.Clear(ByteStream.LONG_LEN);
#if JAVA
                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(tempBys);
#else
								if (BitConverter.IsLittleEndian)
								Array.Reverse (tempBys);
#endif
                return BitConverter.ToUInt64(tempBys, 0);
            }
            LogMgr.LogError("ulong Read from ByteBuffer Error");
            left.error = true;
            return default(ulong);
        }

        public static explicit operator byte(NetByteBuffer left)
        {
            if (left != null && left.DataCount >= ByteStream.BYTE_LEN)
            {
                byte tempBys = new byte();

                tempBys = left.buffer[0];

                left.Clear(ByteStream.BYTE_LEN);


                return tempBys;
            }
            LogMgr.LogError("byte Read from ByteBuffer Error");
            left.error = true;
            return default(byte);
        }

        public static explicit operator long(NetByteBuffer left)
        {
            if (left != null && left.DataCount >= ByteStream.LONG_LEN)
            {
                byte[] tempBys = new byte[ByteStream.LONG_LEN];
                Array.Copy(left.buffer, 0, tempBys, 0, ByteStream.LONG_LEN);
                left.Clear(ByteStream.LONG_LEN);
#if JAVA
                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(tempBys);
#else
								if (BitConverter.IsLittleEndian)
								Array.Reverse (tempBys);
#endif
                return BitConverter.ToInt64(tempBys, 0);
            }
            LogMgr.LogError("long Read from ByteBuffer Error");
            left.error = true;
            return default(long);
        }

        public static explicit operator string(NetByteBuffer left)
        {
            if (left != null && left.DataCount >= ByteStream.SHORT16_LEN)
            {
                int strLen = (short)left;
                if (strLen == 0)
                {
                    return "";
                }
                byte[] tempBys = new byte[strLen];
                Array.Copy(left.buffer, 0, tempBys, 0, strLen);
                left.Clear(strLen);
#if JAVA
                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(tempBys);
#else
								if (BitConverter.IsLittleEndian)
								Array.Reverse (tempBys);
#endif
                return System.Text.Encoding.UTF8.GetString(tempBys);
            }
            LogMgr.LogError("string Read from ByteBuffer Error");
            left.error = true;
            return "";
        }

        public static explicit operator List<int>(NetByteBuffer left)
        {
            if (left != null && left.DataCount >= ByteStream.SHORT16_LEN)
            {
                int strLen = (short)left;
                if (!left.HasError)
                {
                    List<int> list = new List<int>();
                    for (int i = 0; i < strLen; ++i)
                    {
                        if (!left.HasError)
                            list.Add((int)left);
                        else
                            break;

                    }
                    return list;
                }


            }
            LogMgr.LogError("List<int> Read from ByteBuffer Error");
            left.error = true;
            return null;
        }

        public static explicit operator List<short>(NetByteBuffer left)
        {
            if (left != null && left.DataCount >= ByteStream.SHORT16_LEN)
            {
                int strLen = (short)left;
                if (!left.HasError)
                {
                    List<short> list = new List<short>();
                    for (int i = 0; i < strLen; ++i)
                    {
                        if (!left.HasError)
                            list.Add((short)left);
                        else
                            break;
                    }
                    return list;
                }
            }
            LogMgr.LogError("List<short> Read from ByteBuffer Error");
            left.error = true;
            return null;
        }

        public static explicit operator List<long>(NetByteBuffer left)
        {
            if (left != null && left.DataCount >= ByteStream.SHORT16_LEN)
            {
                int strLen = (short)left;
                if (!left.HasError)
                {
                    List<long> list = new List<long>();
                    for (int i = 0; i < strLen; ++i)
                    {
                        if (!left.HasError)
                            list.Add((long)left);
                        else
                            break;
                    }
                    return list;
                }


            }
            LogMgr.LogError("List<long> Read from ByteBuffer Error");
            left.error = true;
            return null;
        }

        public static explicit operator List<uint>(NetByteBuffer left)
        {
            if (left != null && left.DataCount >= ByteStream.SHORT16_LEN)
            {
                int strLen = (short)left;
                if (!left.HasError)
                {
                    List<uint> list = new List<uint>();
                    for (int i = 0; i < strLen; ++i)
                    {
                        if (!left.HasError)
                            list.Add((uint)left);
                        else
                            break;
                    }
                    return list;
                }

            }
            LogMgr.LogError("List<uint> Read from ByteBuffer Error");
            left.error = true;
            return null;
        }

        public static explicit operator List<ushort>(NetByteBuffer left)
        {
            if (left != null && left.DataCount >= ByteStream.SHORT16_LEN)
            {
                int strLen = (short)left;
                if (!left.HasError)
                {

                    List<ushort> list = new List<ushort>();
                    for (int i = 0; i < strLen; ++i)
                    {
                        if (!left.HasError)
                            list.Add((ushort)left);
                        else
                            break;
                    }
                    return list;
                }

            }
            LogMgr.LogError("List<ushort> Read from ByteBuffer Error");
            left.error = true;
            return null;
        }

        public static explicit operator List<ulong>(NetByteBuffer left)
        {
            if (left != null && left.DataCount >= ByteStream.SHORT16_LEN)
            {
                int strLen = (short)left;
                if (!left.HasError)
                {
                    List<ulong> list = new List<ulong>();
                    for (int i = 0; i < strLen; ++i)
                    {
                        if (!left.HasError)
                            list.Add((ulong)left);
                        else
                            break;
                    }
                    return list;
                }

                
            }
            LogMgr.LogError("List<ulong> Read from ByteBuffer Error");
            left.error = true;
            return null;
        }

        public static explicit operator List<string>(NetByteBuffer left)
        {
            if (left != null && left.DataCount >= ByteStream.SHORT16_LEN)
            {
                int strLen = (short)left;
                if (!left.HasError)
                {
                    List<string> list = new List<string>();
                    for (int i = 0; i < strLen; ++i)
                    {
                        if (!left.HasError)
                            list.Add((string)left);
                        else
                            break;
                    }
                    return list;
                }

                
            }
            LogMgr.LogError("List<string>  Read from ByteBuffer Error");
            left.error = true;
            return null;
        }


        public List<T> ReadRespList<T>() where T : NetDataSubRespInterface, new()
        {
            if (this.DataCount >= ByteStream.SHORT16_LEN)
            {
                short strLen = (short)this;
                if (!this.HasError)
                {
                    List<T> list = new List<T>();
                    for (int i = 0; i < strLen; ++i)
                    {
                        if (!this.HasError)
                        {
                            T data = new T();
                            data.DeSerialize(this);
                            list.Add(data);
                        }
                        else
                            break;
    
                    }
                    return list;
                }

            }
            LogMgr.LogError("ReadRespList Read from ByteBuffer Error");
            this.error = true;
            return null;
        }

        #endregion

        public void SeriableList<T>(List<T> list) where T : NetDataSubReqInterface, new()
        {
            byte[] right = BitConverter.GetBytes((short)list.Count);
            if (right != null)
            {
#if debug
								LogMgr.Log("原来的数据大小为  "+left.DataCount.ToString()+"  新数据大小为 "+right.Length.ToString());
#endif

                int nextsize = right.Length + this.Position;
                if (nextsize > this.buffer.Length)
                {
                    this.IncreaseCapacity(nextsize);
                }

#if JAVA
                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(right);
#else
								if (BitConverter.IsLittleEndian)
										Array.Reverse (right);
#endif

                Array.Copy(right, 0, this.buffer, this.Position, right.Length);
                this.Position += right.Length;
            }

            for (int i = 0; i < list.Count; ++i)
            {
                if (!this.HasError)
                    list[i].Serialize(this);
                else
                    break;
            }

        }

        #endregion
    }
}


