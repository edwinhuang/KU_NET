
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kubility;
using System;

public struct ErrorResult
{
    bool success;

    ErrorType error;

    int MsgID;



    public ErrorResult(ErrorType type, bool suc, int msg = -1)
    {
        success = suc;
        MsgID = msg;
        error = type;
    }

    public static implicit operator bool(ErrorResult result)
    {
        return result.success;
    }

    public static implicit operator ErrorType(ErrorResult result)
    {
        return result.error;
    }

    public static implicit operator string(ErrorResult result)
    {
        return "返回结果 >> " + result.success.ToString() + " Type >> " + result.error.ToString();
    }
}


/// <summary>
/// socket服务  暂定为单tcp连接
/// </summary>
public class SocketService : SingleTon<SocketService>
{
    public bool Debug
    {
        get
        {
            return false;
        }
    }

    public const int TimeOutTime = 15;

    private const int ReconnectTime = 3;

    private int Current_Reconnect = 0;


    List<int> msglist = new List<int>();

    KTcpClient tcp;

    public KTcpClient Tcp
    {
        get
        {
            return tcp;
        }
    }

    private Dictionary<int, ClsTuple<float, object>> NetReq = new Dictionary<int, ClsTuple<float, object>>();

    public const float DeltaTime = 10f;
    #region HEART BEAT
    private float _recordHeratBeatTime = 0f;

    public float recordHeratBeatTime
    {
        get { return _recordHeratBeatTime; }
        set
        {
            _recordHeratBeatTime = value;
        }
    }

    IEnumerator HeartTask;

    IEnumerator HeartCheckTask;


    public void StartHeartBeat()
    {


        HeartTask = HertBeat();

        HeartCheckTask = HeartBeatCheck();

        GlobalHelper.mIns.StartCoroutine(HeartTask);

        GlobalHelper.mIns.StartCoroutine(HeartCheckTask);

    }

    IEnumerator HeartBeatCheck()
    {
        Current_Reconnect = 0;
        recordHeratBeatTime = Time.realtimeSinceStartup;
        while (true)
        {

            if (Current_Reconnect < ReconnectTime)
            {
                float delta = Time.realtimeSinceStartup - recordHeratBeatTime;
                if (delta > DeltaTime + 5f)// 10s差是为了网络和协程延迟
                {
                    if (Current_Reconnect == 0)
                    {
                       // DialogController.mIns.DialogControllerShow(102035);
                    }


                    Reconnect((data) =>
                    {
                        if (data)
                            Current_Reconnect = 0;
                        else
                            Current_Reconnect++;
                    });

                    yield return new WaitForSeconds(3f);
                }
                else
                {
                    this.CancelReconnect();

                    yield return new WaitForSeconds(1f);
                }



            }
            else
            {
                ReconnectionFail();
                yield break;
            }


        }
    }

	public bool isInDebugPause = false;

    IEnumerator HertBeat()
    {
        recordHeratBeatTime = Time.realtimeSinceStartup;

        while (true)
        {
			if (!isInDebugPause)
            {
                EmptyReq req = new EmptyReq();

                SocketService.mIns.Send<EmptyReq, EmptyResp>(TcpMainCMD.HEART_BEAT, TcpSubCMD.CS_HEART_BEAT, TcpSubCMD.SC_HEART_BEAT, req, delegate (EmptyResp arg1, ErrorResult arg2)
                {
                    if (arg2)
                    {
                        float delta = Time.realtimeSinceStartup - recordHeratBeatTime;
                        if (delta > 0f)
                        {
                            LogMgr.Log("差值为多少  :" + delta.ToString());
                        }


                        recordHeratBeatTime = Time.realtimeSinceStartup;
                    }
                });
            }


            yield return new WaitForSeconds(DeltaTime);
        }
    }

    public void EndHeartBeat()
    {
        if (HeartCheckTask != null)
            GlobalHelper.mIns.StopCoroutine(HeartCheckTask);

        if (HeartTask != null)
            GlobalHelper.mIns.StopCoroutine(HeartTask);




    }

    public void ClearNetReq()
    {
        NetReq.Clear();
    }

    #endregion

    public SocketService()
    {
        FactoryUtils.InitFactory();
    }


