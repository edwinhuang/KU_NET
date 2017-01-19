using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kubility;
using System;

public class BattleDropItemManager : ICommonClass
{
/*
    public override void DispatcherEvents(ValueType data, BaseEnum main, BaseEnum sub, object callback)
    {
        BattleDropItemResp msg = (BattleDropItemResp)System.Convert.ChangeType(data, typeof(BattleDropItemResp));

#if UNITY_EDITOR
        LogMgr.LogError("发现战斗掉落包");
#endif

        AutoDealMsg(msg);


    }

    void AutoDealMsg(BattleDropItemResp msg)
    {
        if (msg.DpitemList != null && msg.DpitemList.Count > 0)
        {
            BuildComponent build = BuildComponent.BuildComponentList.Find(p => !MMtool.IsMonster(p.mBuildingInfo) && p.mBuildingInfo.buildID == msg.BuildID);
            for (int i=0; i < msg.DpitemList.Count;++i)
            {
                BonusItemTo item = msg.DpitemList[i];
                LogMgr.Log("战斗掉落  "+ item.itemId +" Num ="+ item.num);
               // MMtool.GainItem(item.itemId.ToString(),item.num);
                if (KBattleResFunction.mIns != null)
                {
                    KBattleResFunction.mIns.BattleDropitem_Add(item);
                    if (build != null)
                    {
                        Transform tr = PoolManager.Pools["Pool"].Spawn("BattlePropsFall");
                        if (tr != null)
                        {
                            KAssetBundleManger.AddChild(build.gameObject, tr.gameObject);
                            BattlePropsFall propfall = tr.GetComponent<BattlePropsFall>();
                            if (propfall != null)
                            {
                                propfall.Show(item.itemId, item.num);
                            }
                        }
                    }
                    
                }
            }
        }

    }

    public BattleDropItemManager(TcpSubCMD subdata)
    {
        this.data = subdata;
        CommonManager.mIns.Register(this);
    }

    public override void Destroy()
    {

    }



    public static void PushMsg(BattleDropItemResp errormsg)
    {
        CommonManager.mIns.PushMsg(errormsg, TcpMainCMD.COMMON, TcpSubCMD.SC_DROP_ITEM);
    }

	//*/
}
