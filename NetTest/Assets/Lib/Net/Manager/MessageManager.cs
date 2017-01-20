//#define debug
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;

namespace Kubility
{


    public class MessageManager :MonoBehaviour
    {

        public static MessageManager mIns;

        QuequeTuple SendQueue;
        QuequeTuple ReceiveQueue;

        Queue<MessageHead> m_DataBufferList;

        Dictionary<int, LinkedList<KeyValuePair<int, object>>> callbackDic;

        Action<BaseMessage> custom;

        object mlock;

        ByteBuffer cache;


        bool HasNewMsg
        {
            get
            {
                return cache.DataCount > 0;
            }
        }

        void Awake()
        {
            mIns = this;
            if (this.cache == null)
            {
                this.cache = new ByteBuffer(2048);
                this.mlock = new object();

                this.ReceiveQueue = new QuequeTuple();
                this.SendQueue = new QuequeTuple();

                this.m_DataBufferList = new Queue<MessageHead>();
                this.callbackDic = new Dictionary<int, LinkedList<KeyValuePair<int, object>>>();
            }
        }

        public QuequeTuple GetSendQueue()
        {
            return SendQueue;
        }

        public QuequeTuple GetReceiveQueue()
        {
            return ReceiveQueue;
        }


        public Dictionary<int, LinkedList<KeyValuePair<int, object>>> GetCallbcks()
        {
            return callbackDic;
        }

        public void ClearCallbacks()
        {
            callbackDic.Clear();
        }



        public void Close()
        {
            callbackDic.Clear();

            ReceiveQueue.Clear();

            cache.Clear();

        }

        public MessageManager()
        {
            int thiscode = this.GetHashCode();
        }

        public void PushToReceiveBuffer(byte[] data)
        {
            lock (mlock)
            {
                int thiscode = this.GetHashCode();
                int hashcode = cache.GetHashCode();
                string s = cache.ToString();
                cache += data;
            }
        }
        /// <summary>
        /// SyncDealWithMsg
        /// </summary>
        void Update()
        {
            lock(mlock)
            {
                int thiscode = this.GetHashCode();
                int hashcode = cache.GetHashCode();
                string s = cache.ToString();
                while (HasNewMsg)
                {
                    CheckNewData();
                }
            }
        }

        public void PushToWaitQueue<T>(BaseMessage message, int ReceveID, Action<T, int> callback, int uniqueID = -1)
        {

            int uid = ReceveID;
            if (!callbackDic.ContainsKey(uid))
            {
                var stack = new LinkedList<KeyValuePair<int, object>>();
                stack.AddLast(new KeyValuePair<int, object>(uniqueID, callback));
                callbackDic.Add(uid, stack);

            }
            else
            {
                callbackDic[uid].AddLast(new KeyValuePair<int, object>(uniqueID, callback));
            }
        }

        public void RegiseterCustomDeal(Action<BaseMessage> ev)
        {
            custom = ev;
        }

        public static int GenerateUniqueID(int main, int sub)
        {
            int old = sub;
            while (sub > 0)
            {
                sub /= 10;
                main *= 10;
            }

            return main + old;
        }


        byte[] CacheRead(int begin = 0, int len = -1)
        {
            byte[] readdata;
            if (len < 0)
            {
                lock (mlock)
                {
                    readdata = cache.ConverToBytes();
                    if (readdata != null)
                        cache.Clear(readdata.Length);
                }
            }
            else
            {
                lock (mlock)
                {
                    readdata = cache.Read(begin, len);
                    if (readdata != null)
                        cache.Clear(readdata.Length);
                }
            }

            return readdata;
        }

        void CheckNewData()
        {
//            try
//            {
                MessageHead head = null;

                if (m_DataBufferList.Count > 0)
                {
                    head = m_DataBufferList.Peek();
                }

//#if debug
				LogMgr.Log("step1 ---- "+cache.DataCount);
//#endif

                //等待直到缓存区数据大于包头数据
                if (cache.DataCount >= MessageInfo.ReceiveHeadLen)
                {

                    int leftLen = cache.DataCount;

                    if (head == null)
                    {
                        head = BaseMessage.ReadHead(CacheRead(0, MessageInfo.ReceiveHeadLen));
                        m_DataBufferList.Enqueue(head);
                    }

                    int blen = head.bodyLen;//包头长度 + 包体长度
//#if debug
					LogMgr.Log("step2 ---- "+cache.DataCount +" blen ="+ blen);
//#endif

                    //等待直到缓存区数据大于总长度
                    if (leftLen > 0 && leftLen + MessageInfo.ReceiveHeadLen >= blen)
                    {
                        BaseMessage message = null;

                        byte[] readBys = CacheRead(0, blen - MessageInfo.ReceiveHeadLen);
                        //												lock (m_lock) {
                        if (readBys != null && !TryCallLuaFunc(head.MainCMD, head.SubCMD, readBys))
                        {

                            message = DataInterface.TryGet(head).DynamicCreate(readBys, head);

                            if (message == null)
                            {
                                LogMgr.LogError("Receive Null Or Deserialze failed or connection Closed");
                            }
                            else
                            {
                                if (custom == null)
                                    DealWithMessage(message);
                                else
                                    custom(message);
                            }
                        }

                        m_DataBufferList.Dequeue();

                    }
                    else
                    {
//#if debug
												LogMgr.Log("step3 ---- "+cache.DataCount +" blen ="+ blen);
//#endif
                    }

                }
                else
                {
//#if debug
										LogMgr.Log("step4 ---- "+cache.DataCount );
//#endif
                }
//            }
//            catch (Exception ex)
//            {
//                LogMgr.LogError(ex);
//            }

        }

