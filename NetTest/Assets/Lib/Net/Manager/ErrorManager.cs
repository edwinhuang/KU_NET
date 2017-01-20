using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Kubility
{
    public enum ErrorType
    {
        None = -1,
        NormalGameError = 1,

        ConnectClose,
        ConnectFailed,
        TimeOut,
        NullRef,
        ArgError,
        NetError,
        UnKnown,

        GreatError = 99,
    }



    public class ErrorManager : ICommonClass
	{

		public override void DispatcherEvents(ValueType data, BaseEnum main, BaseEnum sub, object callback)
		{
		}
		public override void Destroy()
		{}

		public static void PushMsg(string errormsg, ErrorType error = ErrorType.None)
		{

//			UnkownError(new CustomException(errormsg, error));

		}

	}
		/*
        private static Queue<ErrorMsgResp> errorList = new Queue<ErrorMsgResp>();


        public override void DispatcherEvents(ValueType data, BaseEnum main, BaseEnum sub, object callback)
        {
            ErrorMsgResp error = (ErrorMsgResp)System.Convert.ChangeType(data, typeof(ErrorMsgResp));

#if UNITY_EDITOR
            LogMgr.Log("发现错误包");
#endif

            if (callback != null)
            {
                if (callback is Action<int>)
                {
                    var realCall = callback as Action<int>;
                    realCall.TryCall(error.MsgID);
                }
                else if (callback is Action)
                {
                    var realCall = callback as Action;
                    realCall.TryCall();

                }
                else if (callback is Action<int, ErrorType>)
                {
                    var realCall = callback as Action<int, ErrorType>;
    
                    realCall.TryCall(error.MsgID,(ErrorType)error.ErrorType);

                }

            }


            AutoDealError(error);

            while (errorList.Count > 0)
                DispatcherEvents(errorList.Dequeue(), main, sub, errorList.Dequeue());

        }

        void AutoDealError(ErrorMsgResp error)
        {
            if (error.ErrorType == (int)ErrorType.NormalGameError)
            {
                LogMgr.LogError("普通错误 ！" + error.MsgID);
                int msgid = error.MsgID;
                if (msgid != 0)
                {
                    if (msgid == 4)
                        DialogController.mIns.DialogControllerShow(msgid, GameController.ReLogin);
                    else if (msgid == 12003)
                    {
                        DialogController.mIns.DialogControllerShow(12001, (data) =>
                        {
                            if (GlobalHelper.mIns.curGameScene == GameScene.BattleScene && BattleResultController.mIns != null)
                                BattleResultController.mIns.BackHome();
                        });
                    }
                    else
                        DialogController.mIns.DialogControllerShow(msgid);
                }

            }
            else if (error.ErrorType == (int)ErrorType.GreatError)
            {
                LogMgr.LogError("重大错误！");

//                if(GlobalHelper.mIns.curGameScene != GameScene.LoginScene)
//                    GameController.ReLogin(null);

                int msgid = error.MsgID;
                if (msgid != 0)
                {
                        DialogController.mIns.DialogControllerShow(msgid);
                }

            }
            else if (error.ErrorType == (int)ErrorType.ConnectClose || error.ErrorType == (int)ErrorType.ConnectFailed || error.ErrorType == (int)ErrorType.TimeOut || error.ErrorType == (int)ErrorType.NetError)
            {

                LogUtils.LogError("网络错误");
            }
            else
                LogMgr.Log("ErrorManager 传入了错误的类型");
        }

        public ErrorManager(TcpSubCMD subdata)
        {
            this.data = subdata;
            CommonManager.mIns.Register(this);
        }

        public override void Destroy()
        {
            errorList.Clear();
            errorList = null;
        }

        #region connectevents

        public static void Register<T>(T obj) where T : ConnectEvents
        {
            obj.m_ConnectCloseEvent += () =>
            {
                errorList.Enqueue((int)ErrorType.ConnectClose);
            };
            obj.m_ConnectFailedEvent += () =>
            {
                errorList.Enqueue((int)ErrorType.ConnectFailed);
            };
            obj.m_OthersErrorEvent += UnkownError;
            obj.m_TimeOutEvent += () =>
            {
                errorList.Enqueue((int)ErrorType.TimeOut);
            };
        }

        public static void Wait_Error(TcpMainCMD main, TcpSubCMD sub, Action Callback)
        {

            CommonManager.mIns.Wait_Resp(TcpMainCMD.COMMON, TcpSubCMD.SC_ERRO_MSG, Callback);
        }

        public static void Wait_Error(TcpMainCMD main, TcpSubCMD sub, Action<int> Callback)
        {
            CommonManager.mIns.Wait_Resp<int>(TcpMainCMD.COMMON, TcpSubCMD.SC_ERRO_MSG, Callback);
        }

        public static void Wait_Error(TcpMainCMD main, TcpSubCMD sub, Action<int,ErrorType> Callback)
        {
            CommonManager.mIns.Wait_Resp(TcpMainCMD.COMMON, TcpSubCMD.SC_ERRO_MSG, Callback);
        }

        public static void Wait_Error_AutoRomove(TcpMainCMD main, TcpSubCMD sub, Action Callback)
        {
            Wait_Error(TcpMainCMD.COMMON, TcpSubCMD.SC_ERRO_MSG, Callback);
            MonoDelegate.mIns.Coroutine_Delay(1.5f, delegate ()
            {

                CommonManager.mIns.Remove_Resp(TcpMainCMD.COMMON, TcpSubCMD.SC_ERRO_MSG, Callback);
            });

        }

        public static void Wait_Error_AutoRomove(TcpMainCMD main, TcpSubCMD sub, Action<int> Callback)
        {
            Wait_Error(TcpMainCMD.COMMON, TcpSubCMD.SC_ERRO_MSG, Callback);
            MonoDelegate.mIns.Coroutine_Delay(3f, delegate ()
            {

                CommonManager.mIns.Remove_Resp<int>(TcpMainCMD.COMMON, TcpSubCMD.SC_ERRO_MSG, Callback);
            });

        }

        public static void Wait_Error_AutoRomove( Action<int,ErrorType> Callback)
        {
            Wait_Error(TcpMainCMD.COMMON, TcpSubCMD.SC_ERRO_MSG, Callback);
            MonoDelegate.mIns.Coroutine_Delay(3f, delegate ()
            {

                CommonManager.mIns.Remove_Resp<int,ErrorType>(TcpMainCMD.COMMON, TcpSubCMD.SC_ERRO_MSG, Callback);
            });

        }

        public static void PushMsg(ErrorMsgResp errormsg)
        {
            CommonManager.mIns.PushMsg(errormsg, TcpMainCMD.COMMON, TcpSubCMD.SC_ERRO_MSG);
        }

        public static void PushMsg(string errormsg, ErrorType error = ErrorType.None)
        {

            UnkownError(new CustomException(errormsg, error));

        }

        static void UnkownError(Exception ex)
        {
            if (ex.GetType() == typeof(NullReferenceException))
            {
                LogMgr.LogError("Null Ref Error");
                errorList.Enqueue((int)ErrorType.NullRef);

                var msg = new ErrorMsgResp();
                msg.ErrorType = (short)ErrorType.NullRef;
                msg.MsgID = 0;
                msg.Sub = 0;
                CommonManager.mIns.PushMsg(msg, TcpMainCMD.COMMON, TcpSubCMD.SC_ERRO_MSG);

            }
            else if (ex.GetType() == typeof(ArgumentException))
            { //
                LogMgr.LogError("参数错误");
                errorList.Enqueue((short)ErrorType.ArgError);
                var msg = new ErrorMsgResp();
                msg.ErrorType = (short)ErrorType.ArgError;
                msg.MsgID = 0;
                msg.Sub = 0;
                CommonManager.mIns.PushMsg(msg, TcpMainCMD.COMMON, TcpSubCMD.SC_ERRO_MSG);
            }
            else if (ex.GetType() == typeof(System.Net.WebException))
            {
                LogMgr.LogError("网络错误");
                errorList.Enqueue((int)ErrorType.NetError);
                var msg = new ErrorMsgResp();
                msg.ErrorType = (short)ErrorType.NetError;
                msg.MsgID = 0;
                msg.Sub = 0;
                CommonManager.mIns.PushMsg(msg, TcpMainCMD.COMMON, TcpSubCMD.SC_ERRO_MSG);
            }
            else if (ex.GetType() == typeof(CustomException))
            {
                CustomException cex = ex as CustomException;

                LogMgr.LogError(cex.Message);

                errorList.Enqueue((int)cex.ErrorCode);
                var msg = new ErrorMsgResp();
                msg.ErrorType = (short)cex.ErrorCode;
                msg.MsgID = 0;
                msg.Sub = 0;
                CommonManager.mIns.PushMsg(msg, TcpMainCMD.COMMON, TcpSubCMD.SC_ERRO_MSG);
            }
            else
            {
                errorList.Enqueue((int)ErrorType.UnKnown);
                var msg = new ErrorMsgResp();
                msg.ErrorType = (short)ErrorType.UnKnown;
                msg.MsgID = 0;
                msg.Sub = 0;
                CommonManager.mIns.PushMsg(msg, TcpMainCMD.COMMON, TcpSubCMD.SC_ERRO_MSG);
            }
        }


        #endregion

    }
    */
}