    public void Create(string IP, int port, Action<bool> DoneCallback)
    {
        if (tcp == null)
        {
            tcp = new KTcpClient();

        }

        Action<bool> callback = null;

        callback = (value) =>
        {
            LogMgr.Log(" 连接 callback");
            tcp.ConnectEvent -= callback;

            GameNotificationCenter.AddNetObserver((int)SKSCMD.NET_CONNECT, delegate ()
            {
                DoneCallback.TryCall(value);
            });

            //TODO 
            tcp.ConnectEvent = null;

        };

        tcp.ConnectEvent += callback;
        tcp.Init(IP, port, DoneCallback);


    }
   
    /// <summary>
    /// 非回调模式
    /// </summary>
    /// <param name="Main">Main.</param>
    /// <param name="Sub">Sub.</param>
    /// <param name="data">Data.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public void Send<T>(TcpMainCMD Main, TcpSubCMD Sub, T data) where T : struct, NetDataReqInterface
    {
        try
        {
            if (Alive())
            {

                SocketStructData<T> socketData = new SocketStructData<T>();
                socketData.Head.MainCMD = (ushort)Main;
                socketData.Head.SubCMD = (ushort)Sub;
                socketData.Content = data;
                StructMessage message = socketData.Init();

                LogUtils.Log("发送请求", Main.ToString(), Sub.ToString());
                tcp.Send(message);
            }
        }
        catch (System.Exception ex)
        {
            MessageManager.mIns.ClearCallbacks();
        }

    }

  
    public void Send<T, U>(TcpMainCMD Main, TcpSubCMD Sub, TcpMainCMD RMain, TcpSubCMD RSub, T data, Action<U, ErrorResult> callback) where T : struct, NetDataReqInterface where U : NetDataRespInterface
    {
        int uid = MessageManager.GenerateUniqueID(RMain.Value, RSub.Value);
        Send<T, U>(Main, Sub, data, callback, uid);
    }

    public void Send<T, U>(TcpMainCMD Main, TcpSubCMD Sub, TcpSubCMD RSub, T data, Action<U, ErrorResult> callback) where T : struct, NetDataReqInterface where U : NetDataRespInterface
    {
        int uid = MessageManager.GenerateUniqueID(Main.Value, RSub.Value);
        Send<T, U>(Main, Sub, data, callback, uid);
    }

    void Send<T, U>(TcpMainCMD Main, TcpSubCMD Sub, T data, Action<U, ErrorResult> callback, params int[] ids) where T : struct, NetDataReqInterface where U : NetDataRespInterface
    {

        try
        {
            //if (Alive())
            //{
            int uid = MessageManager.GenerateUniqueID(Main, Sub);


            if (CheckNet<U>(Main, Sub, ids))
            {
                var head = new StructMessageHead();
                head.MainCMD = (ushort)Main;
                head.SubCMD = (ushort)Sub;

                StructMessage message = StructMessage.CreateReq(head, data);

                Action<U> MsgCallback = null;
                Action<int, ErrorType> errorCallback = null;

                //正常消息回调
                MsgCallback = (pdata) =>
                {
                    NetReq.Remove(uid);
                    CommonManager.mIns.Remove_Resp<int, ErrorType>(TcpMainCMD.COMMON, TcpSubCMD.SC_ERRO_MSG, errorCallback);
                    CallBackRemove(MsgCallback, ids);
                    callback.TryCall(pdata, new ErrorResult(ErrorType.None, true));

                };

                float time = Time.realtimeSinceStartup;

                NetReq.Add(uid, new ClsTuple<float, object>(time, MsgCallback));


                //错误消息回调
                errorCallback = (id, type) =>
                {
                    NetReq.Remove(uid);
                    CommonManager.mIns.Remove_Resp<int, ErrorType>(TcpMainCMD.COMMON, TcpSubCMD.SC_ERRO_MSG, errorCallback);
                    CallBackRemove(MsgCallback, ids);
                    LogUtils.LogError(Main.ToString(), Sub.ToString(), uid.ToString(), " 发生了错误");
                    callback.TryCall(default(U), new ErrorResult(type, false, id));

                };
                //超时回调
                MonoDelegate.mIns.Coroutine_Delay(TimeOutTime, () =>
                 {
                     if (NetReq.ContainsKey(uid))
                     {
                         CommonManager.mIns.Remove_Resp<int, ErrorType>(TcpMainCMD.COMMON, TcpSubCMD.SC_ERRO_MSG, errorCallback);
                         float delta = Time.realtimeSinceStartup - NetReq[uid].field0;
                         LogMgr.Log("timeout delta  " + delta + " current " + Time.realtimeSinceStartup + "   old  " + NetReq[uid].field0);
                         if (delta > TimeOutTime - 0.1f)///float 误差
                             {
                             //DialogController.mIns.DialogControllerShow(102007);
                             LogUtils.LogError(Main.ToString(), Sub.ToString(), " 超时");
                             callback.TryCall(default(U), new ErrorResult(ErrorType.TimeOut, false));
                         }

                         NetReq.Remove(uid);

                     }


                 });

                ///错误消息注册，并且自动移除
                ErrorManager.Wait_Error_AutoRomove(errorCallback);

                LogUtils.Log("发送请求", Main.ToString(), Sub.ToString());


                tcp.Send<U>(message, MsgCallback, ids);

            }
            else
            {
                LogUtils.Log("重复请求  ", Main.ToString(), Sub.ToString());
            }


            //}
        }
        catch (System.Exception ex)
        {
            MessageManager.mIns.ClearCallbacks();
        }

    }