        void DealWithReceiveBuffer()
        {
            if (ReceiveQueue.Size > 0)
            {
                BaseMessage message = ReceiveQueue.Get_First();

                int uid = GenerateUniqueID(message.DataHead.MainCMD, message.DataHead.SubCMD);
                LogMgr.Log("进入 接受缓存中");

                if (callbackDic.ContainsKey(uid) && callbackDic[uid].Count > 0)
                {
                    lock (mlock)
                    {
                        ReceiveQueue.Remove(0);
                    }

                    if (custom == null)
                        DealWithMessage(message);
                    else
                        custom(message);
                }
            }
        }

        bool TryCallLuaFunc(int main,int sub,byte[] bys)
        {
/*            LinkedList<KeyValuePair<int, object>> queue;
            bool ret = false;
            int uid = GenerateUniqueID(main, sub);
            lock (mlock)
            {

                ret = callbackDic.TryGetValue(uid, out queue);
            }

            if (ret && queue.Count > 0 )
            {
                object ac = queue.First.Value.Value;
                if (ac is Action<LuaResp, int>)
                {
                    var rac = ac as Action<LuaResp, int>;
                    if (rac != null)
                    {
                        lock (mlock)
                        {
                            queue.RemoveFirst();
                        }
                        var resp = new LuaResp();
                        resp.DeSerialize(bys);
                        resp.main = main;
                        resp.sub = sub;
                        rac(resp, 0);
                        return true;

                    }
 
                }

            }
//*/
            return false;
 
        }

        void DealWithMessage(BaseMessage message)
        {

            DealWithReceiveBuffer();

            if (message.LogNull())
            {
                LinkedList<KeyValuePair<int, object>> queue;
                bool ret = false;
                int uid = GenerateUniqueID(message.DataHead.MainCMD, message.DataHead.SubCMD);
                lock (mlock)
                {

                    ret = callbackDic.TryGetValue(uid, out queue);
                }

                if (ret && queue.Count > 0)
                {
                    object ac = queue.First.Value.Value;

                    lock (mlock)
                    {
                        queue.RemoveFirst();
                    }

                    if (MessageInfo.MessageType == MessageDataType.Json)
                    {
                        JsonMessage json = (JsonMessage)message;
                        Action<string, int> rac = (Action<string, int>)ac;

                        //												if(CommonManager.mIns.Contains(message.SubMessageType))
                        //												{
                        ////														CommonManager.mIns.PushMsg(data.ResquestData,message.DataHead);
                        //												}

                        if (rac != null)
                        {
                            rac(json.jsonData, uid);
                            //														GameNotificationCenter.AddNetObserver (uid, () => {
                            //																rac (json.jsonData, uid);
                            //														});

                        }

                    }
                    else if (MessageInfo.MessageType == MessageDataType.Struct)
                    {


                        StructMessage data = (StructMessage)message;

                        lock (mlock)
                        {
                            if (CommonManager.mIns.Contains(data.DataHead.SubCMD))
                            {
                                CommonManager.mIns.PushMsg(data.S2CData, message.DataHead);
                            }
                        }


                        Action<ValueType, int> rac = (Action<ValueType, int>)ac;

                        if (rac != null)
                        {
                            rac(data.S2CData, uid);
                            //														GameNotificationCenter.AddNetObserver (uid, () => {
                            //																rac (data.S2CData, uid);
                            //														});

                        }


                    }
                    else if (MessageInfo.MessageType == MessageDataType.ProtoBuf)
                    {
                        ProtobufMessage data = (ProtobufMessage)message;
                        Action<byte[], int> rac = (Action<byte[], int>)ac;


                        //												if(CommonManager.mIns.Contains(data.DataHead.SubCMD))
                        //												{
                        //														//CommonManager.mIns.PushMsg(data.ResquestData,message.DataHead);
                        //												}


                        if (rac != null)
                        {
                            rac(data.ProtobufData, uid);
                            //														GameNotificationCenter.AddNetObserver (uid, () => {
                            //																rac (data.ProtobufData, uid);
                            //														});

                        }


                    }



                }
                else
                {

                    if (MessageInfo.MessageType == MessageDataType.Json)
                    {
                        //JsonMessage json = (JsonMessage)message;


                    }
                    else if (MessageInfo.MessageType == MessageDataType.Struct)
                    {
                        StructMessage data = (StructMessage)message;
                        //need lock?
                        lock (mlock)
                        {
                            if (data.LogNull() && CommonManager.mIns.Contains(data.DataHead.SubCMD))
                            {
                                CommonManager.mIns.PushMsg(data.S2CData, message.DataHead);
                            }
                            else
                            {
                                ReceiveQueue.Push_Back(message);
                            }
                        }



                    }
                    else if (MessageInfo.MessageType == MessageDataType.ProtoBuf)
                    {
                        //CommonManager.mIns.PushMsg(data.ResquestData,message.DataHead);

                    }



                }

            }
        }
    }


