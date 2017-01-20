#if UNITY_EDITOR
//#define debug
#endif
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Text;

public enum SKSCMD
{
	NET_CONNECT = 10,

	NET_RECONNECT = 100,
	NET_RECONNECT_DirectLoading = 101,
	NET_RECONNECT_DIAL_RECONNECT = 102,
}

namespace Kubility
{
    /// <summary>
    /// Tcp client.
    /// </summary>
    public class KTcpClient : AbstractNetUnit
    {
        /// <summary>
        /// 自然退出
        /// </summary>
        //				bool quit = false;
        /// <summary>
        /// 内部socket
        /// </summary>
        AsyncSocket _socket;
        //				/// <summary>
        //				/// 发送线程
        //				/// </summary>
        //				KThread m_SendThread;
        /// <summary>
        /// 接收线程
        /// </summary>
        //				KThread m_ReceiveThread;
        /// <summary>
        /// ip地址
        /// </summary>
        IPEndPoint ipend;
        /// <summary>
        /// 锁
        /// </summary>
        //				ManualResetEvent mlock = new ManualResetEvent (false);
        /// <summary>
        /// socket参数
        /// </summary>
        SocketEventArgsExtern m_args;

        int ReceiveCount;

        bool UseNewReceive
        {
            get
            {
                if (ReceiveCount < 0)
                {
                    LogMgr.LogError("接受计数异常");
                }

                return ReceiveCount <= 0;
            }
        }

        private SocetArgType last;

        public SocetArgType LastType
        {
            get
            {
                return last;
            }
        }

        public enum SocetArgType
        {
            Connect,
            Send,
            Receive,
            Other,
        }

        public Action<bool> SendEvent;

        public Action<bool> ReceiveEvent;

        public Action<bool> ConnectEvent;

        #region public Interface

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipaddress">Ipaddress.</param>
        /// <param name="port">Port.</param>
        public bool Init(string ipaddress, int port,Action<bool> DoneCallback)
        {

            if (ipend != null && ipend.Address.ToString().Equals(ipaddress) && ipend.Port == port)
            {
                Action<bool> callback = null;

                callback = (value) =>
                {
                    LogMgr.Log("重连  callback");
                    //TODO
                    ConnectEvent = null;

                    GameNotificationCenter.AddNetObserver((int)SKSCMD.NET_RECONNECT,delegate()
                    {
                        DoneCallback.TryCall(value);
                    });
                    
                };

                ReconnectCallback(callback);
                return true;
            }

            IPAddress ip;
            bool ret = IPAddress.TryParse(ipaddress, out ip);
            if (!ret)
            {
                LogMgr.LogError("ip parse error");
                return false;
            }

            ReceiveCount = 0;

            ipend = new IPEndPoint(ip, port);

            _socket = AsyncSocket.Create(CreateTcpConnect(), ipend);

            if (m_args == null)
            {
                m_args = SocketAsyncEventArgsPool.mIns.CreateSocketArgs(ipend, _socket, AcceptEventArg_OnConnectCompleted);
            }


            StartConnect(_socket, m_args.m_ReceiveArgs);
            return true;
        }

        /// <summary>
        /// 无回调发送 ，
        /// </summary>
        /// <param name="message">Message.</param>
        public void Send(BaseMessage message)
        {
            if (message != null)
            {

                MessageManager.mIns.GetSendQueue().Push_Back(message);

                if (this.SocketAviliable())
                    SendMessage();

            }
            else
            {
                LogMgr.LogError("BaseMessage Send is Null");
            }

        }

        public void Send<T>(BaseMessage message, Action<T> callback, params int[] ReceiveIDs) where T : NetDataRespInterface
        {
            if (message != null)
            {

                MessageManager.mIns.GetSendQueue().Push_Back(message);

//                if (typeof(T) == typeof(LuaResp))
//                {
//                    StructMessage structmess = message as StructMessage;
//                    structmess.Wait_LuaResp(callback as Action<LuaResp>, ReceiveIDs);
//                }
//                else
//                {
                    message.Wait_Deserialize<T>(callback, ReceiveIDs);
//                }
                SendMessage();

            }
            else
            {
                LogMgr.LogError("BaseMessage Send<T> is Null");
            }

        }

