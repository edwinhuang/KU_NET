using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kubility;
using System;

public class MailUpdateManager : ICommonClass
{
	/*
	public override void DispatcherEvents (ValueType data, BaseEnum main, BaseEnum sub, object callback)
	{

		if (main == TcpMainCMD.MAIL) {
			if (sub == TcpSubCMD.SC_NEW_MAIL_NOTICE) {
				NewMailNoticeResp netdata = (NewMailNoticeResp)System.Convert.ChangeType (data, typeof(NewMailNoticeResp));

				if (netdata.to != null)
					NoticeController.mIns.ReceiveMail(netdata);
			}
		}
	}

	public MailUpdateManager (TcpSubCMD subdata)
	{
		this.data = subdata;
		CommonManager.mIns.Register (this);
	}

	public override void Destroy ()
	{

	}

	public static  void Wait_Event (Action<NewMailNoticeResp> Callback)
	{
		CommonManager.mIns.Wait_Resp<NewMailNoticeResp> (TcpMainCMD.MAIL, TcpSubCMD.SC_NEW_MAIL_NOTICE, Callback);
	}

	public static void Wait_Event_AutoRemove (Action<NewMailNoticeResp> Callback)
	{
		CommonManager.mIns.Wait_Resp<NewMailNoticeResp> (TcpMainCMD.MAIL, TcpSubCMD.SC_NEW_MAIL_NOTICE, Callback);
		MonoDelegate.mIns.Coroutine_Delay (delay, delegate() {
			CommonManager.mIns.Remove_Resp (TcpMainCMD.MAIL, TcpSubCMD.SC_NEW_MAIL_NOTICE, Callback);
		});
	}
	//*/
}
