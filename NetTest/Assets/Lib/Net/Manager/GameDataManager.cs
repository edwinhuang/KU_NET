using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kubility;
using System;

public class GameDataManager : ICommonClass
{

/*
    private static Dictionary<int, Action<S2cBaseData>> callbackList = new Dictionary<int, Action<S2cBaseData>>();

    public override void DispatcherEvents(ValueType data, BaseEnum main, BaseEnum sub, object callback)
    {
        S2cGameDataResp Gamedata = (S2cGameDataResp)System.Convert.ChangeType(data, typeof(S2cGameDataResp));

#if UNITY_EDITOR
        LogMgr.Log("发现游戏数据更新包");
#endif

        if (Gamedata.DataList.Count > 0 && callback != null)
        {

            if (callback is Action<S2cGameDataResp>)
            {
                var realCall = callback as Action<S2cGameDataResp>;

                if (callbackList.Count > 0)
                {
                    for (int i = 0; i < Gamedata.DataList.Count; ++i)
                    {
                        S2cBaseData basedata = Gamedata.DataList[i];
                        int subtype = (int)basedata.Type;
                        if (callbackList.ContainsKey(subtype))
                            callbackList[subtype].TryCall(basedata);
                    }
                }

                realCall.TryCall(Gamedata);
            }
            else
            {
                LogMgr.LogError("类型不匹配");
            }
        }
        else
        {
            AutoDealMsg(Gamedata);
        }

    }

    private void AutoDealMsg(S2cGameDataResp Gamedata)
    {
        for (int i = 0; i < Gamedata.DataList.Count; ++i)
        {
            S2cBaseData data = Gamedata.DataList[i];
            if (data != null)
            {
                if (data.Type == (byte)S2CBuildType.GOLD)
                {
                    LogUtils.Log("自动更新  金币 " + data.Value);
                    LocalValue.mIns.gold = data.Value;
                    LocalValue.mIns.loadingGold = data.Value;
                }
                else if (data.Type == (byte)S2CBuildType.DIAMOND)
                {
                    LogUtils.Log("自动更新  钻石 " + data.Value);
                    LocalValue.mIns.gem = data.Value;
                    LocalValue.mIns.loadingGem = data.Value;
                }
                else if (data.Type == (byte)S2CBuildType.COMPETITIVE)
                {
                    LogUtils.Log("自动更新  COMPETITIVE " + data.Value);
                    LocalValue.mIns.competitive = data.Value;
                    LocalValue.mIns.loadingCup = data.Value;
                }
                else if (data.Type == (byte)S2CBuildType.IRON)
                {
                    LogUtils.Log("自动更新 矿 " + data.Value);
                    LocalValue.mIns.ore = data.Value;
                    LocalValue.mIns.loadingOre = data.Value;
                }
                else if (data.Type == (byte)S2CBuildType.POWER)
                {
                    LogUtils.Log("自动更新  魔力泉 " + data.Value);
                    LocalValue.mIns.spring = data.Value;
                    LocalValue.mIns.loadingSpring = data.Value;
                }
                else
                {
                    LogMgr.LogError("异常类型");
                }
            }
        }

        if (LocalValue.mIns.ore == 0 && LocalValue.mIns.spring == 0 && LocalValue.mIns.gold == 0)
        {
            IOSGameCenterManager.mIns.PushAchievement("Penniless");
        }
    }

    public GameDataManager(TcpSubCMD subdata)
    {
        this.data = subdata;
        CommonManager.mIns.Register(this);
    }

    public override void Destroy()
    {
        callbackList.Clear();
        callbackList = null;
    }

    public static void RemoveWait(S2CBuildType type, Action<S2cBaseData> Callback)
    {
        int intType = (int)type;
        if (callbackList.ContainsKey(intType))
        {
            callbackList.Remove(intType);
        }

    }

    public static void Wait_Event(S2CBuildType type, Action<S2cBaseData> Callback)
    {
        int intType = (int)type;
        if (callbackList.ContainsKey(intType))
        {
            callbackList[intType] += Callback;
        }
        else
        {
            callbackList.Add(intType, Callback);
        }

        CommonManager.mIns.Wait_Resp(TcpMainCMD.COMMON, TcpSubCMD.SC_BASE_DATA, Callback);
    }

    public static void Wait_Event(Action<S2cGameDataResp> Callback)
    {
        CommonManager.mIns.Wait_Resp<S2cGameDataResp>(TcpMainCMD.COMMON, TcpSubCMD.SC_BASE_DATA, Callback);
    }

    public static void Wait_Event_AutoRomove(Action<S2cGameDataResp> Callback)
    {
        Wait_Event(Callback);
        MonoDelegate.mIns.Coroutine_Delay(2f, delegate ()
        {

            CommonManager.mIns.Remove_Resp<S2cGameDataResp>(TcpMainCMD.COMMON, TcpSubCMD.SC_BASE_DATA, Callback);
        });

    }

	//*/

}