        public void Reconnect()
        {
            if (!this.SocketAviliable())
            {
                _socket.Reconnect((bool value) =>
                {

                    LogMgr.Log("重连结果 " + value.ToString());
                    if (value)
                    {
                        ReceiveCount = 0;

                        PreReceive();
                        //暂留缓存的请求继续发送
                        while (MessageManager.mIns.GetSendQueue().Size > 0)
                            SendMessage();
                    }
                });
            }
        }

        public void ReconnectCallback(System.Action<bool> callback)
        {
            if (!this.SocketAviliable())
            {
                _socket.Reconnect((bool value) =>
                {

                    LogMgr.Log("重连结果 " + value.ToString());
                    if (value)
                    {
                        ReceiveCount = 0;
						_socket.SetState(AsyncSocket.SocketArgsStats.FREE);
						SocketService.mIns.ClearNetReq();
                        PreReceive();
                        //暂留缓存的请求继续发送
                        while (MessageManager.mIns.GetSendQueue().Size > 0)
                            SendMessage();
                    }

                });
            }
            else
            {
            }
        }

        void SendMessage()
        {
            BaseMessage message;
            if (MessageManager.mIns.GetSendQueue().Pop_First(out message))
            {
                SocketAsyncEventArgsPool.mIns.Pop_FreeForSend(delegate (SocketAsyncEventArgs _socketEvargs, bool retCode)
                {

                    if (!retCode)
                    {
                        _socketEvargs.RemoteEndPoint = _socket.GetRemoteIP();
                        _socketEvargs.Completed += new EventHandler<SocketAsyncEventArgs>(AcceptEventArg_OnConnectCompleted);
                    }

                    byte[] bys = message.Serialize();
//#if debug
										LogMgr.Log ("发送的数据大小 >>" + bys.Length);
//#endif

                    //StringBuilder sb = new StringBuilder();
                    //for (int i = 0; i < bys.Length; ++i)
                    //{
                    //    sb.Append(bys[i].ToString() + " -> ");
                    //}
                    //sb.Append(" End");

                    //LogMgr.Log("Info = " + sb.ToString());
                    _socketEvargs.SetBuffer(bys, 0, bys.Length);


                    _socket.SendAsync(_socketEvargs, OnSendCallback);

                    MessageManager.mIns.GetSendQueue().Remove(message);

                });
            }
        }

        public void CloseConnect()
        {
            {
                _socket.CloseConnect();
            }
        }

        public void Close()
        {
            if (_socket != null)
                _socket.CloseConnect();

            SocketAsyncEventArgsPool.mIns.Close();
        }


        #endregion

        void StartConnect(AsyncSocket psocket, SocketAsyncEventArgs ev)
        {
            bool ret = psocket.ConnectAsync(ev, ConnectCallback);
            if (!ret)
            {
                LogMgr.Log("自动退为同步回调");
            }
        }

        void PreReceive()
        {
            if (UseNewReceive)
            {
                SocketAsyncEventArgsPool.mIns.Pop_FreeForReceive((_socketEvargs, retCode) =>
                {

                    if (!retCode)
                    {
                        _socketEvargs.RemoteEndPoint = _socket.GetRemoteIP();
                        _socketEvargs.Completed += new EventHandler<SocketAsyncEventArgs>(AcceptEventArg_OnConnectCompleted);
                    }
                    //LogMgr.Log ("Pop_FreeForReceive  laststate = " + _socket.GetSocketState () + " retCode =" + retCode);
//#if debug
										LogUtils.Log ("准备接受新数据 ", _socket.GetSocketState ().ToString ());
//#endif

                    _socket.ReceiveAsync(_socketEvargs, OnReveiveCallBack);

                    Interlocked.Increment(ref ReceiveCount);
                });
            }


        }

        void ConnectCallback(SocketAsyncEventArgs args)
        {

            LogUtils.Log("连接成功 " + this._socket.GetSocketState());

            _socket.SetState(AsyncSocket.SocketArgsStats.CONNECTSCUUEED);

            PreReceive();

            if (ConnectEvent != null)
                ConnectEvent(true);
        }


