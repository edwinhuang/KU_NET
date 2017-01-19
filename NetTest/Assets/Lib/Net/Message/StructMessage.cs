//#define KDEBUG
using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text;

namespace Kubility
{



    public class StructMessageHead : MessageHead
    {



        public override ByteBuffer Serialize(byte[] content)
        {
            NetByteBuffer by = new NetByteBuffer(MessageInfo.HeadLen);
            if (content == null)
            {
                by += SubCMD;
                this.bodyLen = MessageInfo.HeadLen;
            }
            else
            {
                by += SubCMD;
                by += content;
                this.bodyLen = MessageInfo.HeadLen + content.Length;
            }



            byte[] needCheck = by.ConverToBytes();
            

            this.CheckedValue = calculateCheckSum(needCheck);
#if KDEBUG
            LogMgr.Log("needCheck  " + needCheck.Length + "  CheckedValue ="+ CheckedValue +" Bodylen = "+ bodyLen);


            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < needCheck.Length; ++i)
            {
                sb.Append(needCheck[i].ToString()+" -> ");
            }
            sb.Append(" End");

            LogMgr.Log("Info = "+ sb.ToString());
#endif

            by.Clear();

            by += bodyLen;// 4+
            by += CheckedValue;//4
            by += MainCMD;//2
            by += SubCMD;//2

            return by;

        }
    }




    public class StructMessage : BaseMessage
    {
        private byte[] _C2SData;

        public bool UnAddHead = false;

        /// <summary>
        /// 发送给服务器的请求数据
        /// </summary>
        /// <value>The struct data.</value>
        public byte[] C2SData
        {
            get
            {
                return _C2SData;
            }
            protected set
            {
                _C2SData = value;
            }
        }

        private ValueType _S2CData;

        /// <summary>
        /// 返回给客户端的请求数据
        /// </summary>
        /// <value>The resquest data.</value>
        public ValueType S2CData
        {
            get
            {
                return _S2CData;
            }
            protected set
            {
                _S2CData = value;
            }
        }

        public static StructMessage CreateReq(MessageHead head, NetDataReqInterface data) 
        {
            if (MessageInfo.MessageTypeCheck(MessageDataType.Struct))
            {
                StructMessage message = new StructMessage();
                message.head = head;
                message._C2SData = data.Serialize();

                return message;
            }

            return null;
        }

        public static StructMessage CreateLuaReq(MessageHead head, LuaInterface.LuaByteBuffer data)
        {
            if (MessageInfo.MessageTypeCheck(MessageDataType.Struct))
            {
                StructMessage message = new StructMessage();
                message.head = head;
                message.UnAddHead = true;


                message._C2SData = data.buffer;
                

                return message;
            }

            return null;
        }

        public static StructMessage CreateResp(MessageHead head, ValueType data)
        {
            if (MessageInfo.MessageTypeCheck(MessageDataType.Struct))
            {
                StructMessage message = new StructMessage();
                message.head = head;
                message._S2CData = data;

                return message;
            }

            return null;
        }

        public void Wait_LuaResp(Action<LuaResp> ev, params int[] ReceveIDs)
        {
            for (int i = 0; i < ReceveIDs.Length; ++i)
            {
                MessageManager.mIns.PushToWaitQueue(this, ReceveIDs[i], delegate(LuaResp data, int eventID)
                {
                    ev(data);
                }, ev.GetHashCode());
            }
 
        }



        public override void Wait_Deserialize<T>(Action<T> ev, params int[] ReceveIDs)
        {

            if (MessageInfo.MessageTypeCheck(MessageDataType.Struct))
            {


                for (int i = 0; i < ReceveIDs.Length; ++i)
                {
                    MessageManager.mIns.PushToWaitQueue(this, ReceveIDs[i], delegate (ValueType value, int eventID)
                    {

                        T data = (T)Convert.ChangeType(value, typeof(T));

                        ev(data);
                    }, ev.GetHashCode());
                }


            }


        }


        public override byte[] Serialize(bool addHead = true)
        {
            if (MessageInfo.MessageTypeCheck(MessageDataType.Struct))
            {

                ByteBuffer buffer = null;

                if (head != null && addHead && !UnAddHead)
                {
                    buffer = head.Serialize(_C2SData);
                }
                else
                {
                    buffer = new ByteBuffer(128);
                }

                if (_C2SData != null)
                    buffer += _C2SData;

                return buffer.ConverToBytes();
            }
            return null;

        }

    }
}