    public class QuequeTuple
    {
        ClsTuple<int, LinkedList<BaseMessage>> queue;

        public int Size
        {
            get
            {
                return queue.field1.Count;
            }
        }

        internal QuequeTuple()
        {
            Queue_Init(out queue);
        }

        void Queue_Init(out ClsTuple<int, LinkedList<BaseMessage>> queue)
        {
            queue = new ClsTuple<int, LinkedList<BaseMessage>>();
            queue.field0 = 0;
            queue.field1 = new LinkedList<BaseMessage>();
        }

        bool ComparePriority(ClsTuple<int, LinkedList<BaseMessage>> queue, int value)
        {
            if (queue.field0 < value)
            {
                queue.field0 = value;
                return true;

            }
            return false;
        }

        bool Contains(BaseMessage message)
        {
            return queue.field1.Contains(message);
        }

        public void Push_Back(BaseMessage t)
        {
            if (t == null)
            {
                LogMgr.LogError("Push_Back  args is Null");
                return;
            }

            if (ComparePriority(queue, t.GetPriority()))
            {
                lock (queue.field1)
                {
                    queue.field1.AddFirst(t);
                }
            }
            else
            {
                lock (queue.field1)
                {
                    queue.field1.AddLast(t);
                }
            }


        }

        public bool Pop_First(out BaseMessage ev)
        {

            if (queue.field1.Count > 0)
            {
                ev = queue.field1.First.Value;
                lock (queue.field1)
                {
                    queue.field1.RemoveFirst();
                }

                return true;
            }
            else
            {
                ev = null;
                return false;
            }
        }

        public bool Pop_Back(out BaseMessage ev)
        {

            if (queue.field1.Count > 0)
            {
                ev = queue.field1.Last.Value;
                lock (queue.field1)
                {
                    queue.field1.RemoveLast();
                }
                return true;
            }
            else
            {
                ev = null;
                return false;
            }
        }

        public bool Remove(BaseMessage message)
        {
            lock (queue.field1)
            {
                return queue.field1.Remove(message);
            }

        }

        public bool Remove(int index)
        {
            if (index > queue.field1.Count)
            {
                return false;
            }
            else
            {
                int i = 0;
                LinkedListNode<BaseMessage> first = queue.field1.First;
                while (first.Next != null)
                {
                    if (i == index)
                    {
                        lock (queue.field1)
                        {
                            queue.field1.Remove(first);
                        }

                        return true;
                    }

                    i++;
                    first = first.Next;
                }
                queue.field1.RemoveFirst();
                return true;
            }
        }


        public BaseMessage Get_First()
        {
            if (queue.field1.Count == 0)
            {
                return null;
            }
            else
            {
                return queue.field1.First.Value;
            }
        }

        public BaseMessage Get_Last()
        {
            if (queue.field1.Count == 0)
            {
                return null;
            }
            else
            {
                return queue.field1.Last.Value;
            }
        }

        public BaseMessage Get(int index)
        {
            if (index > queue.field1.Count)
            {
                return null;
            }
            else
            {
                int i = 0;
                LinkedListNode<BaseMessage> first = queue.field1.First;
                while (first.Next != null)
                {
                    if (i == index)
                    {
                        return first.Value;
                    }

                    i++;
                    first = first.Next;
                }
                return first.Value;

            }
        }

        public void Clear()
        {
            this.queue.field1.Clear();
        }

    }
}