        void PreSetsocketEventArgs(SocketAsyncEventArgs ev)
        {
            if (ev == null)
            {
                this.m_args = SocketAsyncEventArgsPool.mIns.CreateSocketArgs(ipend, _socket, AcceptEventArg_OnConnectCompleted);
                m_args.m_SendArgs.DisconnectReuseSocket = false;//关闭重用socket
                m_args.m_ReceiveArgs.DisconnectReuseSocket = false;//关闭重用socket

                FetChSocketInfo(m_args.m_ReceiveArgs, SocetArgType.Receive);
            }
            else
            {

                if (ev.LastOperation == SocketAsyncOperation.Receive || ev.LastOperation == SocketAsyncOperation.ReceiveFrom || ev.LastOperation == SocketAsyncOperation.ReceiveMessageFrom)
                {
                    //接收socketargs
                    FetChSocketInfo(ev, SocetArgType.Receive);
                }
                else if (ev.LastOperation == SocketAsyncOperation.Send || ev.LastOperation == SocketAsyncOperation.SendPackets || ev.LastOperation == SocketAsyncOperation.SendTo)
                {
                    //发送socketargs
                    FetChSocketInfo(ev, SocetArgType.Send);
                }
                else if (ev.LastOperation == SocketAsyncOperation.Connect || ev.LastOperation == SocketAsyncOperation.None)
                {
                    FetChSocketInfo(ev, SocetArgType.Connect);
                }
                else
                {
                    FetChSocketInfo(ev, SocetArgType.Other);
                }
            }
        }


        void AcceptEventArg_OnConnectCompleted(object obj, SocketAsyncEventArgs ev)
        {
            Socket socket = obj as Socket;


            {
//#if debug
								LogUtils.Log ("LastOperation ", ev.LastOperation.ToString (), " Socket状态", ev.SocketError.ToString (), ev.SocketFlags.ToString ());
//#endif

                PreSetsocketEventArgs(ev);

            }

        }