    bool CheckNet<U>(int Main, int Sub, params int[] ids)
    {
        int uid = MessageManager.GenerateUniqueID(Main, Sub);
        if (!NetReq.ContainsKey(uid))
        {

            return true;
        }
        else
        {
            float lasttime = NetReq[uid].field0;
            float delta = Time.realtimeSinceStartup - lasttime;

            if (delta > TimeOutTime - 0.1f)//float 误差
            {

                CallBackRemove(NetReq[uid].field1 as Action<U>, ids);
                NetReq.Remove(uid);
                return true;
            }
            else
            {
                return false;
            }
        }
    }



    void CallBackRemove<U>(Action<U> callback, params int[] uids)
    {
        if (callback != null)
        {
            var dic = MessageManager.mIns.GetCallbcks();
            if (CallBackContains(uids))
            {
                int hashcode = callback.GetHashCode();
                for (int i = 0; i < uids.Length; ++i)
                {
                    int uid = uids[i];
                    var en = dic[uid].GetEnumerator();
                    while (en.MoveNext())
                    {
                        if (en.Current.Key == hashcode)
                        {
                            dic[uid].Remove(en.Current);
                            break;
                        }
                    }

                }
            }
        }

    }



    bool CallBackContains(params int[] uids)
    {

        var dic = MessageManager.mIns.GetCallbcks();
        bool ret = false;
        for (int i = 0; i < uids.Length; ++i)
        {
            int uid = uids[i];
            ret = dic.ContainsKey(uid) && dic[uid].Count > 0;
            if (ret)
            {
                return true;
            }
        }
        return ret;
    }



    public void Reconnect(Action<bool> callback)
    {
        if (tcp != null)
        {
            if (callback == null)
            {
                tcp.Reconnect();
            }
            else
            {
                Action<bool> ReconnectResult = (value) =>
                 {

                     if (!value)
                     {

                     }
                     else
                     {
                         this.CancelReconnect();
                     }

                     callback.TryCall(value);

                 };


                GameNotificationCenter.AddObserver(GlobalHelper.mIns, GameCMD.RECONNECT, ReconnectResult);



                tcp.ReconnectCallback((result) =>
                {
                    GameNotificationCenter.AddNetObserver((int)SKSCMD.NET_RECONNECT_DIAL_RECONNECT, delegate ()
                    {
                        GameNotificationCenter.PostNotice(GameCMD.RECONNECT, result);
                    });


                });
            }


        }
    }

    public void CloseConnect()
    {
        if (Alive())
        {

            EmptyReq req = new EmptyReq();
            SocketService.mIns.Send<EmptyReq>(TcpMainCMD.HEART_BEAT, TcpSubCMD.CS_PLYAER_EXIT, req);

            EndHeartBeat();
            tcp.CloseConnect();
        }
    }

    public void Close()
    {
        if (tcp != null)
        {
            tcp.Close();
        }
    }

    public bool Alive()
    {
        if (tcp != null)
        {
            return tcp.SocketAviliable();
        }
        return false;
    }


    /// <summary>
    /// 取消重连
    /// </summary>
    public void CancelReconnect()
    {
        //DialogController.mIns.ReconnectDialogHide();//隐藏重连提示

    }

    void ReconnectionFail()
    {
        CancelReconnect();

       // DialogController.mIns.DialogControllerShow(102036, GameController.ReLogin);//重新登录
    }


}
