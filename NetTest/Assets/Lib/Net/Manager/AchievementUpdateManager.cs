using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kubility;
using System;



public class AchievementUpdateManager : ICommonClass
{

    public override void DispatcherEvents(ValueType data, BaseEnum main, BaseEnum sub, object callback)
    {

/*
 * if (main == TcpMainCMD.ACTIVITY)
        {
            if (sub == TcpSubCMD.SC_ALL_ACHIEVEMENT)
            {
                AllAchievementResp netdata = (AllAchievementResp)System.Convert.ChangeType(data, typeof(AllAchievementResp));


                MMtool.RefreshAchievementList(netdata.achieving, netdata.achieved);
                if (LocalValue.mIns.IsFirstGetAchievement)
                {
                    LocalValue.mIns.IsFirstGetAchievement = false;
                }


            }
            else if (sub == TcpSubCMD.SC_UPDATE_ACHIEVEMENT)
            {
                UpdateAchievingResp netdata = (UpdateAchievingResp)System.Convert.ChangeType(data, typeof(UpdateAchievingResp));

                MMtool.UpdateAchieving(netdata.achievingTo);

            }
            else if (sub == TcpSubCMD.SC_COMPLETE_ACHIEVEMENT)
            {
                UpdateAchievedResp netdata = (UpdateAchievedResp)data;// System.Convert.ChangeType(data, typeof(UpdateAchievedResp));
                MMtool.AddAchieved(netdata.achievedTo);


            }
            else if (sub == TcpSubCMD.SC_GET_REWARD)
            {
                RecvAchieveAwardResp netdata = (RecvAchieveAwardResp)System.Convert.ChangeType(data, typeof(RecvAchieveAwardResp));
                MMtool.UpdateAchieved(netdata.achieveList);
            }
        }

    }

    public AchievementUpdateManager(TcpSubCMD subdata)
    {
        this.data = subdata;
        CommonManager.mIns.Register(this);
    }

    public override void Destroy()
    {

    }

    public static void Wait_Event(Action<AllAchievementResp> Callback)
    {
        CommonManager.mIns.Wait_Resp<AllAchievementResp>(TcpMainCMD.ACTIVITY, TcpSubCMD.SC_ALL_ACHIEVEMENT, Callback);
    }

    public static void Wait_Event(Action<UpdateAchievingResp> Callback)
    {
        CommonManager.mIns.Wait_Resp<UpdateAchievingResp>(TcpMainCMD.ACTIVITY, TcpSubCMD.SC_UPDATE_ACHIEVEMENT, Callback);
    }

    public static void Wait_Event(Action<UpdateAchievedResp> Callback)
    {
        CommonManager.mIns.Wait_Resp<UpdateAchievedResp>(TcpMainCMD.ACTIVITY, TcpSubCMD.SC_COMPLETE_ACHIEVEMENT, Callback);
    }

    public static void Wait_Event(Action<RecvAchieveAwardResp> Callback)
    {
        CommonManager.mIns.Wait_Resp<RecvAchieveAwardResp>(TcpMainCMD.ACTIVITY, TcpSubCMD.SC_GET_REWARD, Callback);
    }


    public static void Wait_Event_AutoRemove(Action<AllAchievementResp> Callback)
    {
        CommonManager.mIns.Wait_Resp<AllAchievementResp>(TcpMainCMD.ACTIVITY, TcpSubCMD.SC_ALL_ACHIEVEMENT, Callback);
        MonoDelegate.mIns.Coroutine_Delay(delay, delegate ()
        {
            CommonManager.mIns.Remove_Resp(TcpMainCMD.ACTIVITY, TcpSubCMD.SC_ALL_ACHIEVEMENT, Callback);
        });
    }

    public static void Wait_Event_AutoRemove(Action<UpdateAchievingResp> Callback)
    {
        CommonManager.mIns.Wait_Resp<UpdateAchievingResp>(TcpMainCMD.ACTIVITY, TcpSubCMD.SC_UPDATE_ACHIEVEMENT, Callback);
        MonoDelegate.mIns.Coroutine_Delay(delay, delegate ()
        {
            CommonManager.mIns.Remove_Resp(TcpMainCMD.ACTIVITY, TcpSubCMD.SC_UPDATE_ACHIEVEMENT, Callback);
        });
    }

    public static void Wait_Event_AutoRemove(Action<UpdateAchievedResp> Callback)
    {
        CommonManager.mIns.Wait_Resp<UpdateAchievedResp>(TcpMainCMD.ACTIVITY, TcpSubCMD.SC_COMPLETE_ACHIEVEMENT, Callback);
        MonoDelegate.mIns.Coroutine_Delay(delay, delegate ()
        {
            CommonManager.mIns.Remove_Resp(TcpMainCMD.ACTIVITY, TcpSubCMD.SC_COMPLETE_ACHIEVEMENT, Callback);
        });
    }

    public static void Wait_Event_AutoRemove(Action<RecvAchieveAwardResp> Callback)
    {
        CommonManager.mIns.Wait_Resp<RecvAchieveAwardResp>(TcpMainCMD.ACTIVITY, TcpSubCMD.SC_GET_REWARD, Callback);
        MonoDelegate.mIns.Coroutine_Delay(delay, delegate ()
        {
            CommonManager.mIns.Remove_Resp(TcpMainCMD.ACTIVITY, TcpSubCMD.SC_GET_REWARD, Callback);
        });
    }
    //*/
}