        void ProcessError(SocketAsyncEventArgs args)
        {
            //不分批处理了。
            if (ConnectEvent != null)
                ConnectEvent(false);

            if (ReceiveEvent != null)
                ReceiveEvent(false);

            if (SendEvent != null)
                SendEvent(false);

            if (args.SocketError == SocketError.ConnectionReset)
            {
                LogUtils.LogError("链接重置 将尝试重连");
                ErrorManager.PushMsg("链接重置 将尝试重连。", ErrorType.ConnectClose);
            }
            else if (args.SocketError == SocketError.Interrupted)
            {
                LogUtils.LogError("已取消阻止 Socket 调用的操作。");
                ErrorManager.PushMsg("已取消阻止 Socket 调用的操作。", ErrorType.UnKnown);
            }
            else if (args.SocketError == SocketError.TimedOut)
            {
                LogUtils.LogError("连接尝试超时，或者连接的主机没有响应。");
                ErrorManager.PushMsg("连接尝试超时，或者连接的主机没有响应。", ErrorType.TimeOut);
            }
            else if (args.SocketError == SocketError.AccessDenied)
            {
                LogUtils.LogError("已试图通过被其访问权限禁止的方式访问 Socket。");
                ErrorManager.PushMsg("已试图通过被其访问权限禁止的方式访问 Socket。");
            }
            else if (args.SocketError == SocketError.AddressAlreadyInUse)
            {
                LogUtils.LogError("只允许使用地址一次。");
                ErrorManager.PushMsg("只允许使用地址一次。", ErrorType.ArgError);
            }
            else if (args.SocketError == SocketError.AddressFamilyNotSupported)
            {
                LogUtils.LogError("不支持指定的地址族。如果指定了 IPv6 地址族而未在本地计算机上安装 IPv6 堆栈，则会返回此错误。如果指定了 IPv4 地址族而未在本地计算机上安装 IPv4 堆栈，则会返回此错误。");
                ErrorManager.PushMsg("不支持指定的地址族。如果指定了 IPv6 地址族而未在本地计算机上安装 IPv6 堆栈，则会返回此错误。如果指定了 IPv4 地址族而未在本地计算机上安装 IPv4 堆栈，则会返回此错误。。", ErrorType.ArgError);
            }
            else if (args.SocketError == SocketError.AddressNotAvailable)
            {
                LogUtils.LogError("选定的 IP 地址在此上下文中无效。");
                ErrorManager.PushMsg("选定的 IP 地址在此上下文中无效。", ErrorType.ArgError);
            }
            else if (args.SocketError == SocketError.AlreadyInProgress)
            {
                LogUtils.LogError("非阻止性 Socket 已有一个操作正在进行中。");
                ErrorManager.PushMsg("非阻止性 Socket 已有一个操作正在进行中。", ErrorType.NetError);
            }
            else if (args.SocketError == SocketError.ConnectionAborted)
            {
                LogUtils.LogError("此连接由 .NET Framework 或基础套接字提供程序中止。");
                ErrorManager.PushMsg("此连接由 .NET Framework 或基础套接字提供程序中止。", ErrorType.NetError);
            }
            else if (args.SocketError == SocketError.ConnectionRefused)
            {
                LogUtils.LogError("远程主机正在主动拒绝连接。");
                ErrorManager.PushMsg("远程主机正在主动拒绝连接。", ErrorType.NetError);
            }
            else if (args.SocketError == SocketError.DestinationAddressRequired)
            {
                LogUtils.LogError("在对 Socket 的操作中省略了必需的地址。");
                ErrorManager.PushMsg("在对 Socket 的操作中省略了必需的地址。", ErrorType.ArgError);
            }
            else if (args.SocketError == SocketError.Disconnecting)
            {
                LogUtils.LogError("正常关机正在进行中。");
                ErrorManager.PushMsg("正常关机正在进行中。", ErrorType.UnKnown);
            }
            else if (args.SocketError == SocketError.Fault)
            {
                LogUtils.LogError("基础套接字提供程序检测到无效的指针地址。");
                ErrorManager.PushMsg("基础套接字提供程序检测到无效的指针地址。", ErrorType.NullRef);
            }
            else if (args.SocketError == SocketError.HostDown)
            {
                LogUtils.LogError("由于远程主机被关闭，操作失败。");
                ErrorManager.PushMsg("由于远程主机被关闭，操作失败。", ErrorType.NetError);
            }
            else if (args.SocketError == SocketError.HostNotFound)
            {
                LogUtils.LogError("无法识别这种主机。该名称不是正式的主机名或别名。");
                ErrorManager.PushMsg("无法识别这种主机。该名称不是正式的主机名或别名。", ErrorType.ArgError);
            }
            else if (args.SocketError == SocketError.HostUnreachable)
            {
                LogUtils.LogError("没有到指定主机的网络路由。");
                ErrorManager.PushMsg("没有到指定主机的网络路由。", ErrorType.ArgError);
            }
            else if (args.SocketError == SocketError.InProgress)
            {
                LogUtils.LogError("阻止操作正在进行中。");
                ErrorManager.PushMsg("阻止操作正在进行中。", ErrorType.NetError);
            }
            else if (args.SocketError == SocketError.InvalidArgument)
            {
                LogUtils.LogError("给 Socket 成员提供了一个无效参数。");
                ErrorManager.PushMsg("给 Socket 成员提供了一个无效参数。", ErrorType.ArgError);
            }
            else if (args.SocketError == SocketError.IOPending)
            {
                LogUtils.LogError("应用程序已启动一个无法立即完成的重叠操作。");
                ErrorManager.PushMsg("应用程序已启动一个无法立即完成的重叠操作。", ErrorType.NetError);
            }
            else if (args.SocketError == SocketError.IsConnected)
            {
                LogUtils.LogError("Socket 已连接。");
                ErrorManager.PushMsg("Socket 已连接。", ErrorType.ArgError);
            }
            else if (args.SocketError == SocketError.MessageSize)
            {
                LogUtils.LogError("数据报太长。");
                ErrorManager.PushMsg("数据报太长。", ErrorType.ArgError);
            }
            else if (args.SocketError == SocketError.NetworkDown)
            {
                LogUtils.LogError("网络不可用。");
                ErrorManager.PushMsg("网络不可用。", ErrorType.NetError);
            }
            else if (args.SocketError == SocketError.NetworkReset)
            {
                LogUtils.LogError("应用程序尝试在已超时的连接上设置 KeepAlive。");
                ErrorManager.PushMsg("应用程序尝试在已超时的连接上设置 KeepAlive。", ErrorType.ArgError);
            }
            else if (args.SocketError == SocketError.NetworkUnreachable)
            {
                LogUtils.LogError("不存在到远程主机的路由。");
                ErrorManager.PushMsg("不存在到远程主机的路由。", ErrorType.NetError);
            }
            else if (args.SocketError == SocketError.NoBufferSpaceAvailable)
            {
                LogUtils.LogError("没有可用于 Socket 操作的可用缓冲区空间。");
                ErrorManager.PushMsg("没有可用于 Socket 操作的可用缓冲区空间。", ErrorType.UnKnown);
            }
            else if (args.SocketError == SocketError.NoData)
            {
                LogUtils.LogError("在名称服务器上找不到请求的名称或 IP 地址。");
                ErrorManager.PushMsg("在名称服务器上找不到请求的名称或 IP 地址。", ErrorType.ArgError);
            }
            else if (args.SocketError == SocketError.NoRecovery)
            {
                LogUtils.LogError("错误不可恢复或找不到请求的数据库。");
                ErrorManager.PushMsg("错误不可恢复或找不到请求的数据库。", ErrorType.ArgError);
            }
            else if (args.SocketError == SocketError.NotConnected)
            {
                LogUtils.LogError("应用程序试图发送或接收数据，但是 Socket 未连接。");
                ErrorManager.PushMsg("应用程序试图发送或接收数据，但是 Socket 未连接。", ErrorType.NetError);
            }
            else if (args.SocketError == SocketError.NotInitialized)
            {
                LogUtils.LogError("尚未初始化基础套接字提供程序。");
                ErrorManager.PushMsg("尚未初始化基础套接字提供程序。", ErrorType.ArgError);
            }
            else if (args.SocketError == SocketError.NotSocket)
            {
                LogUtils.LogError("对非套接字尝试 Socket 操作。");
                ErrorManager.PushMsg("对非套接字尝试 Socket 操作。", ErrorType.ArgError);
            }
            else if (args.SocketError == SocketError.OperationNotSupported)
            {
                LogUtils.LogError("协议族不支持地址族。");
                ErrorManager.PushMsg("协议族不支持地址族。", ErrorType.ArgError);
            }
            else if (args.SocketError == SocketError.OperationAborted)
            {
                LogUtils.LogError("由于 Socket 已关闭，重叠的操作被中止。");
                ErrorManager.PushMsg("由于 Socket 已关闭，重叠的操作被中止。", ErrorType.ArgError);
            }
            else if (args.SocketError == SocketError.NotSocket)
            {
                LogUtils.LogError("对非套接字尝试 Socket 操作。");
                ErrorManager.PushMsg("对非套接字尝试 Socket 操作。", ErrorType.ArgError);
            }
            else if (args.SocketError == SocketError.SystemNotReady)
            {
                LogUtils.LogError("网络子系统不可用。");
                ErrorManager.PushMsg("网络子系统不可用。", ErrorType.NetError);
            }
            else if (args.SocketError == SocketError.SocketError)
            {
                LogUtils.LogError("发生了未指定的 Socket 错误。");
                ErrorManager.PushMsg("发生了未指定的 Socket 错误。", ErrorType.ArgError);
            }
            else if (args.SocketError == SocketError.Shutdown)
            {
                LogUtils.LogError("发送或接收数据的请求未得到允许，因为 Socket 已被关闭。");
                ErrorManager.PushMsg("发送或接收数据的请求未得到允许，因为 Socket 已被关闭。", ErrorType.NetError);
            }
            else if (args.SocketError == SocketError.TryAgain)
            {
                LogUtils.LogError("无法解析主机名。请稍后再试。");
                ErrorManager.PushMsg("无法解析主机名。请稍后再试。", ErrorType.ArgError);
            }
            else if (args.SocketError == SocketError.VersionNotSupported)
            {
                LogUtils.LogError("基础套接字提供程序的版本超出范围。");
                ErrorManager.PushMsg("基础套接字提供程序的版本超出范围。", ErrorType.ArgError);
            }
            else if (args.SocketError == SocketError.WouldBlock)
            {
                LogUtils.LogError("对非阻止性套接字的操作不能立即完成。");
                ErrorManager.PushMsg("对非阻止性套接字的操作不能立即完成。", ErrorType.ArgError);
            }
            else if (args.SocketError == SocketError.TooManyOpenSockets)
            {
                LogUtils.LogError("基础套接字提供程序中打开的套接字太多。");
                ErrorManager.PushMsg("基础套接字提供程序中打开的套接字太多。", ErrorType.ArgError);
            }


            this.CloseConnect();

        }


