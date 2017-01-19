using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Kubility
{
    public abstract class ICommonClass : ICommonInterface
    {
        protected TcpSubCMD data;

        public const float delay = 8f;

        public abstract void DispatcherEvents(ValueType data, BaseEnum main, BaseEnum sub, object callback);

        public abstract void Destroy();

        public virtual int GenerateID(BaseEnum Main, BaseEnum Sub)
        {
            int old = Sub;
            int sub = Sub;
            int main = Main;

            while (sub > 0)
            {
                sub /= 10;
                main *= 10;
            }

            return main + old;
        }


        public int SelfType
        {
            get
            {
                return data.Value;
            }
        }
    }

    public class CommonManager : SingleTon<CommonManager>
    {
        private struct Comstruct : IEquatable<Comstruct>
        {
            public ValueType Data;

            public int uid;

            public int main;

            public int sub;

            public bool Equals(Comstruct other)
            {
                if (Data != other.Data)
                    return false;
                else if (uid != other.uid)
                    return false;
                else if (main != other.main)
                    return false;
                else if (sub != other.sub)
                    return false;
                return true;
            }
        }

        Dictionary<int, ICommonInterface> CommonData = new Dictionary<int, ICommonInterface>();

        Queue<Comstruct> NetDataList = new Queue<Comstruct>();


        /// <summary>
        /// 自定义回执
        /// </summary>
        Dictionary<int, LinkedList<object>> CallBackList = new Dictionary<int, LinkedList<object>>();


        public static void StaticDestroy()
        {
            if (CommonManager.mIns != null)
            {
				ICommonInterface[] comInters = new ICommonInterface[CommonManager.mIns.CommonData.Count];
				CommonManager.mIns.CommonData.Values.CopyTo (comInters, 0);
				Array.ForEach<ICommonInterface>(comInters, data => data.Destroy());
                CommonManager.mIns.CallBackList.Clear();
                CommonManager.mIns.NetDataList.Clear();
                CommonManager.mIns.CommonData.Clear();
                CommonManager.Destroy();

            }
        }

        public void Register<T>(T target) where T : ICommonInterface
        {
            if (!CommonData.ContainsKey(target.SelfType))
            {
                CommonData.Add(target.SelfType, target);


                if (CommonData.Count > 0 )
                {
                    if(GlobalHelper.mIns != null)
                        GlobalHelper.mIns.RegisterFixedUpdate(WebServer.mIns, Distpather);
                    else
                        LogMgr.LogError("GlobalHelper is Null");
                }


            }
            else
            {
                LogMgr.LogError("重复注册");
            }
        }

        public bool UnRegister<T>(T target) where T : ICommonInterface
        {

            bool ret = CommonData.Remove(target.SelfType);
            if (CommonData.Count == 0)
                GlobalHelper.mIns.UnRegister(WebServer.mIns, Distpather);

            return ret;
        }

        public bool Contains(int type)
        {
            return CommonData.ContainsKey(type);
        }

        public void PushMsg(ValueType netdata, MessageHead head)
        {

            Comstruct data = new Comstruct();
            data.Data = netdata;
            data.main = head.MainCMD;
            data.sub = head.SubCMD;

            if (CommonData.ContainsKey(data.sub))
            {


                data.uid = CommonData[data.sub].GenerateID((BaseEnum)head.MainCMD, (BaseEnum)head.SubCMD);

                if (!NetDataList.Contains(data))
                {
                    NetDataList.Enqueue(data);
                    Distpather();
                }
                    

            }
            else
            {
                LogMgr.Log("未发现匹配的代理去执行");
            }
        }

        public void PushMsg<T>(T netdata, TcpMainCMD main, TcpSubCMD sub) where T : struct
        {

            Comstruct data = new Comstruct();
            data.Data = netdata;
            data.sub = sub;
            data.main = main;

            if (CommonData.ContainsKey(data.sub))
            {
                data.uid = CommonData[data.sub].GenerateID(main, sub);

                if (!NetDataList.Contains(data))
                {
                    NetDataList.Enqueue(data);
                    Distpather();
                }
                   

            }
            else
            {
                LogMgr.Log("未发现匹配的代理去执行");
            }
        }

        public void Wait_Resp(TcpMainCMD main, TcpSubCMD sub, Action Callback)
        {
            if (CommonData.ContainsKey(sub))
            {
                int uid = CommonData[sub].GenerateID(main, sub);

                if (CallBackList.ContainsKey(uid))
                {

                    var o = CallBackList[uid];
                    o.AddLast(Callback);

                }
                else
                {
                    var q = new LinkedList<object>();
                    q.AddLast(Callback);

                    CallBackList.Add(uid, q);
                }
            }

        }

        public void Wait_Resp<T>(TcpMainCMD main, TcpSubCMD sub, Action<T> Callback)
        {
            if (CommonData.ContainsKey(sub))
            {
                int uid = CommonData[sub].GenerateID(main, sub);

                if (CallBackList.ContainsKey(uid))
                {

                     var o = CallBackList[uid];
                    o.AddLast(Callback);
                }
                else
                {
                    var q = new LinkedList<object>();
                    q.AddLast(Callback);

                    CallBackList.Add(uid, q);
                }
            }

        }

        public void Wait_Resp<T,U>(TcpMainCMD main, TcpSubCMD sub, Action<T,U> Callback)
        {
            if (CommonData.ContainsKey(sub))
            {
                int uid = CommonData[sub].GenerateID(main, sub);

                if (CallBackList.ContainsKey(uid))
                {

                    var o = CallBackList[uid];
                    o.AddLast(Callback);
                }
                else
                {
                    var q = new LinkedList<object>();
                    q.AddLast(Callback);

                    CallBackList.Add(uid, q);
                }
            }

        }

        public bool Remove_Resp(TcpMainCMD main, TcpSubCMD sub)
        {
            if (CommonData.ContainsKey(sub))
            {
                int uid = CommonData[sub].GenerateID(main, sub);

                return CallBackList.Remove(uid);
            }

            return false;
        }

        public bool Remove_Resp(TcpMainCMD main, TcpSubCMD sub, Action callback)
        {
            if (CommonData.ContainsKey(sub))
            {
                int uid = CommonData[sub].GenerateID(main, sub);
                if (CallBackList.ContainsKey(uid))
                {

                    return CallBackList[uid].Remove(callback);
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public bool Remove_Resp<T>(TcpMainCMD main, TcpSubCMD sub, Action<T> callback)
        {
            if (CommonData.ContainsKey(sub))
            {
                int uid = CommonData[sub].GenerateID(main, sub);

                if (CallBackList.ContainsKey(uid))
                {

                    return CallBackList[uid].Remove(callback);
                }
                else
                {
                    return false;
                }

            }

            return false;
        }

        public bool Remove_Resp<T,U>(TcpMainCMD main, TcpSubCMD sub, Action<T,U> callback)
        {
            if (CommonData.ContainsKey(sub))
            {
                int uid = CommonData[sub].GenerateID(main, sub);

                if (CallBackList.ContainsKey(uid))
                {

                    return CallBackList[uid].Remove(callback);
                }
                else
                {
                    return false;
                }

            }

            return false;
        }

        void Distpather()
        {

            while (NetDataList.Count > 0)
            {
                Comstruct data = NetDataList.Dequeue();

                if (data.uid > 0)
                {
                    //同步分发事件
                    if (!CallBackList.ContainsKey(data.uid) )
                    {
                        CommonData[data.sub].DispatcherEvents(data.Data, (BaseEnum)data.main, (BaseEnum)data.sub, null);
                    }
                    else
                    {
                        var value = CallBackList[data.uid];
                        if (value.Count > 0)
                        {
                            CommonData[data.sub].DispatcherEvents(data.Data, (BaseEnum)data.main, (BaseEnum)data.sub, value.First.Value);
                            value.RemoveFirst();
                        }
                        else
                        {
                            CommonData[data.sub].DispatcherEvents(data.Data, (BaseEnum)data.main, (BaseEnum)data.sub, null);
                        }
                       

                    }



                }

            }
        }

    }
}


