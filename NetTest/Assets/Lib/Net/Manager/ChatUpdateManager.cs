using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kubility;
using System;

public class ChatUpdateManager : ICommonClass
{
	/*
	public override void DispatcherEvents (ValueType data, BaseEnum main, BaseEnum sub, object callback)
	{

		if (main == TcpMainCMD.SOCIATY) {
			if (sub == TcpSubCMD.SC_SOCIATY_CHAT) {
				S2SSociatyChatRsp netdata = (S2SSociatyChatRsp)System.Convert.ChangeType (data, typeof(S2SSociatyChatRsp));
				//				NewChatResp netdata = (NewChatResp)System.Convert.ChangeType (data, typeof(NewChatResp));

				//				if (netdata != null)
                GameLuaClient.Instance.OnNewSocietyChat(netdata);
				//ChatController.mIns.OnNewSocietyChat(netdata);
			}
			else if (sub == TcpSubCMD.SC_SOCIATY_KICKED_NOTIFY) {
				S2SSociatyKickedOutRsp netdata = (S2SSociatyKickedOutRsp)System.Convert.ChangeType (data, typeof(S2SSociatyKickedOutRsp));
				if (LocalValue.mIns.isOpenSociety == true)
					SocietyController.mIns.BeKickedOut(netdata);
			}
		}
	}

	public ChatUpdateManager (TcpSubCMD subdata)
	{
		this.data = subdata;
		CommonManager.mIns.Register (this);
	}

	public override void Destroy ()
	{

	}

	public static  void Wait_Event (Action<S2SSociatyChatRsp> Callback)
	{
		CommonManager.mIns.Wait_Resp<S2SSociatyChatRsp> (TcpMainCMD.SOCIATY, TcpSubCMD.SC_SOCIATY_CHAT, Callback);
	}

	public static void Wait_Event_AutoRemove (Action<S2SSociatyChatRsp> Callback)
	{
		CommonManager.mIns.Wait_Resp<S2SSociatyChatRsp> (TcpMainCMD.SOCIATY, TcpSubCMD.SC_SOCIATY_CHAT, Callback);
		MonoDelegate.mIns.Coroutine_Delay (delay, delegate() {
			CommonManager.mIns.Remove_Resp (TcpMainCMD.SOCIATY, TcpSubCMD.SC_SOCIATY_CHAT, Callback);
		});
	}
	
	public static  void Wait_Event (Action<S2SSociatyKickedOutRsp> Callback)
	{
		CommonManager.mIns.Wait_Resp<S2SSociatyKickedOutRsp> (TcpMainCMD.SOCIATY, TcpSubCMD.SC_SOCIATY_KICKED_NOTIFY, Callback);
	}

	public static void Wait_Event_AutoRemove (Action<S2SSociatyKickedOutRsp> Callback)
	{
		CommonManager.mIns.Wait_Resp<S2SSociatyKickedOutRsp> (TcpMainCMD.SOCIATY, TcpSubCMD.SC_SOCIATY_KICKED_NOTIFY, Callback);
		MonoDelegate.mIns.Coroutine_Delay (delay, delegate() {
			CommonManager.mIns.Remove_Resp (TcpMainCMD.SOCIATY, TcpSubCMD.SC_SOCIATY_KICKED_NOTIFY, Callback);
		});
	}
//*/
}