        void FetChSocketInfo(SocketAsyncEventArgs args, SocetArgType type)
        {
            last = type;

            if (args != null && args.SocketError == SocketError.Success)
            {

                _socket.SetState();

                if (type == SocetArgType.Connect)
                {

                    ConnectCallback(args);

                }
                else if (type == SocetArgType.Receive)
                {


                    if (args.BytesTransferred > 0)
                        OnReveiveCallBack(args);
                    else
                        LogUtils.LogError("接受到一个空包");

                }
                else if (type == SocetArgType.Send)
                {


                    if (args.BytesTransferred > 0)
                        OnSendCallback(args);
                    else
                        LogUtils.LogError("发送一个空包");

                }
                else
                {
                    OnOtherCallback(args);

                }
            }
            else
            {
                    ProcessError(args);
            }
        }

        void OnOtherCallback(SocketAsyncEventArgs ev)
        {

//#if debug
						LogUtils.Log ("LastOperation", ev.LastOperation.ToString (), " Socket状态", ev.SocketError.ToString (), ev.SocketFlags.ToString ());
//#endif

            if (ev.SocketError != SocketError.Success)
            {
                ProcessError(ev);
            }

        }

        void OnSendCallback(SocketAsyncEventArgs ev)
        {
            if (ev != null && ev.SocketError == SocketError.Success)
                LogUtils.Log("Client  SendCallback buffer Size ", ev.Buffer.Length.ToString(), "成功");
            else
                LogUtils.Log("Client  SendCallback buffer Size ", ev.Buffer.Length.ToString(), "失败");

            if (SendEvent != null)
                SendEvent(true);
        }

        void OnReveiveCallBack(SocketAsyncEventArgs ev)
        {

            if (ReceiveEvent != null)
                ReceiveEvent(true);

            int len = ev.BytesTransferred;

            LogUtils.Log("接受到数据  ", len.ToString());

            byte[] readData = new byte[len];
            System.Array.Copy(ev.Buffer, 0, readData, 0, len);
            MessageManager.mIns.PushToReceiveBuffer(readData);

            Interlocked.Decrement(ref ReceiveCount);

            PreReceive();

            //						mlock.Set ();

        }

        public bool SocketAviliable()
        {
            //if (_socket.GetState() == AsyncSocket.SocketArgsStats.UNCONNECT
            //    || _socket.GetState() == AsyncSocket.SocketArgsStats.READY 
            //    || _socket.GetState() == AsyncSocket.SocketArgsStats.ERROR 
            //    || _socket.GetState() == AsyncSocket.SocketArgsStats.CONNECTING)
            //{
            //    return false;
            //}

            bool ret = _socket != null && _socket.Connected(); //(_socket.GetSocketState () == AsyncSocket.SocketArgsStats.FREE || _socket.GetSocketState () == AsyncSocket.SocketArgsStats.SEND || _socket.GetSocketState () == AsyncSocket.SocketArgsStats.RECEIVE);
            if (!ret)
            {
                LogUtils.Log("From KtcpClient socket 未激活");
            }

            return ret;
        }


    }

}




